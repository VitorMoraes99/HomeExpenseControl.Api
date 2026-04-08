using HomeExpenseControl.WebAPI.DTOs;

namespace HomeExpenseControl.WebAPI.Services.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionResponseDto>> GetAllAsync();
    Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto);
    
    Task<bool> UpdateAsync(int id, CreateTransactionDto dto);
    Task<bool> DeleteAsync(int id);
}