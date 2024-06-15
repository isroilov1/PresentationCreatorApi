namespace Application.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task DeleteAsync(int id)
    {
        if (id == 1 || id == 2)
            throw new StatusCodeExeption(HttpStatusCode.BadRequest, "Bosh adminni o'chirish mumkin emas!");
        var user = await _unitOfWork.User.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        await _unitOfWork.User.DeleteAsync(user);
        throw new StatusCodeExeption(HttpStatusCode.OK, "Foydalanuvchi muvaffaqqliyatli o'chirildi");
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _unitOfWork.User.GetAllIncludeAsync();
        if (users is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchilar topilmadi!");
        return users.Select(x => (UserDto)x).ToList();
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.User.GetByIdIncludeAsync(id);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        return (UserDto)user;
    }

    public async Task<UserDto> GetByPhoneNumberAsync(string phoneNumber)
    {
        var user = await _unitOfWork.User.GetByPhoneNumberAsync(phoneNumber);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        return (UserDto)user;
    }

    public async Task UpdateAsync(int id, UpdateUserDto dto)
    {
        var model = await _unitOfWork.User.GetByIdIncludeAsync(id);
        if (model is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        var user = (User)dto;
        user.Id = id;
        user.CreatedAt = model.CreatedAt;
        user.Password = model.Password;
        user.Balance = model.Balance;
        user.ReferalId = model.ReferalId;
        user.PresentationCount = model.PresentationCount;
        user.IsVerified = model.IsVerified;
        user.TotalPayments = model.TotalPayments;

        await _unitOfWork.User.UpdateAsync(user);
        throw new StatusCodeExeption(HttpStatusCode.OK, "Foydalanuvchi ma'lumotlari yangilandi");
    }

    public async Task UpdateBalanceAsync(UpdateUserBalanceDto dto)
    {
        var model = await _unitOfWork.User.GetByIdIncludeAsync(dto.Id);
        if (model is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
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
        throw new StatusCodeExeption(HttpStatusCode.OK, "Foydalanuvchi ma'lumotlari yangilandi");


    }
}
