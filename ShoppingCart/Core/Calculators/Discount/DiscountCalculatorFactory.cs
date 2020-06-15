using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System;
using System.Collections.Generic;

namespace ShoppingCart.Core.Calculators.Discount
{
    public class DiscountCalculatorFactory : IDiscountCalculatorFactory
    {
        public int CouponId { get; set; }
        public decimal ShippingCost { get; set; }
        public IRepository<Product> ProductRepository { get; set; }
        public IRepository<Coupon> CouponRepository { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
        public ICalculator Create()
        {
            try
            {
                Coupon coupon = CouponRepository.Get(CouponId);
                if (coupon.Discount.DiscountType == DiscountType.ShippingBased)
                    return new ShippingBasedDiscountCalculator(ShippingCost);

                return (ICalculator)Activator.CreateInstance(
                    Type.GetType($"ShoppingCart.Core.Calculators.Discount.{coupon.Discount.DiscountType}DiscountCalculator"),
                    new object[] { coupon, ProductRepository, CouponRepository, CartItems });
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                return UnknownDiscountCalculator.Instance;
            }
        }
    }
}
