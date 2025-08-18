using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.DataAccess.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Educational_Courses_Platform.Entities.Repositories;

namespace Educational_Courses_Platrom.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("api/[controller]/CreateCourse")]
        public IActionResult CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Course.Add(course);
                _unitOfWork.Complete();
                return Ok("Course Added Successfully");
            }
            return BadRequest();
        }

    }
}
