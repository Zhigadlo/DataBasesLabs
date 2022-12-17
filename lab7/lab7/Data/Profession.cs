using System.ComponentModel.DataAnnotations;

namespace lab7.Data;

public partial class Profession
{
    public int Id { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Name must be not empty or not too short")]
    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
