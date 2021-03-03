using Ardalis.GuardClauses;
using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket.SaleOffers.VisitorPattern;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketPricing.Model1.SuperMarket.Purchase
{
    public interface IPurchaseOrder
    {
        IReadOnlyList<IPurchaseItem> GetList { get; }

        Money TotalCost { get; }
        Currency Currency { get; }

        void AddProduct(IPurchaseItem item);
    }

    public class PurchaseOrder : IPurchaseOrder
    {
        private readonly IDiscountCalculator discountCalculator;
        private IList<IPurchaseItem> items;

        public PurchaseOrder(Currency currency, IDiscountCalculator discountCalculator)
        {
            items = new List<IPurchaseItem>();
            Currency = currency;
            this.discountCalculator = discountCalculator;
        }

        public Money TotalCost
        {
            get
            {
                //JP: what if the currency is set to USD, but the items are in EUR or something else?
                var total = new Money(0, Currency);

                //JP: purchase order should know what to apply to each item, for that it whould feed the purchase item the algo to work with
                //if i dont wanna setup checkings for algo here, then i gotta use visitor, so the visitor checks what algo applies for a given
                //purch item
                items.ToList().ForEach(x =>
                {
                    total += x.SubtotalCost(discountCalculator);
                });

                return total;
            }
        }

        public Currency Currency { get; }

        IReadOnlyList<IPurchaseItem> IPurchaseOrder.GetList => (IReadOnlyList<IPurchaseItem>)items;

        public void AddProduct(IPurchaseItem item)
        {
            Guard.Against.Null(item, nameof(item));

            items.Add(item);
        }
    }
}