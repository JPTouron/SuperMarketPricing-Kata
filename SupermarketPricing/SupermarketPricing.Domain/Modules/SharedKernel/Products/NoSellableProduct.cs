using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.CurrencyModel;

namespace SupermarketPricing.Domain.Modules.SharedKernel.Products
{
    public sealed class NoSellableProduct : SellableProduct
    {
        /// <summary>
        /// Initialize with dollars
        /// </summary>
        public NoSellableProduct() : base(string.Empty, Money.NoMoneyDollars())
        {
        }

        /// <summary>
        /// Initialize with custom currency
        /// </summary>
        public NoSellableProduct(Currency currency) : base(string.Empty, Money.NoMoney(currency))
        {
        }
    }
}