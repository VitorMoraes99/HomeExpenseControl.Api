using HomeExpenseControl.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeExpenseControl.WebAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> People { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            
            entity.HasMany(p => p.Transactions)
                  .WithOne(t => t.Person)
                  .HasForeignKey(t => t.PersonId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(400);
            entity.Property(e => e.Purpose).IsRequired();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(400);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(e => e.Type).IsRequired();
            
            entity.HasOne(t => t.Category)
                  .WithMany()
                  .HasForeignKey(t => t.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict); // Não permite deletar categoria se ela estiver em uso
        });
    }
}