namespace EmployeeManager.Application.Viewmodels;
public class PaginatedViewModel<TViewModel>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<TViewModel> Data { get; set; }
}

public class PaginatedRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

}

