using ShoppingCart.Core.Interfaces;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System.Collections.Generic;

namespace ShoppingCart.Core.Calculators.Basket
{
    public class BasketCalculatorFactory : IBasketCalculatorFactory
    {
        public ICalculator Create(IEnumerable<CartItem> cartItems, IRepository<Product> _productStore)
        {
            return new BasketCalculator(cartItems, _productStore);
        }
    }
}
