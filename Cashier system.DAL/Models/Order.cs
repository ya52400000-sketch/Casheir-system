using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Models;
public class Order
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public decimal TotalAmount { get; set; }

    public decimal Discount { get; set; }

    public decimal NetAmount { get; set; }
    public string AppUserId { get; set; }

    public AppUser AppUser { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
}
