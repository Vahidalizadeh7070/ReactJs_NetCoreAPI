using GameHub.Helper;
using GameHub.Helper.SendEmail;
using GameHub.Models;
using GameHub.Models.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GameHub.Controllers
{
    // AuthController
    // This controller is responsible for Authentication and Authorization 
    // Register and login of a user perform in this controller
    // We use Asp.net Identity and combine it with JWT
    // JWT provides a token based on the user and then you can pass the token Authorize all users using the token
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Fields that are going to use throughout the controller
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWTService _JWTService;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;

        // Constructor
        public AuthController(IHostingEnvironment env, UserManager<ApplicationUser> userManager, JWTService jWTService, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _JWTService = jWTService;
            _env = env;
        }

        // Register action
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // Find any users 
            var userExist = await userManager.FindByEmailAsync(model.Email);

            // If the user exist in the context then return 500 Status code
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exist" });
            }

            // Create and object from ApplicationUser and assign all values that it has gained by the RegisterModel object (model)
            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };

            // Create a new user 
            var result = await userManager.CreateAsync(user, model.Password);

            // If the result variable is not success then it will return the Status code 500
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Registeration failed" });
            }

            // If there is not any role for this member, it will added a User role for that user 
            // All the information of roles is stored in the AspnetRole,AspnetUserRole ...
            if (!await userManager.IsInRoleAsync(user, "User"))
            {
                await userManager.AddToRoleAsync(user, "User");
            }

            // Generate a token and send it to the user email
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = string.Format("http://localhost:3000/ConfirmEmail/token?{0}&userId?{1}",
                HttpUtility.UrlEncode(token),
                HttpUtility.UrlEncode(user.Id)
            );

            // pass the email html file to the user email
            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "EmailHtml/AccountConfirmationHTML.cshtml");
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(file))
            {
                body = reader.ReadToEnd();
            }
            SendEmail sendEmail = new SendEmail();
            body = body.Replace("{UserName}", user.Email);
            body = body.Replace("{ConfirmationLink}", confirmationLink);
            bool IsSendEmail = sendEmail.EmailSend(user.Email, "Gamehub-Confirmation account", body, true);

            // If the email send successfully then it will return Ok with specific message
            if (IsSendEmail)
            {
                return Ok(new Response { Status = "Success", Message = "Confirmation link sended to " + user.Email });
            }
            // Otherwise it will return Status code 500
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Registeration failed" });

        }

        // Login action
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Find user by email
            var user = await userManager.FindByNameAsync(model.Email);

            // If the user is not null and the pass is correct then the if block will be executed
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                // If email is not confirmed yet, it will return the Status code 500
                if (user.EmailConfirmed == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Your Account is not confirmed yet." });
                }

                // Get user roles
                var userRoles = await userManager.GetRolesAsync(user);

                // Add user the claim
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };

                // Add role and user to the Claim type
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                
                // Generate a token
                var authSignKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Tokens:Key"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Audience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                    );
                var tokenCookie = new JwtSecurityTokenHandler().WriteToken(token);

                // Assign token to the cookie
                HttpContext.Response.Cookies.Append("jwt", tokenCookie, new CookieOptions
                {
                    HttpOnly = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(1)
                });

                // Return Ok 
                return Ok(new { token = tokenCookie });
            }

            // If user does not find then it will return Status code 500
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email or Password is wrong." });
        }

        // ConfirmEmail
        // This action is going to confirm the email of the user
        [Route("ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailTokenAndUserId model)
        {
            // If user Id or token is null then it will return Status code 500
            if (model.userId == null || model.token == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Something is going wrong." });
            }

            // Find a user based on the user id
            var user = await userManager.FindByIdAsync(model.userId);

            // If the user is null then return error 500 Status code
            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Something is going wrong." });
            }

            // Check the token that is comming from the email link and if the result variable is succeeeded then it will return Status code 200
            var result = await userManager.ConfirmEmailAsync(user, model.token);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Error", Message = "Your confirmation completed." });

            }

            // Otherwise return Status code 500
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Something is going wrong." });
        }

        // Users action
        // This action check the token and Authorize the user base on the token value
        [HttpGet(template: "users")]
        public async Task<IActionResult> Users()
        {
            try
            {
                // Get cookie 
                var jwt = HttpContext.Request.Cookies["jwt"];

                // If the cookie is null then return Unauthorized
                if (jwt == null)
                {
                    return Unauthorized();
                }

                // Verify the jwt token
                var token = _JWTService.Verify(jwt);

                // Find the email 
                string email = token.Claims.FirstOrDefault(c => c.Issuer == "User").Value;

                // Retrieve all information of a user and assign it to user variable
                var user = await userManager.FindByEmailAsync(email);

                // Return Ok and pass the user object 
                return Ok(user);
            }
            // If there is an error in the try block then the catch body will be executed
            catch (Exception)
            {
                // Return Unauthorized
                return Unauthorized();
            }
        }
    }
}
