using lab5.Data;
using lab5.Data.Models;
using lab5.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab5.Controllers
{
    public class IngridientsController : Controller
    {
        private CafeContext _context;
        public IngridientsController(CafeContext context)
        {
            _context = context;
        }
        
        public IActionResult Index(int page = 1)
        {
            IQueryable<Ingridient> ingridients = _context.Ingridients;

            int pageSize = 10;
            int count = ingridients.Count();
            IEnumerable<Ingridient> items = ingridients.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel<Ingridient> viewModel = new IndexViewModel<Ingridient>
            {
                PageViewModel = pageViewModel,
                Items = items
            };
            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            _context.Ingridients.Remove(_context.Ingridients.ToList().First(x => x.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CreateView()
        {
            return View("Create");
        }
        [HttpGet]
        [Route("Ingridients/Update/{id}")]
        public IActionResult UpdateView(int id)
        {
            var ingridient = _context.Ingridients.First(x => x.Id == id);
            return View("Update", ingridient);
        }

        public IActionResult Create(string name)
        {
            var newIngridient = new Ingridient();
            newIngridient.Name = name;
            _context.Ingridients.Add(newIngridient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("Ingridients/Update/{id}")]
        public IActionResult Update(int id, string name)
        {
            var ingridientForUpdate = _context.Ingridients.First(x => x.Id == id);
            ingridientForUpdate.Name = name;
            _context.Update(ingridientForUpdate);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
