using System;
using System.Collections.Generic;

namespace lab5.Data.Models
{
    public partial class Profession
    {
        public Profession()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
