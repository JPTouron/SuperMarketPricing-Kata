using SupermarketPricing.Api.Model1.Discounts.DiscountRules.Base;
using SupermarketPricing.Domain.Modules.Purchase;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;

namespace SupermarketPricing.Api.Model1.Discounts.DiscountRules
{
    public class DiscountableByPaymentMethodRule : DiscountRule
    {
        public DiscountableByPaymentMethodRule(IPurchaseItem item) : base(item)
        {
        }

        public override Money ProductCost => item.ProductCost;
    }
}