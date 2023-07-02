using SupermarketPricing.Supermarket.Discounts.Configurations;
using SupermarketPricing.Supermarket.Tickets;
using System;
using System.Collections.Generic;

namespace SupermarketPricing.Supermarket.Discounts;

internal interface IDiscountStrategyFactory
{
    DiscountStrategy GetDiscountStrategyForTicketLine(TicketLine ticketLine);
}

//jp: should be a singleton
internal class DiscountStrategyFactory : IDiscountStrategyFactory
{
    private readonly IReadOnlyList<DiscountForItem> discountConfigurations;

    public DiscountStrategyFactory(IReadOnlyList<DiscountForItem> discountConfigurations)
    {
        this.discountConfigurations = discountConfigurations;
    }

    public DiscountStrategy GetDiscountStrategyForTicketLine(TicketLine ticketLine)
    {
        throw new NotImplementedException();
    }
}