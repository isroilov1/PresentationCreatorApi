using Microsoft.EntityFrameworkCore;
using PresentationCreatorAPI.Data;
using PresentationCreatorAPI.Data.Interfaces;

namespace PresentationCreatorAPI.Repositories;

public class UserRepository(AppDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<List<User>?> GetAllIncludeAsync()
        => await _dbContext.Users.Include(u => u.Notifications).Include(p => p.Payments).ToListAsync();

    public async Task<User?> GetByEmailAsync(string email)
        => await _dbContext.Users.Include(u => u.Notifications).Include(p => p.Payments).FirstOrDefaultAsync(mail => mail.Email == email);

    public async Task<User?> GetByIdIncludeAsync(int? id)
        => await _dbContext.Users.Include(u => u.Notifications).Include(p => p.Payments).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        => await _dbContext.Users.Include(u => u.Notifications).Include(p => p.Payments).FirstOrDefaultAsync(phone => phone.PhoneNumber == phoneNumber);
}
