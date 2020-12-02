using Autofac;
using Hermes.Gateway.Ocelot.IoC.Containers;
using Microsoft.Extensions.Configuration;

namespace Hermes.Gateway.Ocelot.IoC
{
    public class SetupContainers
    {
        public IConfiguration Configuration { get; }
        public SetupContainers(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureContainers(ContainerBuilder builder)
        {
            builder.RegisterModule(new ContainerModule(Configuration));
        }
    }
}
