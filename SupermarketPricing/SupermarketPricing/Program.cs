using SupermarketPricing.Model1.SuperMarket;
using SupermarketPricing.Model1.SuperMarket.Products;
using SupermarketPricing.Model1.SuperMarket.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketPricing
{
    internal class Program
    {
        private static SuperMarket sm;

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            sm = new SuperMarket();

            sm.Open();

            var list = new List<IPurchaseItem>();

            list.Add(AddItemToList("Soda Can", 3));
            list.Add(AddItemToList("non-existing-item", 3));
            list.Add(AddItemToList("Fish", 4));
            list.Add(AddItemToList("Tomato Sauce", 2));
            list.Add(AddItemToList("Cat Food", 5));

            var l = list.Where(x => x.Product.ProductName.Length > 0);

            var ticketTotal = sm.MakePurchase(l.ToList().AsReadOnly());

            Console.WriteLine($"Your grand total is: {ticketTotal}");

            sm.Close();
        }

        private static IPurchaseItem AddItemToList(string v1, int v2)
        {
            var product = sm.GetProduct(v1, v2);

            var sp = product.GetValueOrFallback(new ProductItem("", null));

            return new PurchaseItem(sp, v2);
        }
    }
}