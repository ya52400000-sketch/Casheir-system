using Cashier_system.BLL.DTOs.Register;
using Cashier_system.BLL.Services.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashier_system.Controllers.AuthController;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IAuthServices _authService;

    public RegisterController(IAuthServices authService)
    {
        _authService = authService;
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

}
