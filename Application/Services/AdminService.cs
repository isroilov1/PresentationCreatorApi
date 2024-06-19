
namespace Application.Services;

public class AdminService(IUnitOfWork work) : IAdminService
{
    private readonly IUnitOfWork _unitOfWork = work;

    public async Task ChangeUserRoleAsync(int id)
    {
        var user = await _unitOfWork.User.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        if (user.Id == 1)
            throw new StatusCodeExeption(HttpStatusCode.BadRequest, "Jinnilik qilma!");
        
        user.Role = user.Role == Role.Admin ? Role.User : Role.Admin;
        await _unitOfWork.User.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _unitOfWork.User.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        await _unitOfWork.User.DeleteAsync(user);
    }

    public async Task<List<User>> GetAllAdminAsync()
        => (await _unitOfWork.User.GetAllAsync())
            .Where(p => p.Role == Role.Admin)
            .ToList();

    public async Task GieveBonusAsync(int id, int bonus)
    {
        var user = await _unitOfWork.User.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        user.Balance += bonus;
        await _unitOfWork.User.UpdateAsync(user);
    }

    public async Task UpdateBalanceAsync(UpdateUserBalanceDto dto)
    {
        var model = await _unitOfWork.User.GetByIdIncludeAsync(dto.Id);
        if (model is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        dto.Balance = dto.IsAdd ? model.Balance + dto.Balance : model.Balance - dto.Balance;

        var user = (User)dto;
        user.FullName = model.FullName;
        user.PhoneNumber = model.PhoneNumber;
        user.Email = model.Email;
        user.CreatedAt = model.CreatedAt;
        user.Password = model.Password;
        user.ReferalId = model.ReferalId;
        user.PresentationCount = model.PresentationCount;
        user.IsVerified = model.IsVerified;
        user.TotalPayments = model.TotalPayments;

        await _unitOfWork.User.UpdateAsync(user);
        throw new StatusCodeExeption(HttpStatusCode.OK, "Foydalanuvchi balansi yangilandi");
    }
}
