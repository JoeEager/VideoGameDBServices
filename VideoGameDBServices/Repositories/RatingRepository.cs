using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Interfaces;
using VideoGameDBServices.Models;


namespace VideoGameDBServices.Repositories
{
    public class RatingRepository : IRatingRepository
    {

        private readonly VideogamesContext _context;

        public RatingRepository(VideogamesContext context)
        {
            _context = context;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Ratings.AnyAsync(r => r.Id == id);
        }

        public async Task<Ratings> Find(int id)
        {
            return await _context.Ratings.SingleOrDefaultAsync(r=>r.Id==id);
        }

        public IEnumerable<Ratings> GetAll()
        {
            return _context.Ratings;
        }
    }
}
