using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

internal class PageRepository(AppDbContext dbContext) : GenericRepository<Page>(dbContext), IPageRepository
{
}
