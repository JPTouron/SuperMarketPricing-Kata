using SupermarketPricing.Model1.DateRangeModel;
using SupermarketPricing.Model1.MoneyModel;

//DECORATORS FOR ALL!!

namespace SupermarketPricing.Model1
{

    public interface ISellableProduct
    {
        public Money Cost { get; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }

    /// <summary>
    /// product on sale (2 for 1, -10% off, etc), products that have special values (sometimes even within a date range)
    /// </summary>
    public interface IDiscountableProduct
    {
        public decimal Discount { get; }
    }

    /// <summary>
    /// a product that's on sale during a period of time
    /// </summary>
    public interface ITimeableDiscount
    {
        public DateRange Period { get; }
    }
}