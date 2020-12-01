using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hermes.Gateway.Infrastructure.ServiceBus;
using Hermes.Gateway.Ocelot.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

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

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}