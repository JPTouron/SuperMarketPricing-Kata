using SupermarketPricing.Api.Model1.Discounts;
using SupermarketPricing.Api.Model1.PortsImplementations;
using SupermarketPricing.Domain.ApplicationPorts;
using SupermarketPricing.Domain.Modules.Purchase;
using SupermarketPricing.Domain.Modules.Purchase.Aggregate;
using SupermarketPricing.Domain.Modules.SharedKernel.Products;
using SupermarketPricing.Domain.Modules.Stock;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel.CurrencyModel;
using System;
using Storage = SupermarketPricing.Domain.Modules.Stock.Aggregate.Storage;

namespace SupermarketPricing.Api.Model1.SuperMarket
{
    /// <summary>
    /// models the supermarket API and configures sales and offers
    /// </summary>
    public class SuperMarketApi : ISuperMarketApi
    {
        private readonly IDiscountApplier discountApplier;
        private Currency currency;

        private IPurchaseOrder po;

        private IStorage storage;

        public SuperMarketApi(Currency currency)
        {
            this.currency = currency;
            discountApplier = new DiscountApplier(new DiscountRulesRepository());

            LoadStorage();
        }

        private bool PoIsNotInitialized => po == null;

        public void AddItemToPurchaseOrder(string name, int quantity)
        {
            ValidatePO();

            po.AddItemToOrder(name, quantity);
        }

        public void ClosePurchaseOrder()
        {
            ResetPurchaseOrder();

            Console.WriteLine("Thanks for your purchase!");
        }

        public Money GetPurchaseTotalCost()
        {
            ValidatePO();

            var total = po.TotalCost;

            return total;
        }

        public void StartPurchaseOrder()
        {
            po = new PurchaseOrder(currency, storage, discountApplier);
        }

        private ISellableProduct CreateItem(string name, decimal cost)
        {
            return new SellableProduct(name, Money.Create(cost, currency));
        }

        private void LoadStorage()
        {
            storage = new Storage();

            storage.AddProductToStorage(CreateItem("Soda Can", 1.04m), 1);
            storage.AddProductToStorage(CreateItem("Fish", 3.50m), 10);
            storage.AddProductToStorage(CreateItem("Tomato Sauce", 2.32m), 10);
            storage.AddProductToStorage(CreateItem("Cat Food", 3m), 10);
        }

        private void ResetPurchaseOrder()
        {
            po = null;
        }

        private void ValidatePO()
        {
            if (PoIsNotInitialized)
                throw new InvalidOperationException("The Purchase Order is not initialized! Please initialize the PO first by invoking CreatePurchaseOrder method");
        }
    }
}