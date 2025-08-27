using Educational_Courses_Platform.Services.Interfaces;
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
        [Route("EnrollCourse")]
        public IActionResult EnrollCourse(string userId,int  courseId)
        {
           bool check= _enrollmentsRequestsService.EnrollCourse(userId, courseId);
            if(check==true)
            {
                return Ok("Enrollment Done Successfully");
            }
            return BadRequest();
            
        }
        [HttpPost]
        [Route("ApproveEnrollment")]
        public IActionResult ApproveEnrollment(string userId, int courseId)
        {
            bool check = _enrollmentsRequestsService.ApproveEnrollment(userId, courseId);
            if (check == true)
            {
                return Ok("Enrollment Approved Successfully");
            }
            return BadRequest();

        }
        [HttpPost]
        [Route("RejectEnrollment")]
        public IActionResult RejectEnrollment(string userId, int courseId)
        {
            bool check = _enrollmentsRequestsService.RejectEnrollment(userId, courseId);
            if (check == true)
            {
                return Ok("Enrollment Rejected Successfully");
            }
            return BadRequest();

        }
        [HttpPost]
        [Route("UpdateEnrollment")]
        public IActionResult UpdateEnrollment(string userId, int courseId,int newCourseId)
        {
            bool check = _enrollmentsRequestsService.UpdateEnrollment(userId, courseId,newCourseId);
            if (check == true)
            {
                return Ok("Enrollment Updated Successfully");
            }
            return BadRequest();

        }
        [HttpGet]
        [Route("ViewAllEnrollments")]
        public IActionResult ViewAllEnrollments()
        {
            var result=_enrollmentsRequestsService.ViewAllEnrollmentRequests();
            if (result!=null)
            return Ok();
            return BadRequest();
        }
    }
}
