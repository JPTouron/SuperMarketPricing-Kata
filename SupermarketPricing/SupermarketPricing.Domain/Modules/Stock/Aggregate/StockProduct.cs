using Ardalis.GuardClauses;
using SupermarketPricing.Domain.Modules.SharedKernel.Products;
using System;

namespace SupermarketPricing.Domain.Modules.Stock.Aggregate
{
    public partial class Storage
    {
        private class StockProduct : IStockableProduct
        {
            private int currentQuantity;

            public StockProduct(IProduct product, int initialQuantity)
            {
                Guard.Against.Null(product, nameof(product));
                Guard.Against.Negative(initialQuantity, nameof(initialQuantity));

                Product = product;
                currentQuantity = initialQuantity;
            }

            public IProduct Product { get; }

            public int Quantity => currentQuantity;

            public virtual void DecreaseStock(int quantityToReduce)
            {
                if (CanReduceQuantity(quantityToReduce))
                    currentQuantity -= quantityToReduce;
                else
                    throw new InvalidOperationException($"Currently having {currentQuantity} in stock. Cannot reduce {quantityToReduce} units!");
            }

            private bool CanReduceQuantity(int quantityToReduce)
            {
                return currentQuantity > 0 && currentQuantity - quantityToReduce >= 0;
            }
        }
    }
}