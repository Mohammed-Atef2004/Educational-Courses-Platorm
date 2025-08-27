using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Models.Repositories
{
    public interface IEnrollmentRequestsRepository:IGenericRepository<EnrollmentsRequest>
    {
        public void Update(EnrollmentsRequest enrollmentsRequest);
    }
}
