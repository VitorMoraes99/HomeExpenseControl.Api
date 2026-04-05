using HomeExpenseControl.WebAPI.DTOs;

namespace HomeExpenseControl.WebAPI.Services.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<PersonResponseDto>> GetAllAsync();
    Task<PersonResponseDto?> GetByIdAsync(int id);
    Task<PersonResponseDto> CreateAsync(CreatePersonDto dto);
    Task<bool> UpdateAsync(int id, UpdatePersonDto dto);
    Task<bool> DeleteAsync(int id);
}