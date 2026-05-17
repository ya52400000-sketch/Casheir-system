using Cashier_system.BLL.DTOs.LogIn;
using Cashier_system.BLL.Services.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashier_system.Controllers.AuthController;

[Route("api/[controller]")]
[ApiController]
public class LogInController : ControllerBase
{
    private readonly IAuthServices _authServices;
    public LogInController(IAuthServices authServices)
    {
        _authServices = authServices;
    }
    [HttpPost]
    public async Task<IActionResult> Login(LogInDto dto)
    {
        var result = await _authServices.LoginAsync(dto);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}