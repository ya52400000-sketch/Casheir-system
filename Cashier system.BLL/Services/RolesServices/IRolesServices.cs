using Cashier_system.BLL.DTOs;
using Cashier_system.BLL.DTOs.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.Services.RolesServices;

public interface IRolesServices
{
    Task<CommonRespond> AddRoleAsync(AddRoleDto dto);
    Task<CommonRespond> RemoveRoleAsync(RemoveRoleDto dto);
    Task<CommonRespond> UpdateRoleAsync(UpdateRoleDto dto);
    Task<IEnumerable<string>> GetRolesAsync();
}
