namespace EmployeeManager.Application.Viewmodels;
public class BaseViewModel
{
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}

public class BaseViewModel<T>
{
    public T Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}
