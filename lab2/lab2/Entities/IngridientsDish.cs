using System;
using System.Collections.Generic;

namespace lab2.Entities;

public partial class IngridientsDish
{
    public int Id { get; set; }

    public int DishId { get; set; }

    public int IngridientId { get; set; }

    public int IngridientWeight { get; set; }

    public virtual Dish Dish { get; set; } = null!;

    public virtual Ingridient Ingridient { get; set; } = null!;
}
