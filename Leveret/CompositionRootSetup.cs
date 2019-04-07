using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TIKSN.Analytics.Logging;
using TIKSN.DependencyInjection;
using TIKSN.Leveret.BusinessLogic.Calculation;
using TIKSN.Leveret.ViewModels;

namespace TIKSN.Leveret
{
    public class CompositionRootSetup : AutofacPlatformCompositionRootSetupBase
    {
        public CompositionRootSetup(IConfigurationRoot configurationRoot) : base(configurationRoot)
        {
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<CalculationService>().As<ICalculationService>().SingleInstance();
            builder.RegisterType<LoggingSetup>().As<ILoggingSetup>().SingleInstance();
        }

        protected override void ConfigureOptions(IServiceCollection services, IConfigurationRoot configuration)
        {
        }
    }
}
