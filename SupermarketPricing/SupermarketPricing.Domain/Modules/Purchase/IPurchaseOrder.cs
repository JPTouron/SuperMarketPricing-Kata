using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.CurrencyModel;
using System.Collections.Generic;

namespace SupermarketPricing.Domain.Modules.Purchase
{
    public interface IPurchaseOrder
    {
        Currency Currency { get; }

        IReadOnlyList<IPurchaseItem> GetList { get; }

        Money TotalCost { get; }

        void AddItemToOrder(string productName, int quantity);
    }
}