using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.Services;
using EmployeeManager.Application.Viewmodels;
using EmployeeManager.Application.Viewmodels.Employees;
using EmployeeManager.Application.Viewmodels.Positions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers;
[Route("api/employee")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("get/all")]
    public async Task<ActionResult> get()
    {
        if (ModelState.IsValid)
        {
            var res = await _employeeService.GetAllEmployee();
            return Ok(res);
        }
        return BadRequest(new List<EmployeeViewModel>());
    }

    [HttpPost("create")]
    public async Task<ActionResult> post(EmployeeCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var res = await _employeeService.CreateEmployee(model);
            if (res is null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
        return BadRequest();
    }

    [HttpPost("get/paginated")]
    public async Task<ActionResult> GetPaginated(PaginatedRequest request)
    {
        if (ModelState.IsValid)
        {
            var res = await _employeeService.GetPagedAsync(request.PageIndex, request.PageSize);
            if (res is null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
        return BadRequest();
    }

    [HttpPut("update")]
    public async Task<ActionResult> put(EmployeeUpdateModel model)
    {
        if (ModelState.IsValid)
        {
            var res = await _employeeService.UpdateEmployee(model);
            if (res is null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
        return BadRequest();
    }

    [HttpGet("get/id={id}")]
    public async Task<ActionResult> getDetail(string id)
    {
        if (ModelState.IsValid)
        {
            var res = await _employeeService.GetEmployee(id);
            if (res is null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
        return BadRequest();
    }

    [HttpDelete("delete/id={id}")]
    public async Task<ActionResult> delete(string id)
    {
        if (ModelState.IsValid)
        {
            var res = await _employeeService.DeleteEmployee(id);
            if (!res)
            {
                return BadRequest();
            }
            return Ok();
        }
        return BadRequest();
    }
}

