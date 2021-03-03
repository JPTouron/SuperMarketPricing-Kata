using SupermarketPricing.Domain.Modules.SharedKernel.Products;
using SupermarketPricing.Domain.Modules.Stock;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.Extensions;
using Xunit;
using Storage = SupermarketPricing.Domain.Modules.Stock.Aggregate.Storage;

namespace SupermarketPricing.Domain.Tests.Aggregates
{
    public class StorageTests
    {
        private const string ProductName = "Soda Can";
        private IStorage storage;

        public StorageTests()
        {
            storage = new Storage();
        }

        [Fact]
        public void WhenProductDoesNotExist_ReturnsANoSellableProduct()
        {
            var result = storage.GetProduct("non existing product", 1);

            var fallbackItem = new NoSellableProduct();

            var item = result.GetValueOrFallback(fallbackItem);

            Assert.IsType<NoSellableProduct>(item);

            Assert.Equal(Money.NoMoneyDollars(), item.Cost);
            Assert.Equal(string.Empty, item.ProductName);
            Assert.False(item.IsSellable);
        }

        [Fact]
        public void WhenProductExists_ReturnsMaybeOfProduct()
        {
            SetSodaCanInStorage(1);

            var result = storage.GetProduct(ProductName, 1);

            var fallbackItem = new SellableProduct("...", Money.Create(0.1m, "USD".ToCurrency()));

            var item = result.GetValueOrFallback(fallbackItem);

            Assert.NotEqual(fallbackItem, item);

            Assert.Equal(ProductName, item.ProductName);
            Assert.Equal(1.04m, item.Cost.Amount);
        }

        [Fact]
        public void WhenProductIsReturned_ProductQuantityInStorageDecreases()
        {
            int availableProductQuantity = 10;
            SetSodaCanInStorage(availableProductQuantity);

            //request all available items from storage (so we can empty it)

            var result = storage.GetProduct(ProductName, availableProductQuantity);

            var fallbackItem = new NoSellableProduct();

            var item = result.GetValueOrFallback(fallbackItem);

            Assert.IsNotType<NoSellableProduct>(item);

            //try to request just another one item of the same product from storage

            int anExtraItemQuantity = 1;
            result = storage.GetProduct(ProductName, anExtraItemQuantity);

            item = result.GetValueOrFallback(fallbackItem);

            Assert.IsType<NoSellableProduct>(item);
        }

        [Fact]
        public void WhenQuantityExceedsCurrentStock_ReturnsFallBackProductWhenItemRequested()
        {
            SetSodaCanInStorage(1);

            const int MaxQuantityExceededByOne = 11;
            var result = storage.GetProduct(ProductName, MaxQuantityExceededByOne);

            var fallbackItem = new SellableProduct("...", Money.Create(0.1m, "USD".ToCurrency()));

            var item = result.GetValueOrFallback(fallbackItem);

            Assert.Equal(fallbackItem, item);
        }

        private static ISellableProduct CreateItem(string name, decimal cost)
        {
            return new SellableProduct(name, Money.Create(cost, "EUR".ToCurrency()));
        }

        private void SetSodaCanInStorage(int quantity)
        {
            storage.AddProductToStorage(CreateItem(ProductName, 1.04m), quantity);
        }
    }
}