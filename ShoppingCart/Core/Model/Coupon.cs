namespace ShoppingCart.Core.Model
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public Discount Discount { get; set; }
        public bool IsValid { get; set; }
    }
}
