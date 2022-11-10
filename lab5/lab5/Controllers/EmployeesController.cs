﻿using lab5.Data;
using lab5.Data.Models;
using lab5.Models;
using lab5.Models.EmployeeViewModels;
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

        public IActionResult Index(string firstName, string lastName, string middleName, int? profession,
                                    int page = 1, EmployeeSortState sortOrder = EmployeeSortState.AgeAsc)
        {
            IQueryable<Employee> employees = _context.Employees.Include(x => x.Profession);
            if (profession != 0 && profession != null)
            {
                employees = employees.Where(x => x.Profession.Id == profession);
            }
            if (!String.IsNullOrEmpty(firstName))
            {
                employees = employees.Where(x => x.FirstName.Contains(firstName));
            }
            if (!String.IsNullOrEmpty(lastName))
            {
                employees = employees.Where(x => x.LastName.Contains(lastName));
            }
            if (!String.IsNullOrEmpty(middleName))
            {
                employees = employees.Where(x => x.MiddleName.Contains(middleName));
            }

            switch (sortOrder)
            {
                case EmployeeSortState.FirstNameAsc:
                    employees = employees.OrderBy(e => e.FirstName);
                    break;
                case EmployeeSortState.FirstNameDesc:
                    employees = employees.OrderByDescending(e => e.FirstName);
                    break;
                case EmployeeSortState.LastNameAsc:
                    employees = employees.OrderBy(e => e.LastName);
                    break;
                case EmployeeSortState.LastNameDesc:
                    employees = employees.OrderByDescending(e => e.LastName);
                    break;
                case EmployeeSortState.MiddleNameAsc:
                    employees = employees.OrderBy(e => e.MiddleName);
                    break;
                case EmployeeSortState.MiddleNameDesc:
                    employees = employees.OrderByDescending(e => e.MiddleName);
                    break;
                case EmployeeSortState.AgeDesc:
                    employees = employees.OrderByDescending(e => e.Age);
                    break;
                case EmployeeSortState.EducationAsc:
                    employees = employees.OrderBy(e => e.Education);
                    break;
                case EmployeeSortState.EducationDesc:
                    employees = employees.OrderByDescending(e => e.Education);
                    break;
                case EmployeeSortState.ProfessionAsc:
                    employees = employees.OrderBy(e => e.Profession.Name);
                    break;
                case EmployeeSortState.ProfessionDesc:
                    employees = employees.OrderByDescending(e => e.Profession.Name);
                    break;
                default:
                    employees = employees.OrderBy(e => e.Age);
                    break;
            }


            int pageSize = 10;
            int count = employees.Count();
            IEnumerable<Employee> items = employees.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            EmployeeIndexViewModel viewModel = new EmployeeIndexViewModel
            {
                PageViewModel = pageViewModel,
                FilterViewModel = new EmployeeFilterViewModel(_context.Professions.ToList(), profession, firstName, lastName, middleName),
                SortViewModel = new SortEmployeeViewModel(sortOrder),
                Items = items
            };

            return View(viewModel);
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
