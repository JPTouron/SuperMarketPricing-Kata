using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using static SupermarketPricing.Model1.SuperMarket.SaleOffers.DiscountRules;

namespace SupermarketPricing.Model1.SuperMarket.SaleOffers.VisitorPattern
{
    public class DiscountCalculator : IDiscountCalculator
    {
        private IDiscountRulesRepository discountRules;

        public DiscountCalculator(IDiscountRulesRepository discountRules)
        {
            this.discountRules = discountRules;
        }

        public Money CalculateDiscount(IPurchaseItem item)
        {
            var applyingRules = discountRules.GetDiscountRules(item);

            var fallbackValue = new List<NoDiscountRule> { new NoDiscountRule() };
            var result = applyingRules.GetValueOrFallback(fallbackValue);

            decimal calculatedCost;

            LogMessage($"Discounts Are Cummulative: {discountRules.DiscountsAreCumulative}");

            if (!discountRules.DiscountsAreCumulative)
            {

                var discountRule = result.First();


                var fullAmount = item.FullCost.Amount;
                calculatedCost = PercentageCalculator.CalculatePercentageDiscount(fullAmount, discountRule.DiscountPercentage);
                
                LogMessage($"Let's see... you have {item.Quantity} of {item.Product.ProductName} ({item.FullCost}). The {discountRule.GetType().Name} discount applies\r\n. That's a {discountRule.DiscountPercentage}% discount for you! ({calculatedCost })\r\n");
            }
            else
            {
                //calculate discounts one over the other, as they are cummulative

                //JP: POSSIBLE DONT TALK TO STRANGER VIOLATION HERE, CHECK IF WE NEED TO USE THE NAME OF THE ITEM FOR SMTHNG IF NOT, TAKE MONEY AS PARAMETER
                calculatedCost = item.FullCost.Amount;

                foreach (var rule in result)
                {
                    calculatedCost = PercentageCalculator.CalculatePercentageDiscount(calculatedCost, rule.DiscountPercentage);
                }
            }

            var newCost = new Money( calculatedCost, item.Product.Cost);

            return newCost;
        }

        private void LogMessage(string v)
        {
            Console.WriteLine(v);
        }
    }
}