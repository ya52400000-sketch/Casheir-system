using Cashier_system.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.Services;

public interface ICategoryServices
{
    Task <IEnumerable<GetAllCategory>> GetAllCategoriesAsync();
    Task<GetCategoryById> GetCategoryByIdAsync(Guid id);
    Task<CommonRespond> CreatCategory(AddCategoryDto Dto);
    Task<CommonRespond> UpdateCategory(Guid id, AddCategoryDto dto);
    Task<CommonRespond> DeleteCategory(Guid id);
}

