using HomeExpenseControl.WebAPI.DTOs;
using HomeExpenseControl.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
            return BadRequest(new { message = ex.Message }); 
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateTransactionDto dto)
    {
        try
        {
            var updated = await _transactionService.UpdateAsync(id, dto);
            
            if (!updated) 
                return NotFound(new { message = "Transação não encontrada." });
                
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _transactionService.DeleteAsync(id);
        
        if (!deleted) 
            return NotFound(new { message = "Transação não encontrada." });
            
        return NoContent();
    }
}