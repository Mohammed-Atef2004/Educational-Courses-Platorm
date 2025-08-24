using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.Services.Interfaces;

namespace Educational_Courses_Platform.Services.Implementation
{
    public class EpisodeService: IEpisodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EpisodeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Episode> CreateEpisodeAsync(int courseId,EpisodeDto Dto)
        {
            var episode = new Episode
            {
                Name = Dto.Name,
                Description = Dto.Description,
                CourseId = courseId
            };

            _unitOfWork.Episode.Add(episode);
            _unitOfWork.Complete();
            return await Task.FromResult(episode);
        }

       


        public async Task<Episode> GetEpisodeByIdAsync(int id)
        {
            return await Task.Run(() => _unitOfWork.Episode.GetFirstOrDefault(episode => episode.Id == id));
        }

        public async Task<bool> RemoveEpisodeByIdAsync(int id)
        {
            var episode = _unitOfWork.Episode.GetFirstOrDefault(e => e.Id == id);

            if (episode == null)
                return false;

            _unitOfWork.Episode.Remove(episode);
            await Task.Run(() => _unitOfWork.Complete());
            return true;
        }


        public Task<bool> UpdateEpisodeAsync(int id, EpisodeDto courseDto)
        {
            var episode = _unitOfWork.Episode.GetFirstOrDefault(episode => episode.Id == id);

            if (episode == null)
            {
                return null;
            }

            episode.Name = courseDto.Name;
            episode.Description = courseDto.Description;

            _unitOfWork.Episode.Update(episode);
            _unitOfWork.Complete();
            return Task.FromResult(true);
        }

       
    }
}
