using System.Collections.Generic;
using NUnit.Framework;
using ShoppingCart.Core.Repositories;
using ShoppingCart.Core.Model;
using ShoppingCart.Interfaces;
using ShoppingCart.Model;
using ShoppingCart.Core.Builders;
using System;

namespace ShoppingCart.Tests.Core.Calculators
{
    public class PreDiscountCalculatorTests
    {
        private IRepository<Product> _productRepository;

        [SetUp]
        public void Init()
        {
            var usbCable = new Product { Id = 1, Name = "USB Cable", Price = 4 };
            var laptop = new Product { Id = 2, Name = "Laptop", Price = 1000 };
            var iPad = new Product { Id = 3, Name = "iPad", Price = 220 };

            _productRepository = new ProductRepository<Product>();
            _productRepository.Add(usbCable);
            _productRepository.Add(laptop);
            _productRepository.Add(iPad);
        }

        [Test]
        public void WithSmallCart_CheckCalculation()
        {
            var carItem1 = new CartItem (_productRepository) { ProductId = 1, UnitQuantity = 1 };
            var carItem2 = new CartItem (_productRepository) { ProductId = 2, UnitQuantity = 1 };
            var cart = new List<CartItem> { carItem1, carItem2 };
            
            var calc = new ShoppingCartEngineBuilder()
                .AddCartItems(cart)
                .AddProductRepository(_productRepository)
                .GetShoppingCartEngine();                          
            var total = calc.Total();

            Assert.AreEqual(1004, total);
        }

        [Test]
        public void WithOneItem_CheckCalculation()
        {
            var carItem1 = new CartItem (_productRepository) { ProductId = 1, UnitQuantity = 1 };
     
            var cart = new List<CartItem> { carItem1 };

            var calc = new ShoppingCartEngineBuilder()
                .AddCartItems(cart)
                .AddProductRepository(_productRepository)
                .GetShoppingCartEngine();

            var total = calc.Total();

            Assert.AreEqual(11, total);
        }
        
        [Test]
        public void WithNoRepository_CheckExceptionIsThrown()
        {
            var carItem1 = new CartItem (_productRepository) { ProductId = 1, UnitQuantity = 1 };

            var cart = new List<CartItem> { carItem1 };

            var calc = new ShoppingCartEngineBuilder()                
                .AddCartItems(cart)
                .GetShoppingCartEngine();

            var ex = Assert.Throws<ArgumentNullException>(() => calc.Total());
            Assert.That(ex.ParamName, Is.EqualTo("productRepository"));
        }

        [Test]
        public void WithNoCart_CheckExceptionIsThrown()
        {            
            var calc = new ShoppingCartEngineBuilder()
                .AddProductRepository(_productRepository)
                .GetShoppingCartEngine();

            var ex = Assert.Throws<ArgumentNullException>(() => calc.Total());
            Assert.That(ex.ParamName, Is.EqualTo("cartItems"));
        }

        // Test that if an item does not exist in the product repository, we should not be able to add it to the cart
        [Test]
        public void WithInvalidProduct_CheckExceptionIsThrown()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CartItem(_productRepository) { ProductId = 10, UnitQuantity = 1 });
            Assert.That(ex.ParamName, Is.EqualTo("ProductId"));
            Assert.That(ex.Message, Is.EqualTo("ID: 10 cannot be added to cart as it does not exist in the product repository (Parameter 'ProductId')"));
        }
    }
}