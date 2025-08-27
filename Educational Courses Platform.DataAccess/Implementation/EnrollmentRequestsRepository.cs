using Educational_Courses_Platform.DataAccess.Data;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Models.Models;
using Educational_Courses_Platform.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.DataAccess.Implementation
{
    public class EnrollmentRequestsRepository:GenericRepository<EnrollmentsRequest>,IEnrollmentRequestsRepository
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentRequestsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(EnrollmentsRequest enrollmentsRequest)
        {
            var enrollmentsindb = _context.EnrollmentsRequests.FirstOrDefault(x => x.UserId == enrollmentsRequest.UserId);
            if (enrollmentsindb != null)
            {
                enrollmentsindb.UserId = enrollmentsRequest.UserId;
                enrollmentsindb.CourseId = enrollmentsRequest.CourseId;
                
            }
        }
    }
}
