using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab5.Models.DishViewModels
{
    public class FilterDishViewModel
    {
        public string SelectedName { get; private set; }

        public FilterDishViewModel(string selectedName)
        {
            SelectedName = selectedName;
        }
    }
}
