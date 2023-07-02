using SupermarketPricing.BuildingBlocks.MoneyModel;
using System;



namespace SupermarketPricing.BuildingBlocks.MoneyModel.CurrencyModel;
public class Currency : IEquatable<Currency>
{
    /// <summary>
    /// ARS,USD,EU, etc...
    /// </summary>
    public readonly string IsoCode;

    public readonly bool IsDigital;
    public readonly string GeneralName;

    /// <summary>
    /// $, €
    /// </summary>
    public readonly string Symbol;

    public readonly int DecimalPlace;
    public readonly int BaseDecimalPlace;
    public readonly string DecimalMark;
    public readonly string ThousandMark;

    public Currency(string isoCode, bool isDigital, string generalName, string symbol, int decimalPlace, int baseDecimalPlace, string decimalMark, string thousandMark)
    {
        IsoCode = isoCode;
        IsDigital = isDigital;
        GeneralName = generalName;
        Symbol = symbol;
        DecimalPlace = decimalPlace;
        BaseDecimalPlace = baseDecimalPlace;
        DecimalMark = decimalMark;
        ThousandMark = thousandMark;
    }

    public static bool operator ==(Currency left, Currency right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Currency left, Currency right)
    {
        return !Equals(left, right);
    }

    public bool Equals(Currency other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return IsoCode == other.IsoCode;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(Currency)) return false;
        return Equals((Currency)obj);
    }

    public override int GetHashCode()
    {
        return IsoCode.GetHashCode();
    }

    public string GetStringFormat()
    {
        string decimalZero = "";
        for (int i = 1; i <= DecimalPlace; i++)
        {
            decimalZero += "0";
        }
        string specifier = "#" + ThousandMark + "0" + DecimalMark + decimalZero + ";(#,0." + decimalZero + ")";

        return specifier;
    }

    public override string ToString()
    {
        return IsoCode + Symbol;
    }

    /// <summary>
    /// Display any passed in Money object decorated by its own Currency object
    /// </summary>
    public string ToString(Money m)
    {
        string displaySymbol = m.Currency.Symbol;
        decimal displayAmount = m.Amount;

        return displaySymbol + displayAmount.ToString(GetStringFormat());
    }
}