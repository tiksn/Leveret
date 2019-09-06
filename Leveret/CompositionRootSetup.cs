using Autofac;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TIKSN.Analytics.Logging;
using TIKSN.DependencyInjection;
using TIKSN.Leveret.BusinessLogic.Calculation;
using TIKSN.Leveret.BusinessLogic.Factories;
using TIKSN.Leveret.BusinessLogic.Handlers;
using TIKSN.Leveret.BusinessLogic.Messages;
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
            builder.RegisterType<MondStateFactory>().As<IMondStateFactory>().SingleInstance();
            builder.RegisterType<LoggingSetup>().As<ILoggingSetup>().SingleInstance();

            builder.RegisterType<ValueFormatHandler>()
                .As<IRequestHandler<ValueFormatRequest, string>>()
                .SingleInstance();

            builder.RegisterType<ValueFormatBigIntegerBehavior>()
                .As<IPipelineBehavior<ValueFormatRequest, string>>()
                .SingleInstance();

            builder.RegisterType<ValueFormatArrayBehavior>()
                .As<IPipelineBehavior<ValueFormatRequest, string>>()
                .SingleInstance();
        }

        protected override void ConfigureOptions(IServiceCollection services, IConfigurationRoot configuration)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddMediatR(GetType().Assembly);
        }
    }
}