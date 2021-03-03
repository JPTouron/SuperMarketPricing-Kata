using SupermarketPricing.Domain.ApplicationPorts;
using SupermarketPricing.Domain.Modules.SharedKernel.Products;
using SupermarketPricing.Domain.Modules.Stock;
using SuperMarketPricing.Domain.BuildingBlocks.Base;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.CurrencyModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketPricing.Domain.Modules.Purchase.Aggregate
{
    public partial class PurchaseOrder : IPurchaseOrder, IAggregateRoot
    {
        private readonly IDiscountApplier discountApplier;
        private readonly IStorage storage;
        private IList<IPurchaseItem> items;

        public PurchaseOrder(
            Currency currency,
            IStorage storage,
            IDiscountApplier discountApplier)
        {
            items = new List<IPurchaseItem>();
            Currency = currency;
            this.storage = storage;
            this.discountApplier = discountApplier;
        }

        public Currency Currency { get; }

        public Money TotalCost
        {
            get
            {
                var total = Money.NoMoney(Currency);

                LogMessage($"There are {items.Count} items in your order\r\n\r\n");

                items.ToList().ForEach(purchaseItem =>
                {
                    total += purchaseItem.ProductCost;
                });

                return total;
            }
        }

        IReadOnlyList<IPurchaseItem> IPurchaseOrder.GetList => (IReadOnlyList<IPurchaseItem>)items;

        public void AddItemToOrder(string productName, int quantity)
        {
            var product = storage.GetProduct(productName, quantity);

            var sp = product.GetValueOrFallback(new NoSellableProduct(Currency));

            if (sp.IsSellable)
            {
                IPurchaseItem item = new PurchaseItem(sp, quantity);

                item = discountApplier.ApplyDiscount(item);

                items.Add(item);
            }
        }

        private void LogMessage(string v)
        {
            Console.WriteLine(v);
        }
    }
}