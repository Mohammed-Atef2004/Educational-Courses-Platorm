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
        [Route("CreateCourse")]
        public IActionResult CreateCourse(CourseDto dto)
        {
            if (ModelState.IsValid)
            {
                
                _courseService.CreateCourseAsync(dto)
                    .ContinueWith(task => Ok(task.Result))
                    .Wait();

                return Ok("Course Added Successfully");
            }
            return BadRequest();
        }


      
        [HttpGet]
        [Route("GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            
            if (ModelState.IsValid)
            {
               
                return _courseService.GetAllCoursesAsync()
                    .ContinueWith(task => Ok(task.Result))
                    .Result;
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetAllFreeCourses")]
        public IActionResult GetAllFreeCourses()
        {

            if (ModelState.IsValid)
            {

                return _courseService.GetAllFreeCoursesAsync()
                    .ContinueWith(task => Ok(task.Result))
                    .Result;
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllPaidCourses")]
        public IActionResult GetAllPaidCourses()
        {

            if (ModelState.IsValid)
            {

                return _courseService.GetAllPaidCoursesAsync()
                    .ContinueWith(task => Ok(task.Result))
                    .Result;
            }
            return BadRequest();
        }

      
        [HttpGet]
        [Route("GetAllCoursesWithEpisodes")]
        public IActionResult GetAllCoursesWithEpisodes()
        {

            if (ModelState.IsValid)
            {

                return _courseService.GetAllCoursesWithEpisodesAsync()
                    .ContinueWith(task => Ok(task.Result))
                    .Result;
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("RemoveCourseById")]
        public IActionResult RemoveCourse(int id)
        {
            if (ModelState.IsValid)
            {
                var course = _courseService.RemoveCourseByIdAsync(id)
                    .ContinueWith(task => task.Result)
                    .Result;

                if (course == false)
                {
                    return NotFound("Course not found");
                }

               

                return Ok("Course deleted successfully");
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("RemoveCourseByName")]
        public IActionResult RemoveCourseByName(string name)
        {
            if (ModelState.IsValid)
            {
                var course = _courseService.RemoveCourseByNameAsync(name)
                    .ContinueWith(task => task.Result)
                    .Result;

                if (course == false)
                {
                    return NotFound("Course not found");
                }

                return Ok("Course deleted successfully");
            } 
            return BadRequest();
        }


      [HttpPost]
        [Route("UpdateCourse")]
        public IActionResult UpdateCourse(CourseWithIdDto dto)
        {
            if (ModelState.IsValid)
            {
                var course = _courseService.GetCourseByIdAsync(dto.Id)
                    .ContinueWith(task => task.Result)
                    .Result;

                if (course == null)
                {
                    return NotFound("Course not found");
                }


                return Ok("Course updated successfully");

            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetEpisodesByCourse")]
        public async Task<IActionResult> GetEpisodesByCourse(int courseId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var episodes = await _courseService.GetAllEpisodeOfCourseAsync(courseId);

            if (!episodes.Any())
                return NotFound("No episodes found for this course.");

            return Ok(episodes);
        }


    }
}
