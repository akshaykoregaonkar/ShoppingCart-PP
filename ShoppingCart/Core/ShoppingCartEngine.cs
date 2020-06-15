using ShoppingCart.Core.Builders;
using ShoppingCart.Core.Calculators.Basket;
using ShoppingCart.Core.Calculators.Discount;
using ShoppingCart.Core.Calculators.Shipping;
using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System.Collections.Generic;

namespace ShoppingCart.Core
{
    public class ShoppingCartEngine <T> where T : CartItem
    {
        public IEnumerable<T> CartItems { get; set; }
        public IRepository<Product> ProductRepository { get; set; }
        public BasketCalculatorFactory BasketCalculatorFactory { get; set; } = new BasketCalculatorFactory();
        public ShippingCalculatorFactory ShippingCalculatorFactory { get; set; } = new ShippingCalculatorFactory();
        public IRepository<Coupon> CouponRepository { get; set; }
        public DiscountCalculatorFactory DiscountCalculatorFactory { get; set; } = new DiscountCalculatorFactory();
        public int CouponId { get; set; }

        public decimal Total()
        {
            // Calculate the total of the basket 
            var basketCalculator = BasketCalculatorFactory.Create(CartItems, ProductRepository);
            var productTotal = basketCalculator.Calculate();

            // Apply Shipping
            var shippingCalculator = ShippingCalculatorFactory.Create(productTotal);
            var shippingCost = shippingCalculator.Calculate();

            // Apply Discount
            var discountCalculator = new DiscountCalculatorFactoryBuilder()
                .AddCoupon(CouponId)
                .AddShippingCost(shippingCost)
                .AddProductRepository(ProductRepository)
                .AddCouponRepository(CouponRepository)
                .AddCartItems(CartItems)
                .GetDiscountCalculatorFactory()
                .Create();            
            var discount = discountCalculator.Calculate();

            return productTotal + shippingCost - discount;
        }
    }
}