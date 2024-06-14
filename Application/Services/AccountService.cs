using Application.Common.Exceptions;
using Application.Common.Security;
using Application.Common.Validators;
using Application.DTOs.UserDtos;
using Application.Interfaces;
using Data.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Application.Services;

public class AccountService(IUnitOfWork ofWork,
                            IAuthManager authManager,
                            IValidator<User> validator,
                            IMemoryCache cache,
                            IEmailService emailService)
    : IAccountService
{

    public IAuthManager _auth = authManager;

    private readonly IUnitOfWork _ofWork = ofWork;
    private readonly IValidator<User> _validator = validator;
    private readonly IMemoryCache _cache = cache;
    private readonly IEmailService _emailService = emailService;

    public async Task<string> LoginAsync(LoginDto login)
    {
        var user = await _ofWork.User.GetByEmailAsync(login.Email);

        if (user is null) throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        if (!user.Password.Equals(PasswordHasher.GetHash(login.Password)))
            throw new StatusCodeExeption(HttpStatusCode.Conflict, "Telefon raqam yoki parol noto'g'ri kiritildi.");
        if (!user.IsVerified)
            throw new StatusCodeExeption(HttpStatusCode.BadRequest, "Foydalanuvchi verificatsiyadan o'tmagan!");

        return _auth.GeneratedToken(user);
    }
    
    public async Task<bool> RegistrAsync(AddUserDto dto)
    {
        var user = await _ofWork.User.GetByEmailAsync(dto.Email);

        if (user is not null) throw new StatusCodeExeption(HttpStatusCode.AlreadyReported, "Ushbu email bilan allaqachon ro'yxatdan o'tilgan!");

        var userbyphone = await _ofWork.User.GetByPhoneNumberAsync(dto.PhoneNumber);

        if (userbyphone is not null) throw new StatusCodeExeption(HttpStatusCode.AlreadyReported, "Ushbu telefon raqami bilan allaqachon ro'yxatdan o'tilgan!");

        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ValidatorException(result.GetErrorMessages());

        var entity = (User)dto;
        entity.Password = PasswordHasher.GetHash(entity.Password);

        await _ofWork.User.CreateAsync(entity);
        return true;
    }

    public async Task SendCodeAsync(string email)
    {
        var user = await _ofWork.User.GetByEmailAsync(email);
        if(user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        var code = GeneratedCode();
        _cache.Set(email, code, TimeSpan.FromSeconds(120));
        await _emailService.SendMessageAsync(email, "Verificatsiya kodi!", code);
    }

    public async Task<bool> CheckCodeAsync(string email, string code)
    {
        var user = await _ofWork.User.GetByEmailAsync(email);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        if (_cache.TryGetValue(email, out var result))
        {
            if (!code.Equals(result))
                throw new StatusCodeExeption(HttpStatusCode.Conflict, "Kod noto'g'ri!");

            user.IsVerified = true;
            await _ofWork.User.UpdateAsync(user);

            if (user.ReferalId != 0)
            {
                try
                {
                    var referalUser = await _ofWork.User.GetByIdAsync(user.ReferalId);
                    if (referalUser is not null)
                    {
                        referalUser.Balance += 1000;
                        await _ofWork.User.UpdateAsync(referalUser);
                    }
                }
                catch { }
            }
            return true;
        }
        else
            throw new StatusCodeExeption(HttpStatusCode.BadRequest, "Kod amal qilish muddati tugagan");
    }
    private string GeneratedCode()
        => (new Random().Next(10000, 99999)).ToString();
}
