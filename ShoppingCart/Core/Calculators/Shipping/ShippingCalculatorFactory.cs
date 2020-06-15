using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Calculators.Shipping
{
    public class ShippingCalculatorFactory : IShippingCalculatorFactory
    {
        // Factory used as may require a different implementation of the shipping calculator in the future (for e.g. International Shipping calculator)
        public ICalculator Create(decimal productTotal)
        {
            try
            {
                return new ShippingCalculator(productTotal);
            }
            catch
            {
                return new UnknownShippingCalculator();
            }
        }
    }
}
