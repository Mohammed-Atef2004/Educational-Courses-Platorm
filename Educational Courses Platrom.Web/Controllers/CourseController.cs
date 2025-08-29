using Educational_Courses_Platform.DataAccess.Data;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.Services.Implementation;
using Educational_Courses_Platform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Courses_Platrom.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(IUnitOfWork unitOfWork,ICourseService courseService) { 
           
            _courseService = courseService;

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("CreateCourse")]
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromForm] CourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            await _courseService.CreateCourseAsync(dto);
            return Ok("The course created successfully");

        }

        [HttpGet]
        [Authorize(Roles = "Admin,Student")]
        [Route("GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);
    
                return BadRequest(new { 
                    message = "Validation failed", 
                    errors = errors 
                });
            }

            var result = _courseService.GetAllCoursesAsync().Result;
            if (result == null || !result.Any())
            {
                return NotFound("No courses found");
            }

            return Ok(result);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Student")]
        [Route("GetAllFreeCourses")]
        public IActionResult GetAllFreeCourses()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }
            var result = _courseService.GetAllFreeCoursesAsync().Result;
            if (result == null || !result.Any())
            { 
                return NotFound("No free courses found");
            }
            return Ok(result);

        }

        [Authorize]
        [HttpGet]
        [Authorize(Roles = "Admin,Student")]
        [Route("GetAllPaidCourses")]
        public IActionResult GetAllPaidCourses()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }
            var result = _courseService.GetAllPaidCoursesAsync().Result;
            if (result == null || !result.Any())
            {
                return NotFound("No Paid courses found");
            }
            return Ok(result);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Student")]
        [Route("GetCourseById")]
        public IActionResult GetCourseById(int id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }

            var course = _courseService.GetCourseByIdAsync(id).Result;
            if (course == null)
            {
                return NotFound("Course not found");
            }
            return Ok(course);
        }
        

        [HttpGet]
        [Authorize(Roles = "Admin,Student")]
        [Route("GetAllCoursesWithEpisodes")]
        public IActionResult GetAllCoursesWithEpisodes()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }

            var result = _courseService.GetAllCoursesWithEpisodesAsync().Result;
            if (result == null || !result.Any())
            {
                return NotFound("No courses found");
            }

            return Ok(result);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("RemoveCourse")]
        public IActionResult RemoveCourse(int id)
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }
            var course = _courseService.RemoveCourseByIdAsync(id).Result;
            if (course == false)
            {
                return NotFound("Course not found");
            }
            return Ok(course);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("RemoveCourseByName")]
        public IActionResult RemoveCourseByName(string name)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }
            var course = _courseService.RemoveCourseByNameAsync(name).Result;
            if (course == false)
            {
                return NotFound("Course not found");
            }
            return Ok(course);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("UpdateCourse")]
        public IActionResult UpdateCourse(CourseWithIdDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }

            var course = _courseService.GetCourseByIdAsync(dto.Id)
                .ContinueWith(task => task.Result)
                .Result;

            if (course == null)
            {
                return NotFound("Course not found");
            }
            return Ok("Course updated successfully");

        }
        [HttpGet]
        [Authorize(Roles = "Admin,Student")]
        [Route("GetEpisodesByCourse")]
        public async Task<IActionResult> GetEpisodesByCourse(int courseId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }

            var episodes = await _courseService.GetAllEpisodeOfCourseAsync(courseId);

            if (!episodes.Any()||episodes==null)
                return NotFound("No episodes found for this course.");

            return Ok(episodes);
        }


    }
}
