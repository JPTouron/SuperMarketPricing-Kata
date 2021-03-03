using SupermarketPricing.Model1.SuperMarket;
using System;

namespace SupermarketPricing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var bread = new SuperMarketItems.Bread();
            var breadOnSale = new ItemOnSale(bread, 10);

            Console.WriteLine($"hey! the cost of this bread: {bread.ProductName}, is: {bread.Cost}");
            Console.WriteLine($"oh! It just went on sale for: {breadOnSale.DiscountPercentage}%!, it now costs: {breadOnSale.Cost}");
        }
    }
}