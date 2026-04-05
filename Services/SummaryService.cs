using HomeExpenseControl.WebAPI.Data;
using HomeExpenseControl.WebAPI.DTOs;
using HomeExpenseControl.WebAPI.Models;
using HomeExpenseControl.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeExpenseControl.WebAPI.Services;

public class SummaryService : ISummaryService
{
    private readonly AppDbContext _context;

    public SummaryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardResponseDto<PersonSummaryDto>> GetPeopleSummaryAsync()
    {
        var people = await _context.People.Include(p => p.Transactions).ToListAsync();

        var items = people.Select(p => 
        {
            var income = p.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var expense = p.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
            return new PersonSummaryDto(p.Name, income, expense, income - expense);
        }).ToList();

        var grandIncome = items.Sum(i => i.TotalIncome);
        var grandExpense = items.Sum(i => i.TotalExpense);

        return new DashboardResponseDto<PersonSummaryDto>(items, grandIncome, grandExpense, grandIncome - grandExpense);
    }

    public async Task<DashboardResponseDto<CategorySummaryDto>> GetCategoriesSummaryAsync()
    {
        var categories = await _context.Categories
            .Select(c => new 
            {
                Category = c,
                Transactions = _context.Transactions.Where(t => t.CategoryId == c.Id).ToList()
            }).ToListAsync();

        var items = categories.Select(c => 
        {
            var income = c.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var expense = c.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
            return new CategorySummaryDto(c.Category.Description, income, expense, income - expense);
        }).ToList();

        var grandIncome = items.Sum(i => i.TotalIncome);
        var grandExpense = items.Sum(i => i.TotalExpense);

        return new DashboardResponseDto<CategorySummaryDto>(items, grandIncome, grandExpense, grandIncome - grandExpense);
    }
}