using HomeExpenseControl.WebAPI.Data;
using HomeExpenseControl.WebAPI.DTOs;
using HomeExpenseControl.WebAPI.Models;
using HomeExpenseControl.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeExpenseControl.WebAPI.Services;

public class TransactionService : ITransactionService
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync()
    {
        var transactions = await _context.Transactions.ToListAsync();
        
        return transactions.Select(t => new TransactionResponseDto(
            t.Id, t.Description, t.Amount, t.Type, t.CategoryId, t.PersonId));
    }

    public async Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto)
    {
        if (dto.Amount <= 0)
            throw new ArgumentException("The transaction amount must be greater than zero.");

        var person = await _context.People.FindAsync(dto.PersonId) 
            ?? throw new ArgumentException("Person not found.");
            
        var category = await _context.Categories.FindAsync(dto.CategoryId) 
            ?? throw new ArgumentException("Category not found.");

        if (person.Age < 18 && dto.Type != TransactionType.Expense)
        {
            throw new ArgumentException("Minors under 18 can only register expenses.");
        }

        if (dto.Type == TransactionType.Expense && category.Purpose == CategoryPurpose.Income)
        {
            throw new ArgumentException("Cannot use an Income category for an Expense transaction.");
        }
        
        if (dto.Type == TransactionType.Income && category.Purpose == CategoryPurpose.Expense)
        {
            throw new ArgumentException("Cannot use an Expense category for an Income transaction.");
        }

        var transaction = new Transaction
        {
            Description = dto.Description,
            Amount = dto.Amount,
            Type = dto.Type,
            CategoryId = dto.CategoryId,
            PersonId = dto.PersonId
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return new TransactionResponseDto(
            transaction.Id, transaction.Description, transaction.Amount, transaction.Type, transaction.CategoryId, transaction.PersonId);
    }
}