namespace HomeExpenseControl.WebAPI.DTOs;

public record CreatePersonDto(string Name, int Age);
public record UpdatePersonDto(string Name, int Age);
public record PersonResponseDto(int Id, string Name, int Age);