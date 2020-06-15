using ShoppingCart.Core.Interfaces;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System;
using System.Collections.Generic;

namespace ShoppingCart.Core.Calculators
{
    public abstract class CalculatorBase : ICalculator
    {
        protected readonly IEnumerable<CartItem> _cartItems;
        protected readonly IRepository<Product> _productRepository;

        public CalculatorBase(IEnumerable<CartItem> cartItems, IRepository<Product> productRepository)
        {
            if (cartItems is null)
            {
                throw new ArgumentNullException(nameof(cartItems), "Cart cannot be empty");
            }
            if (productRepository is null)
            {
                throw new ArgumentNullException(nameof(productRepository), "Product repository must be present");
            }
            _cartItems = cartItems;
            _productRepository = productRepository;
        }

        public abstract decimal Calculate();
    }
}
