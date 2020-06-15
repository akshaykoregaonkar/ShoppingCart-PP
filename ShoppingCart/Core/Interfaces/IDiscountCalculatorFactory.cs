using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Calculators.Discount
{
    public interface IDiscountCalculatorFactory
    {
        ICalculator Create();
    }
}