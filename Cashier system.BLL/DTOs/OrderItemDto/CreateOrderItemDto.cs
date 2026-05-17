using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.DTOs;

public class CreateOrderItemDto
{
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
}
