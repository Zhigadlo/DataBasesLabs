using lab5.Data;
using lab5.Data.Models;
using lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab5.Controllers
{
    public class DishesController : Controller
    {
        private CafeContext _context;

        public DishesController(CafeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var dishes = _context.Dishes.Include(x => x.IngridientsDishes);
            return View(dishes);
        }

        public IActionResult Delete(int id)
        {
            _context.Dishes.Remove(_context.Dishes.ToList().First(x => x.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CreateView()
        {
            return View("Create", _context.Ingridients.ToList());
        }

        [HttpGet]
        [Route("Dishes/Update/{id}")]
        public IActionResult UpdateView(int id)
        {
            Dish dish = GetDishById(id);
            IncludeIngridientDishInDish(dish);
            IEnumerable<Ingridient> ingridients = _context.Ingridients;
            DishUpdateViewModel viewModel = new DishUpdateViewModel(dish, dish.IngridientsDishes, ingridients);
            return View("Update", viewModel);
        }

        public IActionResult Create(string name, int cost, int cookingTime, int[] ingridientIds, int[] weights)
        {
            var newDish = new Dish();
            newDish.Name = name;
            newDish.Cost = cost;
            newDish.CookingTime = cookingTime;
            _context.Dishes.Add(newDish);
            _context.SaveChanges();
            Dish dish = _context.Dishes.First(x => x.Name == name);
            for(int i = 0; i < ingridientIds.Length; i++)
            {
                var ingridientDish = new IngridientsDish();
                ingridientDish.Dish = dish;
                ingridientDish.Ingridient = _context.Ingridients.First(x => x.Id == ingridientIds[i]);
                ingridientDish.IngridientWeight = weights[i];
                _context.IngridientsDishes.Add(ingridientDish);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Description(int id)
        {
            return View(GetDishById(id));
        }

        [HttpPost]
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
