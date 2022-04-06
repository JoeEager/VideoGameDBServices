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
    [Route("api/Publishers")]
    [ResponseCache(Duration = 3600)]
    public class PublishersController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [EnableQuery]
        // GET: api/Publishers
        [HttpGet(Name ="GetPublishers")]
        public IActionResult GetPublishers()
        {
            try
            {
                ObjectResult results = new ObjectResult(_publisherRepository.GetAll())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
                Request.HttpContext.Response.Headers.Add("X-Total-Count", _publisherRepository.GetAll().Count().ToString());

                return results;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }

        // GET: api/Publishers/5
        [HttpGet("{id}",Name = "GetPublisher")]
        public async Task<IActionResult> GetPublishers([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Publishers publishers = await _publisherRepository.Find(id);

                if (publishers == null)
                {
                    return NotFound();
                }

                return Ok(publishers);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [Route("{publisherId}/games")]
        public async Task<IActionResult> GetPublisherGames([FromRoute] int publisherId)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var games = await _publisherRepository.GetGamesByPublisher(publisherId);

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

        private async Task<bool> PublishersExists(int id)
        {
            return await _publisherRepository.Exist(id);
        }
    }
}
