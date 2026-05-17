using Cashier_system.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.Services;

public interface IOrderServices
{
    Task<CommonRespond> CreatOrderAsync(CreateOrderDto Dto,string appUserId);

    Task<CommonRespond> DeleteOrder(DeleteOrderDto Dto);
    Task<CommonRespond> GetOrderById(GetOrderDto dto);
    Task<CommonRespond> GetOrderByDate(GetOrderByDateDto dto);
}
