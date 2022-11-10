using lab5.Data.Models;

namespace lab5.Models.IngridientViewModels
{
    public class IngridientIndexViewModel : IndexViewModel<Ingridient>
    {
        public FilterIngridientViewModel FilterViewModel { get; set; }
        public SortIngridientViewModel SortViewModel { get; set; }
    }
}
