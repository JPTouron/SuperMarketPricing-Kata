using SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors.Base;
using SupermarketPricing.Api.Model1.Discounts.DiscountRules;
using SupermarketPricing.Domain.Modules.Purchase;

namespace SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors
{
    public class NoDiscountDescriptor : IDiscountDescriptor
    {
        public string ProductName => string.Empty;

        public IPurchaseItem ApplyDiscount(IPurchaseItem item)
        {
            return new NoDiscountRule(item);
        }
    }
}