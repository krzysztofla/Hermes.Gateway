using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hermes.Gateway.Infrastructure;
using Hermes.Gateway.Ocelot.Extensions;
using Hermes.Gateway.Ocelot.Infrastructure.Settings;
using Hermes.Gateway.Ocelot.IoC.Containers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Gateway.Ocelot
{
    public class Program
    {
        public static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false)
                        .Build();

            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new ContainerModule(config));
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddJsonFile("ocelot.json")
                        .AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(builder => builder
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();
                    services.AddTransient<AsynchronousRoutesMiddleware>();

                    using var provider = services.BuildServiceProvider();
                    var configuration = provider.GetService<IConfiguration>();
                    services.Configure<AsynchronousRoutesOptions>(configuration.GetSection("AsynchronousRoutes"));
                    services.AddControllers();

                    var authKey = "wH-qwzfHo7sFi7oqphfoA_WdOrwZOJhXFAoecINcFAo";
                    services.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(authKey, x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authKey)),
                            ValidateIssuer = true,
                            ValidIssuer = "Hermes",
                            ValidateAudience = false
                        };
                    });
                    services.AddOcelot();
                })
                .UseIISIntegration()
                .Configure(app =>
                {
                    app.UseWebSockets();
                    app.UseMiddleware<AsynchronousRoutesMiddleware>();
                    app.UseOcelot(PipelineConfiguration.GetOcelotConfiguration()).GetAwaiter().GetResult();
                })
                );
        }
    }
}
