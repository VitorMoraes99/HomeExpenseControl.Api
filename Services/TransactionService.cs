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
            t.Id, 
            t.Description, 
            t.Amount, 
            t.Type, 
            t.CategoryId, 
            t.PersonId
        ));
    }

    public async Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto)
    {
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
            transaction.Id, 
            transaction.Description, 
            transaction.Amount, 
            transaction.Type, 
            transaction.CategoryId, 
            transaction.PersonId
        );
    }

    public async Task<bool> UpdateAsync(int id, CreateTransactionDto dto)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) return false;

        transaction.Description = dto.Description;
        transaction.Amount = dto.Amount;
        transaction.Type = dto.Type;
        transaction.CategoryId = dto.CategoryId;
        transaction.PersonId = dto.PersonId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) return false;

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return true;
    }
}