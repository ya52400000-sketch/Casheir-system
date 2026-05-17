using Cashier_system.BLL.DTOs;
using Cashier_system.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashier_system.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductServices _productService;
    public ProductController(IProductServices productServices)
    {
        _productService = productServices;
    }
    [HttpPost]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Create(AddProductDto dto)
    {
        var result = await _productService.CreateProduct(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    [HttpGet]
    [Authorize(Roles = "Owner,Worker")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllProductsAsync();
        if (result == null)
            return NotFound(result);

        return Ok(result);
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "Owner,Worker")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _productService.GetProductByIdAsync(id);
        if(result == null)
            return NotFound(result);

        return Ok(result);
    }
    [HttpPut("{id}")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Update(Guid id, AddProductDto dto)
    {
        var result = await _productService.UpdateProduct(id, dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _productService.DeleteProduct(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
