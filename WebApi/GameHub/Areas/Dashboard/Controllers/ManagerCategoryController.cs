using GameHub.Areas.Dashboard.Models.Interfaces;
using GameHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Areas.Dashboard.Controllers
{
    // ManagerCategory
    // This controller is responsible for managing category
    // We have used ManagerCategory repository to perform the CRUD operation
    [Route("dashboard/api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ManagerCategoryController : ControllerBase
    {
        // All fields that are going to use through the application
        private readonly ManagerCategoryInterface _managerCategory;
        public static IWebHostEnvironment _webHostEnvironment;

        // Constructor
        public ManagerCategoryController(ManagerCategoryInterface managerCategory, IWebHostEnvironment webHostEnvironment)
        {
            _managerCategory = managerCategory;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: GetCategory
        // This action returns IEnumerable of Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetNews()
        {
            try
            {
                // Get all category and assign the result into a variables
                var result = await _managerCategory.GetAll();

                // If the result variable is null  then ruturn NotFound method
                if (result == null)
                {
                    return NotFound();
                }
                
                // Return list of result
                return result.ToList();
            }

            // If all the tyy block had any errors then executed catch block
            catch (Exception)
            {
                // Return Status code 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }

        // Get: GetCategory
        // This action is going to return a specific category based on their ids
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                // Get a specific category and assign the result into a variable
                var result = await _managerCategory.GetCategoryById(id);

                // If the result variable is null  then ruturn NotFound method
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }

            // If all the tyy block had any errors then executed catch block
            catch (Exception)
            {
                // Return Status code 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }

        // POST: CreateCategory
        // This action is going to add a category to the context
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            try
            {
                // If the category object is null it will be returned BadRequest()
                if(category==null)
                {
                    return BadRequest();
                }
                
                // Assing DateTime.Now tothe category object
                category.Date = DateTime.Now;

                // Create a new category
                var createSlider = await _managerCategory.AddCategory(category);

                // Return Ok
                return Ok();
            }
            // If all the tyy block had any errors then executed catch block
            catch (Exception)
            {
                // Return Status code 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }

        // PUT: Edit
        // This action is going to edit a category to the context
        [HttpPut]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<Category>> Edit(Category category)
        {
            try
            {
                // Edit a category and assign it to the Editcategory 
                var Editcategory = await _managerCategory.GetCategoryById(category.Id);

                // If the Editcategory is null then it will return the NotFound()
                if (Editcategory== null)
                {
                    return NotFound($"Category with Id={category.Id} not found");
                }

                // Edit a category with input parameteres
                await _managerCategory.EditCategory(category);

                // Return Ok
                return Ok();
            }
            // If all the tyy block had any errors then executed catch block
            catch (Exception)
            {
                // Return Status code 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }

        // Delete: DeleteCategory
        // This action is reponsible for deleting a category based on the id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            try
            {
                // Get a value from the context and assign to the Categorydelete
                var Cateogrydelete = await _managerCategory.GetCategoryById(id);

                // if Categorydelete is null then it will return NotFound()
                if (Cateogrydelete == null)
                {
                    return NotFound($"Category with id = {id} not found.");
                }

                // Delete a category
                return await _managerCategory.Delete(id);
            }
            // If all the tyy block had any errors then executed catch block
            catch (Exception)
            {
                // Return Status code 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }
    }
}
