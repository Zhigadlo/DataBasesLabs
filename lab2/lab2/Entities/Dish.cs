﻿using System;
using System.Collections.Generic;

namespace lab2.Entities;

public partial class Dish
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Cost { get; set; }

    public int CookingTime { get; set; }

    public virtual ICollection<IngridientsDish> IngridientsDishes { get; } = new List<IngridientsDish>();

    public virtual ICollection<OrderDish> OrderDishes { get; } = new List<OrderDish>();

    public override string ToString()
    {
        return "Name: " + Name + ", Cost: " + Cost + ",Cooking time: " + CookingTime;
    }
}
