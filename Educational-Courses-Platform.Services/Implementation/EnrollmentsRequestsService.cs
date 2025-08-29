using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.Models.Models;
using Educational_Courses_Platform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Services.Implementation
{
    public class EnrollmentsRequestsService : IEnrollmentsRequestsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseService _courseService;
        private readonly IEmailSender emailSender;
        public EnrollmentsRequestsService(IUnitOfWork unitOfWork, ICourseService courseService, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _courseService = courseService;
            this.emailSender = emailSender;
        }

        public  bool EnrollCourse(string userId, int courseId)
        {

            var user = _unitOfWork.Admin.GetFirstOrDefault(
                 u => u.Id == userId);
            if(user == null)
                return false;
            var course =  _courseService.GetCourseByIdAsync(courseId);
            if (course == null)
                return false;
            EnrollmentsRequest enrollmentsRequest = new EnrollmentsRequest() {
                CourseId = courseId ,
                 UserId = userId ,
            };
            if (enrollmentsRequest != null)
            {
                _unitOfWork.EnrollmnentsRequests.Add(enrollmentsRequest);
                _unitOfWork.Complete();
                return true;
            }
            return false;
            
        }
        public async Task<bool> ApproveEnrollment(string userId, int courseId)
        {

            var user = _unitOfWork.Admin.GetFirstOrDefault(u => u.Id == userId);
            if (user == null)
                return false;


            var course = await _courseService.GetCourseByIdAsync(courseId);
            if (course == null)
                return false;

            if (user.EnrolledCourses == null)
                user.EnrolledCourses = new List<Course>();


            user.EnrolledCourses.Add(course);
            var request = _unitOfWork.EnrollmnentsRequests.GetFirstOrDefault(r => r.UserId == userId && r.CourseId == courseId);
            if (request != null)
                _unitOfWork.EnrollmnentsRequests.Remove(request);

          
           _unitOfWork.Complete();

            user.EmailConfirmed = true;
            if (!string.IsNullOrEmpty(user.Email))
            {
                var subject = "Your enrollment has been approved";
                var body = $"Hello {user.UserName},<br/>Your enrollment in the course <b>{course.Name}</b> has been successfully approved.";

                await emailSender.SendEmailAsync(user.Email, subject, body);
            }

            return true;
        }

        public bool RejectEnrollment(string userId, int courseId)
        {
            var user = _unitOfWork.Admin.GetFirstOrDefault(
                 u => u.Id == userId);
            if (user == null)
                return false;
            var course = _courseService.GetCourseByIdAsync(courseId);
            if (course == null)
                return false;
            EnrollmentsRequest enrollmentsRequest = new EnrollmentsRequest()
            {
                CourseId = courseId,
                UserId = userId
            };
            if (enrollmentsRequest != null)
            {
                _unitOfWork.EnrollmnentsRequests.Remove(enrollmentsRequest);
                _unitOfWork.Complete();
                return true;
            }
            return false;
        }
        public bool UpdateEnrollment(string userId, int oldCourseId, int newCourseId)
        {
            RejectEnrollment(userId, oldCourseId);
            var newEnrollmnet=EnrollCourse(userId, newCourseId);
            if (!newEnrollmnet) {
                return false;
            }
            return true;
        }
        public IEnumerable<EnrollmentsRequest> ViewAllEnrollmentRequests()
        {
            var result= _unitOfWork.EnrollmnentsRequests.GetAll().ToList();
            if(result == null||!result.Any())
                return null;
            return result;
        }


    }
}
