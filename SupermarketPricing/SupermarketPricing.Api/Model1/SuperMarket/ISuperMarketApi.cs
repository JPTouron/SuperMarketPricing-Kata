using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;

namespace SupermarketPricing.Api.Model1.SuperMarket
{
    public interface ISuperMarketApi
    {
        void AddItemToPurchaseOrder(string name, int quantity);

        void ClosePurchaseOrder();

        Money GetPurchaseTotalCost();

        void StartPurchaseOrder();
    }
}