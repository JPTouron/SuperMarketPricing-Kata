using System;

namespace SupermarketPricing.Model1.MoneyModel
{
    public sealed partial class Money
    {
        public bool Equals(Money other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return (Amount == other.Amount && Currency.Equals(other.Currency));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Money)) return false;
            return Equals((Money)obj);
        }

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
    }
}