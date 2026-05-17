using Cashier_system.BLL.DTOs;
using Cashier_system.DAL.Models;
using Cashier_system.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.Services;

public class CategoryServices : ICategoryServices
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CommonRespond> CreatCategory(AddCategoryDto Dto)
    {
        var categories = await _unitOfWork.CategoryRepo.GetAllAsync();

        var exists = categories.Any(c => c.Name == Dto.Name);

        if (exists)
        {
            return new CommonRespond(false, "Category already exists", null!, null!);
        }

        var newCategory = new Category
        {
            Name = Dto.Name
        };

        await _unitOfWork.CategoryRepo.AddAsync(newCategory);
        await _unitOfWork.SaveAsync();

        return new CommonRespond(true, "Category created successfully", null!, null!);
    }

    public async Task<CommonRespond> DeleteCategory(Guid id)
    {
     var category = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
        if (category == null)
        {
            return new CommonRespond(false, "Category not found", null, null);
        }
        category.IsDeleted = true;
        _unitOfWork.CategoryRepo.Update(category);
        await _unitOfWork.SaveAsync();
        return new CommonRespond(true, "Category deleted successfully", null, null);
    }

    public async Task<IEnumerable<GetAllCategory>> GetAllCategoriesAsync()
    {
        var categories = await _unitOfWork.CategoryRepo.GetAllAsync();

        return categories
            .Where(c => !c.IsDeleted)
            .Select(c => new GetAllCategory
            {
                Name = c.Name
            });
    }

    public async Task<GetCategoryById> GetCategoryByIdAsync(Guid id)
    {
        var category = await _unitOfWork.CategoryRepo.GetByIdAsync(id);

        if (category == null || category.IsDeleted)
        {
            throw new Exception("Category not found");
        }

        return new GetCategoryById
        {
            Name = category.Name
        };
    }

    public async Task<CommonRespond> UpdateCategory(Guid id,AddCategoryDto dto)
    {
        var category = await _unitOfWork.CategoryRepo.GetByIdAsync(id);

        if (category == null)
        {
            return new CommonRespond(false, "Category not found", null, null);
        }

        category.Name = dto.Name;

        _unitOfWork.CategoryRepo.Update(category);
        await _unitOfWork.SaveAsync();

        return new CommonRespond(true, "Category updated successfully", null, category);
    }
}
