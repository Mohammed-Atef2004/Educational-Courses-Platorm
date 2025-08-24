using Educational_Courses_Platform.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Courses_Platrom.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("AddCourseToAdmin")]
        public async Task<IActionResult> AddCourseToAdmin([FromQuery] string adminId, [FromQuery] int courseId)
        {

            if (string.IsNullOrWhiteSpace(adminId))
            {
                return BadRequest("Admin ID is required.");
            }

            if (courseId <= 0)
            {
                return BadRequest("Valid course ID is required.");
            }

            var result = await _adminService.AddCourseToUserAsync(adminId, courseId);


            if (result.Contains("not found") || result.Contains("error") || result.Contains("cannot"))
            {
                return BadRequest(result);
            }

            return Ok(new { message = result, success = true });
        }



        [HttpGet("GetCoursesOfAdmin")]
        public async Task<IActionResult> GetCoursesOfAdmin([FromQuery] string adminId)
        {

            if (string.IsNullOrWhiteSpace(adminId))
            {
                return BadRequest("Admin ID is required.");
            }

            var result = await _adminService.GetCoursesOfUserAsync(adminId);
            return Ok(new { courses = result, success = true });



        }
    }
}