using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Enums;
using System.Net;
namespace PresentationCreatorAPI.Application.Services;

public class AdminService(IUnitOfWork work) : IAdminService
{
    private readonly IUnitOfWork _work = work;

    public async Task ChangeUserRoleAsync(int id)
    {
        var user = await _work.User.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        if (user.Id == 1)
            throw new StatusCodeException(HttpStatusCode.BadRequest, "Jinnilik qilma!");

        user.Role = user.Role == Role.Admin ? Role.User : Role.Admin;
        await _work.User.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _work.User.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

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
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        user.Balance += bonus;
        await _work.User.UpdateAsync(user);
    }

    public async Task UpdateBalanceAsync(UpdateUserBalanceDto dto)
    {
        var model = await _work.User.GetByIdIncludeAsync(dto.Id);
        if (model is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
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

        await _work.User.UpdateAsync(user);
        throw new StatusCodeException(HttpStatusCode.OK, "Foydalanuvchi balansi yangilandi");
    }
}

