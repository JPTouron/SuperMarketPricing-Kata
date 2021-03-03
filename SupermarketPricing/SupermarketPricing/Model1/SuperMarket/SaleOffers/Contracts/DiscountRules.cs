using SupermarketPricing.Model1.Core;
using SupermarketPricing.Model1.MoneyModel;

namespace SupermarketPricing.Model1.SuperMarket.Contracts
{
    /// <summary>
    /// product on sale (2 for 1, -10% off, etc), products that have special values (sometimes even within a date range)
    /// </summary>
    public interface IDiscountRule
    {
        //JP: RENAME THIS INTERFACE TO ICALCULATION RULE
        public string ProductName { get;  }

        /// <summary>
        /// percentage discounted
        /// </summary>
        public decimal DiscountPercentage { get;  }
    }


    /// <summary>
    /// a product that's on sale during a period of time
    /// </summary>
    public interface ITimeableDiscountRule: IDiscountRule
    {
        DateRange Period { get; }
    }

    public interface IDiscountableByQuantityOfferRule: IDiscountRule
    {
        int Quantity { get; }


    }

    public interface IDiscountableByPaymentMethodRule: IDiscountRule
    {
        //JP: NEED TO MODEL PAYMENT METHODS
        string PaymentMethod { get; }
    }
}