using SupermarketPricing.Domain.Modules.SharedKernel.Products;

namespace SupermarketPricing.Domain.Modules.Stock
{
    public interface IStockableProduct
    {
        IProduct Product { get; }

        int Quantity { get; }

        void DecreaseStock(int quantityToReduce);
    }
}