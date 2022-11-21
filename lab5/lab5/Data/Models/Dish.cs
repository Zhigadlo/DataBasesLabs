namespace lab5.Data.Models
{
    public partial class Dish
    {
        public Dish()
        {
            IngridientsDishes = new HashSet<IngridientsDish>();
            OrderDishes = new HashSet<OrderDish>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Cost { get; set; }
        public int CookingTime { get; set; }

        public virtual ICollection<IngridientsDish> IngridientsDishes { get; set; }
        public virtual ICollection<OrderDish> OrderDishes { get; set; }
    }
}
