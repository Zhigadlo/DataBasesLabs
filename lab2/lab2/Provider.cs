using System;
using System.Collections.Generic;

namespace lab2;

public partial class Provider
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return Name;
    }

    public virtual ICollection<IngridientsWarehouse> IngridientsWarehouses { get; } = new List<IngridientsWarehouse>();
}
