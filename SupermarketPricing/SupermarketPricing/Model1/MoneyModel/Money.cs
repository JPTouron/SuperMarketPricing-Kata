using System;

namespace SupermarketPricing.Model1.MoneyModel
{
    /// <summary>
    /// based on https://github.com/JPTouron/Money (forked from jasonhoi)
    /// </summary>
    public partial class Money : IEquatable<Money>, IComparable, IComparable<Money>
    {
        public readonly Currency Currency;

        public Money(decimal amount, Currency currency)
        {
            AssertNotNull(currency);
            AssertPositiveAmount(amount);
            Amount = amount;
            Currency = currency;
        }

        public Money(decimal amount, Money money)
        {
            AssertNotNull(money);
            AssertPositiveAmount(amount);

            Amount = amount;
            Currency = money.Currency;
        }

        public decimal Amount { get; private set; }

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

    public sealed class NoMoney : Money
    {
        public NoMoney() : base(0.0m, "USD".ToCurrency())
        {
        }
    }
}