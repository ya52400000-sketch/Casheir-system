using Cashier_system.DAL.Data;
using Cashier_system.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Repositores;

public class ProductRepo : GenricRepo<Product>, IProductRepo
{
    private readonly AppDbContext _context;
    public ProductRepo(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllWithCategory()
    {
        return await _context.Products
          .Include(p => p.Category)
          .ToListAsync();
    }

    public async Task<Product> GetByIdWithCategory(Guid id)
    {
        return await _context.Products
          .Include(p => p.Category)
          .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task ProductnotActive(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            throw new Exception("Product not found");

        if (!product.IsActive)
            throw new Exception("Product is already inactive");

        product.IsActive = false;

        _context.Products.Update(product);
    }
}
