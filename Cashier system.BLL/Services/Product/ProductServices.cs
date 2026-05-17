using Cashier_system.BLL.DTOs;
using Cashier_system.DAL.Models;
using Cashier_system.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.Services;

public class ProductServices : IProductServices
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CommonRespond> CreateProduct(AddProductDto Dto)
    {
        var category =  await _unitOfWork.CategoryRepo.GetByIdAsync(Dto.CategoryId);

        if (category == null || category.IsDeleted)
        {
            return new CommonRespond(false, "Category not found", null, null);
        }

        var product = new Product
        {
            Name = Dto.Name,
            Price = Dto.Price,
            CategoryId = Dto.CategoryId,
            IsActive = true
        };

        await _unitOfWork.ProductRepo.AddAsync(product);
        await _unitOfWork.SaveAsync();

        return new CommonRespond(true, "Product created successfully", null, new AddProductDto
        {
            Name = product.Name,
            Price = product.Price
        });
    }

    public async Task<CommonRespond> DeleteProduct(Guid id)
    {
        var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);

        if (product == null)
            return new CommonRespond(false, "Product not found", null, null);

        product.IsActive = false;

        _unitOfWork.ProductRepo.Update(product);
        await _unitOfWork.SaveAsync();

        return new CommonRespond(true, "Product deleted successfully", null, null);
    }



    public async Task<IEnumerable<GetproductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.ProductRepo.GetAllWithCategory();

        return products
            .Where(p => p.IsActive)
            .Select(p => new GetproductDto
            {
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category?.Name
            });
    }

    public async Task<GetproductDto> GetProductByIdAsync(Guid id)
    {
        var product = await _unitOfWork.ProductRepo.GetByIdWithCategory(id);

        if (product == null || !product.IsActive)
            throw new Exception("Product not found");

        return new GetproductDto
        {
            Name = product.Name,
            Price = product.Price,
            CategoryName = product.Category?.Name
        };
    }

    public async Task<CommonRespond> UpdateProduct(Guid id, AddProductDto dto)
    {
        var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);

        if (product == null || !product.IsActive)
            return new CommonRespond(false, "Product not found", null, null);

        var category = await _unitOfWork.CategoryRepo.GetByIdAsync(dto.CategoryId);

        if (category == null || category.IsDeleted)
            return new CommonRespond(false, "Category not found", null, null);

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        _unitOfWork.ProductRepo.Update(product);
        await _unitOfWork.SaveAsync();

        return new CommonRespond(true, "Product updated successfully", null, new AddProductDto
        {
            Name = product.Name,
            Price = product.Price
        });
    }


}
