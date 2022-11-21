using lab3.Cafe.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace lab3.Cafe.Services
{
    public class CachedDishesService
    {
        private CafeContext _db;
        private IMemoryCache _cache;
        private int _rowsNumber;

        public CachedDishesService(CafeContext context, IMemoryCache memoryCache)
        {   
            _db = context;
            _cache = memoryCache;
            _rowsNumber = 20;
        }

        public IEnumerable<Dish> GetDish()
        {
            return _db.Dishes.Take(_rowsNumber).ToList();
        }

        public void AddDishes(string cacheKey)
        {
            IEnumerable<Dish> dishes = _db.Dishes.Take(_rowsNumber);

            _cache.Set(cacheKey, dishes, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }

        public IEnumerable<Dish> GetDishes(string cacheKey)
        {
            IEnumerable<Dish> dishes = null;
            if (!_cache.TryGetValue(cacheKey, out dishes))
            {
                dishes = _db.Dishes.Take(_rowsNumber).ToList();
                if (dishes != null)
                {
                    _cache.Set(cacheKey, dishes,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(260)));
                }
            }
            return dishes;
        }

    }
}
