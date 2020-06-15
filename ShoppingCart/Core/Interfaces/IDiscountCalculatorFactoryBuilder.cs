using ShoppingCart.Core.Calculators.Discount;
using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System.Collections.Generic;

namespace ShoppingCart.Core.Interfaces
{
    public interface IDiscountCalculatorFactoryBuilder
    {
        DiscountCalculatorFactory GetDiscountCalculatorFactory();
        IDiscountCalculatorFactoryBuilder AddCoupon(int couponId);
        IDiscountCalculatorFactoryBuilder AddShippingCost(decimal shippingCost);
        IDiscountCalculatorFactoryBuilder AddProductRepository(IRepository<Product> productRepository);
        IDiscountCalculatorFactoryBuilder AddCouponRepository(IRepository<Coupon> couponRepository);
        IDiscountCalculatorFactoryBuilder AddCartItems(IEnumerable<CartItem> cartItems);
    }
}
