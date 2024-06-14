using Domain.Models;

namespace Application.Interfaces;

public interface IAuthManager
{
    string GeneratedToken(User user);
}
