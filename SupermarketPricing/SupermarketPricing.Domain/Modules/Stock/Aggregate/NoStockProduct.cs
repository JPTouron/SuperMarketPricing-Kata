using SupermarketPricing.Domain.Modules.SharedKernel.Products;

namespace SupermarketPricing.Domain.Modules.Stock.Aggregate
{
    public partial class Storage
    {
        private class NoStockProduct : StockProduct
        {
            public NoStockProduct() : base(new NoSellableProduct(), 0)
            {
            }

            public override void DecreaseStock(int quantityToReduce)
            {
            }
        }
    }
}