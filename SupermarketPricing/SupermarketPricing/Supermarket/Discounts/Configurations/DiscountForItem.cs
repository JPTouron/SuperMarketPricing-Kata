using SupermarketPricing.Supermarket.Items;
using System;

namespace SupermarketPricing.Supermarket.Discounts.Configurations;

internal class DiscountForItem
{
    public DiscountForItem(string discountType,
                           DateTime startDate,
                           DateTime endDate,
                           ItemType itemTypeOnDiscount)
    //, DiscountConfiguration)
    {
        DiscountType = discountType;
        StartDate = startDate;
        EndDate = endDate;
        ItemTypeOnDiscount = itemTypeOnDiscount;
    }

    public string DiscountType { get; }

    public DateTime StartDate { get; }

    public DateTime EndDate { get; }

    public ItemType ItemTypeOnDiscount { get; }
}