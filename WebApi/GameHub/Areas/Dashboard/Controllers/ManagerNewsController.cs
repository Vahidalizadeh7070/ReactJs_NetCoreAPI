using GameHub.Areas.Dashboard.Models.Interfaces;
using GameHub.Models;
using Microsoft.AspNetCore.Authorization;
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
    // ManagerNews
    // This controller is responsible for managing News
    // We have used ManagerCategory repository to perform the CRUD operation
    [Route("dashboard/api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ManagerNewsController : ControllerBase
    {
        // All fields that are going to use through the application
        private readonly ManagerNewsInterface managerNews;
        public static IWebHostEnvironment _webHostEnvironment;

        // Constructor
        public ManagerNewsController(ManagerNewsInterface managerNews, IWebHostEnvironment webHostEnvironment)
        {
            this.managerNews = managerNews;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: GetNews
        // This action returns IEnumerable of News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetNews()
        {
            try
            {
                // Assign all news to the result variable
                var result = await managerNews.GetAll();

                // If the result is null the it will return NotFound()
                if (result == null)
                {
                    return NotFound();
                }

                // Return a list of news
                return result.ToList();
            }
            // If all the tyy block had any errors then executed catch block
            catch (Exception)
            {
                // Return Status code 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }

        // GET: GetNews 
        // Get a specific news based on the id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<News>> GetNews(int id)
        {
            try
            {
                // Get a category and assign it to a variable
                var result = await managerNews.GetNewsById(id);

                // If the result is null then it will return the NotFound()
                if (result == null)
                {
                    return NotFound();
                }

                // Return a news
                return result;
            }
            // If all the tyy block had any errors then executed catch block
            catch (Exception)
            {
                // Return Status code 500
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving data from the database");
            }
        }

        // POST: CreateNews
        // This action is going to add a news 
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<News>> CreateNews([FromForm] News news)
        {
            try
            {
                // Implement a path
                string path = _webHostEnvironment.WebRootPath + "\\News\\";

                #region Assign Image
                
                // Image One
                // If the lenght of the file is more than 0 then the if block will be executed
                if (news.ImageFileOne.Length > 0)
                {
                    // If the directory does not exist then the if block will be executed
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fileStream = System.IO.File.Create(path + news.ImageFileOne.FileName))
                    {
                        news.ImageFileOne.CopyTo(fileStream);

                        // Clears buffers
                        fileStream.Flush();
                    }
                }

                // Assign image 1
                news.ImageOne = news.ImageFileOne.FileName;
                
                // Image Two
                if (news.ImageFileTwo.Length > 0)
                {
                    // If the directory does not exist then the if block will be executed
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + news.ImageFileTwo.FileName))
                    {
                        news.ImageFileTwo.CopyTo(fileStream);

                        // Clears buffers
                        fileStream.Flush();
                    }
                }

                // Assign image 2
                news.ImageTwo = news.ImageFileTwo.FileName;

                // Image Three
                if (news.ImageFileThree.Length > 0)
                {
                    // If the directory does not exist then the if block will be executed
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + news.ImageFileThree.FileName))
                    {
                        news.ImageFileThree.CopyTo(fileStream);

                        // Clears buffers
                        fileStream.Flush();
                    }
                }

                // Assign image 3
                news.ImageThree = news.ImageFileThree.FileName;
                #endregion

                // Assign DateTime.Now
                news.Date = DateTime.Now;
                
                // Assign CategoryId
                news.CategoryId = Convert.ToInt32(news.CategoryId);

                // Assign Trend
                news.Trend = Convert.ToBoolean(news.Trend);

                // Create news 
                var createNews= await managerNews.AddNews(news);

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
        // This action is going to edit a news
        [HttpPut]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<News>> Edit([FromForm] News news)
        {

            try
            {
                // Implement a path
                string path = _webHostEnvironment.WebRootPath + "\\News\\";


                // Image One
                // If the ImageFileOne is not null then the if block will be executed
                if (news.ImageFileOne != null)
                {
                    // If the directory does not exist then the if block will be executed
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // Set path 
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "News");

                    // Get the last path to prepare for delete file 
                    string lastUrl = Path.Combine(uploadFolder, news.ImageOne);

                    // Delete file
                    if (System.IO.File.Exists(lastUrl))
                    {
                        System.IO.File.Delete(lastUrl);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + news.ImageFileOne.FileName))
                    {
                        news.ImageFileOne.CopyTo(fileStream);

                        // Clears buffers
                        fileStream.Flush();
                    }

                    // Assign Id to the news object
                    news.Id = Convert.ToInt32(news.Id);

                    // Assign image 1
                    news.ImageOne = news.ImageFileOne.FileName;
                }
                else
                {
                    // Do not change the image 1
                    news.Id = Convert.ToInt32(news.Id);
                    news.ImageOne = news.ImageOne;
                }

                // Image Twho
                // If the ImageFileTwo is not null then the if block will be executed
                if (news.ImageFileTwo != null)
                {
                    // If the directory does not exist then the if block will be executed
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // Set path 
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "News");

                    // Get the last path to prepare for delete file 
                    string lastUrl = Path.Combine(uploadFolder, news.ImageTwo);

                    // Delete file
                    if (System.IO.File.Exists(lastUrl))
                    {
                        System.IO.File.Delete(lastUrl);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + news.ImageFileTwo.FileName))
                    {
                        news.ImageFileTwo.CopyTo(fileStream);

                        // Clears buffers
                        fileStream.Flush();
                    }

                    // Assign image 2
                    news.Id = Convert.ToInt32(news.Id);
                    news.ImageTwo = news.ImageFileTwo.FileName;
                }
                else
                {
                    // Do not change the image 2
                    news.Id = Convert.ToInt32(news.Id);
                    news.ImageTwo = news.ImageTwo;
                }

                // Image Three
                // If the ImageFileThree is not null then the if block will be executed
                if (news.ImageFileThree != null)
                {
                    // If the directory does not exist then the if block will be executed
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // Set path
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "News");

                    // Get the last path to prepare for delete file 
                    string lastUrl = Path.Combine(uploadFolder, news.ImageThree);

                    // Delete file
                    if (System.IO.File.Exists(lastUrl))
                    {
                        System.IO.File.Delete(lastUrl);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + news.ImageFileThree.FileName))
                    {
                        news.ImageFileThree.CopyTo(fileStream);

                        // Clears buffers
                        fileStream.Flush();
                    }

                    // Assign image 3
                    news.Id = Convert.ToInt32(news.Id);

                    // Do not change the image 3
                    news.ImageThree = news.ImageFileThree.FileName;
                }
                else
                {
                    news.Id = Convert.ToInt32(news.Id);

                    // Do not change the image 3
                    news.ImageThree = news.ImageThree;
                }

                // Assign DateTime.Now
                news.Date=DateTime.Now;

                // Assign CategoryId
                news.CategoryId = Convert.ToInt32(news.CategoryId);

                // Assign Trend
                news.Trend = Convert.ToBoolean(news.Trend);

                // Edit a news
                var EditSlider = await managerNews.EditNews(news);

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

        // Delete: DeleteNews
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<News>> DeleteNews(int id)
        {
            try
            {
                // Get a value by GetNewsById and assign it to the NewsToDelete variable
                var NewsToDelete = await managerNews.GetNewsById(id);

                // If the variable is null then it will return NotFound() method
                if (NewsToDelete== null)
                {
                    return NotFound($"News with id = {id} not found.");
                }

                // Set path
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "News");

                // Provide path for delete image 1-3
                string lastUrlOne = Path.Combine(uploadFolder, NewsToDelete.ImageOne);
                string lastUrlTwo = Path.Combine(uploadFolder, NewsToDelete.ImageTwo);
                string lastUrlThree = Path.Combine(uploadFolder, NewsToDelete.ImageThree);

                // Delete image 1-3
                if (System.IO.File.Exists(lastUrlOne))
                {
                    System.IO.File.Delete(lastUrlOne);
                }
                if (System.IO.File.Exists(lastUrlTwo))
                {
                    System.IO.File.Delete(lastUrlTwo);
                }
                if (System.IO.File.Exists(lastUrlThree))
                {
                    System.IO.File.Delete(lastUrlThree);
                }

                // Delete news from database
                return await managerNews.Delete(id);
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
