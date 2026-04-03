using HomeExpenseControl.WebAPI.Models;

namespace HomeExpenseControl.WebAPI.DTOs;

public record CreateCategoryDto(string Description, CategoryPurpose Purpose);
public record CategoryResponseDto(int Id, string Description, CategoryPurpose Purpose);