using Ardalis.GuardClauses;
using SupermarketPricing.Model1.Core;
using SupermarketPricing.Model1.SuperMarket.Contracts;

namespace SupermarketPricing.Model1.SuperMarket.SaleOffers
{
    public class DiscountRules
    {
        //JP: RENAME THIS CLASS TO NORMAL CALCULATION RULE
        public class NoDiscountRule : IDiscountRule
        {

            //JP: watch out for this!!!
            public string ProductName => "Not following the INTERFACE SEGREGATION HERE";

            public decimal DiscountPercentage => 0;
        }

        public class ItemOnSaleRule : IDiscountRule
        {
            public ItemOnSaleRule(string productName, decimal discountPercentage)
            {
                Guard.Against.NullOrEmpty(productName, nameof(productName));
                Guard.Against.OutOfRange(discountPercentage, nameof(discountPercentage), 0.1m, 100);

                ProductName = productName;
                DiscountPercentage = discountPercentage;
            }

            public string ProductName { get; }

            public decimal DiscountPercentage { get; }
        }

        public class PercentOffOnVolumeRule : IDiscountableByQuantityOfferRule, IDiscountRule
        {
            public PercentOffOnVolumeRule(string productName, int quantity, decimal discountPercentage)
            {
                Guard.Against.NullOrEmpty(productName, nameof(productName));
                Guard.Against.NegativeOrZero(quantity, nameof(quantity));
                Guard.Against.OutOfRange(discountPercentage, nameof(discountPercentage), 0.1m, 100m);

                ProductName = productName;
                Quantity = quantity;
                DiscountPercentage = discountPercentage;
            }

            public string ProductName { get; }

            public int Quantity { get; }

            public decimal DiscountPercentage { get; }
        }

        public class PercentOffOnVolumeWithinTimeRule : IDiscountableByQuantityOfferRule, ITimeableDiscountRule, IDiscountRule
        {
            public PercentOffOnVolumeWithinTimeRule(string productName, int quantity, decimal discountPercentage, DateRange dateRange)
            {
                Guard.Against.NullOrEmpty(productName, nameof(productName));
                Guard.Against.NegativeOrZero(quantity, nameof(quantity));
                Guard.Against.OutOfRange(discountPercentage, nameof(discountPercentage), 0.1m, 100m);

                ProductName = productName;
                Quantity = quantity;
                DiscountPercentage = discountPercentage;
                Period = dateRange;
            }

            public string ProductName { get; }

            public int Quantity { get; }

            public decimal DiscountPercentage { get; }

            public DateRange Period { get; }
        }
    }
}