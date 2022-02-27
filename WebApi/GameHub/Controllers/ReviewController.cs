using GameHub.Models;
using GameHub.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Controllers
{
    // Category Controller
    // This controller is reponsible for the Review model
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ReviewController : ControllerBase
    {
        // The field that we need to use throughout the controller
        // This field return the Interface that is reponsible for CRUD operation
        private readonly ReviewInterface reviewInterface;
        public static IWebHostEnvironment _webHostEnvironment;

        // Constructor
        public ReviewController(ReviewInterface reviewInterface, IWebHostEnvironment webHostEnvironment)
        {
            this.reviewInterface = reviewInterface;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: GetReview
        // This action get all review that exist in the context
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReview()
        {
            try
            {
                var result = await reviewInterface.GetLast4Reviews();
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

        // GET: GetReview
        // This action return a specific review base on the id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            try
            {
                var result = await reviewInterface.GetReviewById(id);
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

        // GET: GetReviewByUserId
        // This action return a specific review base on the userId
        [HttpGet]
        [Route("[action]/{userId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewByUserId(string userId)
        {
            try
            {
                var result = await reviewInterface.GetReviewByUserId(userId);
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

        // GET: GetLast4Reviews
        // This action return last 4 reviews 
        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Review>>> GetLast4Reviews()
        {
            try
            {
                var result = await reviewInterface.GetLast4Reviews();
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

        // POST: CreateReview
        // This action Post a new review to the context
        [HttpPost]
        [DisableRequestSizeLimit]

        public async Task<ActionResult<Review>> CreateReview([FromForm] Review review)
        {
            try
            {
                // Create a path
                string path = _webHostEnvironment.WebRootPath + "\\ReviewImages\\";
                // assign the image to the file name
                string filename = Guid.NewGuid().ToString() + review.ImageFile.FileName;

                // If the file nam lenght is more than 0 then the if body will be executed
                if (review.ImageFile.Length > 0)
                {
                    // If the directory does not exist then the if block will be executed
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    // We use this code to resize a image that come from our IFormFile type with SixLabors library
                    // Use SixLabors with : Install-Package SixLabors.ImageSharp -IncludePrerelease

                    using var image = Image.Load(review.ImageFile.OpenReadStream());
                    image.Mutate(x => x.Resize(800, 600));

                    image.Save(path + filename);

                    // If you have another file which are not Image, for example you have a pdf or docx or another type of files
                    // You can use below code to store your file in wwwroot folder
                    //using (FileStream fileStream = System.IO.File.Create(path + review.ImageFile.FileName))
                    //{
                    //    review.ImageFile.CopyTo(fileStream);
                    //}

                }

                // Assign file to the review object
                review.Image = filename;

                // Assign DateTime.Now
                review.Date = DateTime.Now;

                // Create a new review
                var createReview = await reviewInterface.AddReview(review);

                // Return Ok
                return Ok("Success");
            }
            // If the try block has any error then the catch block will be executed
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }

        // DELETE: Delete action
        // This action delete a review based on the id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Review>> Delete(int id)
        {
            try
            {
                // Get a review 
                var reviewToDelete = await reviewInterface.GetReviewById(id);
                
                // Check the existance of review
                if (reviewToDelete == null)
                {
                    return NotFound($"Review with id = {id} not found.");
                }

                // implement a variable with path
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ReviewImages");

                // Assign last url
                string lastUrl = Path.Combine(uploadFolder, reviewToDelete.Image);

                // If the url exist then delete it will remove the file
                if (System.IO.File.Exists(lastUrl))
                {
                    System.IO.File.Delete(lastUrl);
                }
                
               // Detele 
                return await reviewInterface.Delete(id);
            }
            // If the try block has any error then the catch block will be executed
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }
    }
}
