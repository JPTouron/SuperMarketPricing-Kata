using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket.Contracts;

namespace SupermarketPricing.Model1.SuperMarket.Products
{
    public class NoProduct : ProductItem
    {
        public NoProduct() : base(string.Empty, new NoMoney())
        {
        }
    }

    public class ProductItem : ISellableProduct
    {
        public ProductItem(string name, Money cost)
        {
            Cost = cost;
            ProductName = name;
        }

        public Money Cost { get; }

        public string ProductName { get; }

        public override bool Equals(object obj)
        {
            var other = obj as ProductItem;
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