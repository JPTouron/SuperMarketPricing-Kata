using SuperMarketPricing.Domain.BuildingBlocks.Base;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.CurrencyModel;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.Extensions;
using System;

namespace SuperMarketPricing.Domain.BuildingBlocks.MoneyModel
{
    /// <summary>
    /// based on https://github.com/JPTouron/Money (forked from jasonhoi)
    /// </summary>
    public partial class Money : ValueObject<Money>, IComparable, IComparable<Money>
    {
        public readonly Currency Currency;

        private Money(decimal amount, Currency currency)
        {
            AssertNotNull(currency);
            AssertPositiveAmount(amount);

            Amount = amount;
            Currency = currency;
        }

        private Money(decimal amount, Money money)
        {
            AssertNotNull(money);
            AssertPositiveAmount(amount);

            Amount = amount;
            Currency = money.Currency;
        }

        public decimal Amount { get; private set; }

        public static Money Create(decimal amount, Currency currency) => new Money(amount, currency);

        public static Money Create(decimal amount, Money money) => new Money(amount, money);

        public static Money NoMoney(Currency currency) => new Money(0.0m, currency);

        public static Money NoMoney(Money money) => new Money(0.0m, money.Currency);

        public static Money NoMoneyDollars() => new Money(0.0m, "USD".ToCurrency());

        public override int GetHashCode()
        {
            unchecked
            {
                return (Amount.GetHashCode() * 397) ^ Currency.GetHashCode();
            }
        }

        /// <summary>
        /// Use the decorated interal Currency object to display the string
        /// </summary>
        ///
        /// <returns>string</returns>
        public override string ToString()
        {
            return Currency.ToString(this);
        }
    }
}