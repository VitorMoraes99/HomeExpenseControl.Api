using HomeExpenseControl.WebAPI.DTOs;
using HomeExpenseControl.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeExpenseControl.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await _transactionService.GetAllAsync();
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto)
    {
        try
        {
            var transaction = await _transactionService.CreateAsync(dto);
            return Ok(transaction);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message }); 
        }
    }
}