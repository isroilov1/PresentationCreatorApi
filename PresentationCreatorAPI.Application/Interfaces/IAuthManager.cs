using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.Interfaces;

public interface IAuthManager
{
    string GeneratedToken(User user);
}
