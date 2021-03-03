using Ardalis.GuardClauses;
using SupermarketPricing.Model1.Core;
using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket.Contracts;
using SupermarketPricing.Model1.SuperMarket.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketPricing.Model1.SuperMarket.Stock
{
    public interface IStorage
    {
        Maybe<ISellableProduct> GetProduct(string name, int quantity);

        void LoadAllProducts();
    }

    /// <summary>
    /// most likely to be an aggregate, since when a sale is made then this should be impacted
    /// (we should decrease quantity in a centralized manner) to keep track of products' stock
    /// </summary>
    public class Storage : IStorage
    {
        private IList<IStockableProduct> products;

        public void LoadAllProducts()
        {
            products = new List<IStockableProduct>();

            products.Add(CreateStockProduct("Soda Can", 1.04m, 1));
            products.Add(CreateStockProduct("Fish", 3.50m, 10));
            products.Add(CreateStockProduct("Tomato Sauce", 2.32m, 10));
            products.Add(CreateStockProduct("Cat Food", 3.40m, 10));
        }

        public Maybe<ISellableProduct> GetProduct(string name, int quantity)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NegativeOrZero(quantity, nameof(quantity));
            var stockable = GetProductOrNone(name);



            if (!(stockable is NoStockProduct) && stockable.Quantity >= quantity)
            {
                stockable.DecreaseStock(quantity);

                LogMessage($"Took {quantity} {stockable.Product.ProductName} from the storage. {stockable.Quantity} remaining");

                return new Maybe<ISellableProduct>((ISellableProduct)stockable.Product);
            }
            else if (stockable is NoStockProduct)
                LogMessage($"The requested {name} product is not in our stock! Sorry! ");
            else
                LogMessage($"You cannot take {quantity} {stockable.Product.ProductName}. We only have {stockable.Quantity} left, sorry!");

            return new Maybe<ISellableProduct>();
        }

        private IStockableProduct GetProductOrNone(string name)
        {
            var result = products.FirstOrDefault(x => x.Product.ProductName == name);

            return result ?? new NoStockProduct();
        }

        private void LogMessage(string v)
        {
            Console.WriteLine(v);
        }

        private IStockableProduct CreateStockProduct(string name, decimal cost, int quantity)
        {
            var item = CreateItem(name, cost);
            var sp = new StockProduct(item, quantity);
            return sp;
        }

        private ProductItem CreateItem(string name, decimal cost)
        {
            return new ProductItem(name, new Money(cost, "EUR".ToCurrency()));
        }
    }
}