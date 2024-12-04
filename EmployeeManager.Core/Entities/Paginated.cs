namespace EmployeeManager.Core.Entities;
public class Paginated<TEntity>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<TEntity> Entities { get; set; }
}

