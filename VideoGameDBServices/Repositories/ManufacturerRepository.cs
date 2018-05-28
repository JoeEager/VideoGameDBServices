using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Interfaces;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private videogamesContext _context;

        public ManufacturerRepository(videogamesContext context)
        {
            _context = context;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Manufacturers.AnyAsync(m => m.Id == id);
        }

        public async Task<Manufacturers> Find(int id)
        {
            return await _context.Manufacturers.SingleOrDefaultAsync(m => m.Id == id);
        }

        public IEnumerable<Manufacturers> GetAll()
        {
            return _context.Manufacturers;
        }
    }
}
