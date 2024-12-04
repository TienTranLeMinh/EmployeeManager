using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Application.Viewmodels.Employees;
public class EmployeeUpdateModel
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Ngày sinh theo định dạng dd/MM/yyyy.")]
    public string BirthDate { get; set; }
    [Required]
    public long PositionId { get; set; }
}

