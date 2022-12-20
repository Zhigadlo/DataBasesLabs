using lab7.Data;
using Microsoft.EntityFrameworkCore;

namespace lab7.Services
{
    public class EmployeeService
    {
        private CafeContext _context;
        public EmployeeService(CafeContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>?> GetAll()
        {
            return await Task.FromResult(_context.Employees.Include(e => e.Profession).ToList());
        }

        public Employee? Delete(int id)
        {
            Employee? employee = _context.Employees.First(e => e.Id == id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee? Get(int id)
        {
            Employee? employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            return employee;
        }

        public bool Update(Employee? employee)
        {
            if (employee != null)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Create(Employee? employee)
        {
            if (employee != null)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
