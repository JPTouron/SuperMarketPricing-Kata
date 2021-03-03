using Ardalis.GuardClauses;
using SupermarketPricing.Api.Model1.Discounts.DiscountDescriptors;
using SupermarketPricing.Api.Model1.Discounts.DiscountRules.Base;
using SupermarketPricing.Domain.Modules.Purchase;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using System.Collections.Generic;

namespace SupermarketPricing.Api.Model1.Discounts.DiscountRules
{
    public sealed class DiscountableByQuantityRule : DiscountRule
    {
        private readonly IDiscountableByQuantityOfferDescriptor descriptor;

        public DiscountableByQuantityRule(IPurchaseItem item, IDiscountableByQuantityOfferDescriptor descriptor) : base(item)
        {
            Guard.Against.Null(descriptor, nameof(descriptor));

            this.descriptor = descriptor;
        }

        public override Money ProductCost
        {
            get
            {
                var calc = new DiscountCalculator(item, descriptor);

                var groupsSubtotal = calc.GetDiscountGroupsSubtotal();
                var individualSubtotal = calc.GetIndividualUnitsSubtotal();

                return groupsSubtotal + individualSubtotal;
            }
        }

        private class DiscountCalculator
        {
            private readonly IDiscountableByQuantityOfferDescriptor descriptor;
            private readonly IPurchaseItem item;
            private IList<DiscountGroup> groups;

            public DiscountCalculator(IPurchaseItem item, IDiscountableByQuantityOfferDescriptor descriptor)
            {
                this.descriptor = descriptor;
                this.item = item;
                groups = GetDiscountGroups();
            }

            public Money GetDiscountGroupsSubtotal()
            {
                var totalGroupsCost = Money.NoMoney(item.ProductCost);

                foreach (var group in groups)
                    totalGroupsCost += group.GroupCost;

                var subtotal = totalGroupsCost.CalculatePercentageDiscount(descriptor.DiscountPercentage);

                return subtotal;
            }

            public Money GetIndividualUnitsSubtotal()
            {
                var unitAlone = GetIndividualUnitsCount();
                var singleUnitsSubtotal = item.ProductCost * unitAlone;

                return singleUnitsSubtotal;
            }

            private IList<DiscountGroup> GetDiscountGroups()
            {
                var groupsFound = new List<DiscountGroup>();
                var count = GetDiscountGroupsCount(item.Quantity, descriptor.DiscountVolumeQuantity);

                for (int i = 0; i < count; i++)
                {
                    groupsFound.Add(new DiscountGroup(descriptor.DiscountVolumeQuantity, item.ProductCost));
                }

                return groupsFound;
            }

            private int GetDiscountGroupsCount(int purchasedQuantity, int discountVolumeQuantity)
            {
                return purchasedQuantity / discountVolumeQuantity;
            }

            private int GetIndividualUnitsCount()
            {
                var itemsInDiscountGroups = groups.Count * descriptor.DiscountVolumeQuantity;
                var singleUnitsCount = item.Quantity - itemsInDiscountGroups;

                return singleUnitsCount;
            }

            private class DiscountGroup
            {
                private readonly Money unitCost;
                private readonly int unitsInDiscountGroup;

                public DiscountGroup(int unitsInDiscountGroup, Money unitCost)
                {
                    this.unitsInDiscountGroup = unitsInDiscountGroup;
                    this.unitCost = unitCost;
                }

                public Money GroupCost => unitCost * unitsInDiscountGroup;
            }
        }
    }
}