using Cashier_system.DAL.Data;
using Cashier_system.DAL.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public ICategoryRepo CategoryRepo { get; }
    public IProductRepo ProductRepo { get; }
    public IOrderRepo OrderRepo { get; }
    public IOrderItemRepo OrderItemRepo { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        CategoryRepo = new CategoryRepo(context);
        ProductRepo = new ProductRepo(context);
        OrderRepo = new OrderRepo(context);
        OrderItemRepo = new OrderItemRepo(context);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}