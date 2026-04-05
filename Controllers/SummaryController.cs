using HomeExpenseControl.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeExpenseControl.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SummaryController : ControllerBase
{
    private readonly ISummaryService _summaryService;

    public SummaryController(ISummaryService summaryService)
    {
        _summaryService = summaryService;
    }

    [HttpGet("people")]
    public async Task<IActionResult> GetPeopleSummary()
    {
        var result = await _summaryService.GetPeopleSummaryAsync();
        return Ok(result);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategoriesSummary()
    {
        var result = await _summaryService.GetCategoriesSummaryAsync();
        return Ok(result);
    }
}