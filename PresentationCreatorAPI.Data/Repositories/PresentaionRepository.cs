using PresentationCreatorAPI.Data;
using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Data.Interfaces;

namespace PresentationCreatorAPI.Repositories;

public class PresentationRepository(AppDbContext dbContext) : GenericRepository<Presentation>(dbContext), IPresentationRepository
{
}