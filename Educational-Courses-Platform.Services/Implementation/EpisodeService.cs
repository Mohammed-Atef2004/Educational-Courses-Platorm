using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Educational_Courses_Platform.Services.Implementation
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EpisodeService> _logger;
        public EpisodeService(IUnitOfWork unitOfWork, ILogger<EpisodeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Episode> CreateEpisodeAsync(int courseId, EpisodeDto dto)
        {
            try
            {
                var episode = new Episode
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    CourseId = courseId,
                    ImageUrl = dto.ImageUrl,
                    Link = dto.Link
                };

                _unitOfWork.Episode.Add(episode);
                _unitOfWork.Complete();
                return await Task.FromResult(episode);
            }
            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "Episode DTO was null or contained invalid data for courseId {CourseId}", courseId);
                throw;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while creating episode for courseId {CourseId}", courseId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating episode for courseId {CourseId}", courseId);
                throw;
            }
        }





        public Task<Episode> GetEpisodeByIdAsync(int id)
        {
            try
            {

                var episode = _unitOfWork.Episode.GetFirstOrDefault(e => e.Id == id);

                if (episode == null)
                {
                    _logger.LogWarning("Episode not found with id {EpisodeId}", id);
                }

                return Task.FromResult(episode);
            }
            catch (ArgumentOutOfRangeException argEx)
            {
                _logger.LogError(argEx, "Invalid Episode ID provided: {EpisodeId}", id);
                throw;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while fetching episode with id {EpisodeId}", id);
                throw new InvalidOperationException("Database error occurred while fetching the episode", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching episode with id {EpisodeId}", id);
                throw;
            }
        }



        public async Task<bool> RemoveEpisodeByIdAsync(int id)
        {
            try
            {

                var episode = _unitOfWork.Episode.GetFirstOrDefault(e => e.Id == id);

                if (episode == null)
                {
                    _logger.LogWarning("Episode not found with id {EpisodeId}", id);
                    return false;
                }

                _unitOfWork.Episode.Remove(episode);
                _unitOfWork.Complete();

                return true;
            }
            catch (ArgumentOutOfRangeException argEx)
            {
                _logger.LogError(argEx, "Invalid Episode ID provided: {EpisodeId}", id);
                throw;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while removing episode with id {EpisodeId}", id);
                throw new InvalidOperationException("Database error occurred while removing the episode", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while removing episode with id {EpisodeId}", id);
                throw;
            }
        }



        public Task<bool> UpdateEpisodeAsync(int id, EpisodeDto courseDto)
        {
            try
            {

                var episode = _unitOfWork.Episode.GetFirstOrDefault(e => e.Id == id);
                if (episode == null)
                {
                    _logger.LogWarning("Episode not found with id {EpisodeId}", id);
                    return Task.FromResult(false);
                }

                episode.Name = courseDto.Name;
                episode.Description = courseDto.Description;
                episode.ImageUrl = courseDto.ImageUrl;
                episode.Link = courseDto.Link;

                _unitOfWork.Episode.Update(episode);
                _unitOfWork.Complete();

                return Task.FromResult(true);
            }
            catch (ArgumentOutOfRangeException argEx)
            {
                _logger.LogError(argEx, "Invalid Episode ID provided: {EpisodeId}", id);
                throw;
            }
            catch (ArgumentNullException nullEx)
            {
                _logger.LogError(nullEx, "Episode DTO is null for Episode ID {EpisodeId}", id);
                throw;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while updating episode with id {EpisodeId}", id);
                throw new InvalidOperationException("Database error occurred while updating the episode", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating episode with id {EpisodeId}", id);
                throw;
            }
        }



    }
}
