using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;

namespace SupermarketPricing.Domain.Modules.SharedKernel.Products
{
    public class SellableProduct : ISellableProduct
    {
        public SellableProduct(string name, Money cost)
        {
            Cost = cost;
            ProductName = name;
        }

        public Money Cost { get; }

        public bool IsSellable => this is SellableProduct && !(this is NoSellableProduct);

        public string ProductName { get; }

        public override bool Equals(object obj)
        {
            var other = obj as SellableProduct;
            if (other == null)
                return false;

            return Equals(Cost, other.Cost) &&
                    Equals(ProductName, other.ProductName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Cost.GetHashCode() ^ ProductName.GetHashCode();
            }
        }
    }
}