using Educational_Courses_Platform.DataAccess.Data;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.Models.Dto;
using Educational_Courses_Platform.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Services.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CourseWithIdDto>> GetAllCoursesAsync()
        {
            List<Course> courses = _unitOfWork.Course.GetAll().ToList();
            List<CourseWithIdDto> courseDtos = courses.Select(c => new CourseWithIdDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                ImageUrl= c.ImageUrl

            }).ToList();
            return courseDtos;
        }
        public async Task<IEnumerable<CourseWithIdDto>> GetAllFreeCoursesAsync()
        {
            List<Course> courses = _unitOfWork.Course.GetAll(c=>c.Price==0).ToList();
            List<CourseWithIdDto> courseDtos = courses.Select(c => new CourseWithIdDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                ImageUrl = c.ImageUrl

            }).ToList();
            return courseDtos;
        }
        public async Task<IEnumerable<CourseWithIdDto>> GetAllPaidCoursesAsync()
        {
            List<Course> courses = _unitOfWork.Course.GetAll(c => c.Price > 0).ToList();
            List<CourseWithIdDto> courseDtos = courses.Select(c => new CourseWithIdDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                ImageUrl = c.ImageUrl

            }).ToList();
            return courseDtos;
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await Task.Run(() => _unitOfWork.Course.GetFirstOrDefault(x => x.Id == id, includeword: "Episodes"));
        }
        public async Task<Course> CreateCourseAsync(CourseDto courseDto)
        {
            var course = new Course
            {
                Name = courseDto.Name,
                Description = courseDto.Description,
                Price = courseDto.Price
            };

            _unitOfWork.Course.Add(course);
            _unitOfWork.Complete();
            return await Task.FromResult(course);
        }
        public Task<bool> RemoveCourseByIdAsync(int id)
        {
            var course = _unitOfWork.Course.GetFirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return null;
            }

            _unitOfWork.Course.Remove(course);
            _unitOfWork.Complete();
            return Task.FromResult(true);
        }
        public Task<bool> RemoveCourseByNameAsync(string name)
        {
            var course = _unitOfWork.Course.GetFirstOrDefault(c => c.Name == name);
            if (course == null)
            {
                return null;
            }
            _unitOfWork.Course.Remove(course);
            _unitOfWork.Complete();
            return Task.FromResult(true);
        }

        public Task<bool> UpdateCourseAsync(int id, CourseDto courseDto)
        {
            var course = _unitOfWork.Course.GetFirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return null;
            }

            course.Name = courseDto.Name;
            course.Description = courseDto.Description;
            course.Price = courseDto.Price;
            course.ImageUrl = courseDto.ImageUrl;

            _unitOfWork.Course.Update(course);
            _unitOfWork.Complete();
            return Task.FromResult(true);
        }

       public async Task<IEnumerable<CourseWithEpisodesDto>> GetAllCoursesWithEpisodesAsync()
        {
            List<Course> courses = _unitOfWork.Course.GetAll(includeword: "Episodes").ToList();
            List<CourseWithEpisodesDto> courseDtos = courses.Select(c => new CourseWithEpisodesDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUrl=c.ImageUrl,

                Episodes = c.Episodes.Select(e => new EpisodeDto
                {

                    Name = e.Name,
                    Description = e.Description,
                    ImageUrl = e.ImageUrl,
                    Link = e.Link

                }).ToList()
            }).ToList();
            return courseDtos;
        }
        public Task<IEnumerable<EpisodeDto>> GetAllEpisodeOfCourseAsync(int courseId)
        {
            var episodes = _unitOfWork.Episode
                .GetAll(c => c.CourseId == courseId)
                .ToList();

            if (!episodes.Any())
                return Task.FromResult(Enumerable.Empty<EpisodeDto>());

            var episodeDtos = episodes.Select(e => new EpisodeDto
            {
                Name = e.Name,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                Link=e.Link
                //  CourseId = e.CourseId
            });

            return Task.FromResult(episodeDtos);
        }
    }
}