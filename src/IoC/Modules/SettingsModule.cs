


using Autofac;
using Hermes.Gateway.Ocelot.Extensions;
using Hermes.Gateway.Ocelot.IoC.Settings;
using Microsoft.Extensions.Configuration;

namespace Hermes.Gateway.Ocelot.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration configuration;

        public SettingsModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(configuration.GetSettings<ServiceBusSettings>())
                   .SingleInstance();
            builder.RegisterInstance(configuration.GetSettings<JwtSettings>())
                   .SingleInstance();
        }
    }
}
