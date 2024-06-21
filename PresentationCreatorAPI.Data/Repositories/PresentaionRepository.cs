using PresentationCreatorAPI.Data;
using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PresentationCreatorAPI.Repositories;

public class PresentationRepository(AppDbContext dbContext) : GenericRepository<Presentation>(dbContext), IPresentationRepository
{

    public async Task<int> CreatePresentationAsync(Presentation presentation)
    {
        await _dbContext.Presentation.AddAsync(presentation);
        await _dbContext.SaveChangesAsync();
        return presentation.Id;
    }
}