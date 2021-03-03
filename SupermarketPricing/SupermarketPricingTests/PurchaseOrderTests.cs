using AutoFixture;
using AutoFixture.AutoMoq;
using SupermarketPricing.Model1.SuperMarket.Purchase;
using System;
using System.Linq;
using Xunit;

namespace SupermarketPricingTests
{
    public class PurchaseOrderTests
    {
        private IPurchaseOrder order;
        private IFixture fixture;

        public PurchaseOrderTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

        [Fact]
        public void WhenCreated_HasListEmpty()
        {
            order = fixture.Create<PurchaseOrder>();

            Assert.NotNull(order.GetList);
            Assert.Empty(order.GetList);
        }

        [Fact]
        public void AddProduct_AddsAProductToTheList()
        {
            var item = fixture.Create<PurchaseItem>();
            order = fixture.Create<PurchaseOrder>();

            order.AddProduct(item);

            Assert.Equal(item, order.GetList.Single());
        }

        [Fact]
        public void AddProduct_WhenNulThrows()
        {
            order = fixture.Create<PurchaseOrder>();

            Assert.Throws<ArgumentNullException>(() => order.AddProduct(null));
        }
    }
}