using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Application.Viewmodels.Positions;

public class PositionCreateModel
{
    [Required]
    public string Name { get; set; }
}


