using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Services.Interfaces
{
    public interface ICourseService
    {
        public Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        public Task<Course> GetCourseByIdAsync(int id);
        public Task<Course> CreateCourseAsync(CourseDto courseDto);
        public Task<bool> RemoveCourseByIdAsync(int id);
        public Task<bool> RemoveCourseByNameAsync(string name);

        public Task<bool> UpdateCourseAsync(int id, CourseDto courseDto);

        public Task<IEnumerable<CourseWithEpisodesDto>> GetAllCoursesWithEpisodesAsync();


    }
}