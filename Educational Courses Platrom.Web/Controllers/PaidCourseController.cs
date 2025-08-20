using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.DataAccess.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Educational_Courses_Platform.Entities.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Educational_Courses_Platrom.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class PaidCourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaidCourseController(IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("CreatePaidCourse")]
        public IActionResult CreatePaidCourse(PaidCourseDto dto)
        {
            if (ModelState.IsValid)
            {
                var course = new PaidCourse
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price
                };

                _unitOfWork.PaidCourse.Add(course);
                _unitOfWork.Complete();

                return Ok("PaidCourse Added Successfully");
            }
            return BadRequest();
        }


       // [Authorize]
        [HttpGet]
        [Route("GetAllPaidCourses")]
        public IActionResult GetAllPaidCourses()
        {
            
            if (ModelState.IsValid)
            {
                List<PaidCourse> courses = _unitOfWork.PaidCourse.GetAll().ToList();
                return Ok(courses);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetAllPaidCoursesWithEpisodes")]
        public IActionResult GetAllPaidCoursesWithEpisodes()
        {

            if (ModelState.IsValid)
            {
                List<PaidCourse> paidCourses = _unitOfWork.PaidCourse.GetAll(includeword: "Episodes").ToList();
                List<PaidCourseDto> paidCourseDtos = paidCourses.Select(c => new PaidCourseDto
                {
                    Price= c.Price,
                    Name = c.Name,
                    Description = c.Description,
                    Episodes = c.Episodes.Select(e => new EpisodeDto
                    {

                        Name = e.Name,
                        Description = e.Description
                    }).ToList()
                }).ToList();
                return Ok(paidCourseDtos);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("RemovePaidCourseById")]
        public IActionResult RemovePaidCourse(int id)
        {
            if (ModelState.IsValid)
            {
                var course = _unitOfWork.PaidCourse.GetFirstOrDefault(c => c.Id == id);

                if (course == null)
                {
                    return NotFound("PaidCourse not found");
                }

                _unitOfWork.PaidCourse.Remove(course);
                _unitOfWork.Complete();

                return Ok("PaidCourse deleted successfully");
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("RemovePaidCourseByName")]
        public IActionResult RemovePaidCourseByName(string name)
        {
            if (ModelState.IsValid)
            {
                var course = _unitOfWork.PaidCourse.GetFirstOrDefault(c => c.Name == name);

                if (course == null)
                {
                    return NotFound("PaidCourse not found");
                }

                _unitOfWork.PaidCourse.Remove(course);
                _unitOfWork.Complete();

                return Ok("PaidCourse deleted successfully");
            } 
            return BadRequest();
        }


      [HttpPost]
        [Route("UpdatePaidCourse")]
        public IActionResult UpdatePaidCourse(PaidCourseDto dto,int Id)
        {
            if (ModelState.IsValid)
            {
                var course = _unitOfWork.PaidCourse.GetFirstOrDefault(c => c.Id == Id);

                if (course == null)
                {
                    return NotFound("PaidCourse not found");
                }

                course.Name = dto.Name;
                course.Description = dto.Description;
                course.Price = dto.Price;

                _unitOfWork.PaidCourse.Update(course);
                _unitOfWork.Complete();

                return Ok("PaidCourse updated successfully");

            }
            return BadRequest();
        }
    }
}
