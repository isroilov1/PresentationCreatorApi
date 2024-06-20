using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.Interfaces;

public interface IPageService
{
    Task CreateAsync(Page page);
    Task UpdateAsync(Page page);
    Task<Page> GetByIdAsync(int id);
    Task<List<Page>> GetAllPagesAsync();
    Task DeleteAsync(int id);
}
