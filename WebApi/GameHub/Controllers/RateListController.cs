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
    // RateList Controller
    // This controller is reponsible for the RateList model
    [Route("api/[controller]")]
    [ApiController]
    public class RateListController : ControllerBase
    {
        // The field that we need to use throughout the controller
        // This field return the Interface that is reponsible for CRUD operation
        private readonly RateListInterface _rateListInterface;

        // Constructor
        public RateListController(RateListInterface rateListInterface)
        {
            _rateListInterface = rateListInterface;
        }

        // GET: GetRateList
        // This action get all ratelist that exist in the context
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateList>>> GetRateList()
        {
            try
            {
                var result = await _rateListInterface.Get7RateList();
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
