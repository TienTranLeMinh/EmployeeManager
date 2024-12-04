namespace EmployeeManager.Core.Entities;
public class Position : BaseEntity<long>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}

