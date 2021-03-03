using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors;
using SupermarketPricing.Api.Model1.Discounts.DiscountRules;
using SupermarketPricing.Domain.Modules.Purchase;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace SupermarketPricing.Api.Tests
{
    public class DiscountableByQuantityOfferRuleTests
    {
        private IFixture fixture;
        private DiscountableByQuantityRule rule;

        public DiscountableByQuantityOfferRuleTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

        [Fact]
        public void Create_NullChecks()
        {
            var pi = MockAPurchaseItem().Object;
            var validDiscountPercentage = 0.1m;
            var validDiscountVolumeQuantity = 1;

            var d = new DiscountableByQuantityRuleDescriptor(fixture.Create<string>(), validDiscountPercentage, validDiscountVolumeQuantity);

            Assert.Throws<ArgumentNullException>(() => new DiscountableByQuantityRule(null, d));
            Assert.Throws<ArgumentNullException>(() => new DiscountableByQuantityRule(pi, null));
        }

        [Theory]
        [ClassData(typeof(CalculatorTestData))]
        public void DiscountCalculation(int prodQty, Money unitCost, int percentDiscount, int qtyForDiscount, Money expectedTotalCost)
        {
            var pi = MockAPurchaseItem();
            pi.SetupGet(x => x.Quantity).Returns(prodQty);
            pi.SetupGet(x => x.ProductCost).Returns(unitCost);

            var d = new DiscountableByQuantityRuleDescriptor(fixture.Create<string>(), percentDiscount, qtyForDiscount);

            rule = new DiscountableByQuantityRule(pi.Object, d);
            var calculatedCost = rule.ProductCost;

            Assert.Equal(expectedTotalCost, calculatedCost);
        }

        private static Money CreateDollarsFrom(decimal unitCost)
        {
            return Money.Create(unitCost, "USD".ToCurrency());
        }

        private Mock<IPurchaseItem> MockAPurchaseItem()
        {
            return fixture.Create<Mock<IPurchaseItem>>();
        }

        public class CalculatorTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 10, CreateDollarsFrom(10), 10, 10, CreateDollarsFrom(90) };
                yield return new object[] { 10, CreateDollarsFrom(10), 10, 5, CreateDollarsFrom(90) };
                yield return new object[] { 10, CreateDollarsFrom(2), 10, 5, CreateDollarsFrom(18) };
                yield return new object[] { 10, CreateDollarsFrom(2), 10, 7, CreateDollarsFrom(18.6m) };
                yield return new object[] { 10, CreateDollarsFrom(3), 15, 3, CreateDollarsFrom(25.95m) };
            }

            //private object[] blah() {
            //}

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}