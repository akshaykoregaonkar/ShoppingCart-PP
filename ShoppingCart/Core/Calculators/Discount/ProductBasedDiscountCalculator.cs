using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Core.Calculators.Discount
{
    public class ProductBasedDiscountCalculator : DiscountCalculatorDecorator
    {       
        public ProductBasedDiscountCalculator(Coupon coupon, IRepository<Product> productRepository, IRepository<Coupon> availableCoupons, IEnumerable<CartItem> cartItems) 
            : base(coupon, productRepository, availableCoupons, cartItems)
        {
        }

        protected internal override decimal CalculateForCartItems()
        {
            // No discount when the discount's product category is None or if a supplier is chosen
            if (_coupon.Discount.ProductCategory.Equals(ProductCategory.None) || _coupon.Discount.Supplier != Supplier.All)
                return 0m;


            return GetDiscount((a) =>
            {
                var product = _productRepository.Get(a.ProductId);
                return product.HasAssociatedCategories && product.AssociatedCategories.Contains(_coupon.Discount.ProductCategory);
            });
        } 
    }
}
