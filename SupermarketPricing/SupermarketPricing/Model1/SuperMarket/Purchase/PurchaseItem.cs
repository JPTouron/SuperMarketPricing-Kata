using Ardalis.GuardClauses;
using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket.Contracts;
using SupermarketPricing.Model1.SuperMarket.SaleOffers.VisitorPattern;

namespace SupermarketPricing.Model1.SuperMarket.Purchase
{
    public interface IPurchaseItem : IDiscountable
    {
        ISellableProduct Product { get; }

        int Quantity { get; }

        Money FullCost { get; }
    }

    public class PurchaseItem : IPurchaseItem
    {
        public PurchaseItem(ISellableProduct product, int quantity)
        {
            Guard.Against.Null(product, nameof(product));
            Guard.Against.NegativeOrZero(quantity, nameof(quantity));

            Product = product;
            Quantity = quantity;
        }

        public ISellableProduct Product { get; }

        public int Quantity { get; }

        public Money FullCost => Product.Cost * Quantity;

        public Money SubtotalCost(IDiscountCalculator visitor)
        {
            var calculatedCost = visitor.CalculateDiscount(this);
            return calculatedCost;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PurchaseItem;
            if (other == null)
                return false;

            return Equals(Product, other.Product) &&
                    Equals(Quantity, other.Quantity);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Product.GetHashCode() ^ Quantity.GetHashCode();
            }
        }
    }
}