namespace Data.Repositories;

internal class PaymentRepository(AppDbContext dbContext) : GenericRepository<Payment>(dbContext), IPaymentRepository
{
}
