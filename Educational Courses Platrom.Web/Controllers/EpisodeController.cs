using System.Threading.Tasks;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.Services.Implementation;
using Educational_Courses_Platform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Educational_Courses_Platrom.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;
        private readonly ILogger<EpisodeController> _logger;

        public EpisodeController(IUnitOfWork unitOfWork, IEpisodeService episodeService, ILogger<EpisodeController> logger)
        {
            _episodeService = episodeService;
            _logger = logger;
        }




        [HttpGet("GetEpisodesById")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetEpisodes(int id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }

            var episode = await _episodeService.GetEpisodeByIdAsync(id);

            if (episode == null)
            {
                _logger.LogWarning("Episode not found with id {EpisodeId}", id);
                return NotFound($"Episode with ID {id} not found.");
            }

            return Ok(episode);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("AddEpisode")]
        public async Task<IActionResult> AddEpisode(int courseId, [FromBody] EpisodeDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }

            var episode = await _episodeService.CreateEpisodeAsync(courseId, dto);

            if (episode == null)
            {
                _logger.LogWarning("Failed to add episode for courseId {CourseId}", courseId);
                return BadRequest("Failed to add episode.");
            }

            return CreatedAtAction(
                nameof(GetEpisodes),
                new { id = episode.Id },
                new
                {
                    Message = "Episode Added Successfully",
                    Episode = new
                    {
                        episode.Name,
                        episode.Description,
                        episode.CourseId
                    }
                });
        }




        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateEpisode")]
        public async Task<IActionResult> UpdateEpisode(int id, [FromBody] EpisodeDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }

            var updated = await _episodeService.UpdateEpisodeAsync(id, dto);

            if (!updated)
            {
                _logger.LogWarning("Episode not found for update with id {EpisodeId}", id);
                return NotFound($"Episode with ID {id} not found.");
            }

            return Ok(new
            {
                Message = "Episode Updated Successfully",
                EpisodeId = id
            });
        }




        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteEpisode")]
        public async Task<IActionResult> DeleteEpisode(int id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = errors
                });
            }

            var deleted = await _episodeService.RemoveEpisodeByIdAsync(id);

            if (!deleted)
            {
                _logger.LogWarning("Episode not found for deletion with id {EpisodeId}", id);
                return NotFound($"Episode with ID {id} not found.");
            }

            return Ok(new
            {
                Message = "Episode Deleted Successfully",
                EpisodeId = id
            });
        }

    }
}
