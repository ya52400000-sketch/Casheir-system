using Cashier_system.DAL.Data;
using Cashier_system.DAL.Models;
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
    public async Task ProductnotActive(Guid id)
    {
       var product =  _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        if (!product.IsActive)
        {
            throw new Exception("Product is not active");
        }
     product.IsActive = false;
        _context.Products.Update(product);
   
    }
}
