using Ardalis.GuardClauses;
using SupermarketPricing.Api.Model1.Discounts;
using SupermarketPricing.Domain.ApplicationPorts;
using SupermarketPricing.Domain.Modules.Purchase;

namespace SupermarketPricing.Api.Model1.PortsImplementations
{
    internal class DiscountApplier : IDiscountApplier
    {
        private readonly IDiscountRulesRepository rulesRepository;

        public DiscountApplier(IDiscountRulesRepository rulesRepository)
        {
            Guard.Against.Null(rulesRepository, nameof(rulesRepository));

            this.rulesRepository = rulesRepository;
        }

        public IPurchaseItem ApplyDiscount(IPurchaseItem item)
        {
            var discountedItem = rulesRepository.ApplyDiscountRuleTo(item);

            return discountedItem;
        }
    }
}