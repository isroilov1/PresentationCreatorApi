namespace PresentationCreatorAPI.Data.Interfaces;

public interface IPresentationRepository : IGenericRepository<Presentation>
{
    Task<int> CreatePresentationAsync(Presentation presentation);
    Task<List<Presentation>?> GetAllIncludeAsync();
    Task<Presentation?> GetByIdIncludeAsync(int id);
}
