using ManagerGameHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ManagerGameHub.Controllers
{
    // Slider Controller 
    // This controller is going to perform CRUD operation for Slider 
    public class SliderController : Controller
    {
        // object of HttpClient that is going to provide accessibilty to our api through the controller
        // Ajax is the another way that we can have access to the api 
        HttpClientHandler _clientHandler = new HttpClientHandler();

        // Constructor
        public SliderController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        }

        // Get: GetAllSliders action
        // This action retrieve all News that through the api
        [HttpGet]
        public IActionResult GetAllSliders()
        {
            // Create an IEnumerable object from Slider class 
            IEnumerable<Slider> slider = null;

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
                var responseTask = client.GetAsync("https://localhost:44352/Dashboard/api/ManagerSlider/");

                // Wait for the task to complete execution
                responseTask.Wait();
                var result = responseTask.Result;

                // If the result is successful the body of if clause will run
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Slider>>();
                    readTask.Wait();

                    slider = readTask.Result;
                }
                else
                {
                    // Web api sent error response and it will redirect to the Index action
                    return RedirectToAction("Index", "home");
                }
            }

            // Return view with null slider
            return View(slider);
        }

        // GET: GetSliderById action
        // Get method for display detail of a slider 
        [HttpGet]
        public async Task<Slider> GetSliderById(int id)
        {
            // create an object from Slider class
            var _slider = new Slider();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44352/Dashboard/api/ManagerSlider/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _slider = JsonConvert.DeserializeObject<Slider>(apiResponse);
                }
            }
            return _slider;
        }

        // GET: Add action
        // This action is responsible for the view of the add action
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: Add action
        // This action post a slider to the api to add it to the database
        [HttpPost]
        public IActionResult Add(Slider slider)
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

                    // Base address
                    client.BaseAddress = new Uri("https://localhost:44352/Dashboard/api/ManagerSlider/");

                    // If the slider image is not null the assign the file name of the image to slider image
                    if (slider.ImageFile != null)
                    {
                        slider.Image = slider.ImageFile.FileName;
                    }
                    else
                    {
                        // Show a message in viewbag to upload a file 
                        ViewBag.FileMessage = "Please upload file";

                        // Return a view and pass the slider object to the view
                        return View(slider);
                    }

                    //HTTP POST
                    using (var content = new MultipartFormDataContent())
                    {
                        // String Caption
                        content.Add(new StringContent(slider.Caption), "Caption");

                        // String About
                        content.Add(new StringContent(slider.About), "About");

                        // String Link
                        content.Add(new StringContent(slider.Link), "Link");

                        // StreamContent Image file
                        content.Add(new StreamContent(slider.ImageFile.OpenReadStream()), "ImageFile", slider.ImageFile.FileName);

                        // postTask send the whole content to the api
                        var postTask = client.PostAsync("https://localhost:44352/Dashboard/api/ManagerSlider/", content);

                        // Wait for the task to complete execution
                        postTask.Wait();
                        var result = postTask.Result;

                        // If the result is successful the body of if clause will run
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetAllSliders");
                        }
                    }
                }
            }

            // Display the error message
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            // Return view and pass the slider object to the view
            return View(slider);
        }

        // GET: Edit action
        // This action is responsible for edit a slider and display all values in news to the form control
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // create an object from Slider class
            Slider slider = null;

            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                
                //HTTP GET
                var responseTask = await client.GetAsync("https://localhost:44352/Dashboard/api/ManagerSlider/" + id);
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<Slider>();
                    readTask.Wait();

                    slider = readTask.Result;
                }
                else 
                {
                    return RedirectToAction("Index", "home");
                }
            }

            // Return a view and pass the slider object to the view
            return View(slider);
        }

        // POST: Edit action
        // Post method for editing a slider
        [HttpPost]
        public IActionResult Edit(Slider slider)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    // Get value that is stored in cookie and use it in a Token variable 
                    var tokenValue = HttpContext.Request.Cookies["jwt"];
                    var Token = tokenValue;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    // Base address
                    client.BaseAddress = new Uri("https://localhost:44352/Dashboard/api/ManagerSlider/");

                    //HTTP POST
                    using (var content = new MultipartFormDataContent())
                    {
                        // String Id
                        content.Add(new StringContent(slider.Id.ToString()), "id");

                        // String Captions
                        content.Add(new StringContent(slider.Caption), "Caption");

                        // String About
                        content.Add(new StringContent(slider.About), "About");

                        // String Link
                        content.Add(new StringContent(slider.Link), "Link");

                        // String Image
                        content.Add(new StringContent(slider.Image), "Image");

                        // If the image file is not null then StreamContent will be added
                        if (slider.ImageFile != null)
                        {
                            slider.Image = slider.ImageFile.FileName;
                            content.Add(new StreamContent(slider.ImageFile.OpenReadStream()), "ImageFile", slider.ImageFile.FileName);
                        }

                        // Put whole content to the api 
                        var postTask = client.PutAsync("https://localhost:44352/Dashboard/api/ManagerSlider/", content);

                        // Wait for the task to complete execution
                        postTask.Wait();
                        var result = postTask.Result;

                        // If the result is successful it will redirected to the GetAllSliders action
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetAllSliders");
                        }
                    }
                }
            }

            // Add error to the model 
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            // Return view and pass the slider object to it
            return View(slider);
        }

        // GET: Delete action
        // This action Delete a slider based on the id of it
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // create an object from Slider class
            Slider slider = null;
            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                // Get the specific Slider through the api
                //HTTP GET
                var responseTask = await client.GetAsync("https://localhost:44352/Dashboard/api/ManagerSlider/" + id);

                // If the responseTask is successful the if clause will be executed
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<Slider>();
                    readTask.Wait();
                    slider = readTask.Result;
                }
                else 
                {
                    // If the responseTask is not successful it will be redirected to the Index action
                    return RedirectToAction("Index", "home");
                }
            }

            // Return view and pass the slider object to it
            return View(slider);
        }

        // POST: Delete action
        // This action perform the delete operation through the api
        [HttpPost]
        public IActionResult Delete(Slider slider, int id)
        {
            using (var client = new HttpClient())
            {
                // Get value that is stored in cookie and use it in a Token variable 
                var tokenValue = HttpContext.Request.Cookies["jwt"];
                var Token = tokenValue;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                // Base address
                client.BaseAddress = new Uri("https://localhost:44352/Dashboard/api/ManagerSlider/");

                //HTTP POST
                // Pass the id to the api

                var postTask = client.DeleteAsync("https://localhost:44352/Dashboard/api/ManagerSlider/" + id);
                postTask.Wait();
                var result = postTask.Result;

                // If the result is successful the if clause will be executed and redirect to the GetAllSliders action
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllSliders");
                }
            }

            // Return view and pass the slider to it
            return View(slider);
        }
    }
}
