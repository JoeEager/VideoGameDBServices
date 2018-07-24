using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Interfaces;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Repositories
{
    public class YearRepository : IYearRepository
    {
        private VideogamesContext _context;

        public YearRepository(VideogamesContext context)
        {
            _context = context;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Years.Where(e => e.Id == id || e.Year==id.ToString()).AnyAsync();
        }

        public async Task<Years> Find(int id)
        {
            return await _context.Years.SingleOrDefaultAsync(y => y.Id == id || y.Year == id.ToString());
        }

        public IEnumerable<Years> GetAll()
        {
            return _context.Years;
        }

        public async Task<List<Games>> GetGamesByYear(int yearId)
        {
            var year = _context.Years.SingleOrDefault(g => g.Id ==yearId ||g.Year == yearId.ToString() );
            return await _context.Games.Where(m => m.Year == year.Year).ToListAsync();

        }
    }
}
