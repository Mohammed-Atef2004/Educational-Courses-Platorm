using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Entities.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        ICourseRepository Course { get; }
       
        IAdminRepository Admin { get; }
        IEpisodeRepository Episode { get; }
        IEnrollmentRequestsRepository EnrollmnentsRequests { get; }
        int Complete();
    }
}
