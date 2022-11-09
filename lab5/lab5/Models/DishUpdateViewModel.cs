using lab5.Data.Models;

namespace lab5.Models
{
    public record DishUpdateViewModel(Dish Dish, IEnumerable<IngridientsDish> IngridientsDishes, IEnumerable<Ingridient> Ingridients);
}
