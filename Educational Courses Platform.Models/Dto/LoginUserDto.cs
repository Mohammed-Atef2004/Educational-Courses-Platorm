using System.ComponentModel.DataAnnotations;

namespace Educational_Courses_Platform.Models.Dto
{
    public class LoginUserDto
    {
        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
