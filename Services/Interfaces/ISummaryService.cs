using HomeExpenseControl.WebAPI.DTOs;

namespace HomeExpenseControl.WebAPI.Services.Interfaces;

public interface ISummaryService
{
    Task<DashboardResponseDto<PersonSummaryDto>> GetPeopleSummaryAsync();
    Task<DashboardResponseDto<CategorySummaryDto>> GetCategoriesSummaryAsync();
}