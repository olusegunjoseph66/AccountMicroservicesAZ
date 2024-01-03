using Shared.ExternalServices.DTOs;

namespace Shared.ExternalServices.Interfaces
{
    public interface IMessagingService
    {
        Task PublishTopicMessage(dynamic message, string subscriberName);
        Task PublishTopicMessage(List<string> messages, string subscriberName);
    }
}
