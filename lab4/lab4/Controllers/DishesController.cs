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
        [ResponseCache(Duration = 260, Location = ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
            return View(_context.Dishes);
        }
    }
}
