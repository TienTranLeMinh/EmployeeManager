using EmployeeManager.Application.Viewmodels.Positions;

namespace EmployeeManager.Application.Viewmodels.Employees;
public class EmployeeViewModel : BaseViewModel<string>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public DateOnly BirthDate { get; set; }
    public long Age { get; set; }
    public PositionViewModel Position { get; set; }
}

