using SupermarketPricing.Model1.Core;
using SupermarketPricing.Model1.SuperMarket.Contracts;
using SupermarketPricing.Model1.SuperMarket.Purchase;
using System.Collections.Generic;
using System.Linq;
using static SupermarketPricing.Model1.SuperMarket.SaleOffers.DiscountRules;

namespace SupermarketPricing.Model1.SuperMarket.SaleOffers
{
    public interface IDiscountRulesRepository
    {
        Maybe<IReadOnlyList<IDiscountRule>> GetDiscountRules(IPurchaseItem item);

        bool DiscountsAreCumulative { get; }
    }

    public class DiscountRulesRepository : IDiscountRulesRepository
    {
        public readonly IReadOnlyList<IDiscountRule> discountRules;

        public DiscountRulesRepository()
        {
            //this could be read from a json file

            discountRules = new List<IDiscountRule>
            {
                new ItemOnSaleRule("Soda Can", 10),
                new PercentOffOnVolumeRule("Cat Food", 2, 20)
            };

            DiscountsAreCumulative = false;
        }

        public bool DiscountsAreCumulative { get; }

        public Maybe<IReadOnlyList<IDiscountRule>> GetDiscountRules(IPurchaseItem item)
        {
            var items = discountRules.Where(x => x.ProductName == item.Product.ProductName).ToList();


            return items.Count > 0 ? new Maybe<IReadOnlyList<IDiscountRule>>(items) : new Maybe<IReadOnlyList<IDiscountRule>>();
        }
    }
}