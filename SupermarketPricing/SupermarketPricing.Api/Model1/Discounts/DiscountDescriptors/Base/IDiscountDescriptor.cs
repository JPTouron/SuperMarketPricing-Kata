using SupermarketPricing.Domain.Modules.Purchase;

namespace SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors.Base
{
    public interface IDiscountDescriptor
    {
        public string ProductName { get; }

        public IPurchaseItem ApplyDiscount(IPurchaseItem item);
    }
}