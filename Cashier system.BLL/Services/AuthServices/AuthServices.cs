using Cashier_system.BLL.DTOs;
using Cashier_system.BLL.DTOs.LogIn;
using Cashier_system.BLL.DTOs.Register;
using Cashier_system.BLL.Handler;
using Cashier_system.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.Services.AuthServices;

public class AuthServices : IAuthServices
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    public AuthServices(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<CommonRespond> LoginAsync(LogInDto logInDto)
    {
        var user = await _userManager.FindByEmailAsync(logInDto.Email);
        if (user == null) 
        {
            return new CommonRespond(false, "Invalid email or password", null, null);
        }
        var passwordValid = await _userManager.CheckPasswordAsync(user, logInDto.Password);
        if (!passwordValid)
        {
            return new CommonRespond(false, "Invalid email or password", null, null);
        }
        var token = await TokenHandler.CreateTokenAsync(user,_configuration, _userManager);
        return new CommonRespond(true, "Login successful", null, token);
    }

    public async Task<CommonRespond> RegisterAsync(RegisterDto registerDto)
    {
        var ExistingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (ExistingUser != null)
        {
            return new CommonRespond(false, "User already exists", null, null);
        } 
        var user = new AppUser
        {
          FullName = registerDto.FullName,
            UserName = registerDto.Email,
            Email = registerDto.Email
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new CommonRespond(false, "User creation failed", errors, null);
        }
        var addroleResult = await _userManager.AddToRoleAsync(user, "Worker");
        if (!addroleResult.Succeeded)
        {
            var errors = addroleResult.Errors.Select(e => e.Description).ToList();
            return new CommonRespond(false, "Adding role failed", errors, null);
        }
        return new CommonRespond(true, "User created successfully", null, null);
    }
}
