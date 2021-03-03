using AutoFixture;
using AutoFixture.AutoMoq;
using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket.Contracts;
using SupermarketPricing.Model1.SuperMarket.Products;
using SupermarketPricing.Model1.SuperMarket.Stock;
using System;
using System.Collections.Generic;
using Xunit;

namespace SupermarketPricingTests
{
    public class StockProductTests
    {
        private IProduct p;
        private IFixture fixture;

        public StockProductTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            p = fixture.Create<ProductItem>();
        }

        public static IEnumerable<object[]> GetData()
        {
            var prod = new ProductItem("item", new Money(0.1m, "USD".ToCurrency()));

            yield return new object[] { prod, 1 };
            yield return new object[] { prod, 0 };
            yield return new object[] { prod, 10 };
            yield return new object[] { prod, 1000 };
        }

        [Fact]
        public void Create_NullProductThrows()
        {
            var iq = 10;
            IProduct prod = null;

            Assert.Throws<ArgumentNullException>(() => new StockProduct(prod, iq));
        }

        [Fact]
        public void Create_NegativeQuantThrows()
        {
            var iq = -1;

            Assert.Throws<ArgumentException>(() => new StockProduct(p, iq));
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Create_Success(IProduct prod, int quantity)
        {
            var sp = new StockProduct(prod, quantity);

            Assert.NotNull(sp);
            Assert.Equal(prod, sp.Product);
            Assert.Equal(quantity, sp.Quantity);
        }

        [Fact]
        public void QuantityReturnInitialQuantity()
        {
            var iq = 1;
            var sp = new StockProduct(p, iq);

            Assert.Equal(iq, sp.Quantity);
        }

        [Fact]
        public void ProductReturnsInitialProduct()
        {
            var iq = 1;
            var sp = new StockProduct(p, iq);

            Assert.IsAssignableFrom<IProduct>(sp.Product);
            Assert.Equal(p, sp.Product);
        }

        [Fact]
        public void DecreaseStock_ReducesStockByQuantity()
        {
            var iq = 11;
            var sp = new StockProduct(p, iq);

            sp.DecreaseStock(10);
            Assert.Equal(1, sp.Quantity);
        }

        [Fact]
        public void DecreaseStock_ThrowsWhenCurrentQuantityIsZero()
        {
            var iq = 0;
            var sp = new StockProduct(p, iq);

            Assert.Throws<InvalidOperationException>(() => sp.DecreaseStock(10));
        }

        [Fact]
        public void DecreaseStock_ThrowsWhenCurrentQuantityWouldBeNegativeAfterOperation()
        {
            var iq = 10;
            var sp = new StockProduct(p, iq);

            Assert.Throws<InvalidOperationException>(() => sp.DecreaseStock(11));
        }
    }
}