using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Interfaces;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Repositories
{
    public class GameRepository : IGameRepository
    {

        private videogamesContext _context;

        public GameRepository(videogamesContext context)
        {
            _context = context;
        }
        
        public async Task<bool> Exist(int id)
        {
            return await _context.Games.AnyAsync(e => e.Id == id);
        }

        public IEnumerable<Games> GetAll()
        {
            return _context.Games;
        }

        public async Task<Games> Find(int id)
        {
            return await _context.Games.SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
