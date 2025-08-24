using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Entities.Models
{ 
    public class Course
    {
   
        public int Id { get; set; }
     
        public string Name { get ; set ; }
        public string Description { get; set; }
        public List<Episode>? Episodes { get; set; } = new List<Episode>();

        public double Price { get; set; }

    }
}
