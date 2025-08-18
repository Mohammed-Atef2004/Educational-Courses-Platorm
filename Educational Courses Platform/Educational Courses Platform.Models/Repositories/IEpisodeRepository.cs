using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;

namespace Educational_Courses_Platform.Entities.Repositories
{
    public interface IEpisodeRepository : IGenericRepository<Episode>
    {
        void Update(Episode episode);
    }
}
