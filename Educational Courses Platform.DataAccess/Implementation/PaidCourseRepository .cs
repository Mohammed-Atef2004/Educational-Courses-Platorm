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
    public class PaidCourseRepository : GenericRepository<PaidCourse>, IPaidCourseRepository
    {
        private readonly ApplicationDbContext _context;
        public PaidCourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(PaidCourse paidCourse)
        {
            var paidcourseindb=_context.PaidCourses.FirstOrDefault(x=>x.Id == paidCourse.Id);
            if (paidcourseindb != null)
            {
                paidcourseindb.Name = paidCourse.Name;
                paidcourseindb.Description = paidCourse.Description;
            }
        }

    }
}
