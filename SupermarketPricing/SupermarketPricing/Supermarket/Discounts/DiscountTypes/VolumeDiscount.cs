using SupermarketPricing.Supermarket.Tickets;

namespace SupermarketPricing.Supermarket.Discounts.DiscountTypes;

internal class VolumeDiscount : IDiscountCalculator
{
    public VolumeDiscount(TicketLine ticketLine, int volumeDiscountMaxThreshold, decimal volumeDiscountMaxPercentage)
    {
    }

    public decimal Calculate()
    {
        throw new System.NotImplementedException();
    }
}