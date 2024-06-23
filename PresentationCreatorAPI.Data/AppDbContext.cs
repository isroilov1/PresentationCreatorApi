using Microsoft.EntityFrameworkCore;
using PresentationCreatorAPI.Domain.Entites;
using PresentationCreatorAPI.Enums;

namespace PresentationCreatorAPI.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Presentation> Presentation { get; set; }
    public DbSet<Notification> Notification { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Page> Pages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    modelBuilder.Entity<User>().HasData(
        new User
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            FullName = "Isroilov Ismoiljon",
            Email = "isroilov0905@gmail.com",
            PhoneNumber = "+998997979898",
            Password = "6724ce39c81234bc9a25eca98b634a94a913e514a2191371b63b30dd3869c754",
            Role = Role.Admin,
            IsVerified = true,
            Balance = 20000,
            PresentationCount = 0
        });
    }
}


