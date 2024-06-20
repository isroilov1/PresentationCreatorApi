namespace Data.Repositories;

public class NotificationRepository(AppDbContext dbContext) : GenericRepository<Notification>(dbContext), INotificationRepository
{
}
