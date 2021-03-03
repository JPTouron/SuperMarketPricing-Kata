using SupermarketPricing.Api.Model1.Discounts.DiscountRules.Base;
using SupermarketPricing.Domain.Modules.Purchase;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;

namespace SupermarketPricing.Api.Model1.Discounts.DiscountRules
{
    public class NoDiscountRule : DiscountRule
    {
        public NoDiscountRule(IPurchaseItem item) : base(item)
        {
        }

        public override Money ProductCost
        {
            get
            {
                LogMessage($"{item.ProductName} - {item.Quantity} x {item.ProductCost} = ({item.ProductCost * item.Quantity }).\r\n");

                return item.ProductCost * item.Quantity;
            }
        }
    }
}