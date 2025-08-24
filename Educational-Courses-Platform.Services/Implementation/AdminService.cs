using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using Educational_Courses_Platform.Models.Dto;
using Educational_Courses_Platform.Services.Interfaces;

namespace Educational_Courses_Platform.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseService _courseService;
        public AdminService(IUnitOfWork unitOfWork, ICourseService courseService)
        {
            _unitOfWork = unitOfWork;
            _courseService = courseService;
        }

        public async Task<string> AddCourseToUserAsync(string userId, int courseId)
        {

            var user = _unitOfWork.Admin.GetFirstOrDefault(
                 u => u.Id == userId);

            var course = await _courseService.GetCourseByIdAsync(courseId);
            user.EnrolledCourses= user.EnrolledCourses ?? new List<Course>();
            user.EnrolledCourses.Add(course);
            _unitOfWork.Complete();

            return $"Course '{course.Name}' has been successfully assigned to user {user.UserName}.";

        }




        public async Task<List<CourseDto>> GetCoursesOfUserAsync(string userId)
        {
            var user = _unitOfWork.Admin.GetFirstOrDefault(
                u => u.Id == userId,
                includeword: "EnrolledCourses");

            if (user == null) return null;
            List<CourseDto> courseDtos = new List<CourseDto>();
            courseDtos = user.EnrolledCourses.Select(c => new CourseDto
            {
               
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
            }).ToList();
            return courseDtos;
        }


    }
}