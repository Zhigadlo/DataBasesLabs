using lab7.Data;

namespace lab7.Services
{
    public class ProfessionService
    {
        private CafeContext _context;
        public ProfessionService(CafeContext context)
        {
            _context = context;
        }

        public async Task<List<Profession>> GetAll()
        {
            return await Task.FromResult(_context.Professions.ToList());
        }

        public Profession? Delete(int id)
        {
            Profession? profession = _context.Professions.First(p => p.Id == id);
            _context.Professions.Remove(profession);
            _context.SaveChanges();
            return profession;
        }

        public Profession? Get(int id)
        {
            Profession? profession = _context.Professions.FirstOrDefault(p => p.Id == id);
            return profession;
        }

        public bool Update(Profession? profession)
        {
            if (profession != null)
            {
                _context.Professions.Update(profession);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Create(Profession? profession)
        {
            if (profession != null)
            {
                _context.Professions.Add(profession);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
