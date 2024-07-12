using PresentationCreatorAPI.Application.DTOs;
using PresentationCreatorAPI.Application.DTOs.UserDtos;

namespace PresentationCreator.MVC.Areas.Admin.Models;

public class UsersViewModel
{
    public IEnumerable<UserDto> Users { get; set; } = new List<UserDto>();
    public UserDto User { get; set; } = null!;
    public AddNotificationDto Notification { get; set; } = null!;
}
