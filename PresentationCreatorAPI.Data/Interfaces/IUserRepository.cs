namespace PresentationCreatorAPI.Data.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<List<User>?> GetAllIncludeAsync();
    Task<User?> GetByIdIncludeAsync(int? id);
}
