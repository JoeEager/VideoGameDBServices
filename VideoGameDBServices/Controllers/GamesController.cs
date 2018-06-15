using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameDBServices.Models;
using System;
using System.Net;
using VideoGameDBServices.Interfaces;

namespace VideoGameDBServices.Controllers
{
    [Produces("application/json")]
    [Route("api/Games")]
    [ResponseCache(Duration = 3600)]
    public class GamesController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        
        // GET: api/Games
        [HttpGet(Name ="GetGames")]
        public IActionResult GetGames()
        {
            try
            {

                ObjectResult results = new ObjectResult(_gameRepository.GetAll())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
                Request.HttpContext.Response.Headers.Add("X-Total-Count", _gameRepository.GetAll().Count().ToString());

                return results;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

            
        }

        // GET: api/Games/5
        [HttpGet("{id}", Name ="GetGame")]
        public async Task<IActionResult> GetGames([FromRoute] int id)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Games games = await _gameRepository.Find(id);

                if (games == null)
                {
                    return NotFound();
                }

                return Ok(games);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }


        private async Task<bool> GamesExists(int id)
        {
            return await _gameRepository.Exist(id);
        }
    }
}