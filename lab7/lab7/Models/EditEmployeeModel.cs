using System.ComponentModel.DataAnnotations;

namespace lab7.Models
{
    public class EditEmployeeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name must be not empty")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Last name must be not empty")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Middle name must be not empty")]
        public string MiddleName { get; set; }
        [Required]
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Education must be not empty")]
        public string Education { get; set; }
        [Required]
        public int ProfessionId { get; set; }
    }
}
