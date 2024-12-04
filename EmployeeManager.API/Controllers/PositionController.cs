using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.Viewmodels.Positions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers;
[Route("api/position")]
[ApiController]
public class PositionController : ControllerBase
{
    private readonly IPositionService _positionService;

    public PositionController(IPositionService positionService)
    {
        _positionService = positionService;
    }

    [HttpGet("get/all")]
    public async Task<ActionResult> get()
    {
        if (ModelState.IsValid)
        {
            var res = await _positionService.GetAllPosition();
            return Ok(res);
        }
        return BadRequest(new List<PositionViewModel>());
    }

    [HttpPost("create")]
    public async Task<ActionResult> post(PositionCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var res = await _positionService.CreatePosition(model);
            if (res is null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
        return BadRequest();
    }

    [HttpPut("update")]
    public async Task<ActionResult> put(PositionUpdateModel model)
    {
        if (ModelState.IsValid)
        {
            var res = await _positionService.UpdatePosition(model);
            if (res is null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
        return BadRequest();
    }

    [HttpGet("get/id={id}")]
    public async Task<ActionResult> getDetail(long id)
    {
        if (ModelState.IsValid)
        {
            var res = await _positionService.GetPosition(id);
            if (res is null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
        return BadRequest();
    }

    [HttpDelete("delete/id={id}")]
    public async Task<ActionResult> delete(long id)
    {
        if (ModelState.IsValid)
        {
            var res = await _positionService.DeletePosition(id);
            if (!res)
            {
                return BadRequest();
            }
            return Ok();
        }
        return BadRequest();
    }
}

