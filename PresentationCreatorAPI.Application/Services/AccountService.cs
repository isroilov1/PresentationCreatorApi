using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.Common.Security;
using PresentationCreatorAPI.Application.Common.Validators;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Enums;
using System.Net;

namespace PresentationCreatorAPI.Application.Services;

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

        if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        if (!user.Password.Equals(PasswordHasher.GetHash(login.Password)))
            throw new StatusCodeException(HttpStatusCode.Conflict, "Telefon raqam yoki parol noto'g'ri kiritildi.");
        if (!user.IsVerified)
            throw new StatusCodeException(HttpStatusCode.BadRequest, "Foydalanuvchi verificatsiyadan o'tmagan!");

        var referalId = user.ReferalId;
        var referalUser = await _ofWork.User.GetByIdIncludeAsync(referalId);

        if (referalUser is not null && !user.ReferalBonus)
        {
            user.ReferalBonus = true;
            await _ofWork.User.UpdateAsync(user);

            referalUser.Balance += 1000;
            var recipientIds = new List<int> { referalUser.Id };
            var notification = new Notification
            {
                Message = $"{user.FullName} muvaffaqqiyatli qo'shildi. Sizning hisobingizga 1000 so'm referal bonusi yuborildi!",
                Status = NotificationStatus.NotRead,
                Type = NotificationType.Input,
                SenderId = 1,
                RecipientIds = recipientIds
            };
            await _ofWork.Notification.CreateAsync(notification);

            if (referalUser.Notifications == null)
                referalUser.Notifications = new List<Notification>();
            referalUser.Notifications.Add(notification);
            await _ofWork.User.UpdateAsync(referalUser);
        }
        return _auth.GeneratedToken(user);
    }

    public async Task<bool> RegistrAsync(AddUserDto dto)
    {
        var user = await _ofWork.User.GetByEmailAsync(dto.Email);

        if (user is not null) throw new StatusCodeException(HttpStatusCode.AlreadyReported, "Ushbu email bilan allaqachon ro'yxatdan o'tilgan!");

        var userbyphone = await _ofWork.User.GetByPhoneNumberAsync(dto.PhoneNumber);

        if (userbyphone is not null) throw new StatusCodeException(HttpStatusCode.AlreadyReported, "Ushbu telefon raqami bilan allaqachon ro'yxatdan o'tilgan!");

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
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        var code = GeneratedCode();
        _cache.Set(email, code, TimeSpan.FromSeconds(120));
        await _emailService.SendMessageAsync(email, "Verificatsiya kodi!", code);
    }

    public async Task<bool> CheckCodeAsync(string email, string code)
    {
        var user = await _ofWork.User.GetByEmailAsync(email);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        if (!_cache.TryGetValue(email, out var result))
            throw new StatusCodeException(HttpStatusCode.BadRequest, "Kod amal qilish muddati tugagan");
        if (!code.Equals(result))
            throw new StatusCodeException(HttpStatusCode.Conflict, "Kod noto'g'ri!");

        user.IsVerified = true;
        await _ofWork.User.UpdateAsync(user);

        return true;
    }
    private string GeneratedCode()
        => (new Random().Next(10000, 99999)).ToString();
}

