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
        public EnrollmentsRequestsService(IUnitOfWork unitOfWork, ICourseService courseService)
        {
            _unitOfWork = unitOfWork;
            _courseService = courseService;
        }

        public  bool EnrollCourse(string userId, int courseId)
        {

            var user = _unitOfWork.Admin.GetFirstOrDefault(
                 u => u.Id == userId);

            var course =  _courseService.GetCourseByIdAsync(courseId);
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
        public bool ApproveEnrollment(string userId, int courseId) 
        {
            var user = _unitOfWork.Admin.GetFirstOrDefault(
                 u => u.Id == userId);

            var course =  _courseService.GetCourseByIdAsync(courseId);
            if (course != null)
            {
                user.EnrolledCourses.Add(course.Result);
                _unitOfWork.Complete();
                return true;
            }
            return false;
        }
        public bool RejectEnrollment(string userId, int courseId)
        {
            var user = _unitOfWork.Admin.GetFirstOrDefault(
                 u => u.Id == userId);

            var course = _courseService.GetCourseByIdAsync(courseId);
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
           return _unitOfWork.EnrollmnentsRequests.GetAll();
        }

    }
}
