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
    // Category Controller
    // This controller is reponsible for the category model
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // The field that we need to use throughout the controller
        // This field return the Interface that is reponsible for CRUD operation
        private readonly CategoryInterface _CateogryInterface;

        // Constructor
        public CategoryController(CategoryInterface categoryInterface)
        {
            _CateogryInterface = categoryInterface;
        }

        // GET: GetCategories
        // This action get all categories that exist in the context
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                return Ok(await _CateogryInterface.ListOfCategory());
            }
            // If the try block has any error then the catch block will be executed
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        // GET: GetCategory
        // This action return a specific category base on the id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var result = await _CateogryInterface.Category(id);
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
    }
}
