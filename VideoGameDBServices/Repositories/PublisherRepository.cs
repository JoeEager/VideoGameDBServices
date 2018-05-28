using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Interfaces;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private videogamesContext _context;

        public PublisherRepository(videogamesContext context)
        {
            _context = context;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Publishers.AnyAsync(p => p.Id == id);
        }

        public async Task<Publishers> Find(int id)
        {
            return await _context.Publishers.SingleOrDefaultAsync(p => p.Id == id);
        }

        public IEnumerable<Publishers> GetAll()
        {
            return _context.Publishers;
        }

        public async Task<List<Games>> GetGamesByPublisher(int publisherId)
        {
            var publisher = _context.Publishers.SingleOrDefault(g => g.Id == publisherId);
            return await _context.Games.Where(m => m.PublisherName == publisher.Name).ToListAsync();
        }
    }
}
