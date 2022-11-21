using lab5.Data.Models;

namespace lab5.Models.DishViewModels
{
    public record DishUpdateViewModel(Dish Dish, IEnumerable<IngridientsDish> IngridientsDishes, IEnumerable<Ingridient> Ingridients);
}
