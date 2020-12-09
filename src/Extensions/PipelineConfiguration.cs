using Ocelot.Middleware;

namespace Hermes.Gateway.Ocelot.Extensions
{
    public class PipelineConfiguration
    {
         public static OcelotPipelineConfiguration GetOcelotConfiguration() => new OcelotPipelineConfiguration {
             AuthenticationMiddleware = async (context, next) => 
             {
                 await next.Invoke();
                 return;
             }
         };
    }
}