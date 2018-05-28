﻿using System.Collections.Generic;
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
    [Route("api/Manufacturers")]
    public class ManufacturersController : Controller
    {

        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturersController(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        // GET: api/Manufacturers
        [HttpGet(Name ="GetManufacturers")]
        public IActionResult GetManufacturers()
        {
            try
            {
                ObjectResult results = new ObjectResult(_manufacturerRepository.GetAll())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
                Request.HttpContext.Response.Headers.Add("X-Total-Count", _manufacturerRepository.GetAll().Count().ToString());

                return results;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }

        // GET: api/Manufacturers/5
        [HttpGet("{id}", Name ="GetManufacturer")]
        public async Task<IActionResult> GetManufacturers([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Manufacturers manufacturers = await _manufacturerRepository.Find(id);

                if (manufacturers == null)
                {
                    return NotFound();
                }

                return Ok(manufacturers);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }


        private async Task<bool> ManufacturersExists(int id)
        {
            return await _manufacturerRepository.Exist(id);
        }
    }
}
