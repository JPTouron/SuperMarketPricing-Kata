using Ardalis.GuardClauses;
using SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors.Base;
using SupermarketPricing.Api.Model1.Discounts.DiscountRules;
using SupermarketPricing.Domain.Modules.Purchase;

namespace SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors
{
    public interface IDiscountableByQuantityOfferDescriptor : IDiscountDescriptor
    {
        decimal DiscountPercentage { get; }

        int DiscountVolumeQuantity { get; }
    }

    public class DiscountableByQuantityRuleDescriptor : IDiscountableByQuantityOfferDescriptor
    {
        public DiscountableByQuantityRuleDescriptor(string productName, decimal discountPercentage, int discountVolumeQuantity)
        {
            Guard.Against.NullOrEmpty(productName, nameof(productName));
            Guard.Against.NegativeOrZero(discountVolumeQuantity, nameof(discountVolumeQuantity));
            Guard.Against.OutOfRange(discountPercentage, nameof(discountPercentage), 0.1m, 100m);

            ProductName = productName;
            DiscountPercentage = discountPercentage;
            DiscountVolumeQuantity = discountVolumeQuantity;
        }

        public decimal DiscountPercentage { get; }

        public int DiscountVolumeQuantity { get; }

        public string ProductName { get; }

        public IPurchaseItem ApplyDiscount(IPurchaseItem item)
        {
            return new DiscountableByQuantityRule(item, this);
        }
    }
}