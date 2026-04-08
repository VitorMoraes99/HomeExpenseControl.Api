using HomeExpenseControl.WebAPI.DTOs;
using HomeExpenseControl.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeExpenseControl.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")] // A rota será: api/categories
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        var category = await _categoryService.CreateAsync(dto);
        return Ok(category); 
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateCategoryDto dto)
    {
        try
        {
            var updated = await _categoryService.UpdateAsync(id, dto);
            if (!updated) return NotFound(new { message = "Categoria não encontrada." });
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _categoryService.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = "Categoria não encontrada." });
        return NoContent();
    }
}