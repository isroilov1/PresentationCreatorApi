using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.Interfaces;

public interface IAdminService
{
    Task ChangeUserRoleAsync(int id);
    Task DeleteUserAsync(int id);
    Task<List<User>> GetAllAdminAsync();
    Task GieveBonusAsync(int id, int bonus);
    Task UpdateBalanceAsync(UpdateUserBalanceDto dto);
}
