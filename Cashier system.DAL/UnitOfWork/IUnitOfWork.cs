using Cashier_system.DAL.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.UnitOfWork;

public interface IUnitOfWork:IDisposable
{
    ICategoryRepo CategoryRepo { get; }
    IProductRepo ProductRepo { get; }
    IOrderRepo OrderRepo { get; }
    IOrderItemRepo OrderItemRepo { get; }
    Task SaveAsync();
}
