using ShoppingCart.Interfaces;
using System.Collections.Generic;
namespace ShoppingCart.Core.Repositories
{
    public abstract class Repository <T> : IRepository<T>
    {
        private readonly Dictionary<int, T> _dictionary = new Dictionary<int, T>();

        public virtual void Add(T item)
        {
            var key = GetKey(item);
            _dictionary.TryAdd(key, item);
        }

        public T Get(int id) => _dictionary.TryGetValue(id, out var item) ? item : default;

        protected abstract int GetKey(T item);
    }
}
