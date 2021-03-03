using SupermarketPricing.Model1;
using SupermarketPricing.Model1.MoneyModel;

namespace SupermarketPricing
{

    public partial class SuperMarketItems
    {
        public class SodaCan : ISellableProduct
        {
            public Money Cost => new Money(2.4m, "EUR".ToCurrency());

            public string ProductName { get => "Pop Soda, belch the best!"; }
        }
    }
}