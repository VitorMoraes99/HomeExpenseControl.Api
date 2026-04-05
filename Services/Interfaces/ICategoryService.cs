using HomeExpenseControl.WebAPI.DTOs;

namespace HomeExpenseControl.WebAPI.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);
}