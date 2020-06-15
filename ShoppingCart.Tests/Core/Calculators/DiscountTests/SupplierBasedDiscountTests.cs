using System.Collections.Generic;
using NUnit.Framework;
using ShoppingCart.Core.Repositories;
using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using ShoppingCart.Core.Builders;

namespace ShoppingCart.Tests.Core.Calculators.DiscountTests
{
    class SupplierBasedDiscountTests
    {
        private IRepository<Product> _productRepository;
        private IRepository<Coupon> _couponRepository;

        [SetUp]
        public void Init()
        {
            var appleHeadphone = new Product { Id = 1, Name = "Headphone", Price = 10, AssociatedCategories = new List<ProductCategory> { ProductCategory.Accessory, ProductCategory.Electronic, ProductCategory.Audio }, Supplier = Supplier.Apple };
            var apppleUsbCable = new Product { Id = 2, Name = "USB Cable", Price = 4, AssociatedCategories = new List<ProductCategory> { ProductCategory.Accessory }, Supplier = Supplier.Apple };
            var hPmonitor = new Product { Id = 3, Name = "Monitor", Price = 100, AssociatedCategories = new List<ProductCategory> { ProductCategory.Electronic }, Supplier = Supplier.HP };
            var dellLaptop = new Product { Id = 4, Name = "Laptop", Price = 1000, AssociatedCategories = new List<ProductCategory> { ProductCategory.Electronic }, Supplier = Supplier.Dell };

            _productRepository = new ProductRepository<Product>();
            _productRepository.Add(appleHeadphone);
            _productRepository.Add(apppleUsbCable);
            _productRepository.Add(hPmonitor);
            _productRepository.Add(dellLaptop);

            var discount1 = new Discount { DiscountType = DiscountType.SupplierBased, ProductCategory = ProductCategory.None, Supplier = Supplier.All, DiscountPercentage = 10 };
            var coupon1 = new Coupon { Id = 1, Code = "APPLE10", IsValid = true, Discount = discount1 };
            var discount2 = new Discount { DiscountType = DiscountType.SupplierBased, ProductCategory = ProductCategory.Electronic, Supplier = Supplier.Dell, DiscountPercentage = 5 };
            var coupon2 = new Coupon { Id = 2, Code = "DELL5", IsValid = true, Discount = discount2 };
            var discount3 = new Discount { DiscountType = DiscountType.SupplierBased, ProductCategory = ProductCategory.None, Supplier = Supplier.Dell, DiscountPercentage = 10 };
            var coupon3 = new Coupon { Id = 3, Code = "DELL10", IsValid = true, Discount = discount3 };

            _couponRepository = new CouponRepository<Coupon>();
            _couponRepository.Add(coupon1);
            _couponRepository.Add(coupon2);
            _couponRepository.Add(coupon3);
        }

        // Test that a discount of 0m is applied where Discount.DiscountType = SupplierBased but Discount.Supplier = All
        [Test]
        public void WithProductCategorySetToNone_CheckCalculationWithAudio10()
        {
            var appleHeadphones = new CartItem (_productRepository) { ProductId = 1, UnitQuantity = 2 };
            var appleUsbCable = new CartItem (_productRepository) { ProductId = 2, UnitQuantity = 1 };
            var hPMonitor = new CartItem (_productRepository) { ProductId = 3, UnitQuantity = 1 };
            var dellLaptop = new CartItem (_productRepository) { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { appleHeadphones, appleUsbCable, hPMonitor, dellLaptop };

            var couponId = 1; // "APPLE10"
            var calc = new ShoppingCartEngineBuilder()
                .AddCartItems(cart)
                .AddProductRepository(_productRepository)
                .AddCoupon(couponId)
                .AddCouponRepository(_couponRepository)
                .GetShoppingCartEngine();
            var total = calc.Total();

            Assert.AreEqual(1124.00m, total);
        }

        // Test that a discount of 0m is applied where Discount.ProductCategory != ProductCategory.None
        [Test]
        public void WithInvalidProductCategory_CheckCalculationWithDell5()
        {
            var appleHeadphones = new CartItem (_productRepository) { ProductId = 1, UnitQuantity = 2 };
            var appleUsbCable = new CartItem (_productRepository) { ProductId = 2, UnitQuantity = 1 };
            var hPMonitor = new CartItem (_productRepository) { ProductId = 3, UnitQuantity = 1 };
            var dellLaptop = new CartItem (_productRepository) { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { appleHeadphones, appleUsbCable, hPMonitor, dellLaptop };

            var couponId = 2; // "DELL5"
            var calc = new ShoppingCartEngineBuilder()
                .AddCartItems(cart)
                .AddProductRepository(_productRepository)
                .AddCoupon(couponId)
                .AddCouponRepository(_couponRepository)
                .GetShoppingCartEngine();
            var total = calc.Total();

            Assert.AreEqual(1124m, total);
        }
    }
}
