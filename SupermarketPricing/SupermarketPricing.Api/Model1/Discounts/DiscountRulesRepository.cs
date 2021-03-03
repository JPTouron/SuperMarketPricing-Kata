using SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors;
using SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors.Base;
using SupermarketPricing.Domain.Modules.Purchase;
using SuperMarketPricing.Domain.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketPricing.Api.Model1.Discounts
{
    public interface IDiscountRulesRepository
    {
        IPurchaseItem ApplyDiscountRuleTo(IPurchaseItem item);
    }

    public class DiscountRulesRepository : IDiscountRulesRepository
    {
        private readonly IReadOnlyList<IDiscountDescriptor> descriptors;

        public DiscountRulesRepository()
        {
            descriptors = new List<IDiscountDescriptor>
            {
                new DiscountableByQuantityRuleDescriptor("Soda Can", 10,1),
                new DiscountableByQuantityRuleDescriptor("Cat Food", 20,2 )
            };
        }

        /// <summary>
        /// Visitor's element's visit method. Gets purchase items decorated with discounts
        /// </summary>
        public IPurchaseItem ApplyDiscountRuleTo(IPurchaseItem item)
        {
            var maybeDescriptor = GetDiscountDescriptorFor(item);
            var descriptor = maybeDescriptor.GetValueOrFallback(new NoDiscountDescriptor());

            var discountedItem = descriptor.ApplyDiscount(item);

            return discountedItem;
        }

        private Maybe<IDiscountDescriptor> GetDiscountDescriptorFor(IPurchaseItem item)
        {
            var descriptor = descriptors.SingleOrDefault(x => x.ProductName.Equals(item.ProductName, StringComparison.OrdinalIgnoreCase));

            return descriptor == null ? new Maybe<IDiscountDescriptor>() : new Maybe<IDiscountDescriptor>(descriptor);
        }
    }
}