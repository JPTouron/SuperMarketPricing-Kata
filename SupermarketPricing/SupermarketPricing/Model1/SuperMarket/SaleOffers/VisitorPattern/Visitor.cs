using SupermarketPricing.Model1.MoneyModel;
using SupermarketPricing.Model1.SuperMarket.Purchase;

namespace SupermarketPricing.Model1.SuperMarket.SaleOffers.VisitorPattern
{
    //IVisitor
    public interface IDiscountCalculator
    {
        Money CalculateDiscount(IPurchaseItem item);
    }

    //IElement
    public interface IDiscountable
    {
        Money SubtotalCost(IDiscountCalculator visitor);

    }
}