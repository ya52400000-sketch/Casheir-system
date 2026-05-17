using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Models;
public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;


    public Guid CategoryId { get; set; }

    public Category Category { get; set; }
}