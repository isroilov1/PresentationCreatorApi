using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Domain.Entites;
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
        if (model is null && dto.PhoneNumber is not null)
        {
            model = await _work.User.GetByPhoneNumberAsync(dto.PhoneNumber);
            if (model is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        }
        else if(model is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        model.Balance = dto.IsAdd ? model.Balance + dto.Balance : model.Balance - dto.Balance;

        await _work.User.UpdateAsync(model);
    }
}

