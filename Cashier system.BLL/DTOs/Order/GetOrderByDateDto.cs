using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.DTOs;

public class GetOrderByDateDto
{
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
}
