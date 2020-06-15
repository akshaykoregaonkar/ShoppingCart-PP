using ShoppingCart.Core.Calculators.Basket;
using ShoppingCart.Core.Calculators.Discount;
using ShoppingCart.Core.Calculators.Shipping;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System.Collections.Generic;

namespace ShoppingCart.Core.Builders
{
    public class ShoppingCartEngineBuilder : IShoppingCartEngineBuilder
    {
        readonly ShoppingCartEngine<CartItem> shoppingCartEngine = new ShoppingCartEngine<CartItem>();
        public ShoppingCartEngine<CartItem> GetShoppingCartEngine()
        {
            return shoppingCartEngine;
        }
        public IShoppingCartEngineBuilder AddCartItems(IEnumerable<CartItem> cartItems)
        {
            shoppingCartEngine.CartItems = cartItems;
            return this;
        }
        public IShoppingCartEngineBuilder AddProductRepository(IRepository<Product> productRepository)
        {
            shoppingCartEngine.ProductRepository = productRepository;
            return this;
        }

        public IShoppingCartEngineBuilder AddBasketCalculatorFactory(BasketCalculatorFactory basketCalculatorFactory)
        {
            shoppingCartEngine.BasketCalculatorFactory = basketCalculatorFactory;
            return this;
        }
        public IShoppingCartEngineBuilder AddShippingCalculatorFactory(ShippingCalculatorFactory shippingCalculatorFactory)
        {
            shoppingCartEngine.ShippingCalculatorFactory = shippingCalculatorFactory;
            return this;
        }
        public IShoppingCartEngineBuilder AddCouponRepository(IRepository<Coupon> couponRepository)
        {
            shoppingCartEngine.CouponRepository = couponRepository;
            return this;
        }
        public IShoppingCartEngineBuilder AddDiscountCalculatorFactory(DiscountCalculatorFactory discountCalculatorFactory)
        {
            shoppingCartEngine.DiscountCalculatorFactory = discountCalculatorFactory;
            return this;
        }
        public IShoppingCartEngineBuilder AddCoupon(int couponId)
        {
            shoppingCartEngine.CouponId = couponId;
            return this;
        }
    }
}
