using Ardalis.GuardClauses;
using SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors.Base;
using SupermarketPricing.Api.Model1.Discounts.DiscountRules;
using SupermarketPricing.Domain.Modules.Purchase;
using SuperMarketPricing.Domain.BuildingBlocks;
using SuperMarketPricing.Domain.BuildingBlocks.Base;

namespace SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors
{
    public interface ITimeableDiscountRuleDescriptor : IDiscountDescriptor
    {
        decimal DiscountPercentage { get; }

        DateRange Period { get; }

        IClock SystemClock { get; }
    }

    public class TimeableDiscountRuleDescriptor : ITimeableDiscountRuleDescriptor
    {
        public TimeableDiscountRuleDescriptor(decimal discountPercentage, IClock systemClock, DateRange period, string productName)
        {
            Guard.Against.OutOfRange(discountPercentage, nameof(discountPercentage), 0.1m, 100m);
            Guard.Against.Null(systemClock, nameof(systemClock));
            Guard.Against.Null(period, nameof(period));
            Guard.Against.NullOrEmpty(productName, nameof(productName));

            DiscountPercentage = discountPercentage;
            SystemClock = systemClock;
            Period = period;
            ProductName = productName;
        }

        public decimal DiscountPercentage { get; }

        public DateRange Period { get; }

        public string ProductName { get; }

        public IClock SystemClock { get; }

        public IPurchaseItem ApplyDiscount(IPurchaseItem item)
        {
            return new TimeableDiscountRule(item, this);
        }
    }
}