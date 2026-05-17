using Cashier_system.BLL.DTOs;
using Cashier_system.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cashier_system.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderServices _orderService;
    public OrderController(IOrderServices orderServices)
    {
        _orderService = orderServices;
    }
    [HttpPost]
    [Authorize(Roles = "Owner,Worker")]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var result = await _orderService.CreatOrderAsync(dto, appUserId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpDelete]
    [Authorize(Roles = "Owner,Worker")]
    public async Task<IActionResult> Delete(DeleteOrderDto dto)
    {
        var result = await _orderService.DeleteOrder(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dto = new GetOrderDto { Id = id };

        var result = await _orderService.GetOrderById(dto);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }
    [HttpGet("by-date")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> GetByDate([FromQuery] GetOrderByDateDto dto)
    {
        var result = await _orderService.GetOrderByDate(dto);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }
    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
