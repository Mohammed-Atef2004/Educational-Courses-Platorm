using Educational_Courses_Platform.Entities.Repositories;
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
        IPaidCourseRepository PaidCourse { get; }
        IEpisodeRepository Episode { get; }
        int Complete();
    }
}
