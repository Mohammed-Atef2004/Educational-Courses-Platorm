using Educational_Courses_Platform.DataAccess.Data;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.DataAccess.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICourseRepository Course { get; private set; }
     
        public IEpisodeRepository Episode { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context= context;
            Course=new CourseRepository(context);
           
            Episode=new EpisodeRepository(context);

        }

        public int Complete()
        {
            return _context.SaveChanges();

        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
