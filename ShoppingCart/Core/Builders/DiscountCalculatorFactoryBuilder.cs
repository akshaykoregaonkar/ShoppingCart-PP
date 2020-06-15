using ShoppingCart.Core.Calculators.Discount;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System.Collections.Generic;

namespace ShoppingCart.Core.Builders
{
    class DiscountCalculatorFactoryBuilder : IDiscountCalculatorFactoryBuilder
    {
        readonly DiscountCalculatorFactory discountCalculatorFactory = new DiscountCalculatorFactory();
        public DiscountCalculatorFactory GetDiscountCalculatorFactory()
        {
            return discountCalculatorFactory;
        }
        public IDiscountCalculatorFactoryBuilder AddCartItems(IEnumerable<CartItem> cartItems)
        {
            discountCalculatorFactory.CartItems = cartItems;
            return this;
        }

        public IDiscountCalculatorFactoryBuilder AddCoupon(int couponID)
        {
            discountCalculatorFactory.CouponId = couponID;
            return this;
        }

        public IDiscountCalculatorFactoryBuilder AddCouponRepository(IRepository<Coupon> couponRepository)
        {
            discountCalculatorFactory.CouponRepository = couponRepository;
            return this;
        }

        public IDiscountCalculatorFactoryBuilder AddProductRepository(IRepository<Product> productRepository)
        {
            discountCalculatorFactory.ProductRepository = productRepository;
            return this;
        }

        public IDiscountCalculatorFactoryBuilder AddShippingCost(decimal shippingCost)
        {
            discountCalculatorFactory.ShippingCost = shippingCost;
            return this;
        }
    }
}
