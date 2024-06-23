using Application.DTOs.UserDtos;
using Newtonsoft.Json;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.Common.Utils;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Domain.Entites;
using System.Net;
namespace PresentationCreatorAPI.Application.Services;

public class UserService(IUnitOfWork unitOfWork,
                         IRedisService redisService)
    : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRedisService _redisService = redisService;
    private const string CACHE_KEY = "users";

    public async Task DeleteAsync(int id)
    {
        if (id == 1 || id == 2)
            throw new StatusCodeException(HttpStatusCode.BadRequest, "Bosh adminni o'chirish mumkin emas!");
        var user = await _unitOfWork.User.GetByIdIncludeAsync(id);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        await _unitOfWork.User.DeleteAsync(user);
        throw new StatusCodeException(HttpStatusCode.OK, "Foydalanuvchi muvaffaqqliyatli o'chirildi");
    }

    //public async Task<List<UserDto>> GetAllAsync()
    //{
    //    var users = await _unitOfWork.User.GetAllIncludeAsync();
    //    if (users is null)
    //        throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchilar topilmadi!");
    //    var tzTashkent = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tashkent");
    //    return users.Select(user =>
    //    {
    //        var userDto = (UserDto)user;
    //        var tashkentTime = TimeZoneInfo.ConvertTimeFromUtc(user.CreatedAt, tzTashkent);
    //        userDto.CreatedAt = tashkentTime.ToString("dd-MM-yyyy HH:mm");
    //        return userDto;
    //    }).ToList();
    //}

    public async Task<string> GetAllAsync(PaginationParams @params)
    {
        var entities = await _redisService.GetAsync(CACHE_KEY);

        if (entities is not null)
        {
            var data = JsonConvert.DeserializeObject<List<UserDto>>(entities);

            return JsonConvert.SerializeObject(data, Formatting.Indented); //Formatting.Indented
        }

        var users = await _unitOfWork.User.GetAllAsync();
        var json = JsonConvert.SerializeObject(users);
        await _redisService.SetAsync(CACHE_KEY, json);
        return JsonConvert.SerializeObject(users.Select(u => (UserDto)u), Formatting.Indented);
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.User.GetByIdIncludeAsync(id);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        return (UserDto)user;
    }

    public async Task<UserDto> GetByPhoneNumberAsync(string phoneNumber)
    {
        var user = await _unitOfWork.User.GetByPhoneNumberAsync(phoneNumber);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        return (UserDto)user;
    }

    public async Task<UserDto> GetUserAsync(int id)
    {
        var user = await _unitOfWork.User.GetByIdIncludeAsync(id);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
        return (UserDto)user;
    }

    public async Task UpdateAsync(int id, UpdateUserDto dto)
    {
        var model = await _unitOfWork.User.GetByIdIncludeAsync(id);
        if (model is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");
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
        throw new StatusCodeException(HttpStatusCode.OK, "Foydalanuvchi ma'lumotlari yangilandi");
    }
}

