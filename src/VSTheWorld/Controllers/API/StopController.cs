using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VSTheWorld.Models;
using VSTheWorld.Services;
using VSTheWorld.ViewModels;

namespace VSTheWorld.Controllers.API
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private CoordService _coordService;
        private ILogger<StopController> _logger;
        private IWorldRepository _repository;

        public StopController(IWorldRepository repository, ILogger<StopController> logger, CoordService coordService)
        {
            _repository = repository;
            _logger = logger;
            _coordService = coordService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            // HTTP Verb Get

            // Get the tripName first
            var trip = _repository.GetTripByName(tripName);

            if (trip == null)
            {
                return Json(null);
            }
            else
            {
                return Json(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order)));
            }
        }

        [HttpPost("")]
        public async Task<JsonResult> Post(string tripName, [FromBody]StopViewModel vm)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    // Map to a Model
                    var newStop = Mapper.Map<Stop>(vm);

                    // Lookup up longitude and latitude
                    var coordResult = await _coordService.Lookup(newStop.Name);

                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(coordResult.Message);
                    }

                    newStop.Longitude = coordResult.Longitude;
                    newStop.Latitude = coordResult.Latitude;

                    // Save Model to the DB
                    _logger.LogInformation("Attempting to save a new Stop");
                    _repository.AddStop(tripName, newStop);

                    if (_repository.SaveAll())
                    {
                        // Return success to the caller
                        Response.StatusCode = (int)HttpStatusCode.Created;

                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new stop", ex);

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = false, ModelState = ModelState });

        }

    }
}
