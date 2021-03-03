using SupermarketPricing.Model1.DateRangeModel;
using SupermarketPricing.Model1.MoneyModel;

//DECORATORS FOR ALL!!

namespace SupermarketPricing.Model1
{
    public interface IStockableProduct
    {
        public int Quantity { get; }
    }

    public interface ISellableProduct
    {
        public Money Cost { get; }

        public string ProductName { get; }
    }

    /// <summary>
    /// product on sale (2 for 1, -10% off, etc), products that have special values (sometimes even within a date range)
    /// </summary>
    public interface IDiscountableProduct
    {

        /// <summary>
        /// percentage discounted
        /// </summary>
        public decimal DiscountPercentage { get; }

    }

    /// <summary>
    /// a product that's on sale during a period of time
    /// </summary>
    public interface ITimeableDiscount
    {
        public DateRange Period { get; }
    }
}