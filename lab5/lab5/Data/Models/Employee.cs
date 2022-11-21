namespace lab5.Data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public int? Age { get; set; }
        public string? Education { get; set; }
        public int ProfessionId { get; set; }

        public virtual Profession Profession { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
