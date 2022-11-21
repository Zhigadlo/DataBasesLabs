using lab4.Cafe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab4.Controllers
{
    public class EmployeesController : Controller
    {
        private CafeContext _context;

        public EmployeesController(CafeContext context)
        {
            _context = context;
        }
        [ResponseCache(Duration = 260, Location = ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
            IEnumerable<Employee> employees = _context.Employees.Include(e => e.Profession);
            return View(employees);
        }
    }
}
