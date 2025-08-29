using Educational_Courses_Platform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Courses_Platrom.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentsRequestsService _enrollmentsRequestsService;

        public EnrollmentController (IEnrollmentsRequestsService enrollmentsRequestsService)
        {
           _enrollmentsRequestsService = enrollmentsRequestsService;
        }
        [HttpPost]
        [Authorize(Roles = "Student")]
        [Route("EnrollCourse")]
        public IActionResult EnrollCourse(string userId,int  courseId)
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
            bool check= _enrollmentsRequestsService.EnrollCourse(userId, courseId);
            if(check==false)
            {
                return BadRequest("Enrollment Failed");
            }
            
               return CreatedAtAction("Enrollment Done Successfully",check);
         
            
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("ApproveEnrollment")]
        public async Task<IActionResult> ApproveEnrollment(string userId, int courseId)
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
            bool check = await _enrollmentsRequestsService.ApproveEnrollment(userId, courseId);

            if (check)
                return Ok("Enrollment Approved Successfully");

            return NotFound("Failed to approve enrollment,Enrollment not found");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("RejectEnrollment")]
        public IActionResult RejectEnrollment(string userId, int courseId)
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
            bool check = _enrollmentsRequestsService.RejectEnrollment(userId, courseId);
            if (check == true)
            {
                return Ok("Enrollment Rejected Successfully");
            }
            return NotFound("Can't Found this enrollment ");

        }
        [HttpPost]
        [Authorize(Roles = "Student")]
        [Route("UpdateEnrollment")]
        public IActionResult UpdateEnrollment(string userId, int courseId,int newCourseId)
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
            bool check = _enrollmentsRequestsService.UpdateEnrollment(userId, courseId,newCourseId);
            if (check == true)
            {
                return Ok("Enrollment Updated Successfully");
            }
            return BadRequest("Can't Update The Enrollment");

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ViewAllEnrollments")]
        public IActionResult ViewAllEnrollments()
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
            var result=_enrollmentsRequestsService.ViewAllEnrollmentRequests();
            if (result==null)
            {
                return NotFound("No Enrollment Requests Found");
            }
            return Ok(result);
            
        }
    }
}
