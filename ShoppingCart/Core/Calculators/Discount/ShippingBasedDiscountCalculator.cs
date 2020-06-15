using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Calculators.Discount
{
    public class ShippingBasedDiscountCalculator : ICalculator
    {        
        private readonly decimal _shippingCost;

        public ShippingBasedDiscountCalculator(decimal shippingCost)
        {
            _shippingCost = shippingCost;            
        }

        public decimal Calculate()
        {
            return _shippingCost;
        }
    }
}
