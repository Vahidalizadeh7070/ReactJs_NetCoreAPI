using GameHub.Models;
using GameHub.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Controllers
{
    // FutureReleaseGame Controller
    // This controller is reponsible for the FutureReleaseGame mode
    [Route("api/[controller]")]
    [ApiController]
    public class FutureReleaseGameController : ControllerBase
    {
        // The field that we need to use throughout the controller
        // This field return the Interface that is reponsible for CRUD operation
        private readonly FutureReleaseGameInterface _futureReleaseGameInterface;

        // Constructor
        public FutureReleaseGameController(FutureReleaseGameInterface futureReleaseGameInterface)
        {
            _futureReleaseGameInterface= futureReleaseGameInterface;
        }

        // GET: GetRateList
        // This action retrieve all rate list 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FutureReleaseGame>>> GetRateList()
        {
            try
            {
                var result = await _futureReleaseGameInterface.Get7FutureReleaseGames();
                if (result == null)
                {
                    return NotFound();
                }
                return result.ToList();
            }
            // If the try block has any error then the catch block will be executed
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }
    }
}
