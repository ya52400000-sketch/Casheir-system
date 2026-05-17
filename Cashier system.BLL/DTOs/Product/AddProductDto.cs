using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.DTOs;

public class AddProductDto
{
    public string Name { get; set; }

    public decimal Price { get; set; }
   
    public Guid CategoryId { get; set; }
}
