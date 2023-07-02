using SupermarketPricing.Supermarket.Tickets;
using System;

namespace SupermarketPricing.Supermarket.Discounts.DiscountTypes;

internal class SeasonalDiscount : IDiscountCalculator
{
    private readonly TicketLine ticketLine;
    private readonly DateTime seasonaleDiscountStartDate;
    private readonly DateTime seasonalDiscountEndDate;

    public SeasonalDiscount(TicketLine ticketLine,
                            DateTime seasonaleDiscountStartDate,
                            DateTime seasonalDiscountEndDate)
    {
        this.ticketLine = ticketLine;
        this.seasonaleDiscountStartDate = seasonaleDiscountStartDate;
        this.seasonalDiscountEndDate = seasonalDiscountEndDate;
    }

    public decimal Calculate()
    {
        throw new System.NotImplementedException();
    }
}