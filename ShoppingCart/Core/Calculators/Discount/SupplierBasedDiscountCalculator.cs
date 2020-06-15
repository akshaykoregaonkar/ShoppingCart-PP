using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System;
using System.Collections.Generic;

namespace ShoppingCart.Core.Calculators.Discount
{
    class SupplierBasedDiscountCalculator : DiscountCalculatorDecorator
    {
        public SupplierBasedDiscountCalculator(Coupon coupon, IRepository<Product> productRepository, IRepository<Coupon> availableCoupons, IEnumerable<CartItem> cartItems)
            : base(coupon, productRepository, availableCoupons, cartItems)
        {
        }

        protected internal override decimal CalculateForCartItems()
        {
            // Supplier discounts must be supplier specific and applied on all products
            if (_coupon.Discount.Supplier == Supplier.All || _coupon.Discount.ProductCategory != ProductCategory.None)
                return 0m;

            return GetDiscount((a) => _productRepository.Get(a.ProductId).Supplier.Equals(_coupon.Discount.Supplier));
        }
    }
}
