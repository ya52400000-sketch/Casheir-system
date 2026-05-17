using Cashier_system.DAL.Data;
using Cashier_system.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Repositores;

public class CategoryRepo:GenricRepo<Category>, ICategoryRepo
{
    private readonly AppDbContext _context;
    public CategoryRepo(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task SoftDelete(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
            throw new Exception("Category not found");

        category.IsDeleted = true;

        _context.Categories.Update(category);
    }
}
