using ShoppingCart.Core.Interfaces;
using System;

namespace ShoppingCart.Core.Calculators.Shipping
{
    class UnknownShippingCalculator : ICalculator
    {
        public decimal Calculate()
        {
            throw new ArgumentNullException("Product total must be passed to the shipping calculator");
        }
    }
}
