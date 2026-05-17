using Cashier_system.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Repositores;

public interface IProductRepo : IGenricRepo<Product>
{
    Task ProductnotActive(Guid id);
    Task<List<Product>> GetAllWithCategory();
    Task<Product> GetByIdWithCategory(Guid id);
}
