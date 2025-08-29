using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educational_Courses_Platform.Entities.Models;

namespace Educational_Courses_Platform.Services.Interfaces
{
    public interface IEpisodeService
    {

        public Task<Episode> GetEpisodeByIdAsync(int id);
        public Task<Episode> CreateEpisodeAsync(int courseId, EpisodeDto Dto);
        public Task<bool> RemoveEpisodeByIdAsync(int id);
        public Task<bool> UpdateEpisodeAsync(int id, EpisodeDto courseDto);



    }
}
