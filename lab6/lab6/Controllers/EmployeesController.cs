using lab6.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab6.Controllers
{
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        private CafeContext _context;
        public EmployeesController(CafeContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _context.Employees.AsEnumerable();
        }

        [HttpGet("{id}")]
        public Employee? Get(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        { 
            Employee? employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody]Employee employee)
        {
            if(employee == null || employee.ProfessionId == 0 || employee.FirstName == null 
                || employee.LastName == null || employee.Age == 0)
                return BadRequest();
            else
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Employee employee)
        {
            if(_context.Employees.Count(e => e.Id == employee.Id) == 0)
                return BadRequest();
            else
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
