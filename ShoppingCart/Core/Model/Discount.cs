namespace ShoppingCart.Core.Model
{
    public class Discount
    {
        public DiscountType DiscountType { get; set; }
        public decimal DiscountPercentage { get; set; } = 0;
        public Supplier Supplier { get; set; } = Supplier.All;
        public ProductCategory ProductCategory { get; set; } = ProductCategory.None;
    }
}
