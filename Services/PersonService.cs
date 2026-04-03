using HomeExpenseControl.WebAPI.Data;
using HomeExpenseControl.WebAPI.DTOs;
using HomeExpenseControl.WebAPI.Models;
using HomeExpenseControl.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeExpenseControl.WebAPI.Services;

public class PersonService : IPersonService
{
    private readonly AppDbContext _context;

    public PersonService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PersonResponseDto>> GetAllAsync()
    {
        var people = await _context.People.ToListAsync();
        return people.Select(p => new PersonResponseDto(p.Id, p.Name, p.Age));
    }

    public async Task<PersonResponseDto?> GetByIdAsync(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return null;
        
        return new PersonResponseDto(person.Id, person.Name, person.Age);
    }

    public async Task<PersonResponseDto> CreateAsync(CreatePersonDto dto)
    {
        var person = new Person
        {
            Name = dto.Name,
            Age = dto.Age
        };

        _context.People.Add(person);
        await _context.SaveChangesAsync();

        return new PersonResponseDto(person.Id, person.Name, person.Age);
    }

    public async Task<bool> UpdateAsync(int id, UpdatePersonDto dto)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return false;

        person.Name = dto.Name;
        person.Age = dto.Age;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return false;

        _context.People.Remove(person);
        await _context.SaveChangesAsync();
        return true;
    }
}