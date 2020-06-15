using System.Collections.Generic;
using NUnit.Framework;
using ShoppingCart.Core.Repositories;
using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using ShoppingCart.Core.Builders;
using System;

namespace ShoppingCart.Tests.Core.Calculators.DiscountTests
{
    class GenericDiscountTests
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

            var discount1 = new Discount { DiscountType = DiscountType.SupplierBased, ProductCategory = ProductCategory.None, Supplier = Supplier.Apple, DiscountPercentage = 5 };
            var coupon1 = new Coupon { Id = 1, Code = "APPLE5", IsValid = false, Discount = discount1 };
            var discount2 = new Discount { DiscountType = DiscountType.SupplierBased, ProductCategory = ProductCategory.None, Supplier = Supplier.Dell, DiscountPercentage = 0 };
            var coupon2 = new Coupon { Id = 2, Code = "DELL5", IsValid = true, Discount = discount2 };
            var discount3 = new Discount { DiscountType = DiscountType.SupplierBased, ProductCategory = ProductCategory.None, Supplier = Supplier.All };
            var coupon3 = new Coupon { Id = 3, Code = "HP10", IsValid = true, Discount = discount3 };
            var discount4 = new Discount { DiscountType = DiscountType.ProductBased, ProductCategory = ProductCategory.Electronic, Supplier = Supplier.Apple, DiscountPercentage = 101 };
            var coupon4 = new Coupon { Id = 4, Code = "LAPTOP101", IsValid = true, Discount = discount4 };
            var discount5 = new Discount { DiscountType = DiscountType.ProductBased, ProductCategory = ProductCategory.Electronic, Supplier = Supplier.Apple, DiscountPercentage = 101 };
            var coupon5 = new Coupon { Id = 5, Code = "MINUSONE", IsValid = true, Discount = discount5 };

            _couponRepository = new CouponRepository<Coupon>();
            _couponRepository.Add(coupon1);
            _couponRepository.Add(coupon2);
            _couponRepository.Add(coupon3);
            _couponRepository.Add(coupon4);
            _couponRepository.Add(coupon5);
        }       

        // Test that a discount of 0m is applied where Discount.IsValid = false        
        [Test]
        public void WithInvalidDiscount_CheckCalculationWithApple5()
        {
            var appleHeadphones = new CartItem (_productRepository) { ProductId = 1, UnitQuantity = 2 };
            var appleUsbCable = new CartItem (_productRepository) { ProductId = 2, UnitQuantity = 1 };
            var hPMonitor = new CartItem (_productRepository) { ProductId = 3, UnitQuantity = 1 };
            var dellLaptop = new CartItem (_productRepository) { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { appleHeadphones, appleUsbCable, hPMonitor, dellLaptop };

            var couponId = 1; // "APPLE5"
            var calc = new ShoppingCartEngineBuilder()
                .AddCartItems(cart)
                .AddProductRepository(_productRepository)
                .AddCoupon(couponId)
                .AddCouponRepository(_couponRepository)
                .GetShoppingCartEngine();
            var total = calc.Total();

            Assert.AreEqual(1124.00m, total);
        }

        // Test that a discount of 0m is Discount.DiscountPercentage = 0
        [Test]
        public void WithNoDiscountPercentageSpecified_CheckCalculationWithHp10()
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

        // Test that a discount of 0m is Discount.DiscountPercentage is not supplied
        [Test]
        public void WithDiscountPercentageZero_CheckCalculationWithHp10()
        {
            var appleHeadphones = new CartItem (_productRepository) { ProductId = 1, UnitQuantity = 2 };
            var appleUsbCable = new CartItem (_productRepository) { ProductId = 2, UnitQuantity = 1 };
            var hPMonitor = new CartItem (_productRepository) { ProductId = 3, UnitQuantity = 1 };
            var dellLaptop = new CartItem (_productRepository) { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { appleHeadphones, appleUsbCable, hPMonitor, dellLaptop };

            var couponId = 3; // "HP10"
            var calc = new ShoppingCartEngineBuilder()
                .AddCartItems(cart)
                .AddProductRepository(_productRepository)
                .AddCoupon(couponId)
                .AddCouponRepository(_couponRepository)
                .GetShoppingCartEngine();
            var total = calc.Total();

            Assert.AreEqual(1124m, total);
        }                

        // Test that if a discount percentage is above 100%, we return a discount of 0m
        [Test]
        public void WithDiscountPercentageAboveHundred_CheckCalculationWithLaptop101()
        {
            var appleHeadphones = new CartItem (_productRepository) { ProductId = 1, UnitQuantity = 2 };
            var appleUsbCable = new CartItem (_productRepository) { ProductId = 2, UnitQuantity = 1 };
            var hPMonitor = new CartItem (_productRepository) { ProductId = 3, UnitQuantity = 1 };
            var dellLaptop = new CartItem (_productRepository) { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { appleHeadphones, appleUsbCable, hPMonitor, dellLaptop };

            var couponId = 4; // "LAPTOP101"
            var calc = new ShoppingCartEngineBuilder()
                .AddCartItems(cart)
                .AddProductRepository(_productRepository)
                .AddCoupon(couponId)
                .AddCouponRepository(_couponRepository)
                .GetShoppingCartEngine();
            var total = calc.Total();
            Assert.AreEqual(1124m, total);
        }

        // Test that if a discount percentage is not between 0-100%, we return a discount of 0m
        [Test]
        public void WithDiscountPercentageBelowZero_CheckCalculationWithMinusOne()
        {
            var appleHeadphones = new CartItem (_productRepository) { ProductId = 1, UnitQuantity = 2 };
            var appleUsbCable = new CartItem (_productRepository) { ProductId = 2, UnitQuantity = 1 };
            var hPMonitor = new CartItem (_productRepository) { ProductId = 3, UnitQuantity = 1 };
            var dellLaptop = new CartItem (_productRepository) { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { appleHeadphones, appleUsbCable, hPMonitor, dellLaptop };

            var couponId = 5; // "MINUSONE"
            var calc = new ShoppingCartEngineBuilder()
                .AddCartItems(cart)
                .AddProductRepository(_productRepository)
                .AddCoupon(couponId)
                .AddCouponRepository(_couponRepository)
                .GetShoppingCartEngine();
            var total = calc.Total();
            Assert.AreEqual(1124m, total);
        }

        // Test that a discount of 0m if the coupon does not exist in the coupon repository (null checks are handled by the UnknownDiscountCalculator)
        [Test]
        public void WithInvalidCoupon_CheckCalculation()
        {
            var appleHeadphones = new CartItem (_productRepository) { ProductId = 1, UnitQuantity = 2 };
            var appleUsbCable = new CartItem (_productRepository) { ProductId = 2, UnitQuantity = 1 };
            var hPMonitor = new CartItem (_productRepository) { ProductId = 3, UnitQuantity = 1 };
            var dellLaptop = new CartItem (_productRepository) { ProductId = 4, UnitQuantity = 1 };

            var cart = new List<CartItem> { appleHeadphones, appleUsbCable, hPMonitor, dellLaptop };

            var couponId = 1000; // Invalid coupon
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
