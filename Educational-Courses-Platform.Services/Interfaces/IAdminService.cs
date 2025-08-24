using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Models.Dto;

namespace Educational_Courses_Platform.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<string> AddCourseToUserAsync(string userId, int courseId);
        public Task<List<CourseDto>> GetCoursesOfUserAsync(string userId);
    }
}