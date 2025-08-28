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
using Educational_Courses_Platform.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;

namespace Educational_Courses_Platform.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly IEmailSender emailSender;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.config = config;
            this.emailSender = emailSender;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {
            var existingUser = await userManager.FindByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new ResponseDto<object>(
                    success: false,
                    message: "Registration Failed",
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
                Email = userDto.Email,
                EmailConfirmed = false

            };

            IdentityResult result = await userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Student");

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action(
                    action: "ConfirmEmail",
                    controller: "Account",
                    values: new { userId = user.Id, token = token },
                    protocol: HttpContext.Request.Scheme
                );


                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ConfirmEmail.cshtml");
                var templateContent = await System.IO.File.ReadAllTextAsync(templatePath);

                var emailBody = templateContent
                    .Replace("{{UserName}}", user.UserName)
                    .Replace("{{ConfirmUrl}}", confirmationLink);


                await emailSender.SendEmailAsync(user.Email, "Confirm your email", emailBody);

                return Ok(new ResponseDto<object>(
                    success: true,
                    message: "Account created successfully. Please check your email to confirm your account."
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

            if (!user.EmailConfirmed)
            {
                return Unauthorized(new ResponseDto<object>(
                    success: false,
                    errors: new[] { "Email is not confirmed. Please check your inbox." }
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



        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return BadRequest("Invalid confirmation request.");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            var result = await userManager.ConfirmEmailAsync(user, token);


            string templateFile = result.Succeeded ? "ConfirmEmailSuccess.cshtml" : "ConfirmEmailFailed.cshtml";
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", templateFile);

            if (!System.IO.File.Exists(templatePath))
            {

                return result.Succeeded
                    ? Ok("Email confirmed successfully. You can now login.")
                    : BadRequest("Email confirmation failed.");
            }

            var templateContent = await System.IO.File.ReadAllTextAsync(templatePath);


            var htmlContent = templateContent.Replace("{{UserName}}", user.UserName);

            return Content(htmlContent, "text/html");
        }


        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request." });

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                return Ok(new { message = "If an account with that email exists, a password reset link has been sent." });


            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // Generate reset URL pointing to HTML page in wwwroot
            var resetUrl = $"{Request.Scheme}://{Request.Host}/reset-password.html?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";


            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ForgotPasswordEmail.html");

            if (!System.IO.File.Exists(templatePath))
            {
                return StatusCode(500, "Email template not found.");
            }

            var templateContent = await System.IO.File.ReadAllTextAsync(templatePath);

            // Replace placeholders with actual values
            var emailBody = templateContent
                .Replace("{{UserName}}", user.UserName)
                .Replace("{{ResetUrl}}", resetUrl);

            // Send email
            await emailSender.SendEmailAsync(
                user.Email,
                "Reset Your Password",
                emailBody
            );

            return Ok(new { message = "If an account with that email exists, a password reset link has been sent." });
        }





        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound(new { success = false, message = "User not found" });

            if (model.NewPassword != model.ConfirmPassword)
                return BadRequest(new { success = false, message = "Passwords do not match" });

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
                return Ok(new { success = true, message = "Password reset successful!" });

            return BadRequest(new
            {
                success = false,
                message = "Password reset failed",
                errors = result.Errors.Select(e => e.Description)
            });
        }



    }
}