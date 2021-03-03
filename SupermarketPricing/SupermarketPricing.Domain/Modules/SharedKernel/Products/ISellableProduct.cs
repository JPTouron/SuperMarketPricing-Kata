using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;

namespace SupermarketPricing.Domain.Modules.SharedKernel.Products
{
    public interface ISellableProduct : IProduct
    {
        public Money Cost { get; }

        public bool IsSellable { get; }
    }
}