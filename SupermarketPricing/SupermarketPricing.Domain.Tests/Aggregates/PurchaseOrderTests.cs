using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using SupermarketPricing.Domain.ApplicationPorts;
using SupermarketPricing.Domain.Modules.Purchase;
using SupermarketPricing.Domain.Modules.Purchase.Aggregate;
using SupermarketPricing.Domain.Modules.SharedKernel.Products;
using SupermarketPricing.Domain.Modules.Stock.Aggregate;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.CurrencyModel;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SupermarketPricing.Domain.Tests.Aggregates
{
    public class PurchaseOrderTests
    {
        private static Currency currency = "EUR".ToCurrency();
        private static IFixture fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        private Mock<IDiscountApplier> discountApplier;
        private IPurchaseOrder order;
        private Storage storage;

        public PurchaseOrderTests()
        {
            discountApplier = new Mock<IDiscountApplier>();
            storage = new Storage();

            order = new PurchaseOrder(currency, storage, discountApplier.Object);
        }

        public static IEnumerable<object[]> ItemsForCostCalculation()
        {
            Func<IPurchaseItem, IPurchaseItem> simpleStrat = (pi) => CreatePurchaseItemWithDiscount(pi, () => pi.ProductCost * pi.Quantity);
            Func<IPurchaseItem, IPurchaseItem> complexStrat = (pi) => CreatePurchaseItemWithDiscount(pi, () => pi.ProductCost * pi.Quantity / 10);

            yield return new object[] {
                new KeyValuePair<ISellableProduct, int>(GetAProductCosting(1.04m), 10),//data for storage, cost and quantity
                GetADiscountCalculator(simpleStrat),
                Money.Create(10.4m,currency)
            };

            yield return new object[] {
                new KeyValuePair<ISellableProduct, int>(GetAProductCosting(2.5m), 2),
                GetADiscountCalculator(complexStrat),
                Money.Create(0.5m,currency)
            };
        }

        [Fact]
        public void AddItemToOrder_AddsAProductToTheList()
        {
            var product = fixture.Create<ISellableProduct>();
            storage.AddProductToStorage(product, 10);
            order = new PurchaseOrder("EUR".ToCurrency(), storage, fixture.Create<IDiscountApplier>());

            discountApplier
                .Setup(x => x.ApplyDiscount(It.IsAny<IPurchaseItem>()))
                .Returns<IPurchaseItem>(x => x);

            SetupValidPurchaseOrder(discountApplier.Object);

            var desiredQuantity = 2;
            order.AddItemToOrder(product.ProductName, desiredQuantity);

            var item = order.GetList.Single();

            Assert.Equal(product.ProductName, item.ProductName);
            Assert.Equal(desiredQuantity, item.Quantity);
        }

        [Fact]
        public void WhenCreated_HasListEmptyAndCurrencySetAndTotalCostIsZero()
        {
            var currency = "EUR".ToCurrency();
            var totalCost = Money.Create(0m, currency);

            SetupValidPurchaseOrder(discountApplier.Object);

            Assert.NotNull(order.GetList);
            Assert.Empty(order.GetList);

            Assert.Equal(currency, order.Currency);
            Assert.Equal(totalCost, order.TotalCost);
        }

        [Theory]
        [MemberData(nameof(ItemsForCostCalculation))]
        public void WhenExistingProductInStorageIsAdded_ThenItsIncludedInTotalCost(
            KeyValuePair<ISellableProduct, int> productsForStorage,
            IDiscountApplier discountApplier,
            Money expectedTotal)
        {
            var prod = productsForStorage.Key;
            var qty = productsForStorage.Value;

            //discountApplier
            //   .Setup(x => x.ApplyDiscount(It.IsAny<IPurchaseItem>()))
            //   .Returns<IPurchaseItem>(x => x);

            SetupValidPurchaseOrder(discountApplier);

            AddProductToStorage(productsForStorage.Key, qty);

            order.AddItemToOrder(prod.ProductName, qty);

            Assert.Equal(expectedTotal, order.TotalCost);
        }

        private static IPurchaseItem CreatePurchaseItemWithDiscount(IPurchaseItem pi, Func<Money> discountRule)
        {
            var m = new Mock<IPurchaseItem>();
            m.SetupGet(x => x.ProductCost).Returns(discountRule());
            return m.Object;
        }

        private static IDiscountApplier GetADiscountCalculator(Func<IPurchaseItem, IPurchaseItem> calculationStrategy)
        {
            var discountCalculator = fixture.Create<Mock<IDiscountApplier>>();

            discountCalculator
                .Setup(x => x.ApplyDiscount(It.IsAny<IPurchaseItem>()))
                .Returns<IPurchaseItem>(x => calculationStrategy(x));
            return discountCalculator.Object;
        }

        private static ISellableProduct GetAProductCosting(decimal cost = 0.0m)
        {
            cost = cost == 0.0m ? fixture.Create<decimal>() : cost;

            var price = Money.Create(cost, currency);
            var prod = fixture.Create<Mock<ISellableProduct>>();

            prod.SetupGet(x => x.Cost).Returns(price);
            prod.SetupGet(x => x.IsSellable).Returns(true);

            return prod.Object;
        }

        private void AddProductToStorage(ISellableProduct prod, int qty)
        {
            storage.AddProductToStorage(prod, qty);
        }

        private void SetupValidPurchaseOrder(IDiscountApplier discountApplier)
        {
            order = new PurchaseOrder(currency, storage, discountApplier);
        }
    }
}