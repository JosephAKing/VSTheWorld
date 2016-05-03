using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using VSTheWorld.Models;
using VSTheWorld.ViewModels;

namespace VSTheWorld.Controllers.API
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> _logger;
        private IWorldRepository _repository;

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            // HTTP Verb Get

            // Need to convert the Trip Model to a View Model
            var results = Mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllTripsWithStops());

            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vm)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    // Map to a Model
                    var newTrip = Mapper.Map<Trip>(vm);

                    // Save Model to the DB
                    _logger.LogInformation("Attempting to save a new Trip");
                    _repository.AddTrip(newTrip);

                    if (_repository.SaveAll())
                    {
                        // Return success to the caller
                        Response.StatusCode = (int)HttpStatusCode.Created;

                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new trip", ex);

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = false, ModelState = ModelState });

        }

    }
}
