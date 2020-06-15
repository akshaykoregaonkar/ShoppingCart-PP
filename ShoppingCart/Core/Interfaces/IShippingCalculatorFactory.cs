using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Calculators.Shipping
{
    public interface IShippingCalculatorFactory
    {
        ICalculator Create(decimal productTotal);
    }
}