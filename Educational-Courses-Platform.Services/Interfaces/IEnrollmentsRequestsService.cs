using Educational_Courses_Platform.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Services.Interfaces
{
    public interface IEnrollmentsRequestsService
    {
        public bool EnrollCourse(string userId, int courseId);
        Task<bool> ApproveEnrollment(string userId, int courseId);
        public bool RejectEnrollment(string userId, int courseId);
        public bool UpdateEnrollment(string userId, int oldCourseId,int newCourseId);
        public IEnumerable<EnrollmentsRequest> ViewAllEnrollmentRequests();
    }
}
