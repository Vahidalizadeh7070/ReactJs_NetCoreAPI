using GameHub.Models;
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
    // News Controller
    // This controller is reponsible for the News model
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        // The field that we need to use throughout the controller
        // This field return the Interface that is reponsible for CRUD operation
        private readonly NewsInterface _NewsInterface;

        // Constructor
        public NewsController(NewsInterface newsInterface)
        {
            _NewsInterface = newsInterface;
        }

        // GET: GetNews
        // This action get all news that exist in the context
        [HttpGet]
        public async Task<ActionResult> GetNews()
        {
            try
            {
                return Ok(await _NewsInterface.ListOfNews());
            }
            // If the try block has any error then the catch block will be executed
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        // GET: GetNewsById
        // This action return a specific news base on the id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<News>> GetNewsById(int id)
        {
            try
            {
                var result = await _NewsInterface.GetNewsById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            // If the try block has any error then the catch block will be executed
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }

        // GET: GetNewsByCategoryId
        // This action return a a news base on the category Id
        [HttpGet]
        [Route("[action]/{categoryId}")]
        public async Task<ActionResult<IList<News>>> GetNewsByCategoryId(int categoryId)
        {
            try
            {
                var result = await _NewsInterface.GetNewsByCategoryId(categoryId);
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

        // GET: GetNewsByTrend
        // This action returns a news if the trend property is true
        [HttpGet]
        [Route("[action]/")]
        public async Task<ActionResult<IList<News>>> GetNewsByTrend()
        {
            try
            {
                var result = await _NewsInterface.GetNewsByTrend();
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
