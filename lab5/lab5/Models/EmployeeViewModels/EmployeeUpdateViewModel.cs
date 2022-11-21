using lab5.Data.Models;

namespace lab5.Models.EmployeeViewModels
{
    public record EmployeeUpdateViewModel(Employee Employee, IEnumerable<Profession> Professions);
}
