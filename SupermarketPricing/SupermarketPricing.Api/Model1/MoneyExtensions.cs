using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;

namespace SupermarketPricing.Api.Model1
{
    internal static class MoneyExtensions
    {
        public static Money CalculatePercentageDiscount(this Money amount, decimal discountPercentage)
        {
            var discountedAmount = amount - amount * discountPercentage / 100;

            return discountedAmount;
        }
    }
}