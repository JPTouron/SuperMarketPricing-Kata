using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket.Contracts;
using SupermarketPricing.Model1.SuperMarket.Products;
using SupermarketPricing.Model1.SuperMarket.SaleOffers;
using System;
using Xunit;
using static SupermarketPricing.Model1.SuperMarket.SaleOffers.DiscountRules;

namespace SupermarketPricingTests
{
    public class ItemOnSaleTests
    {
        private IFixture fixture;
        private CurrencyTypeRepository repo;

        public ItemOnSaleTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            repo = new CurrencyTypeRepository();
        }

        [Fact]
        public void WhenCreated_HoldsADiscountAndAName()
        {
            var discount = 1.0m;
            var prod = new ProductItem("Bread", new Money(10, repo.Get("USD")));
            var item = new ItemOnSaleRule(prod.ProductName, discount);

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
            var prod = new ProductItem("Bread", new Money(10, repo.Get("USD")));
            Assert.Throws<ArgumentOutOfRangeException>(() => new ItemOnSaleRule(prod.ProductName, discountPercentage));
        }

        [Theory]
        [InlineData(new object[] { 0.1 })]
        [InlineData(new object[] { 100 })]
        public void WhenCreated_AcceptsValidDiscountValues(decimal discountPercentage)
        {
            var prod = new ProductItem("Bread", new Money(10, repo.Get("USD")));

            var item = new ItemOnSaleRule(prod.ProductName, discountPercentage);
            Assert.Equal(discountPercentage, item.DiscountPercentage);
        }

        [Fact]
        public void WhenCreated_ThrowsIfProductIsNull()
        {
            var discount = 1.0m;

            Assert.Throws<ArgumentNullException>(() => new ItemOnSaleRule(null, discount));
        }


        //JP: THIS TEST MUST BE DONE FOR CLASS DiscountCalculator, UNCOMMENT AND APPLY
        //[Theory]
        //[InlineData(new object[] { 10, 10, 9 })]
        //[InlineData(new object[] { 10, 1, 9.9 })]
        //[InlineData(new object[] { 10, 30, 7 })]
        //public void Cost_CalculateDiscountProperly(decimal originalCost, decimal discount, decimal discountedCost)
        //{
        //    var repo = new CurrencyTypeRepository();

        //    var prodM = fixture.Create<Mock<ISellableProduct>>();

        //    prodM.SetupGet(x => x.Cost).Returns(new Money(originalCost, repo.Get("EUR")));
        //    var prod = prodM.Object;

        //    var sale = new ItemOnSaleRule(prod.ProductName, discount);

        //    Assert.Equal(sale..Amount, discountedCost);
        //}
    }
}