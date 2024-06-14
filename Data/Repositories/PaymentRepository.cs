using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

internal class PaymentRepository(AppDbContext dbContext) : GenericRepository<Payment>(dbContext), IPaymentRepository
{
}
