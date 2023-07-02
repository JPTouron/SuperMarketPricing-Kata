using SupermarketPricing.Supermarket.Items;
using System;

namespace SupermarketPricing.Supermarket.Tickets;

internal class TicketLine
{
    public TicketLine(Item item, int itemQuantity)
    {
        Item = item;
        ItemQuantity = itemQuantity;
        PurchaseDate = DateTime.UtcNow;
    }

    public Item Item { get; }

    public int ItemQuantity { get; }

    public DateTime PurchaseDate { get; }
}