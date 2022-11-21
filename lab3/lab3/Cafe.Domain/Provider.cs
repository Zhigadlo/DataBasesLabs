using System;
using System.Collections.Generic;

namespace lab3.Cafe.Domain;

public partial class Provider
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<IngridientsWarehouse> IngridientsWarehouses { get; } = new List<IngridientsWarehouse>();
}
