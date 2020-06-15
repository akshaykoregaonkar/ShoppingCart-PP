using ShoppingCart.Core.Interfaces;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System.Collections.Generic;

namespace ShoppingCart.Core.Calculators.Basket
{
    public interface IBasketCalculatorFactory
    {
        ICalculator Create(IEnumerable<CartItem> cartItems, IRepository<Product> _productStore);
    }
}