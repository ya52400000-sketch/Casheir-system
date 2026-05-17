using Cashier_system.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Repositores;

public class GenricRepo<T> : IGenricRepo<T> where T : class
{
    private readonly AppDbContext _context;
    public GenricRepo(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);

    }

    public async Task DeleteAsync(T entity)
    {
 
            _context.Set<T>().Remove(entity);
        
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
    return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public  void Update(T entity)
    {
    _context.Update(entity);
    }
}
