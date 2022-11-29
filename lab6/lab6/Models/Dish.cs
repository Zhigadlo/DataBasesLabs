﻿using System;
using System.Collections.Generic;

namespace lab6.Models;

public partial class Dish
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Cost { get; set; }

    public int CookingTime { get; set; }

    public virtual ICollection<IngridientsDish> IngridientsDishes { get; } = new List<IngridientsDish>();

    public virtual ICollection<OrderDish> OrderDishes { get; } = new List<OrderDish>();
}
