namespace SupermarketPricing.Model1.SuperMarket.SaleOffers
{
    internal static class PercentageCalculator
    {
        public static decimal CalculatePercentageDiscount(this decimal amount, decimal discountPercentage)
        {
            var discountedAmount = amount - amount * discountPercentage / 100;

            return discountedAmount;
        }
    }
}