using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.DTOs;

public class OrderItemDto
{
 

    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; } = null;
}
