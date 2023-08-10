using OnMed.Persistance.Dtos.Notifications;

namespace OnMed.Service.Interfaces.Notifications;

public interface ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage);
}
