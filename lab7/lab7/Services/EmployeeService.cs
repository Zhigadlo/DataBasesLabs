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
    }
}
