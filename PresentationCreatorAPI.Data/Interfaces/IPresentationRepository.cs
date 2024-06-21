using Microsoft.EntityFrameworkCore;
using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Data.Interfaces;

public interface IPresentationRepository : IGenericRepository<Presentation>
{
    Task<int> CreatePresentationAsync(Presentation presentation);
}
