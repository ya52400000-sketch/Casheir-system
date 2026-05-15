using Cashier_system.BLL.DTOs;
using Cashier_system.BLL.DTOs.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.Services.RolesServices;

public class RolesSevices : IRolesServices
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public RolesSevices(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task<CommonRespond> AddRoleAsync(AddRoleDto dto)
    {
        var ExistingRole = await _roleManager.FindByNameAsync(dto.RoleName);
        if (ExistingRole != null)
        {
            return new CommonRespond(false, "Role already exists", null, null);
        }
        var result = await _roleManager.CreateAsync(new IdentityRole(dto.RoleName));
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new CommonRespond(false, "Failed to create role", errors, null);
        }
        return new CommonRespond(true, "Role created successfully", null, null);
    }

    public async Task<IEnumerable<string>> GetRolesAsync()
    {
        return await _roleManager.Roles
            .Where(r => r.Name != null)
            .Select(r => r.Name!)
            .ToListAsync();
    }

    public async Task<CommonRespond> RemoveRoleAsync(RemoveRoleDto dto)
    {
        var ExistingRole = await _roleManager.FindByNameAsync(dto.RoleName);
        if (ExistingRole == null)
        {
            return new CommonRespond(false, "Role not found", null, null);
        }
        var result = await _roleManager.DeleteAsync(ExistingRole);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new CommonRespond(false, "Failed to delete role", errors, null);
        }
        return new CommonRespond(true, "Role deleted successfully", null, null);
    }

    public async Task<CommonRespond> UpdateRoleAsync(UpdateRoleDto dto)
    {

        var existingRole = await _roleManager.FindByNameAsync(dto.RoleName);

        if (existingRole == null)
        {
            return new CommonRespond(false, "Role not found", null, null);
        }


        var roleWithSameName = await _roleManager.FindByNameAsync(dto.NewRoleName);

        if (roleWithSameName != null)
        {
            return new CommonRespond(false, "Role name already exists", null, null);
        }

        existingRole.Name = dto.NewRoleName;
        existingRole.NormalizedName = dto.NewRoleName.ToUpper();

        var result = await _roleManager.UpdateAsync(existingRole);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new CommonRespond(false, "Failed to update role", errors, null);
        }

        return new CommonRespond(true, "Role updated successfully", null, null);

    }
}
