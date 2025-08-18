using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.DataAccess.Implementation;

namespace Educational_Courses_Platform.DataAccess.Implementation
{
    internal class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Course course)
        {
            var courseindb=_context.Courses.FirstOrDefault(x=>x.Id == course.Id);
            if (courseindb != null)
            {
                courseindb.Name = course.Name;
                courseindb.Description = course.Description;
            }
        }

    }
}
