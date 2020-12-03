using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hermes.Gateway.Infrastructure.ServiceBus;
using Hermes.Gateway.Ocelot.Infrastructure.ServiceBus;
using Hermes.Gateway.Ocelot.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.Logging;
using Ocelot.Middleware;

namespace Hermes.Gateway.Infrastructure
{
    public class AsynchronousRoutesMiddleware : IMiddleware
    {
        private readonly IMessageBroker messageBroker;
        private readonly IDictionary<string, AsynchronousRouteOptions> routes;
        public AsynchronousRoutesMiddleware(IMessageBroker messageBroker, IOptions<AsynchronousRoutesOptions> asynchronousRoutesOptions)
        {
            this.messageBroker = messageBroker;
            this.routes = asynchronousRoutesOptions.Value.Routes;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var routingKey = GetKeyFormRequest(context);
            var isKeyPresent = routes.TryGetValue(routingKey, out var route);

            if (routes is null || !routes.Any())
            {
                await next(context);
                return;
            }

            if(!isKeyPresent)
            {
                await next(context);
                return;
            }

            await messageBroker.SendMessagesAsync(new HermesMessage(route.Queue, route.Topic, routingKey, context.Request.Body.ToString()));

        }

        private string GetKeyFormRequest(HttpContext context)
        {
            return $"{context.Request.Method} {context.Request.Path}";
        }
    }
}