using Cashier_system.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.DTOs;

public class CreateOrderDto
{
    public List<CreateOrderItemDto> Items { get; set; }

    public DiscountType? DiscountType { get; set; }

    public decimal? DiscountValue { get; set; }

}
