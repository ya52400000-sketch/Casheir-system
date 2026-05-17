using Cashier_system.DAL.Data;
using Cashier_system.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Repositores;

public class OrderItemRepo: GenricRepo<OrderItem>, IOrderItemRepo
{
    private readonly AppDbContext _context;
    public OrderItemRepo(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
