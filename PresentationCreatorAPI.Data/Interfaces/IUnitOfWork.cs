namespace PresentationCreatorAPI.Interfaces;
public interface IUnitOfWork
{
    IPresentationRepository Presentation{ get; }
    IPaymentRepository Payment { get; }
    INotificationRepository Notification { get; }
    IUserRepository User { get; }
}
