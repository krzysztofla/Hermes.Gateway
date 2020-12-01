using System.Threading.Tasks;

namespace Hermes.Gateway.Infrastructure.ServiceBus
{
    public interface IMessageBroker
    {
        Task SendMessagesAsync(params object[] events);
    }
}