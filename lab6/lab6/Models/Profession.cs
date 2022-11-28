﻿using System;
using System.Collections.Generic;

namespace lab6.Models;

public partial class Profession
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
