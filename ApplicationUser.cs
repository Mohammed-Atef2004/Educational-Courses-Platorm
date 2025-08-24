using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Educational_Courses_Platform.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public List<Course>? EnrolledCourses { get; set; }
    }
}

