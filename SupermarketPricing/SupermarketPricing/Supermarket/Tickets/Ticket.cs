using SupermarketPricing.Supermarket.Discounts;
using System;
using System.Collections.Generic;

namespace SupermarketPricing.Supermarket.Tickets;

internal class Ticket
{
    private readonly IDiscountStrategyFactory discountStrategyFactory;

    public Ticket(IDiscountStrategyFactory discountStrategyFactory,
                  IReadOnlyList<TicketLine> ticketLines,
                  string storeName,
                  string cashierName)
    {
        this.discountStrategyFactory = discountStrategyFactory;
        PurchaseDate = DateTime.UtcNow;
        TicketLines = ticketLines;
        StoreName = storeName;
        CashierName = cashierName;
    }

    public DateTime PurchaseDate { get; }

    public IReadOnlyList<TicketLine> TicketLines { get; }

    public string StoreName { get; }

    public string CashierName { get; }
}