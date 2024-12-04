using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Application.Viewmodels.Positions;

public class PositionUpdateModel
{
    [Required]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
}
