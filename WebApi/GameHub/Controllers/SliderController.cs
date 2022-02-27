using GameHub.Models.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Controllers
{
    // Slider Controller
    // This controller is reponsible for the Slider model
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        // The field that we need to use throughout the controller
        // This field return the Interface that is reponsible for CRUD operation
        private readonly SliderInterface _SliderInterface;

        // Constructor
        public SliderController(SliderInterface SliderInterface)
        {
            _SliderInterface = SliderInterface;   
        }

        // GET: GetSlider
        // This action get all slider that exist in the context
        [HttpGet]
        public async Task<ActionResult> GetSlider()
        {
            try
            {
                return Ok(await _SliderInterface.ListOfSlider());
            }
            // If the try block has any error then the catch block will be executed
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
