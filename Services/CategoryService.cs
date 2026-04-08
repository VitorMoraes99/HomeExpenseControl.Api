using HomeExpenseControl.WebAPI.Data;
using HomeExpenseControl.WebAPI.DTOs;
using HomeExpenseControl.WebAPI.Models;
using HomeExpenseControl.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeExpenseControl.WebAPI.Services.Interfaces;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories.Select(c => new CategoryResponseDto(c.Id, c.Description, c.Purpose));
    }

    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Description = dto.Description,
            Purpose = dto.Purpose
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return new CategoryResponseDto(category.Id, category.Description, category.Purpose);
    }
    public async Task<bool> UpdateAsync(int id, CreateCategoryDto dto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        category.Description = dto.Description;
        category.Purpose = dto.Purpose;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}