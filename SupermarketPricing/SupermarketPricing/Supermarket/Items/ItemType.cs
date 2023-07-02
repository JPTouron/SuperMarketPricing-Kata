using System;

namespace SupermarketPricing.Supermarket.Items;

internal class ItemType
{
    public ItemType(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }

    public string Name { get; }
}