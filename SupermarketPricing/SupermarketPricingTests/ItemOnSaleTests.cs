using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using SupermarketPricing.Model1;
using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket;
using System;
using Xunit;
using static SupermarketPricing.SuperMarketItems;

namespace SupermarketPricingTests
{
    public class ItemOnSaleTests
    {
        private IFixture fixture;

        public ItemOnSaleTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

        [Fact]
        public void WhenCreated_HoldsADiscountAndAName()
        {
            var discount = 1.0m;
            var prod = new Bread();
            var item = new ItemOnSale(prod, discount);

            Assert.Equal(discount, item.DiscountPercentage);
            Assert.Equal(prod.ProductName, item.ProductName);
        }

        [Theory]
        [InlineData(new object[] { -1 })]
        [InlineData(new object[] { -0 })]
        [InlineData(new object[] { 0.01 })]
        [InlineData(new object[] { 100.001 })]
        public void WhenCreated_ThrowsIfDiscountIsOutOfRange(decimal discountPercentage)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ItemOnSale(new Bread(), discountPercentage));
        }

        [Theory]
        [InlineData(new object[] { 0.1 })]
        [InlineData(new object[] { 100 })]
        public void WhenCreated_AcceptsValidDiscountValues(decimal discountPercentage)
        {
            var item = new ItemOnSale(new Bread(), discountPercentage);
            Assert.Equal(discountPercentage, item.DiscountPercentage);
        }

        [Fact]
        public void WhenCreated_ThrowsIfProductIsNull()
        {
            var discount = 1.0m;

            Assert.Throws<ArgumentNullException>(() => new ItemOnSale(null, discount));
        }

        [Theory]
        [InlineData(new object[] { 10, 10, 9 })]
        [InlineData(new object[] { 10, 1, 9.9 })]
        [InlineData(new object[] { 10, 30, 7 })]
        public void Cost_CalculateDiscountProperly(decimal originalCost, decimal discount, decimal discountedCost)
        {
            var repo = new CurrencyTypeRepository();

            var prodM = fixture.Create<Mock<ISellableProduct>>();

            prodM.SetupGet(x => x.Cost).Returns(new Money(originalCost, repo.Get("EUR")));
            var prod = prodM.Object;

            var sale = new ItemOnSale(prod, discount);

            Assert.Equal(sale.Cost.Amount, discountedCost);
        }
    }
}