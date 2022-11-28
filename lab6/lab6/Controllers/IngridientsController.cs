using lab6.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab6.Controllers
{
    [Route("api/[controller]")]
    public class IngridientsController : Controller
    {
        private CafeContext _context;

        public IngridientsController(CafeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Ingridient> Get()
        {
            return _context.Ingridients.AsEnumerable();
        }
        [HttpGet("{id}")]
        public Ingridient? Get(int id)
        {
            return _context.Ingridients.FirstOrDefault(i => i.Id == id);
        }

        [HttpPost]
        public IActionResult Add(Ingridient ingridient)
        {
            if (ingridient != null)
            {
                _context.Ingridients.Add(ingridient);
                _context.SaveChanges();
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Ingridient? ingridient = _context.Ingridients.FirstOrDefault(i => i.Id == id);
            if (ingridient == null)
                return NotFound();
            else
            {
                _context.Ingridients.Remove(ingridient);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpPut]
        public IActionResult Update(Ingridient ingridient)
        {
            if (_context.Ingridients.Count(i => i.Id == ingridient.Id) == 0)
                return BadRequest();
            else
            {
                _context.Ingridients.Update(ingridient);
                _context.SaveChanges();
                return Ok();
            } 
        }
    }
}
