using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.DTOs.UserDtos;
public class AddUserDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int ReferalId { get; set; }

    public static implicit operator User(AddUserDto dto)
    {
        return new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Password = dto.Password,
            ReferalId = dto.ReferalId,
            ReferalBonus = false
        };
    }
}
