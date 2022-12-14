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

        public void Delete(int id)
        {
            _context.Employees.Remove(_context.Employees.First(e => e.Id == id));
            _context.SaveChanges();
        }

        //public void Create(string firstName, string lastName)
    }
}
