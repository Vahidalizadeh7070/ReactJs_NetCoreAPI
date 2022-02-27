using GameHub.Areas.Dashboard.Models.Interfaces;
using GameHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Areas.Dashboard.Controllers
{
    // ManagerSlider
    // This controller is responsible for managing Sliders
    // We have used ManagerSlider repository to perform the CRUD operation
    [Route("dashboard/api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ManagerSliderController : ControllerBase
    {
        // All fields that are going to use through the application
        private readonly ManagerSliderInterface managerSlider;
        public static IWebHostEnvironment _webHostEnvironment;

        // Constructor
        public ManagerSliderController(ManagerSliderInterface managerSlider, IWebHostEnvironment webHostEnvironment)
        {
            this.managerSlider = managerSlider;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: GetSlider
        // This action returns IEnumerable of Slider
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slider>>> GetSlider()
        {
            try
            {
                // Assign all slider to the result variable
                var result = await managerSlider.GetAll();

                // If the result is null the it will return NotFound()
                if (result == null)
                {
                    return NotFound();
                }

                // Return a list of slider
                return result.ToList();
            }
            // If all the tyy block had any errors then executed catch block
            catch (Exception)
            {
                // Return Status code 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }

        // GET: GetSlider 
        // Get a specific slider based on the id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Slider>> GetSlider(int id)
        {
            try
            {
                // Get a category and assign it to a variable
                var result = await managerSlider.GetSliderById(id);

                // If the result is null then it will return the NotFound()
                if (result == null)
                {
                    return NotFound();
                }

                // Return a slider
                return result;
            }
            // If all the tyy block had any errors then executed catch block
            catch (Exception)
            {
                // Return Status code 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }

        // POST: CreateSlider
        // This action is going to add a slider 
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<Slider>> CreateSlider([FromForm] Slider slider)
        {
            try
            {
                // Implement a path
                string path = _webHostEnvironment.WebRootPath + "\\SliderImages\\";

                // Image
                // If the lenght of the file is more than 0 then the if block will be executed
                if (slider.ImageFile.Length > 0)
                {
                    // If the directory does not exist then the if block will be executed
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + slider.ImageFile.FileName))
                    {
                        slider.ImageFile.CopyTo(fileStream);

                        // Clears buffers
                        fileStream.Flush();
                    }
                }

                // Assign image 
                slider.Image = slider.ImageFile.FileName;

                // Create slider 
                var createSlider = await managerSlider.AddSlider(slider);

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

        // Put: Edit
        // This action is going to edit a slider
        [HttpPut]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<Slider>> Edit([FromForm] Slider slider, int id)
        {
            
            try
            {
                // Implement a path
                string path = _webHostEnvironment.WebRootPath + "\\SliderImages\\";

                // Image
                // If the ImageFile is not null then the if block will be executed
                if (slider.ImageFile!=null)
                {
                    // If the directory does not exist then the if block will be executed
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // Set path 
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "SliderImages");

                    // Get the last path to prepare for delete file 
                    string lastUrl = Path.Combine(uploadFolder, slider.Image);

                    // Delete file
                    if (System.IO.File.Exists(lastUrl))
                    {
                        System.IO.File.Delete(lastUrl);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + slider.ImageFile.FileName))
                    {
                        slider.ImageFile.CopyTo(fileStream);

                        // Clears buffers
                        fileStream.Flush();
                    }

                    // Assign Id and image to the slider object
                    slider.Id = Convert.ToInt32(slider.Id);
                    slider.Image = slider.ImageFile.FileName;
                }
                else
                {
                    slider.Id = Convert.ToInt32(slider.Id);

                    // Do not change the image 1
                    slider.Image = slider.Image;
                }

                // Edit a slider
                var EditSlider = await managerSlider.EditSlider(slider);

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

        // Delete: DeleteSlider
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Slider>> DeleteSlider(int id)
        {
            try
            {
                // Get a value by GetSliderById and assign it to the SliderToDelete variable
                var SliderToDelete = await managerSlider.GetSliderById(id);

                // If the variable is null then it will return NotFound() method
                if (SliderToDelete == null)
                {
                    return NotFound($"Slider with id = {id} not found.");
                }

                // Set path
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "SliderImages");

                // Provide path for delete image
                string lastUrl = Path.Combine(uploadFolder, SliderToDelete.Image);

                // Delete image
                if (System.IO.File.Exists(lastUrl))
                {
                    System.IO.File.Delete(lastUrl);
                }

                // Delete news from database
                return await managerSlider.Delete(id);
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
