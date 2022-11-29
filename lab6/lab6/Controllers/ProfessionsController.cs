using lab6.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab6.Controllers
{
    [Route("api/Professions")]
    public class ProfessionsController : Controller
    {
        private CafeContext _context;

        public ProfessionsController(CafeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Profession> Get()
        {
            return _context.Professions.AsEnumerable();
        }
        [HttpGet("{id}")]
        public Profession? Get(int id)
        {
            return _context.Professions.FirstOrDefault(i => i.Id == id);
        }

        [HttpPost]
        public IActionResult Add(Profession profession)
        {
            if (profession != null)
            {
                _context.Professions.Add(profession);
                _context.SaveChanges();
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Profession? profession = _context.Professions.FirstOrDefault(i => i.Id == id);
            if (profession == null)
                return NotFound();
            else
            {
                _context.Professions.Remove(profession);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpPut]
        public IActionResult Update(Profession profession)
        {
            if (_context.Ingridients.Count(i => i.Id == profession.Id) == 0)
                return BadRequest();
            else
            {
                _context.Professions.Update(profession);
                _context.SaveChanges();
                return Ok();
            } 
        }
    }
}
