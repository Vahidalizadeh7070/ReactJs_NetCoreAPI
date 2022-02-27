using ManagerGameHub.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ManagerGameHub.Controllers
{
    // Category Controller 
    // This controller is going to perform CRUD operation for Category 
    public class CategoryController : Controller
    {
        // object of HttpClient that is going to provide accessibilty to our api through the controller
        // Ajax is the another way that we can have access to the api 
        HttpClientHandler _clientHandler = new HttpClientHandler();

        // Constructor
        public CategoryController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        // Get: GetAllCategories action
        // This action retrieve all Categories that through the api
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            // Create an IEnumerable object from Category class 
            IEnumerable<Category> categories = null;

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

            // Return view with null category
            return View(categories);
        }

        // GET: Add action
        // This action is responsible for the view of the add action
        public IActionResult Add()
        {
            return View();
        }

        // POST: Add action
        // This action post a category to the api to add it to the database
        [HttpPost]
        public IActionResult Add(Category category)
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

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync("https://localhost:44352/Dashboard/api/ManagerCategory/", category);
                    
                    // Wait for the task to complete execution
                    postTask.Wait();
                    var result = postTask.Result;

                    // If the result is successful the body of if clause will run
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllCategories");
                    }
                }
            }

            // Display the error message
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            // Return view and pass the category object to the view
            return View(category);
        }

        // GET: Edit action
        // This action is responsible for edit a category and display all values in news to the form control
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // create an object from Category class
            Category category = null;

            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                
                //HTTP GET
                var responseTask = await client.GetAsync("https://localhost:44352/Dashboard/api/ManagerCategory/" + id);


                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<Category>();
                    readTask.Wait();

                    category = readTask.Result;
                }
                else 
                {
                    return RedirectToAction("Index", "home");
                }
            }

            // Return view and pass the category object to the view
            return View(category);
        }

        // POST: Edit action
        // Post method for editing a category
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            // If the category model is valid the if clause will be executed
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    // Get value that is stored in cookie and use it in a Token variable 
                    var tokenValue = HttpContext.Request.Cookies["jwt"];
                    var Token = tokenValue;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    // Put whole content to the api
                    var postTask = client.PutAsJsonAsync("https://localhost:44352/Dashboard/api/ManagerCategory/", category);

                    // Wait for the task to complete execution
                    postTask.Wait();
                    var result = postTask.Result;

                    // If the result is successful it will redirected to the GetAllCategories action
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllCategories");
                    }
                }
            }

            // If the category model is not valid then add the error to the ModelState 
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            // Return a view and pass the category object to it
            return View(category);
        }

        // GET: Delete action
        // This action Delete a category based on the id of it
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // create an object from Category class
            Category category = null;

            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;



                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                // Get the specific Category through the api
                //HTTP GET
                var responseTask = await client.GetAsync("https://localhost:44352/Dashboard/api/ManagerCategory/" + id);

                // If the responseTask is successful the if clause will be executed
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<Category>();
                    readTask.Wait();
                    category = readTask.Result;
                }
                else 
                {
                    // If the responseTask is not successful it will be redirected to the Index action
                    return RedirectToAction("Index", "home");
                }
            }

            // Return view and pass the category object to it
            return View(category);
        }

        // POST: Delete action
        // This action perform the delete operation through the api
        [HttpPost]
        public IActionResult Delete(Category category, int id)
        {
            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                //HTTP POST
                // Pass the id to the api
                var postTask = client.DeleteAsync("https://localhost:44352/Dashboard/api/ManagerCategory/" + id);
                postTask.Wait();
                var result = postTask.Result;

                // If the result is successful the if clause will be executed and redirect to the GetAllCategories action
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllCategories");
                }
            }

            // Return view and pass the category object to it
            return View(category);
        }
    }
}
