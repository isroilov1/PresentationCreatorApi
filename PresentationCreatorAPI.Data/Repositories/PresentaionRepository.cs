using PresentationCreatorAPI.Data;
using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Interfaces;

namespace PresentationCreatorAPI.Repositories;

public class PresentationRepository(AppDbContext dbContext) : GenericRepository<Presentation>(dbContext), IPresentationRepository
{
}