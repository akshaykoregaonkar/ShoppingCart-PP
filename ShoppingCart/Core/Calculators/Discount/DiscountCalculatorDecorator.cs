using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Core.Calculators.Discount
{
    public abstract class DiscountCalculatorDecorator: CalculatorBase
    {
        protected readonly Coupon _coupon;
        protected readonly IRepository<Coupon> _availableCoupons;

        public DiscountCalculatorDecorator(Coupon coupon, IRepository<Product> productRepository, IRepository<Coupon> availableCoupons, IEnumerable<CartItem> cartItems) 
            : base(cartItems, productRepository)
        {
            if (!coupon.IsValid || availableCoupons.Get(coupon.Id) == null)
                throw new ArgumentException("The coupon entered is invalid");
            if (coupon.Discount.DiscountPercentage < 0 || coupon.Discount.DiscountPercentage > 100)
                throw new ArgumentOutOfRangeException("Discount must be between 0-100 percent");
            _coupon = coupon;
            _availableCoupons = availableCoupons;
        }

        public override decimal Calculate() 
        {
            if (_coupon.Discount.DiscountPercentage == 0)
                return 0;
            return CalculateForCartItems();
        }

        protected internal abstract decimal CalculateForCartItems();

        protected decimal GetDiscount(Predicate<CartItem> discountAction)
        {
            var discountItems = _cartItems.ToList()
                .FindAll(discountAction);

            var discountItemsSum = discountItems
                .ToList()
                .Sum(i => i.UnitQuantity * _productRepository.Get(i.ProductId).Price);

            // This calculation needs to be independent of the above pipeline to avoid shared mutability between cartItem and coupon
            return discountItemsSum * (_coupon.Discount.DiscountPercentage / 100);
        }
    }
}
