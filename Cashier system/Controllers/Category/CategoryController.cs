using Cashier_system.BLL.DTOs;
using Cashier_system.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashier_system.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryServices _categoryService;

    public CategoryController(ICategoryServices categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpPost]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Create(AddCategoryDto dto)
    {
        var result = await _categoryService.CreatCategory(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    [HttpGet]
    [Authorize(Roles = "Owner,Worker")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAllCategoriesAsync();
        if (result == null)
            return NotFound(result);
        return Ok(result);
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "Owner,Worker")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _categoryService.GetCategoryByIdAsync(id);
        if(result == null)
            return NotFound(result);

        return Ok(result);
    }
    [HttpPut("{id}")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Update(Guid id, AddCategoryDto dto)
    {
        var result = await _categoryService.UpdateCategory(id, dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _categoryService.DeleteCategory(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
