using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Models.Dto
{
    public class CourseWithEpisodesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ?ImageUrl { get; set; }
        public string Description { get; set; }

        public List<EpisodeDto>? Episodes { get; set; }  
    }
}
