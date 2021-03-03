using SupermarketPricing.Model1.Core;
using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket.Contracts;
using SupermarketPricing.Model1.SuperMarket.Purchase;
using SupermarketPricing.Model1.SuperMarket.SaleOffers;
using SupermarketPricing.Model1.SuperMarket.SaleOffers.VisitorPattern;
using SupermarketPricing.Model1.SuperMarket.Stock;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketPricing.Model1.SuperMarket
{
    /// <summary>
    /// models the supermarket API and configures sales and offers
    /// </summary>
    public class SuperMarket
    {
        //we should solve all these with IoC
        private IStorage storage;

        private IDiscountCalculator discountCalculator;
        private IDiscountRulesRepository discountRules;

        public SuperMarket()
        {
            storage = new Storage();
            discountRules = new DiscountRulesRepository();
            discountCalculator = new DiscountCalculator(discountRules);
        }

        public void Open()
        {
            storage.LoadAllProducts();
        }

        public Money MakePurchase(IReadOnlyList<IPurchaseItem> products)
        {
            //find a better way to do this here...
            var currency = products.First().Product.Cost.Currency;

            var po = new PurchaseOrder(currency, discountCalculator);

            foreach (var item in products)
            {
                po.AddProduct(item);
            }

            var total = po.TotalCost;

            return total;
        }

        public Maybe<ISellableProduct> GetProduct(string name, int quantity)
        {
            return storage.GetProduct(name, quantity);
        }

        public void Close()
        {
            //should close the supermarket
        }
    }
}