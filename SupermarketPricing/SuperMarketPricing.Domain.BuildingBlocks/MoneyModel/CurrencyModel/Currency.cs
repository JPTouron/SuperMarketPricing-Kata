using SuperMarketPricing.Domain.BuildingBlocks.Base;

namespace SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.CurrencyModel
{
    public class Currency : ValueObject<Currency>// IEquatable<Currency>
    {
        public readonly int BaseDecimalPlace;

        public readonly string DecimalMark;

        public readonly int DecimalPlace;

        public readonly string GeneralName;

        public readonly bool IsDigital;

        /// <summary>
        /// ARS,USD,EU, etc...
        /// </summary>
        public readonly string IsoCode;

        /// <summary>
        /// $, €
        /// </summary>
        public readonly string Symbol;

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

        public static bool operator !=(Currency left, Currency right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(Currency left, Currency right)
        {
            return Equals(left, right);
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

        protected override bool InternalEquals(Currency other)
        {
            return IsoCode == other.IsoCode;
        }
    }
}