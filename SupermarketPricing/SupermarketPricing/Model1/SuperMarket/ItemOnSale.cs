using Ardalis.GuardClauses;
using SupermarketPricing.Model1;
using SupermarketPricing.Model1.MoneyModel;

namespace SupermarketPricing.Model1.SuperMarket
{
    public class ItemOnSale : IDiscountableProduct, ISellableProduct
    {
        private readonly ISellableProduct product;

        public ItemOnSale(ISellableProduct product, decimal discountPercentage)
        {
            Guard.Against.Null(product, nameof(product));
            Guard.Against.OutOfRange(discountPercentage, nameof(discountPercentage), 0.1m, 100);

            this.product = product;

            DiscountPercentage = discountPercentage;
            ProductName = product.ProductName;
        }

        public Money Cost
        {
            get
            {
                var amount = product.Cost.Amount;
                var newAmount = amount - amount * DiscountPercentage / 100;

                return new Money(product.Cost, newAmount);
            }
        }

        public string ProductName { get; }

        public decimal DiscountPercentage { get; }
    }
}