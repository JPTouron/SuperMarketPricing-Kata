using Ardalis.GuardClauses;
using SupermarketPricing.Domain.Modules.SharedKernel.Products;
using SuperMarketPricing.Domain.BuildingBlocks;
using SuperMarketPricing.Domain.BuildingBlocks.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketPricing.Domain.Modules.Stock.Aggregate
{
    public partial class Storage : IStorage, IAggregateRoot
    {
        private IList<IStockableProduct> products;

        public Storage()
        {
            products = new List<IStockableProduct>();
        }

        public void AddProductToStorage(ISellableProduct product, int initialQuantity)
        {
            var stockProd = new Storage.StockProduct(product, initialQuantity);
            products.Add(stockProd);
        }

        public Maybe<ISellableProduct> GetProduct(string name, int quantity)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NegativeOrZero(quantity, nameof(quantity));

            var stockable = GetProductOrNone(name);

            if (!(stockable is Storage.NoStockProduct) && stockable.Quantity >= quantity)
            {
                stockable.DecreaseStock(quantity);

                LogMessage($"Took {quantity} {stockable.Product.ProductName} from the storage. {stockable.Quantity} remaining");

                return new Maybe<ISellableProduct>((ISellableProduct)stockable.Product);
            }
            else if (stockable is Storage.NoStockProduct)
                LogMessage($"The requested {name} product is not in our stock! Sorry! ");
            else
                LogMessage($"You cannot take {quantity} {stockable.Product.ProductName}. We only have {stockable.Quantity} left, sorry!");

            return new Maybe<ISellableProduct>();
        }

        private IStockableProduct GetProductOrNone(string name)
        {
            var result = products.FirstOrDefault(x => x.Product.ProductName == name);

            return result ?? new Storage.NoStockProduct();
        }

        private void LogMessage(string v)
        {
            Console.WriteLine(v);
            Console.WriteLine("");
        }
    }
}