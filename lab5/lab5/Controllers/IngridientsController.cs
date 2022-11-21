using lab5.Data;
using lab5.Data.Models;
using lab5.Models;
using lab5.Models.IngridientViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace lab5.Controllers
{
    public class IngridientsController : BaseController
    {
        private CafeContext _context;
        private string _key = "ingridients";
        public IngridientsController(CafeContext context, IMemoryCache cache) : base(cache)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index(int? ingridient, string name, int page = 1,
                                    IngridientSortState sortOrder = IngridientSortState.NameAsc)
        {
            IQueryable<Ingridient> ingridients;
            if(!_cache.TryGetValue(_key, out ingridients))
            {
                ingridients = _context.Ingridients;
                _cache.Set(_key, ingridients.ToList());
            }

            if (ingridient != null)
            {
                HttpContext.Session.SetInt32("ingridient", (int)ingridient);
            }
            else
            {
                ingridient = HttpContext.Session.Keys.Contains("ingridient")
                           ? HttpContext.Session.GetInt32("ingridient") : -1;
            }
            if (ingridient != -1)
                ingridients = ingridients.Where(x => x.Id == ingridient);

            name = GetStringFromSession("ingridintname", name);
            ingridients = ingridients.Where(x => x.Name.Contains(name));

            switch (sortOrder)
            {
                case IngridientSortState.NameDesc:
                    ingridients = ingridients.OrderByDescending(i => i.Name);
                    break;
                default:
                    ingridients = ingridients.OrderBy(i => i.Name);
                    break;
            }

            ingridients.OrderBy(x => x.Name);

            int pageSize = 10;
            int count = ingridients.Count();
            IEnumerable<Ingridient> items = ingridients.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IngridientIndexViewModel viewModel = new IngridientIndexViewModel
            {
                PageViewModel = pageViewModel,
                Items = items,
                FilterViewModel = new FilterIngridientViewModel(_context.Ingridients.ToList(), ingridient, name),
                SortViewModel = new SortIngridientViewModel(sortOrder)
            };
            return View(viewModel);
        }
        
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            _context.Ingridients.Remove(_context.Ingridients.ToList().First(x => x.Id == id));
            CacheClear();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        public IActionResult CreateView()
        {
            return View("Create");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("Ingridients/Update/{id}")]
        public IActionResult UpdateView(int id)
        {
            var ingridient = _context.Ingridients.First(x => x.Id == id);
            return View("Update", ingridient);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Create(string name)
        {
            var newIngridient = new Ingridient();
            newIngridient.Name = name;
            _context.Ingridients.Add(newIngridient);

            CacheClear();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("Ingridients/Update/{id}")]
        public IActionResult Update(int id, string name)
        {
            var ingridientForUpdate = _context.Ingridients.First(x => x.Id == id);
            ingridientForUpdate.Name = name;
            CacheClear();
            _context.Update(ingridientForUpdate);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
