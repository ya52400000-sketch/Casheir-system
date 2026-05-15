using Cashier_system.BLL.DTOs;
using Cashier_system.BLL.DTOs.LogIn;
using Cashier_system.BLL.DTOs.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.Services.AuthServices;

public interface IAuthServices
{
    Task<CommonRespond> RegisterWorkerAsync(RegisterDto dto);
    Task<CommonRespond> RegisterOwnerAsync(RegisterDto dto);
    Task<CommonRespond> LoginAsync(LogInDto logInDto);
}
