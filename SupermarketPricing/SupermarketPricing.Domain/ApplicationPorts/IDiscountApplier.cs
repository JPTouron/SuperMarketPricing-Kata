using SupermarketPricing.Domain.Modules.Purchase;
using SupermarketPricing.Domain.Modules.Stock;

namespace SupermarketPricing.Domain.ApplicationPorts
{
    public interface IDiscountApplier
    {
        IPurchaseItem ApplyDiscount(IPurchaseItem item);
    }
}