using Cashier_system.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.Services;

public interface IProductServices
{
    Task <IEnumerable<GetproductDto>> GetAllProductsAsync();
    Task <GetproductDto> GetProductByIdAsync(Guid id);
    Task<CommonRespond> CreateProduct(AddProductDto Dto);
    Task <CommonRespond> UpdateProduct(Guid id, AddProductDto dto);
    Task<CommonRespond> DeleteProduct(Guid id);
}
