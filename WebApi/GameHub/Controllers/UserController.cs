using GameHub.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameHub.Models.DTOS;
using GameHub.Models;
using Microsoft.AspNetCore.Cors;
using GameHub.Helper;
using Microsoft.AspNetCore.Identity;

namespace GameHub.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserInterface _repository;
        private readonly JWTService _JWTService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(UserInterface repository, JWTService jWTService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _repository = repository;
            _JWTService = jWTService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost(template: "register")]
        public IActionResult Register(RegisterDTO dto)
        {
            var user = new User
            {
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            return Created("Success", _repository.Create(user));
        }

        [HttpPost(template: "login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = _repository.GetUserByEmail(dto.Email);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            //var jwt = _JWTService.Generate(user.Id);
            //Response.Cookies.Append("jwt",jwt, new CookieOptions
            //{
            //    HttpOnly = true,
            //    IsEssential = true,
            //});
            return Ok(new { message = "Success" });
        }

        [HttpGet(template: "users")]
        public IActionResult Users()
        {
            
            try
            {
                var jwt = HttpContext.Request.Cookies["jwt"];
                var token = _JWTService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _repository.GetUserById(userId);
                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost(template: "logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Success" });
        }
    }
}
