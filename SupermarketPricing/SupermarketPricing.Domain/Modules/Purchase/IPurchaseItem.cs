using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;

namespace SupermarketPricing.Domain.Modules.Purchase
{
    public interface IPurchaseItem
    {
        Money ProductCost { get; }

        string ProductName { get; }

        int Quantity { get; }
    }
}