using HomeExpenseControl.WebAPI.Models;

namespace HomeExpenseControl.WebAPI.DTOs;

public record CreateTransactionDto(
    string Description, 
    decimal Amount, 
    TransactionType Type, 
    int CategoryId, 
    int PersonId);

public record TransactionResponseDto(
    int Id, 
    string Description, 
    decimal Amount, 
    TransactionType Type, 
    int CategoryId, 
    int PersonId);