using System.ComponentModel.DataAnnotations;

namespace lab7.Data
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Login must contaib from 8 to 20 letters")]
        public string Login { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
