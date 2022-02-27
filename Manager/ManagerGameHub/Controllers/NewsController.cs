using ManagerGameHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ManagerGameHub.Controllers
{
    // News Controller 
    // This controller is going to perform CRUD operation for News 
    public class NewsController : Controller
    {
        // object of HttpClient that is going to provide accessibilty to our api through the controller
        // Ajax is the another way that we can have access to the api 
        HttpClientHandler _clientHandler = new HttpClientHandler();

        // Constructor
        public NewsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        // Get: GetAllNews action
        // This action retrieve all News that through the api
        [HttpGet]
        public IActionResult GetAllNews()
        {
            // Create an IEnumerable object from News class 
            IEnumerable<News> news = null;

            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                //HTTP GET
                // responsoeTask is connected with following link
                // We can use the main section of below link "https://localhost:44352/Dashboard/api/" in the appsettings.json
                // Then inject IConfiguration and then use the key value
                var responseTask = client.GetAsync("https://localhost:44352/Dashboard/api/ManagerNews/");

                // Wait for the task to complete execution
                responseTask.Wait();
                var result = responseTask.Result;

                // If the result is successful the body of if clause will run
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<News>>();
                    readTask.Wait();
                    news = readTask.Result;
                }
                
                else
                {
                    // Web api sent error response and it will redirect to the Index action
                    return RedirectToAction("Index", "home");
                }
            }
            
            // Return view with null news
            return View(news);
        }

        // GET: Add action
        // This action is responsible for the view of the add action
        [HttpGet]
        public IActionResult Add()
        {
            // IEnumerable of Category class
            // It is going to use to store categories and demonstrate to admin to select a category from a dropdown
            IEnumerable<Category> categories = null;

            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                //HTTP GET
                var responseTask = client.GetAsync("https://localhost:44352/Dashboard/api/ManagerCategory/");

                // Wait for the task to complete execution
                responseTask.Wait();
                var result = responseTask.Result;

                // If the result is successful the body of if clause will run
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Category>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
                else
                {
                    // Web api sent error response and it will redirect to the Index action
                    return RedirectToAction("Index", "home");
                }
            }
            
            // use categories that we set all categories through the api in a viewbag
            ViewBag.CategoryId = new SelectList(categories,"Id", "CategoryName");

            // return empty view 
            return View();
        }

        // POST: Add action
        // This action post a news to the api to add it to the database
        [HttpPost]
        public IActionResult Add(News news)
        {
            // If the model is valid it will execute the if clause
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    // Get value that is stored in cookie and use it in a Token variable 
                    var tokenValue = HttpContext.Request.Cookies["jwt"];
                    var Token = tokenValue;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    // Check the first image which is null or not
                    // We can encapsulate these if clause 
                    if (news.ImageFileOne != null)
                    {
                        news.ImageOne = news.ImageFileOne.FileName;
                    }
                    else
                    {
                        ViewBag.FileMessageOne = "Please upload file one";
                        return View(news);
                    }

                    // Check the second image which is null or not
                    if (news.ImageFileTwo != null)
                    {
                        news.ImageTwo = news.ImageFileTwo.FileName;
                    }
                    else
                    {
                        ViewBag.FileMessageTwo = "Please upload file two";
                        return View(news);
                    }

                    // Check the third image which is null or not
                    if (news.ImageFileThree != null)
                    {
                        news.ImageThree = news.ImageFileThree.FileName;
                    }
                    else
                    {
                        ViewBag.FileMessageThree = "Please upload file three";
                        return View(news);
                    }

                    //HTTP POST
                    using (var content = new MultipartFormDataContent())
                    {
                        // String title
                        content.Add(new StringContent(news.Title), "Title");
                        
                        // String about
                        content.Add(new StringContent(news.About), "About");

                        // String description 1-3
                        content.Add(new StringContent(news.DescriptionOne), "DescriptionOne");
                        content.Add(new StringContent(news.DescriptionTwo), "DescriptionTwo");
                        content.Add(new StringContent(news.DescriptionThree), "DescriptionThree");

                        // StreamContent image 1-3
                        content.Add(new StreamContent(news.ImageFileOne.OpenReadStream()), "ImageFileOne", news.ImageFileOne.FileName);
                        content.Add(new StreamContent(news.ImageFileTwo.OpenReadStream()), "ImageFileTwo", news.ImageFileTwo.FileName);
                        content.Add(new StreamContent(news.ImageFileThree.OpenReadStream()), "ImageFileThree", news.ImageFileThree.FileName);

                        // String category 
                        content.Add(new StringContent(news.CategoryId.ToString()), "CategoryId");

                        // String source
                        content.Add(new StringContent(news.Source), "Source");

                        // String video url 
                        content.Add(new StringContent(news.VideoUrl), "VideoUrl");

                        // String trend
                        content.Add(new StringContent(news.Trend.ToString()), "Trend");

                        // postTask send the whole content to the api
                        var postTask = client.PostAsync("https://localhost:44352/Dashboard/api/ManagerNews/", content);

                        // Wait for the task to complete execution
                        postTask.Wait();
                        var result = postTask.Result;

                        // If the result is successful the body of if clause will run
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetAllNews","News");
                        }
                    }
                }
            }

            // Display the error message
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            // Assing all categories using Category() method and then set it to the viewbag
            IEnumerable<Category> categories = Category();
            ViewBag.CategoryId = new SelectList(categories, "Id", "CategoryName");
            return View(news);
        }

        // GET: Edit action
        // This action is responsible for edit a news and display all values in news to the form control
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // create an object from News class
            News news = null;

            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                
                //HTTP GET
                var responseTask = await client.GetAsync("https://localhost:44352/Dashboard/api/ManagerNews/" + id);
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<News>();
                    readTask.Wait();
                    news = readTask.Result;
                }
                else 
                {
                    return RedirectToAction("Index", "home");
                }
            }

            // Assign Categor to the IEnumerable object 
            IEnumerable<Category> categories = Category();

            // Set categories to the object
            ViewBag.CategoryId = new SelectList(categories, "Id", "CategoryName");

            // Return view and pass news object to the view
            return View(news);
        }

        // POST: Edit action
        // Post method for editing a news
        [HttpPost]
        public IActionResult Edit(News news)
        {
            // If the News model is valid then the If clause will be executed
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    // Get value that is stored in cookie and use it in a Token variable 
                    var tokenValue = HttpContext.Request.Cookies["jwt"];
                    var Token = tokenValue;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    // Base address
                    client.BaseAddress = new Uri("https://localhost:44352/Dashboard/api/ManagerNews/");

                    //HTTP POST
                    using (var content = new MultipartFormDataContent())
                    {
                        // String Id
                        content.Add(new StringContent(news.Id.ToString()), "Id");

                        // String Title
                        content.Add(new StringContent(news.Title), "Title");

                        // String About
                        content.Add(new StringContent(news.About), "About");

                        // String Description 1-3
                        content.Add(new StringContent(news.DescriptionOne), "DescriptionOne");
                        content.Add(new StringContent(news.DescriptionTwo), "DescriptionTwo");
                        content.Add(new StringContent(news.DescriptionThree), "DescriptionThree");

                        // String Category
                        content.Add(new StringContent(news.CategoryId.ToString()), "CategoryId");

                        // If source is not null it will be added 
                        if(news.Source!=null)
                        {
                            // String Source
                            content.Add(new StringContent(news.Source), "Source");
                        }

                        // If VideoUrl is not null it will be added
                        if (news.VideoUrl != null)
                        {
                            // String Video Url
                            content.Add(new StringContent(news.VideoUrl), "VideoUrl");//string
                        }
                        
                        // String Trend
                        content.Add(new StringContent(news.Trend.ToString()), "Trend");

                        // String Image 1-3
                        content.Add(new StringContent(news.ImageOne), "ImageOne");
                        content.Add(new StringContent(news.ImageTwo), "ImageTwo");
                        content.Add(new StringContent(news.ImageThree), "ImageThree");

                        // If image 1 is not null it will be added
                        // StreamContent Image 1-3
                        if (news.ImageFileOne != null)
                        {
                            news.ImageOne = news.ImageFileOne.FileName;
                            content.Add(new StreamContent(news.ImageFileOne.OpenReadStream()), "ImageFileOne", news.ImageFileOne.FileName);
                        }
                        if (news.ImageFileTwo != null)
                        {
                            news.ImageTwo = news.ImageFileTwo.FileName;
                            content.Add(new StreamContent(news.ImageFileTwo.OpenReadStream()), "ImageFileTwo", news.ImageFileTwo.FileName);
                        }
                        if (news.ImageFileThree != null)
                        {
                            news.ImageThree = news.ImageFileThree.FileName;
                            content.Add(new StreamContent(news.ImageFileThree.OpenReadStream()), "ImageFileThree", news.ImageFileThree.FileName);
                        }

                        // Put whole content to the api
                        var postTask = client.PutAsync("https://localhost:44352/Dashboard/api/ManagerNews/", content);

                        // Wait for the task to complete execution
                        postTask.Wait();
                        var result = postTask.Result;

                        // If the result is successful it will redirected to the GetAllNews action
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetAllNews");
                        }
                    }
                }
            }

            // Add error to the model 
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            // Assign all categories using Category method
            IEnumerable<Category> categories = Category();

            // Set categories to the viewbag
            ViewBag.CategoryId = new SelectList(categories, "Id", "CategoryName");

            // Return view and pass the news object to the view
            return View(news);
        }

        // GET: Details action
        // Get method for display detail of a news 
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // create an object from News class
            News news = null;
            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                
                // Get the specific News through the api
                //HTTP GET
                var responseTask = await client.GetAsync("https://localhost:44352/Dashboard/api/ManagerNews/" + id);

                // If it is successful the if clause will be added
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<News>();
                    readTask.Wait();
                    news = readTask.Result;
                }
                else 
                {
                    // If the responseTask is not successful it will be redirected to the Index action
                    return RedirectToAction("Index", "home");
                }
            }

            // Return view and pass the news object to it
            return View(news);
        }

        // GET: Delete action
        // This action Delete a news based on the id of it
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // create an object from News class
            News news = null;
            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                // Get the specific News through the api
                //HTTP GET
                var responseTask = await client.GetAsync("https://localhost:44352/Dashboard/api/ManagerNews/" + id);

                // If the responseTask is successful the if clause will be executed
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<News>();
                    readTask.Wait();
                    news = readTask.Result;
                }
                else 
                {
                    // If the responseTask is not successful it will be redirected to the Index action
                    return RedirectToAction("Index", "home");
                }
            }
            
            // Return view and pass the news object to the it
            return View(news);
        }

        // POST: Delete action
        // This action perform the delete operation through the api
        [HttpPost]
        public IActionResult Delete(News news, int id)
        {
            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                //HTTP POST
                // Pass the id to the api
                var postTask = client.DeleteAsync("https://localhost:44352/Dashboard/api/ManagerNews/" + id);
                postTask.Wait();
                var result = postTask.Result;

                // If the result is successful the if clause will be executed and redirect to the GetAllNews action
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllNews");
                }
            }

            // Return view and pass the news object to the it
            return View(news);
        }

        // Functiom for categories
        [NonAction]
        public IEnumerable<Category> Category()
        {
            // Returning all categories
            IEnumerable<Category> categories = null;

            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                
                //HTTP GET
                // Retrieve all categories throught the api
                var responseTask = client.GetAsync("https://localhost:44352/Dashboard/api/ManagerCategory/");
                responseTask.Wait();
                var result = responseTask.Result;

                // If the result is successful the if clause will be executed
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Category>>();
                    readTask.Wait();
                    return categories = readTask.Result;
                }
                
                // If the result is not successful null value will be returned
                return null;
            }
        }
    }
}
