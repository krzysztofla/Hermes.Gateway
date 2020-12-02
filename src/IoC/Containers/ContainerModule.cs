using Autofac;
using Hermes.Gateway.Ocelot.IoC.Modules;
using Microsoft.Extensions.Configuration;

namespace Hermes.Gateway.Ocelot.IoC.Containers
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration configuration;

        public ContainerModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<ServiceBusModule>();
            builder.RegisterModule(new SettingsModule(configuration));
        }
    }
}
