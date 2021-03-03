using SupermarketPricing.Api.Model1.SuperMarket;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.Extensions;
using System;

namespace SupermarketPricing.Client
{
    internal class Program
    {
        private static ISuperMarketApi sm;

        private static void Main(string[] args)
        {
            Console.WriteLine("Let's buy groceries!");
            Console.WriteLine("");

            sm = new SuperMarketApi("EUR".ToCurrency());

            sm.StartPurchaseOrder();

            sm.AddItemToPurchaseOrder("Soda Can", 3);
            sm.AddItemToPurchaseOrder("non-existing-item", 3);
            sm.AddItemToPurchaseOrder("Fish", 4);
            sm.AddItemToPurchaseOrder("Tomato Sauce", 2);
            sm.AddItemToPurchaseOrder("Cat Food", 5);

            Console.WriteLine("Calculating Total...");
            Console.WriteLine("");
            Console.WriteLine("");

            var ticketTotal = sm.GetPurchaseTotalCost();

            Console.WriteLine($"Your grand total is: {ticketTotal}");

            sm.ClosePurchaseOrder();
        }
    }
}