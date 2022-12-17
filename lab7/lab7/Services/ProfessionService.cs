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

        public List<Profession> GetAll()
        {
            return _context.Professions.ToList();
        }
    }
}
