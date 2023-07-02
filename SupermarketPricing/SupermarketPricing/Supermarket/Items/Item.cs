using SupermarketPricing.BuildingBlocks.MoneyModel;
using System;

namespace SupermarketPricing.Supermarket.Items;

internal class Item
{
    public Item(Guid id, string name, Money price, int quantity, ItemType itemType)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        ItemType = itemType;
    }

    public string Name { get; }

    public Money Price { get; }

    public int Quantity { get; }

    public ItemType ItemType { get; }

    public Guid Id { get; }
}