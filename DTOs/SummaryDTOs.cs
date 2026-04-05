namespace HomeExpenseControl.WebAPI.DTOs;

public record PersonSummaryDto(string PersonName, decimal TotalIncome, decimal TotalExpense, decimal Balance);

public record CategorySummaryDto(string CategoryDescription, decimal TotalIncome, decimal TotalExpense, decimal Balance);
public record DashboardResponseDto<T>(
    IEnumerable<T> Items,
    decimal GrandTotalIncome,
    decimal GrandTotalExpense,
    decimal GrandBalance
);