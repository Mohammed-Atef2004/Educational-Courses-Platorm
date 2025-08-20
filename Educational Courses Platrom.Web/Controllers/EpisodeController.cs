using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Educational_Courses_Platrom.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        public EpisodeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        [Route("GetEpisodesById")]
        public IActionResult GetEpisodes(int id)
        {
            if (ModelState.IsValid)
            {
                var episodes = _unitOfWork.Episode.GetFirstOrDefault(c => c.Id == id);

                if (episodes == null)
                {
                    return NotFound("Not found ");
                }

                return Ok(episodes);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("GetByCourse")]
        public IActionResult GetEpisodesByCourse(int courseId)
        {
            if (ModelState.IsValid)
            {
                var episodes = _unitOfWork.Episode
                    .GetAll(e => e.CourseId == courseId)
                    .ToList();

                if (episodes == null || !episodes.Any())
                {
                    return NotFound("No episodes found for this course");
                }

                return Ok(episodes);
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("AddEpisode")]
        public IActionResult AddEpisode(int courseId, EpisodeDto dto)
        {
            if (ModelState.IsValid)
            {
                var episode = new Episode
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    CourseId = courseId   
                };

                _unitOfWork.Episode.Add(episode);
                _unitOfWork.Complete();

                return Ok("Episode Added Successfully");
            }

            return BadRequest();
        }
        [HttpPost]
        [Route("AddEpisodeToPaidCourse")]
        public IActionResult AddEpisodeToPaidCourse(int courseId, EpisodeDto dto)
        {
            if (ModelState.IsValid)
            {
                var episode = new Episode
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    PaidCourseId = courseId
                };

                _unitOfWork.Episode.Add(episode);
                _unitOfWork.Complete();

                return Ok("Episode Added Successfully to Paid Course");
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("UpdateEpisode")]
        public IActionResult UpdateEpisode(int id, EpisodeDto dto)
        {
            if (ModelState.IsValid)
            {
                var episode = _unitOfWork.Episode.GetFirstOrDefault(e => e.Id == id);
                if (episode == null)
                {
                    return NotFound("Episode not found");
                }

                episode.Name = dto.Name;
                episode.Description = dto.Description;
                //episode.CourseId =dto.CourseId;

                _unitOfWork.Episode.Update(episode);
                _unitOfWork.Complete();
                return Ok("Episode Updated Successfully");
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("UpdateEpisodeCourseId")]
        public IActionResult UpdateEpisodeCourseId(int id, EpisodeCourseIdDto dto)
        {
            if (ModelState.IsValid)
            {
                var episode = _unitOfWork.Episode.GetFirstOrDefault(e => e.Id == id);
                if (episode == null)
                {
                    return NotFound("Episode not found");
                }

               
                episode.CourseId =dto.CourseId;
              
                _unitOfWork.Episode.Update(episode);
                _unitOfWork.Complete();
                return Ok("Episode Updated Successfully");
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("DeleteEpisode")]
        public IActionResult DeleteEpisode(int id)
        {
            if (ModelState.IsValid)
            {
                var episode = _unitOfWork.Episode.GetFirstOrDefault(e => e.Id == id);
                if (episode == null) { return NotFound("Episode not found"); }
                _unitOfWork.Episode.Remove(episode);
                _unitOfWork.Complete();
                return Ok("Episode Deleted Successfully");
            }
            return BadRequest();



        }
    }


   

   
}
