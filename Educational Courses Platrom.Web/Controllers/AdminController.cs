using Educational_Courses_Platform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Courses_Platrom.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("AddCourseToUser")]
        public async Task<IActionResult> AddCourseToUser([FromQuery] string UserId, [FromQuery] int courseId)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(UserId))
            {
                return BadRequest("Admin ID is required.");
               
            }

            if (courseId <= 0)
            {
                return BadRequest("Valid course ID is required.");
            }

            var result = await _adminService.AddCourseToUserAsync(UserId, courseId);


            if (result.Contains("not found") || result.Contains("error") || result.Contains("cannot"))
            {
                return BadRequest(result);
            }

            return Ok(new { message = result, success = true });
        }



        [HttpGet("GetCoursesOfUser")]
        public async Task<IActionResult> GetCoursesOfUser([FromQuery] string UserId)
        {

            if (string.IsNullOrWhiteSpace(UserId))
            {
                return BadRequest("Admin ID is required.");
            }

            var result = await _adminService.GetCoursesOfUserAsync(UserId);
            return Ok(new { courses = result, success = true });



        }
    }
}