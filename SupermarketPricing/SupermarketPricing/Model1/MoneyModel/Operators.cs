namespace SupermarketPricing.Model1.MoneyModel
{
    public partial class Money
    {
        public static bool operator ==(Money left, Money right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !Equals(left, right);
        }

        public static bool operator >(Money left, Money right)
        {
            AssertSameCurrency(left, right);
            return left.Amount > right.Amount;
        }

        public static bool operator >=(Money left, Money right)
        {
            AssertSameCurrency(left, right);
            return left.Amount >= right.Amount;
        }

        public static bool operator <(Money left, Money right)
        {
            AssertSameCurrency(left, right);
            return left.Amount < right.Amount;
        }

        public static bool operator <=(Money left, Money right)
        {
            AssertSameCurrency(left, right);
            return left.Amount <= right.Amount;
        }

        public static Money operator +(Money left, Money right)
        {
            AssertSameCurrency(left, right);
            return new Money(left.Amount + right.Amount, left.Currency);
        }

        public static Money operator +(Money left, decimal right)
        {
            AssertNotNull(left);
            return new Money(left.Amount + right, left.Currency);
        }

        public static Money operator -(Money left, Money right)
        {
            AssertSameCurrency(left, right);
            return new Money(left.Amount - right.Amount, left.Currency);
        }

        public static Money operator -(Money left, decimal right)
        {
            AssertNotNull(left);
            return new Money(left.Amount - right, left.Currency);
        }

        public static Money operator *(Money left, decimal right)
        {
            AssertNotNull(left);
            return new Money(left.Amount * right, left.Currency);
        }

        public static Money operator /(Money left, decimal right)
        {
            AssertNotNull(left);
            return new Money(left.Amount / right, left.Currency);
        }
    }
}