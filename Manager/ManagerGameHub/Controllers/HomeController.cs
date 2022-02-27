using ManagerGameHub.Models;
using ManagerGameHub.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ManagerGameHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClientHandler _clientHandler = new HttpClientHandler();
        public HomeController(ILogger<HomeController> logger)
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            ViewBag.ErrorMessage = "";
            using (var client = new HttpClient(_clientHandler))
            {
                // Check the content and serialized all the object to application/json that we want to send toward the api
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var postTask = client.PostAsync("https://localhost:44352/api/auth/login/", content);
                
                postTask.Wait();
                // Check the result and if it wasn't success don't redirect to any pages.
                var result = postTask.Result;
                if(!result.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "There are some problems. Please contact with Admin.";
                    return View(model);
                }
                var x = result.Headers.GetValues("set-cookie").ToList();
                var fulltoken = "";
                string token = null;
                foreach (var item in x)
                {
                    fulltoken = item.Split("jwt=")[1];
                    fulltoken = fulltoken.Split(";")[0];
                    token = fulltoken;
                }

                HttpContext.Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(1)

                });
                var JWT = client.GetAsync("https://localhost:44352/api/auth/users/");
                var jwtResult = JWT.Result;
                if (jwtResult.IsSuccessStatusCode)
                {
                    if (result.IsSuccessStatusCode)
                    {
                        HttpContext.Response.Cookies.Append("Username", model.Email, new CookieOptions
                        {
                            HttpOnly = true,
                            IsEssential = true,
                            Expires = DateTime.Now.AddDays(1)

                        });
                        return RedirectToAction("GetAllSliders", "Slider");
                    }
                }
                else
                {
                    return View(model);
                }

            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(model);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Logout()
        {


            HttpContext.Response.Cookies.Delete("jwt");
            HttpContext.Response.Cookies.Delete("Username");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
