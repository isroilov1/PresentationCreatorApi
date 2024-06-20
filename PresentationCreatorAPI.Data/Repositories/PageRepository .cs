using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Interfaces;

namespace PresentationCreatorAPI.Data;

internal class PageRepository(AppDbContext dbContext) : GenericRepository<Page>(dbContext), IPageRepository
{
}
