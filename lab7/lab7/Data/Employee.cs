using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace lab7.Data;

public partial class Employee
{
    public int Id { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "First name must be not empty or not too short")]
    public string FirstName { get; set; } = null!;
    [Required]
    [StringLength(20, ErrorMessage = "Last name must be not empty or not too short")]
    public string LastName { get; set; } = null!;
    [Required]
    [StringLength(20, ErrorMessage = "Middle name must be not empty or not too short")]
    public string? MiddleName { get; set; }
    [Required]
    [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
    public int Age { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Education must be not empty or not too short")]
    public string? Education { get; set; }
    [Required]
    public int ProfessionId { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Profession Profession { get; set; } = null!;
}
