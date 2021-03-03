using Ardalis.GuardClauses;
using SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors;
using SupermarketPricing.Api.Model1.Discounts.DiscountRules.Base;
using SupermarketPricing.Domain.Modules.Purchase;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;

namespace SupermarketPricing.Api.Model1.Discounts.DiscountRules
{
    /// <summary>
    /// a product that's on sale during a period of time
    /// </summary>
    public class TimeableDiscountRule : DiscountRule
    {
        private readonly ITimeableDiscountRuleDescriptor descriptor;

        public TimeableDiscountRule(
            IPurchaseItem item,
            ITimeableDiscountRuleDescriptor descriptor) : base(item)
        {
            Guard.Against.Null(descriptor, nameof(descriptor));

            this.descriptor = descriptor;
        }

        public override Money ProductCost => ApplyDiscountRule(item, descriptor);

        private Money ApplyDiscountRule(IPurchaseItem item, ITimeableDiscountRuleDescriptor descriptor)
        {
            var cost = item.ProductCost;
            var discountPercentage = descriptor.DiscountPercentage;
            var period = descriptor.Period;
            var systemClock = descriptor.SystemClock;

            LogMessage($"Applying {discountPercentage}% to {item.ProductName} = {cost} / {GetType().Name} \r\n");

            if (period.IsDateWithinRange(systemClock.Now))
                cost = cost.CalculatePercentageDiscount(discountPercentage);

            return cost;
        }
    }
}