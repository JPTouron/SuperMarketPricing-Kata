namespace SupermarketPricing.Domain.Modules.SharedKernel.Products
{
    /// <summary>
    /// all supermarket products fulfill this contract at the very least
    /// </summary>
    public interface IProduct
    {
        public string ProductName { get; }
    }
}