using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Interfaces;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private VideogamesContext _context;

        public DeveloperRepository(VideogamesContext context)
        {
            _context = context;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Developers.AnyAsync(e => e.Id == id);
        }

        public async Task<Developers> Find(int id)
        {
            return await _context.Developers.SingleOrDefaultAsync(d => d.Id == id);
        }

        public IEnumerable<Developers> GetAll()
        {
            return _context.Developers;
        }
    }
}
