using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using VideoGameDBServices.Models;
using System;
using System.Net;
using VideoGameDBServices.Interfaces;

namespace VideoGameDBServices.Controllers
{
    [Produces("application/json")]
    [Route("api/Years")]
    [ResponseCache(Duration = 3600)]
    public class YearsController : Microsoft.AspNetCore.Mvc.Controller
    {
        
        private readonly IYearRepository _yearRepository;

        public YearsController(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        [EnableQuery]
        // GET: api/Years
        [HttpGet(Name = "GetYears")]
        public IActionResult GetYears()
        {
            try
            {
                ObjectResult results = new ObjectResult(_yearRepository.GetAll())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
                Request.HttpContext.Response.Headers.Add("X-Total-Count", _yearRepository.GetAll().Count().ToString());

                return results;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }

        // GET: api/Years/5
        [HttpGet("{id}", Name = "GetYear")]
        public async Task<IActionResult> GetYears([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Years years = await _yearRepository.Find(id);

                if (years == null)
                {
                    return NotFound();
                }

                return Ok(years);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [Route("{yearId}/games")]
        public async Task<IActionResult> GetYearGames([FromRoute] int yearId)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //if (! await YearsExists(yearId))
                //{
                //    return NotFound();
                //}


                var games = await _yearRepository.GetGamesByYear(yearId);

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

       
        private async Task<bool> YearsExists(int id)
        {
            return await _yearRepository.Exist(id);
        }

        
    }
}