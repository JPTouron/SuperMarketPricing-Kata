﻿using SupermarketPricing.BuildingBlocks.MoneyModel.CurrencyModel;
using System;

namespace SupermarketPricing.BuildingBlocks.MoneyModel;

/// <summary>
/// based on https://github.com/JPTouron/Money (forked from jasonhoi)
/// </summary>
public sealed partial class Money : IEquatable<Money>, IComparable, IComparable<Money>
{
    public readonly Currency Currency;

    public Money(decimal amount, Currency currency)
    {
        AssertNotNull(currency);
        Amount = amount;
        Currency = currency;
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