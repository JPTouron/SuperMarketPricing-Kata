using AutoFixture;
using AutoFixture.AutoMoq;
using SupermarketPricing.Model1.SuperMarket.Purchase;
using Xunit;

namespace SupermarketPricingTests
{
    public class PurchasItemTests
    {
        private IFixture fixture;

        private IPurchaseItem item;

        public PurchasItemTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

        [Fact]
        public void WhenCreated_HasProductAndQuantity()
        {
            item = fixture.Create<PurchaseItem>();

            Assert.NotNull(item.Product);
            Assert.True(item.Quantity > 0);
        }
    }
}