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
    [Route("api/Systems")]
    [ResponseCache(Duration = 3600)]
    public class SystemsController : Controller
    {
        private readonly ISystemRepository _systemRepository;

        public SystemsController(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        // GET: api/Systems
        [HttpGet(Name = "GetSystems")]
        public IActionResult GetSystems()
        {
            try
            {
                ObjectResult results = new ObjectResult(_systemRepository.GetAll())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
                Request.HttpContext.Response.Headers.Add("X-Total-Count", _systemRepository.GetAll().Count().ToString());

                return results;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }

        // GET: api/Systems/5
        [HttpGet("{id}", Name = "GetSystem")]
        public async Task<IActionResult> GetSystems([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Systems systems = await _systemRepository.Find(id);

                if (systems == null)
                {
                    return NotFound();
                }

                return Ok(systems);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [Route("{systemId}/games")]
        public async Task<IActionResult> GetSystemGames([FromRoute] int systemId)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var games = await _systemRepository.GetGamesBySystem(systemId);

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


        private async Task<bool> SystemsExists(int id)
        {
            return await _systemRepository.Exist(id);
        }
    }
}
