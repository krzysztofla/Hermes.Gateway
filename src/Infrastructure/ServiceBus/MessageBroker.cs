using Hermes.Gateway.Ocelot.Extensions.Markers;
using Hermes.Gateway.Ocelot.Infrastructure.ServiceBus;
using Hermes.Gateway.Ocelot.IoC.Settings;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Gateway.Infrastructure.ServiceBus
{
    public class MessageBroker : IMessageBroker, IService
    {
        private readonly ILogger<MessageBroker> logger;
        private readonly ServiceBusSettings serviceBusSettings;
        public MessageBroker(ServiceBusSettings serviceBusSettings, ILogger<MessageBroker> logger)
        {
            this.logger = logger;
            this.serviceBusSettings = serviceBusSettings;
        }

        public async Task SendMessagesAsync(HermesMessage hermesMessage)
        {
            try
            {
                    var topicClient = new TopicClient(serviceBusSettings.ConnectionString, hermesMessage.Topic);

                    var messageBody = JsonConvert.SerializeObject(hermesMessage.Message);
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    message.CorrelationId = hermesMessage.CorelationId;

                    await topicClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                logger.LogInformation($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
