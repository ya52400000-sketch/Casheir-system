using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.DTOs.LogIn;

public class LogInDto
{
    [Required(ErrorMessage = "Username is required.")]
public string UserName { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
}
