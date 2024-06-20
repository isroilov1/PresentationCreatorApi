using PresentationCreatorAPI.Data;
using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Data.Interfaces;

namespace PresentationCreatorAPI.Repositories;

internal class PaymentRepository(AppDbContext dbContext) : GenericRepository<Payment>(dbContext), IPaymentRepository
{
}
