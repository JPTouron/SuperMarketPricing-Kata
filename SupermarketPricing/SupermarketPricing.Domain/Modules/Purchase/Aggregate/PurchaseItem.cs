using Ardalis.GuardClauses;
using SupermarketPricing.Domain.Modules.SharedKernel.Products;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;

namespace SupermarketPricing.Domain.Modules.Purchase.Aggregate
{
    public partial class PurchaseOrder
    {
        /// <summary>
        /// this class is inside PO as we wanted it to be private and conceal it completely from the outside
        /// </summary>
        private class PurchaseItem : IPurchaseItem
        {
            private readonly ISellableProduct product;

            public PurchaseItem(ISellableProduct product, int quantity)
            {
                Guard.Against.Null(product, nameof(product));
                Guard.Against.NegativeOrZero(quantity, nameof(quantity));

                this.product = product;
                Quantity = quantity;
            }

            public Money ProductCost => product.Cost;

            public int Quantity { get; }

            public string ProductName => product.ProductName;

            public override bool Equals(object obj)
            {
                var other = obj as PurchaseItem;

                if (other == null)
                    return false;

                return string.Equals(ProductName, other.ProductName, System.StringComparison.InvariantCultureIgnoreCase)
                        && Equals(Quantity, other.Quantity);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ProductName.GetHashCode()
                            ^ Quantity.GetHashCode();
                }
            }
        }
    }
}