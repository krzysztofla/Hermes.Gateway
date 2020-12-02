using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hermes.Gateway.Infrastructure.ServiceBus;
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
            await next(context);
            return;
        }
    }
}