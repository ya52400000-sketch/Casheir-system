using Cashier_system.BLL.DTOs.Roles;
using Cashier_system.BLL.Services.RolesServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashier_system.Controllers.RolesController;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRolesServices _rolesService;
    public RolesController(IRolesServices rolesService)
    {
        _rolesService = rolesService;
    }
    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _rolesService.GetRolesAsync();

        if (result == null || !result.Any())
            return NotFound("No roles found");

        return Ok(result);
    }
    [HttpPost("add")]
    public async Task<IActionResult> AddRole([FromBody] AddRoleDto dto)
    {
        var result = await _rolesService.AddRoleAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }
    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveRole([FromBody] RemoveRoleDto dto)
    {
        var result = await _rolesService.RemoveRoleAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return Ok(result.Message);
    }
    [HttpPut("update")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto dto)
    {
        var result = await _rolesService.UpdateRoleAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return Ok(result.Message);
    }
}