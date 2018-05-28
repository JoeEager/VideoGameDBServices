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
    [Route("api/Developers")]
    public class DevelopersController : Controller
    {
        private readonly IDeveloperRepository _developerRepostiory;

        public DevelopersController(IDeveloperRepository developerRepository)
        {
            _developerRepostiory = developerRepository;
        }

        // GET: api/Developers
        [HttpGet(Name ="GetDevelopers")]
        public IActionResult GetDevelopers()
        {
            try
            {
                ObjectResult results = new ObjectResult(_developerRepostiory.GetAll())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
                Request.HttpContext.Response.Headers.Add("X-Total-Count", _developerRepostiory.GetAll().Count().ToString());

                return results;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }

        // GET: api/Developers/5
        [HttpGet("{id}", Name ="GetDeveloper")]
        public async Task<IActionResult> GetDevelopers([FromRoute] int id)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Developers developers = await _developerRepostiory.Find(id);

                if (developers == null)
                {
                    return NotFound();
                }

                return Ok(developers);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }


        private async Task<bool> DevelopersExists(int id)
        {
            return await _developerRepostiory.Exist(id);
        }

    }
}