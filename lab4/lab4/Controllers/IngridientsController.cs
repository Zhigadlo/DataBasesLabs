using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers
{
    public class IngridientsController : Controller
    {
        private CafeContext _context;
        public IngridientsController(CafeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Ingridients);
        }
    }
}
