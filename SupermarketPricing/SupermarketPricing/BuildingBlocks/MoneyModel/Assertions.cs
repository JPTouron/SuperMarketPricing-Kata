using SupermarketPricing.BuildingBlocks.MoneyModel.CurrencyModel;
using System;

namespace SupermarketPricing.BuildingBlocks.MoneyModel;

public sealed partial class Money
{
    public static void AssertNotNull(Money money)
    {
        if (money == null) throw new ArgumentNullException("Money Is Null");
    }

    public static void AssertPositiveAmount(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Amount must be a positive or Zero number");
    }

    public static void AssertNotNull(Currency currency)
    {
        if (currency == null) throw new ArgumentNullException("Currency Is Null");
    }

    public static void AssertSameCurrency(Money first, Money second)
    {
        if (first == null || second == null)
            throw new ArgumentNullException("Any Money Is Null");
        if (first.Currency != second.Currency)
            throw new ArgumentException("Money Currency Not Equal");
    }
}