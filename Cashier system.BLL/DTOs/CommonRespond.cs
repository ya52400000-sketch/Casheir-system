using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.DTOs;

public class CommonRespond
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; }
    public object additionalData { get; set; }
    public CommonRespond(bool isSuccess, string message, List<string> errors = null, object additionalData = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors;
        this.additionalData = additionalData;
    }

}
