
using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository(AppDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<List<User>?> GetAllIncludeAsync()
        => await _dbContext.Users.Include(u => u.Notifications).ToListAsync();

    public async Task<User?> GetByEmailAsync(string email)
        => await _dbContext.Users.FirstOrDefaultAsync(mail => mail.Email == email);

    public async Task<User?> GetByIdIncludeAsync(int id)
        => await _dbContext.Users.Include(u => u.Notifications).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        => await _dbContext.Users.FirstOrDefaultAsync(mail => mail.PhoneNumber == phoneNumber);
}
