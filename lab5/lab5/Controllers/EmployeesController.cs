using lab5.Data;
using lab5.Data.Models;
using lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab5.Controllers
{
    public class EmployeesController : Controller
    {
        private CafeContext _context;

        public EmployeesController(CafeContext cafeContext)
        {
            _context = cafeContext;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.Include(x => x.Profession);
            return View(employees);
        }

        public IActionResult CreateView()
        {
            return View("Create", _context.Professions);
        }

        [HttpGet]
        [Route("Employees/Update/{id}")]
        public IActionResult UpdateView(int id)
        {
            var employee = _context.Employees.Include(x => x.Profession).First(x => x.Id == id);
            var viewModel = new EmployeeUpdateViewModel(employee, _context.Professions);
            return View("Update", viewModel);
        }

        [HttpPost]
        [Route("Employees/Update/{id}")]
        public IActionResult Update(int id, Employee employee, string profession)
        {
            employee.Id = id;
            employee.Profession = _context.Professions.First(x => x.Name == profession);
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _context.Employees.Remove(_context.Employees.First(x => x.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create(Employee employee, string profession)
        {
            employee.Profession = _context.Professions.First(x => x.Name == profession);
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
