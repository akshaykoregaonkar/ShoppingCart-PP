using ShoppingCart.Core.Model;

namespace ShoppingCart.Core.Repositories
{
    public class CouponRepository<T> : Repository<T> where T : Coupon
    {
        protected override int GetKey(T item)
        {
            return item.Id;
        }
    }
}
