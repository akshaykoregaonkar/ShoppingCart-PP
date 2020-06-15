using ShoppingCart.Interfaces;
using System;

namespace ShoppingCart.Model
{
    public class CartItem
    {
        private readonly IRepository<Product> _productRepository;
        private int productId;

        public CartItem(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        
        public int ProductId
        {
            get => productId; 
            set
            {
                if (_productRepository.Get(value) == null)
                    throw new ArgumentNullException(nameof(ProductId),"ID: " + value + " cannot be added to cart as it does not exist in the product repository");

                productId = value;
            }
        }
        public int UnitQuantity { get; set; }
    }
}