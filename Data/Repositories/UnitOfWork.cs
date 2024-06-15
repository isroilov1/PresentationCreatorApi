namespace Data.Repositories;
public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public IPresentationRepository Presentation => new PresentationRepository(_appDbContext);

    public IPaymentRepository Payment => new PaymentRepository(_appDbContext);

    public INotificationRepository Notification => new NotificationRepository(_appDbContext);

    public IUserRepository User => new UserRepository(_appDbContext);
}
