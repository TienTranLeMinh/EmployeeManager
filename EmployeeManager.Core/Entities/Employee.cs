namespace EmployeeManager.Core.Entities;
public class Employee : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public long PositionId { get; set; }
    public Position Position { get; set; }
}

