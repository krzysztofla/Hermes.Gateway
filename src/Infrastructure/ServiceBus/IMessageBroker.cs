using Hermes.Gateway.Ocelot.Infrastructure.ServiceBus;
using System.Threading.Tasks;

namespace Hermes.Gateway.Infrastructure.ServiceBus
{
    public interface IMessageBroker
    {
        Task SendMessagesAsync(HermesMessage hermesMessage);
    }
}