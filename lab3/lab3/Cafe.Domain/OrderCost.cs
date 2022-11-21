using System;
using System.Collections.Generic;

namespace lab3.Cafe.Domain;

public partial class OrderCost
{
    public DateTime OrderDate { get; set; }

    public string? CustomerName { get; set; }

    public double? OrderCost1 { get; set; }
}
