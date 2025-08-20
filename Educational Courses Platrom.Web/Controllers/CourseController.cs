using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.DataAccess.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Educational_Courses_Platform.Entities.Repositories;
using Microsoft.AspNetCore.Authorization;
using Educational_Courses_Platform.Services.Interfaces;

namespace Educational_Courses_Platrom.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork,ICourseService courseService) { 
            _unitOfWork = unitOfWork;
            _courseService = courseService;

        }
        [HttpPost]
        [Route("CreateCourse")]
        public IActionResult CreateCourse(CourseDto dto)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Name = dto.Name,
                    Description = dto.Description
                };

                _unitOfWork.Course.Add(course);
                _unitOfWork.Complete();

                return Ok("Course Added Successfully");
            }
            return BadRequest();
        }


        //[Authorize]
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

        [HttpDelete]
        [Route("RemoveCourseById")]
        public IActionResult RemoveCourse(int id)
        {
            if (ModelState.IsValid)
            {
                var course = _unitOfWork.Course.GetFirstOrDefault(c => c.Id == id);

                if (course == null)
                {
                    return NotFound("Course not found");
                }

                _unitOfWork.Course.Remove(course);
                _unitOfWork.Complete();

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
                var course = _unitOfWork.Course.GetFirstOrDefault(c => c.Name == name);

                if (course == null)
                {
                    return NotFound("Course not found");
                }

                _unitOfWork.Course.Remove(course);
                _unitOfWork.Complete();

                return Ok("Course deleted successfully");
            } 
            return BadRequest();
        }


      [HttpPost]
        [Route("UpdateCourse")]
        public IActionResult UpdateCourse(CourseDto dto,string name)
        {
            if (ModelState.IsValid)
            {
                var course = _unitOfWork.Course.GetFirstOrDefault(c => c.Name == name);

                if (course == null)
                {
                    return NotFound("Course not found");
                }

                course.Name = dto.Name;
                course.Description = dto.Description;

                _unitOfWork.Course.Update(course);
                _unitOfWork.Complete();

                return Ok("Course updated successfully");

            }
            return BadRequest();
        }




    }
}
