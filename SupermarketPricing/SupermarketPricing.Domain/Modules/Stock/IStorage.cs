using SupermarketPricing.Domain.Modules.SharedKernel.Products;
using SuperMarketPricing.Domain.BuildingBlocks;

namespace SupermarketPricing.Domain.Modules.Stock
{
    public interface IStorage
    {
        void AddProductToStorage(ISellableProduct product, int initialQuantity);

        Maybe<ISellableProduct> GetProduct(string name, int quantity);
    }
}