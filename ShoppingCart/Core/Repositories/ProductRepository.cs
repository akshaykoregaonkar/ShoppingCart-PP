using ShoppingCart.Model;

namespace ShoppingCart.Core.Repositories
{
    public class ProductRepository<T> : Repository<T> where T : Product
    {
        protected override int GetKey(T item)
        {
            return item.Id;
        }
    }
}