using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using System.Net;
using VideoGameDBServices.Interfaces;


namespace VideoGameDBServices.OdataControllers
{
    [Produces("application/json")]
    [Route("odata/SystemsData")]
    public class SystemsDataController : ODataController
    {
        private readonly ISystemRepository _systemRepository;

        public SystemsDataController(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        // GET: odata/SystemsData
        [EnableQuery]
        public IActionResult GetSystems()
        {
            try
            {
                ObjectResult results = new ObjectResult(_systemRepository.GetAll())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return results;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }
    }
}