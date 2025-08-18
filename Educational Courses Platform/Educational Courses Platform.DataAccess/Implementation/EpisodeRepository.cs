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
    internal class EpisodeRepository : GenericRepository<Episode>, IEpisodeRepository
    {
        private readonly ApplicationDbContext _context;
        public EpisodeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Episode episode)
        {
            var episodeindb=_context.Episodes.FirstOrDefault(x=>x.Id == episode.Id);
            if (episodeindb != null)
            {
                episodeindb.Name = episode.Name;
                episodeindb.Description = episode.Description;
                episodeindb.CourseId = episode.CourseId;
            }
        }

    }
}
