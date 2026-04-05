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
        return Ok(category); // Usando Ok para simplificar, mas Created seria o ideal também
    }
}