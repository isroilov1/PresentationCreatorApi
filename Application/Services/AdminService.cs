namespace Application.Services;

public class AdminService(IUnitOfWork work) : IAdminService
{
    private readonly IUnitOfWork _work = work;

    public async Task ChangeUserRoleAsync(int id)
    {
        var user = await _work.User.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        if (user.Id == 1)
            throw new StatusCodeExeption(HttpStatusCode.BadRequest, "Jinnilik qilma!");
        
        user.Role = user.Role == Role.Admin ? Role.User : Role.Admin;
        await _work.User.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _work.User.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        await _work.User.DeleteAsync(user);
    }

    public async Task<List<User>> GetAllAdminAsync()
        => (await _work.User.GetAllAsync())
            .Where(p => p.Role == Role.Admin)
            .ToList();

    public async Task GieveBonusAsync(int id, int bonus)
    {
        var user = await _work.User.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        user.Balance += bonus;
        await _work.User.UpdateAsync(user);
    }
}
