namespace PresentationCreatorAPI.Data.Interfaces;
public interface IUnitOfWork
{
    IPresentationRepository Presentation{ get; }
    IPaymentRepository Payment { get; }
    INotificationRepository Notification { get; }
    IUserRepository User { get; }
    IPageRepository Page { get; }
}
