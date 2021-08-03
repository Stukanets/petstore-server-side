using Autofac;
using Kpi.ServerSide.AutomationFramework.Assignment.Assignment;
using Kpi.ServerSide.AutomationFramework.Assignment.User;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Assignment;
using Kpi.ServerSide.AutomationFramework.Model.Domain.User;
using Kpi.ServerSide.AutomationFramework.Model.Platform.Communication;
using Kpi.ServerSide.AutomationFramework.Platform.Communication;
using Kpi.ServerSide.AutomationFramework.Platform.Configuration.Environment;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Serilog;
using Serilog.Events;

namespace Kpi.ServerSide.AutomationFramework.Bootstrap
{
    public class Bootstraper
    {
        private ContainerBuilder _builder;

        public ContainerBuilder Builder => _builder ??= new ContainerBuilder();

        public void ConfigureServices(IConfigurationBuilder configurationBuilder)
        {
            var configurationRoot = configurationBuilder.Build();

            // Serilog
            Builder.Register<ILogger>((c, p) => new LoggerConfiguration()
                .WriteTo.File(
                    "log.txt",
                    LogEventLevel.Verbose,
                    "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Debug()
                .CreateLogger())
                .SingleInstance();

            // Configurations
            Builder.Register<IEnvironmentConfiguration>(context => configurationRoot.Get<EnvironmentConfiguration>())
                .SingleInstance();

            Builder.RegisterType<Client>().As<IClient>().InstancePerDependency();
            Builder.RegisterType<RestClient>().As<IRestClient>().InstancePerDependency();

            // Api Clients
            Builder.RegisterType<UserApiClient>().As<IUserApiClient>().SingleInstance();
            Builder.RegisterType<AssignmentApiClient>().As<IAssignmentApiClient>().SingleInstance();

            // Logic
            Builder.RegisterType<UserContext>().As<IUserContext>().InstancePerDependency();
            Builder.RegisterType<AssignmentContext>().As<IAssignmentContext>().SingleInstance();
        }
    }
}
