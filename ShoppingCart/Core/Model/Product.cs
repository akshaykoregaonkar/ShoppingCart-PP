using ShoppingCart.Core.Model;
using System.Collections.Generic;

namespace ShoppingCart.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<ProductCategory> AssociatedCategories { get; set; } 
        public Supplier Supplier { get; set; }
        public bool HasAssociatedCategories => AssociatedCategories != null;
    }
}
