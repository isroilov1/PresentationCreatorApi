using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PresentationRepository(AppDbContext dbContext) : GenericRepository<Presentation>(dbContext), IPresentationRepository
{
}