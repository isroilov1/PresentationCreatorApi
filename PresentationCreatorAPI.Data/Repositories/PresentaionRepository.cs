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

    public async Task<List<Presentation>?> GetAllIncludeAsync()
         => await _dbContext.Presentation.Include(u => u.Pages).ToListAsync();

    public async Task<Presentation?> GetByIdIncludeAsync(int id)
        => await _dbContext.Presentation.Include(u => u.Pages).FirstOrDefaultAsync(p => p.Id == id);
}