using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.DTOs;

public class GetOrderDto
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal FinalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<OrderItemDto> Items { get; set; }
}
