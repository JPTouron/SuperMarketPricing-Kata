using AutoFixture;
using AutoFixture.AutoMoq;
using SupermarketPricing.Domain.Modules.SharedKernel.Products;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.Extensions;
using Xunit;

namespace SupermarketPricing.Domain.Tests
{
    public class SellableProductTests
    {
        private IFixture fixture;

        private ISellableProduct prod;

        public SellableProductTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

        [Fact]
        public void Equals_ComparingAProductToNoProductReturnFalse()
        {
            string name;
            Money cost;
            prod = GetValidSellableProduct(out name, out cost);

            string name2;
            Money cost2;
            ISellableProduct noProd = GetValidNoSellableProduct(out name, out cost);

            Assert.False(prod.Equals(noProd));
            Assert.False(noProd.Equals(prod));
        }

        [Fact]
        public void WhenCreated_HasNameAndPriceAndIsSellable()
        {
            string name;
            Money cost;
            prod = GetValidSellableProduct(out name, out cost);

            Assert.Equal(cost, prod.Cost);
            Assert.Equal(name, prod.ProductName);
            Assert.True(prod.IsSellable);
        }

        [Fact]
        public void WhenCreatedAsNoProduct_HasEmptyNameAndNoMoneyAndIsSellableIsFalse()
        {
            string name;
            Money cost;
            prod = GetValidNoSellableProduct(out name, out cost);

            Assert.Equal(cost, prod.Cost);
            Assert.Equal(name, prod.ProductName);
            Assert.False(prod.IsSellable);
        }

        private NoSellableProduct GetValidNoSellableProduct(out string name, out Money cost)
        {
            name = string.Empty;
            cost = Money.NoMoneyDollars();
            return new NoSellableProduct();
        }

        private SellableProduct GetValidSellableProduct(out string name, out Money cost)
        {
            name = "name";
            cost = Money.Create(1.0m, "USD".ToCurrency());
            return new SellableProduct(name, cost);
        }
    }
}