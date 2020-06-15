using System;
using System.Collections.Generic;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;

namespace ShoppingCart.Core.Calculators.Basket
{   
    public class BasketCalculator: CalculatorBase
    {
        public BasketCalculator(IEnumerable<CartItem> cartItems, IRepository<Product> productRepository) : base(cartItems, productRepository)
        {            
        }

        public override decimal Calculate()
        {
            // TODO: Convert to lambda
            decimal runningTotal = 0;
            foreach (var item in _cartItems)
            {
                var product = _productRepository.Get(item.ProductId);
                if (product != null)
                {
                    runningTotal += item.UnitQuantity * product.Price;
                }
            }
            return runningTotal;
        }
    }
}