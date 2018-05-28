using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Models;
using VideoGameDBServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VideoGameDBServices.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        private readonly videogamesContext _context;

        public SystemRepository(videogamesContext context)
        {
            _context = context;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Systems.AnyAsync(s => s.Id == id);
        }

        public async Task<Systems> Find(int id)
        {
            return await _context.Systems.SingleOrDefaultAsync(s => s.Id == id);
        }

        public IEnumerable<Systems> GetAll()
        {
            return _context.Systems;
        }

        public async Task<List<Games>> GetGamesBySystem(int systemId)
        {
            var system = _context.Systems.SingleOrDefault(g => g.Id == systemId);
            return await _context.Games.Where(m => m.SystemName == system.SystemName).ToListAsync();
        }
    }
}
