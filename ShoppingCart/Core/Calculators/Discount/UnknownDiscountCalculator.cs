using ShoppingCart.Core.Interfaces;
using System;

namespace ShoppingCart.Core.Calculators.Discount
{
    class UnknownDiscountCalculator : ICalculator
    {
        // ensures every object will see only one instance and we'll always return 0 when discount is unknown/not provided
        [ThreadStatic]
        private static UnknownDiscountCalculator instance;

        private UnknownDiscountCalculator() { }

        public static UnknownDiscountCalculator Instance
        {
            get
            {
                if (instance == null)
                    instance = new UnknownDiscountCalculator();
                return instance;
            }
        }
        public decimal Calculate()
        {
            return 0m;
        }
    }
}
