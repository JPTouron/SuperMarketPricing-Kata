using SupermarketPricing.Model1;
using SupermarketPricing.Model1.MoneyModel;

namespace SupermarketPricing
{

    public partial class SuperMarketItems
    {
        public class Bread : ISellableProduct
        {
            public Money Cost => new Money(100m, "USD".ToCurrency());

            public string ProductName { get => "Bread loaf"; }
        }
    }
}