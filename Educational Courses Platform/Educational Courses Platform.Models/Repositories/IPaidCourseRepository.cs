using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;

namespace Educational_Courses_Platform.Entities.Repositories
{
    public interface IPaidCourseRepository : IGenericRepository<PaidCourse>
    {
       public void Update(PaidCourse paidCourse);
    }
}
