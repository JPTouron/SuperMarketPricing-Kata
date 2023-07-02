using SupermarketPricing.Supermarket.Tickets;
using System;

namespace SupermarketPricing.Supermarket.Discounts;

internal class DiscountStrategy
{
    private readonly TicketLine ticketLine;
    private readonly DiscountStrategy discountStrategy;

    public DiscountStrategy(TicketLine ticketLine, DiscountStrategy discountStrategy)
    {
        this.ticketLine = ticketLine;
        this.discountStrategy = discountStrategy;
    }

    public decimal GetDiscountedAmount()
    {
        throw new NotImplementedException();
    }

    public decimal GetSubtotal()
    {
        throw new NotImplementedException();
    }
}