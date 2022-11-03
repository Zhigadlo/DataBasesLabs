using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers
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
            return View(_context.Dishes);
        }
    }
}
