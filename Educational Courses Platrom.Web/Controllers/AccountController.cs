using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Educational_Courses_Platform.DataAccess;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Models.Dto;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace Educational_Courses_Platform.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        [HttpPost("Register")] // api/account/Register
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {

            var existingUser = await userManager.FindByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new ResponseDto<object>(
                  success: false,
                  message: " Registration Failed",
                  errors: new[] { "Email is already registered." }
                ));

            }
            if (!ModelState.IsValid)
            {
               
                var validationErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new ResponseDto<object>(
                  success: false,
                  errors: validationErrors
                ));

            }
          

            var user = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };

            IdentityResult result = await userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                return Ok(new ResponseDto<object>(
                 success: true,
                  message: "Account added successfully"
                ));
            }

            var identityErrors = result.Errors.Select(e => e.Description);

            return BadRequest(new ResponseDto<object>(
              success: false,
              errors: identityErrors
            ));

        }


        [HttpPost("Login")] // api/account/Login
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                var validationErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new ResponseDto<object>(
                  success: false,
                  errors: validationErrors
                ));
            }

            var user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                return Unauthorized(new ResponseDto<object>(
                     success: false,
                     errors: new[] { "Email not found" }
                ));

            }

            if (!await userManager.CheckPasswordAsync(user, userDto.Password))
            {
                return Unauthorized(new ResponseDto<object>(

                    success: false,
                    errors: new[] { "Invalid password" }
                ));
            }

            // Create claims with null checks
            var claims = new List<Claim>
            {
             new Claim(ClaimTypes.NameIdentifier, user.Id ?? ""),
             new Claim(ClaimTypes.Name, user.UserName ?? ""),
             new Claim(ClaimTypes.Email, user.Email ?? ""),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).
             ToUnixTimeSeconds().
             ToString(), ClaimValueTypes.Integer64)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Signing credentials
            var signingCred = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:IssuerSigningKey"])),
                SecurityAlgorithms.HmacSha256
            );

            // Generate token with UTC time
            var tokenExpiry = DateTime.UtcNow.AddHours(1);
            var token = new JwtSecurityToken(
                issuer: config["JWT:ValidIssuer"],
                audience: config["JWT:ValidAudience"],
                claims: claims,
                expires: tokenExpiry,
                signingCredentials: signingCred
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            Response.Cookies.Append("accessToken", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, 
                SameSite = SameSiteMode.Strict,
                Expires = tokenExpiry
            });


            return Ok(new ResponseDto<object>(
                  success: true,
                  message: "Login successful",
                  data: new
                  {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = tokenExpiry,
                     user = new
                     {
                       Id = user.Id,
                       UserName = user.UserName,
                       Email = user.Email
                     }
                  }
            ));

        }

    }
}
