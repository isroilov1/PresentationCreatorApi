namespace Data.Repositories;

internal class PageRepository(AppDbContext dbContext) : GenericRepository<Page>(dbContext), IPageRepository
{
}
