using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket.Products;
using SupermarketPricing.Model1.SuperMarket.Stock;
using Xunit;

namespace SupermarketPricingTests
{
    public class StorageTests
    {
        private Storage storage;
        private CurrencyTypeRepository repo;

        public StorageTests()
        {
            storage = new Storage();
            storage.LoadAllProducts();
            repo = new CurrencyTypeRepository();
        }

        [Fact]
        public void WhenProductDoesNotExist_ReturnsAnEmptyMaybe()
        {
            var result = storage.GetProduct("non existing product", 1);

            var fallbackItem = new ProductItem("...", new Money(0.1m, repo.Get("USD")));

            var item = result.GetValueOrFallback(fallbackItem);

            Assert.Equal(fallbackItem, item);
        }

        [Fact]
        public void WhenProductExists_ReturnsMaybeOfProduct()
        {
            var result = storage.GetProduct("Soda Can", 1);

            var fallbackItem = new ProductItem("...", new Money(0.1m, repo.Get("USD")));

            var item = result.GetValueOrFallback(fallbackItem);

            Assert.NotEqual(fallbackItem, item);

            Assert.Equal("Soda Can", item.ProductName);
            Assert.Equal(1.04m, item.Cost.Amount);
        }

        [Fact]
        public void WhenQuantityExceedsCurrentStock_ReturnsMaybeOfProduct()
        {
            const int MaxQuantityExceededByOne = 11;
            var result = storage.GetProduct("Soda Can", MaxQuantityExceededByOne);

            var fallbackItem = new ProductItem("...", new Money(0.1m, repo.Get("USD")));

            var item = result.GetValueOrFallback(fallbackItem);

            Assert.Equal(fallbackItem, item);
        }

        [Fact]
        public void WhenProductIsReturned_QuantityDecreases()
        {
            const int MaxQuantity = 10;
            var result = storage.GetProduct("Soda Can", MaxQuantity);

            var fallbackItem = new ProductItem("...", new Money(0.1m, repo.Get("USD")));

            var item = result.GetValueOrFallback(fallbackItem);

            Assert.NotEqual(fallbackItem, item);

            Assert.Equal("Soda Can", item.ProductName);
            Assert.Equal(1.04m, item.Cost.Amount);

            int anExtraItemQuantity = 1;
            result = storage.GetProduct("Soda Can", anExtraItemQuantity);

            item = result.GetValueOrFallback(fallbackItem);

            Assert.Equal(fallbackItem, item);
        }
    }
}