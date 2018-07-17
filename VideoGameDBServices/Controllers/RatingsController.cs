using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;
using VideoGameDBServices.Models;
using System;
using System.Net;
using VideoGameDBServices.Interfaces;

namespace VideoGameDBServices.Controllers
{
    [Produces("application/json")]
    [Route("api/Ratings")]
    [ResponseCache(Duration = 3600)]
    public class RatingsController : Controller
    {

        private readonly IRatingRepository _ratingRepository;

        public RatingsController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        [EnableQuery]
        // GET: api/Ratings
        [HttpGet(Name ="GetRatings")]
        public IActionResult GetRatings()
        {
            try
            {
                ObjectResult results = new ObjectResult(_ratingRepository.GetAll())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
                Request.HttpContext.Response.Headers.Add("X-Total-Count", _ratingRepository.GetAll().Count().ToString());

                return results;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }

        // GET: api/Ratings/5
        [HttpGet("{id}", Name = "GetRating")]
        public async Task<IActionResult> GetRatings([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Ratings ratings = await _ratingRepository.Find(id);

                if (ratings == null)
                {
                    return NotFound();
                }

                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }


        private async Task<bool> RatingsExists(int id)
        {
            return await _ratingRepository.Exist(id);
        }
    }
}