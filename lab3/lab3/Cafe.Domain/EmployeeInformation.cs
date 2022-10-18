using System;
using System.Collections.Generic;

namespace lab3.Cafe.Domain;

public partial class EmployeeInformation
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public int? Age { get; set; }

    public string? Education { get; set; }

    public string? Name { get; set; }
}
