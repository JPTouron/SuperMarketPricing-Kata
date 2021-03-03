using SupermarketPricing.Model1.MoneyModel;

namespace SupermarketPricing.Model1.SuperMarket.Contracts
{
    public interface IStockableProduct
    {
        IProduct Product { get; }

        int Quantity { get; }

        void DecreaseStock(int quantityToReduce);
    }

    /// <summary>
    /// all supermarket products fulfill this contract at the very least
    /// </summary>
    public interface IProduct
    {
        public string ProductName { get; }
    }

    /// <summary>
    /// all supermarket products fulfill this contract at the very least
    /// </summary>
    public interface ISellableProduct : IProduct
    {
        public Money Cost { get; }


    }
}