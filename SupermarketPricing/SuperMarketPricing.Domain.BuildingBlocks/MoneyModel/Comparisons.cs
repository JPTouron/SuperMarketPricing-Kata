using System;

namespace SuperMarketPricing.Domain.BuildingBlocks.MoneyModel
{
    public partial class Money
    {
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (!(obj is Money)) throw new ArgumentException("Object is not Money object");

            return CompareTo((Money)obj);
        }

        public int CompareTo(Money other)
        {
            if (other == null) return 1;
            if (this < other) return -1;
            if (this > other) return 1;
            return 0;
        }

        protected override bool InternalEquals(Money other)
        {
            return (Amount == other.Amount && Currency.Equals(other.Currency));
        }
    }
}