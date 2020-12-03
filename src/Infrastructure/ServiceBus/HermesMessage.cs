using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hermes.Gateway.Ocelot.Infrastructure.ServiceBus
{
    public class HermesMessage
    {
        public HermesMessage(string queue, string topic, string routingKey, string message)
        {
            CorelationId = Guid.NewGuid().ToString("N");
            MessageId = Guid.NewGuid().ToString("N");
            Queue = queue;
            Topic = topic;
            RoutingKey = routingKey;
            Message = message;
        }

        public string CorelationId { get; set; }
        public string MessageId { get; set; }
        public string Message { get; set; }
        public string Queue { get; set; }
        public string Topic { get; set; }
        public string RoutingKey { get; set; }
    }
}
