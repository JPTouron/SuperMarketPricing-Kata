using SupermarketPricing.Model1.SuperMarket.Products;
using SupermarketPricing.Model1.SuperMarket.Stock;
using Xunit;

namespace SupermarketPricingTests
{
    public class NoStockProductTests
    {
        public NoStockProductTests()
        {
        }

        [Fact]
        public void DecreaseStock_DoesNothing()
        {
            var np = new NoStockProduct();
            np.DecreaseStock(10);

            Assert.Equal(0, np.Quantity);
        }

        [Fact]
        public void Create_ContainsNoProductAndZeroQuantity()
        {
            var np = new NoStockProduct();

            Assert.Equal(0, np.Quantity);
            Assert.IsType<NoProduct>(np.Product);
        }
    }
}