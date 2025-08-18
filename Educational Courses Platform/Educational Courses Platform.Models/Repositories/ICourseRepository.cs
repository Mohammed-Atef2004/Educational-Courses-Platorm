using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;

namespace Educational_Courses_Platform.Entities.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        public void Update(Course course);
    }
}
