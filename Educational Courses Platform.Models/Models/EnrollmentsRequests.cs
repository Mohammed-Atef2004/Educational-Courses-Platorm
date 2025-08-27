using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Models.Models
{
    public class EnrollmentsRequest
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
    }
}
