using SupermarketPricing.Supermarket.Tickets;

namespace SupermarketPricing.Supermarket.Discounts.DiscountTypes;

internal class BogoDiscount : IDiscountCalculator
{
    private readonly TicketLine ticketLine;
    private readonly int bogoQuantityThreshold;

    public BogoDiscount(TicketLine ticketLine, int bogoQuantityThreshold)
    {
        this.ticketLine = ticketLine;
        this.bogoQuantityThreshold = bogoQuantityThreshold;
    }

    public decimal Calculate()
    {
        throw new System.NotImplementedException();
    }
}