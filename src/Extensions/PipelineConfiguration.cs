using Ocelot.Middleware;

namespace src.Extensions
{
    public class PipelineConfiguration
    {
        public static OcelotPipelineConfiguration GetOcelotConfiguration() => new OcelotPipelineConfiguration {
            AuthenticationMiddleware = async (context, next) => 
            {
               
            }
        };
    }
}