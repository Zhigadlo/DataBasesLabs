using System.ComponentModel.DataAnnotations;

namespace lab7.Models
{
    public class UserModel
    {
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Login must contain from 8 to 20 letters")]
        public string Login { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
