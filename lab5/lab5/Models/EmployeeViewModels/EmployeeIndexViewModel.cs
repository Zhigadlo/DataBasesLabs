using lab5.Data.Models;

namespace lab5.Models.EmployeeViewModels
{
    public class EmployeeIndexViewModel : IndexViewModel<Employee>
    {
        public EmployeeFilterViewModel FilterViewModel { get; set; }
        public SortEmployeeViewModel SortViewModel { get; set; }
    }
}
