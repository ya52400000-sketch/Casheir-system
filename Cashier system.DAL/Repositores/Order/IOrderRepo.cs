using Cashier_system.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Repositores;

public interface IOrderRepo: IGenricRepo<Order>
{

    Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
    

}
