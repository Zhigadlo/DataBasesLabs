using System;
using System.Collections.Generic;

namespace lab2;

public partial class Profession
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
