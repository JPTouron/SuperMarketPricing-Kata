using SupermarketPricing.Supermarket.Tickets;

namespace SupermarketPricing.Supermarket.Discounts.DiscountTypes;

internal class LoyaltyDiscount : IDiscountCalculator
{
    private readonly TicketLine ticketLine;
    private readonly int purchaseQuantity;
    private readonly decimal loyaltyDiscountPercentage;

    public LoyaltyDiscount(TicketLine ticketLine,
                           int purchaseQuantity,
                           decimal loyaltyDiscountPercentage)
    {
        this.ticketLine = ticketLine;
        this.purchaseQuantity = purchaseQuantity;
        this.loyaltyDiscountPercentage = loyaltyDiscountPercentage;
    }

    public decimal Calculate()
    {
        throw new System.NotImplementedException();
    }
}