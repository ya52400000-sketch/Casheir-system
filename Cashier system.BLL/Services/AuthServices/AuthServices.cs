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
        var user = await _userManager.FindByNameAsync(logInDto.UserName);
        if (user == null) 
        {
            return new CommonRespond(false, "Invalid UserName or password", null, null);
        }
        var passwordValid = await _userManager.CheckPasswordAsync(user, logInDto.Password);
        if (!passwordValid)
        {
            return new CommonRespond(false, "Invalid UserName or password", null, null);
        }
        var token = await TokenHandler.CreateTokenAsync(user,_configuration, _userManager);
        return new CommonRespond(true, "Login successful", null, token);
    }


    public async Task<CommonRespond> RegisterWorkerAsync(RegisterDto dto)
    {
        return await RegisterWithRole(dto, "Worker");
    }


    public async Task<CommonRespond> RegisterOwnerAsync(RegisterDto dto)
    {
        return await RegisterWithRole(dto, "Owner");
    }

    private async Task<CommonRespond> RegisterWithRole(RegisterDto dto, string role)
    {
        var existingUser = await _userManager.FindByNameAsync(dto.UserName);

        if (existingUser != null)
            return new CommonRespond(false, "User already exists", null, null);

        var user = new AppUser
        {
            UserName = dto.UserName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return new CommonRespond(
                false,
                "User creation failed",
                result.Errors.Select(e => e.Description).ToList(),
                null
            );
        }

    
        var addRoleResult = await _userManager.AddToRoleAsync(user, role);

        if (!addRoleResult.Succeeded)
        {
            return new CommonRespond(
                false,
                "Adding role failed",
                addRoleResult.Errors.Select(e => e.Description).ToList(),
                null
            );
        }

        return new CommonRespond(true, $"{role} created successfully", null, null);
    }
}
