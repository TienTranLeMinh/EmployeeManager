namespace EmployeeManager.Core.Entities;
public class BaseEntity
{
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public bool IsDeleted { get; set; }
}

public class BaseEntity<T>
{
    public T Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public bool IsDeleted { get; set; }
}

