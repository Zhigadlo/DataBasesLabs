using lab5.Data.Models;

namespace lab5.Models.DishViewModels
{
    public class DishIndexViewModel : IndexViewModel<Dish>
    {
        public SortDishViewModel SortViewModel { get; set; }
        public FilterDishViewModel FilterViewModel { get; set; }
    }
}
