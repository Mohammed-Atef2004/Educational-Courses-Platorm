using System.Threading.Tasks;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.Services.Implementation;
using Educational_Courses_Platform.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Educational_Courses_Platrom.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;
        private readonly IUnitOfWork _unitOfWork;

       
        public EpisodeController(IUnitOfWork unitOfWork, IEpisodeService episodeService)
        {
            _unitOfWork = unitOfWork;
            _episodeService = episodeService;
        }

        [HttpGet("GetEpisodesById")]
        public async Task<IActionResult> GetEpisodes(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var episode = await _episodeService.GetEpisodeByIdAsync(id);

            if (episode == null)
                return NotFound("Not found");

            return Ok(episode);
        }

        [HttpGet("GetByCourse")]
        public async Task<IActionResult> GetEpisodesByCourse(int courseId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var episodes = await _episodeService.GetAllEpisodeOfCourseAsync(courseId);

            if (!episodes.Any())
                return NotFound("No episodes found for this course.");

            return Ok(episodes);
        }

        [HttpPost("AddEpisode")]
        public async Task<IActionResult> AddEpisode(int courseId, [FromBody] EpisodeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var episode = await _episodeService.CreateEpisodeAsync(courseId, dto);

            if (episode == null)
                return BadRequest("Failed to add episode.");

            return Ok(new
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

        [HttpPost("UpdateEpisode")]
        public async Task<IActionResult> UpdateEpisode(int id, [FromBody] EpisodeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _episodeService.UpdateEpisodeAsync(id, dto);

            if (!updated)
                return NotFound("Episode not found");

            return Ok("Episode Updated Successfully");
        }

        [HttpDelete("DeleteEpisode")]
        public async Task<IActionResult> DeleteEpisode(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deleted = await _episodeService.RemoveEpisodeByIdAsync(id);

            if (!deleted)
                return NotFound("Episode not found");

            return Ok("Episode Deleted Successfully");
        }


       /* [HttpPost("AddEpisodeToPaidCourse")]
        public async Task<IActionResult> AddEpisodeToPaidCourse(int courseId, EpisodeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var episode = await _episodeService.CreateEpisodePaidCourseAsync(courseId, dto);

            if (episode == null)
                return BadRequest("Failed to add episode.");

            return Ok(new
            {
                Message = "Episode Added Successfully",
                Episode = new
                {
                    episode.Name,
                    episode.Description,
                    episode.PaidCourseId
                }
            });
        }*/


       /* [HttpGet("GetByPaidCourse")]
        public async Task<IActionResult> GetEpisodesByPaidCourse(int _PaidCourseId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var episodes = await _episodeService.GetAllEpisodeOfPaidCourseAsync(_PaidCourseId);

            if (!episodes.Any())
                return NotFound("No episodes found for this course.");

            return Ok(episodes);
        }*/
    }
}
