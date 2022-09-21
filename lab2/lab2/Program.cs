using lab2;
using Microsoft.EntityFrameworkCore;

using (CafeContext context = new CafeContext())
{
    foreach (var dish in context.Dishes)
    {
        Console.WriteLine(dish.Name + " - " + dish.Cost);
    }
}