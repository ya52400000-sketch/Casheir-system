using Cashier_system.DAL.Data;
using Cashier_system.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Repositores;

public class OrderRepo:GenricRepo<Order>, IOrderRepo
{
    private readonly AppDbContext _context;
    public OrderRepo(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Orders
              .Include(o => o.OrderItems)
              .ThenInclude(i => i.Product)
              .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
              .ToListAsync();
    }

    public async Task<Order> GetOrderswithorderitem(Guid id)
    {
        return await _context.Orders
       .Include(o => o.OrderItems)
       .ThenInclude(i => i.Product)
       .FirstOrDefaultAsync(o => o.Id == id);
    }
}
