﻿using Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Common.Utils;
using PresentationCreatorAPI.Application.DTOs.UserDtos;

namespace PresentationCreatorAPI.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> GetByIdAsync(int id);
    Task<UserDto> GetUserAsync(int id);
    Task<UserDto> GetByPhoneNumberAsync(string phoneNumber);
    Task<string> GetAllAsync(PaginationParams @params);
    Task<List<UserDto>> GetAllWithoutpagination();
    Task UpdateAsync(int id, UpdateUserDto dto);
    Task DeleteAsync(int id);
}
