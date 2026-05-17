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


    [HttpPost("register-worker")]
    public async Task<IActionResult> RegisterWorker([FromBody] RegisterDto dto)
    {
        var result = await _authService.RegisterWorkerAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

   
    [HttpPost("register-owner")]
    public async Task<IActionResult> RegisterOwner([FromBody] RegisterDto dto)
    {
        var result = await _authService.RegisterOwnerAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

}
