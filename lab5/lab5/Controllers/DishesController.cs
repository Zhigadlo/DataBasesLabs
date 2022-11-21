using lab5.Data;
using lab5.Data.Models;
using lab5.Models;
using lab5.Models.DishViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Data;

namespace lab5.Controllers
{
    public class DishesController : BaseController
    {
        private CafeContext _context;
        private const string _key = "dishes";
        public DishesController(CafeContext context, IMemoryCache cache) : base(cache)
        {
            _context = context;
        }
        [Authorize(Roles = "admin,user")]
        public IActionResult Index(string name, int page = 1, DishSortState sortOrder = DishSortState.NameAsc)
        {
            IQueryable<Dish> dishes;
            if(!_cache.TryGetValue(_key, out dishes))
            {
                dishes = _context.Dishes.Include(x => x.IngridientsDishes);
                _cache.Set(_key, dishes.ToList());
            }

            if(!String.IsNullOrEmpty(name))
            {
                dishes = dishes.Where(x => x.Name.Contains(name));
            }
            
            switch(sortOrder)
            {
                case DishSortState.CostAsc:
                    dishes = dishes.OrderBy(d => d.Cost);
                    break;
                case DishSortState.CostDesc:
                    dishes = dishes.OrderByDescending(d => d.Cost);
                    break;
                case DishSortState.CookingTimeAsc:
                    dishes = dishes.OrderBy(d => d.CookingTime);
                    break;
                case DishSortState.CookingTimeDesc:
                    dishes = dishes.OrderByDescending(d => d.CookingTime);
                    break;
                case DishSortState.NameDesc:
                    dishes = dishes.OrderByDescending(d => d.Name);
                    break;
                default:
                    dishes = dishes.OrderBy(d => d.Name);
                    break;
            }

            int pageSize = 10;
            int count = dishes.Count();
            IEnumerable<Dish> items = dishes.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            DishIndexViewModel viewModel = new DishIndexViewModel
            {
                PageViewModel = pageViewModel,
                Items = items,
                SortViewModel = new SortDishViewModel(sortOrder),
                FilterViewModel = new FilterDishViewModel(name)
            };
            return View(viewModel);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            CacheClear();
            _context.Dishes.Remove(_context.Dishes.First(x => x.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        public IActionResult CreateView()
        {
            return View("Create", _context.Ingridients.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("Dishes/Update/{id}")]
        public IActionResult UpdateView(int id)
        {
            Dish dish = GetDishById(id);
            IncludeIngridientDishInDish(dish);
            IEnumerable<Ingridient> ingridients = _context.Ingridients;
            DishUpdateViewModel viewModel = new DishUpdateViewModel(dish, dish.IngridientsDishes, ingridients);
            return View("Update", viewModel);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Create(string name, int cost, int cookingTime, int[] ingridientIds, int[] weights)
        {
            var newDish = new Dish();
            newDish.Name = name;
            newDish.Cost = cost;
            newDish.CookingTime = cookingTime;
            _context.Dishes.Add(newDish);
            _context.SaveChanges();
            Dish dish = _context.Dishes.First(x => x.Name == name);
            for (int i = 0; i < ingridientIds.Length; i++)
            {
                var ingridientDish = new IngridientsDish();
                ingridientDish.Dish = dish;
                ingridientDish.Ingridient = _context.Ingridients.First(x => x.Id == ingridientIds[i]);
                ingridientDish.IngridientWeight = weights[i];
                _context.IngridientsDishes.Add(ingridientDish);
            }
            _context.SaveChanges();
            CacheClear();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin,user")]
        public IActionResult Description(int id)
        {
            return View(GetDishById(id));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("Dishes/Update/{id}")]
        public IActionResult Update(int id, string name, int cost, int cookingTime, int[] ingridientIds, int[] weights)
        {
            var dishForUpdate = _context.Dishes.Include(x => x.IngridientsDishes).First(d => d.Id == id);
            dishForUpdate.Name = name;
            dishForUpdate.Cost = cost;
            dishForUpdate.CookingTime = cookingTime;
            for (int i = 0; i < ingridientIds.Length; i++)
            {
                var ingridientDish = _context.IngridientsDishes.FirstOrDefault(x => x.IngridientId == ingridientIds[i] && x.DishId == dishForUpdate.Id);
                if (ingridientDish == null)
                {
                    var newIngridientDish = new IngridientsDish();
                    newIngridientDish.Dish = dishForUpdate;
                    newIngridientDish.Ingridient = _context.Ingridients.First(x => x.Id == ingridientIds[i]);
                    newIngridientDish.IngridientWeight = weights[i];
                    _context.IngridientsDishes.Add(newIngridientDish);
                }
                else
                {
                    ingridientDish.Dish = dishForUpdate;
                    ingridientDish.Ingridient = _context.Ingridients.First(x => x.Id == ingridientIds[i]);
                    ingridientDish.IngridientWeight = weights[i];
                    _context.IngridientsDishes.Update(ingridientDish);
                }
            }
            _context.Dishes.Update(dishForUpdate);
            _context.SaveChanges();
            CacheClear();
            return RedirectToAction("Index");
        }

        private Dish GetDishById(int id)
        {
            var dish = _context.Dishes.Include(x => x.IngridientsDishes).First(x => x.Id == id);
            IncludeIngridientDishInDish(dish);
            return dish;
        }

        private void IncludeIngridientDishInDish(Dish dish)
        {
            foreach (var ingridientDish in dish.IngridientsDishes)
            {
                ingridientDish.Ingridient = _context.Ingridients.First(x => x.Id == ingridientDish.IngridientId);
            }
        }
    }
}
