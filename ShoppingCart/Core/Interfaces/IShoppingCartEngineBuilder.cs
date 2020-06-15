using ShoppingCart.Core.Calculators.Basket;
using ShoppingCart.Core.Calculators.Discount;
using ShoppingCart.Core.Calculators.Shipping;
using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System.Collections.Generic;

namespace ShoppingCart.Core.Interfaces
{
    public interface IShoppingCartEngineBuilder
    {
        ShoppingCartEngine<CartItem> GetShoppingCartEngine();
        IShoppingCartEngineBuilder AddCartItems(IEnumerable<CartItem> cartItems);
        IShoppingCartEngineBuilder AddProductRepository(IRepository<Product> productRepository);
        IShoppingCartEngineBuilder AddBasketCalculatorFactory(BasketCalculatorFactory basketCalculatorFactory);
        IShoppingCartEngineBuilder AddShippingCalculatorFactory(ShippingCalculatorFactory shippingCalculatorFactory);
        IShoppingCartEngineBuilder AddCouponRepository(IRepository<Coupon> couponRepository);
        IShoppingCartEngineBuilder AddDiscountCalculatorFactory(DiscountCalculatorFactory discountCalculatorFactory);
        IShoppingCartEngineBuilder AddCoupon(int couponId);
    }
}
