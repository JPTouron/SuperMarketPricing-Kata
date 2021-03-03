using Ardalis.GuardClauses;
using SupermarketPricing.Domain.Modules.Purchase;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using System;

namespace SupermarketPricing.Api.Model1.Discounts.DiscountRules.Base
{
    public abstract class DiscountRule : IPurchaseItem
    {
        protected readonly IPurchaseItem item;

        protected DiscountRule(IPurchaseItem item)
        {
            Guard.Against.Null(item, nameof(item));

            this.item = item;
        }

        /// <summary>
        /// visitor's visit method. In this case, as we are inherently implementing the decorator pattern, this is a property
        /// </summary>
        public abstract Money ProductCost { get; }

        public string ProductName => item.ProductName;

        public int Quantity => item.Quantity;

        protected void LogMessage(string v)
        {
            Console.WriteLine(v);
        }
    }
}