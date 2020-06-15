using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Calculators.Shipping
{  
    public class ShippingCalculator : ICalculator
    {
        private readonly decimal _basketTotal;

        public ShippingCalculator(decimal basketTotal)
        {
            _basketTotal = basketTotal;
        }

        public decimal Calculate()
        {
            if (_basketTotal < 20) return 7m;
            if (_basketTotal < 40) return 5m;
            return 0m;
        }
    }
}