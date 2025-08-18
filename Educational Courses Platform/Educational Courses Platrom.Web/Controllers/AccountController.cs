using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Educational_Courses_Platform.DataAccess;

using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Dto;

namespace Educational_Courses_Platform.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //Resourse User
    public class AccountController : ControllerBase
    {

        //injection
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }


        [HttpPost("Register")]//api/account/Register 
        public async Task<IActionResult> Registration(RegisterUserDto UserDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = UserDto.UserName;
                user.Email = UserDto.Email;
                IdentityResult result=   await userManager.CreateAsync(user, UserDto.Password);
                if (result.Succeeded)
                {
                    return Ok("Accout Added");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return BadRequest(ModelState);
                }
                
            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]//api/acount/Login
        public async Task<IActionResult> Login(LoginUserDto UserDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user= await userManager.FindByEmailAsync(UserDto.Email);
                if (user != null)
                {
                    bool found =await userManager.CheckPasswordAsync(user, UserDto.Password);
                    if (found) 
                    {

                        
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var role in roles) 
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        SigningCredentials signingCred = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:IssuerSigningKey"])), SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],
                            audience: config["JWT:ValidAudience"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(3),
                            signingCredentials: signingCred
                        );
                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration= token.ValidTo});
                        


                    }
                    
                }
                return Unauthorized();
                
            }
            else 
            { 
                return Unauthorized();
            }

        }
    }
}
