using PresentationCreatorAPI.Data;
using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Interfaces;

namespace PresentationCreatorAPI.Repositories;
public class NotificationRepository(AppDbContext dbContext) : GenericRepository<Notification>(dbContext), INotificationRepository
{
}
