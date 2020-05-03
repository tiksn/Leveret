using Autofac;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using TIKSN.Analytics.Logging;
using TIKSN.DependencyInjection;
using TIKSN.Leveret.Interpretation.Abstractions;
using TIKSN.Leveret.Interpretation.MondConcretion;
using TIKSN.Leveret.Interpretation.MondConcretion.Factories;
using TIKSN.Leveret.Interpretation.MondConcretion.Handlers;
using TIKSN.Leveret.Interpretation.MondConcretion.Messages;
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
            builder.RegisterType<InterpretationService>().As<IInterpretationService>().SingleInstance();
            builder.RegisterType<MondStateFactory>().As<IMondStateFactory>().SingleInstance();

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

        protected override IEnumerable<ILoggingSetup> GetLoggingSetups()
        {
            yield return new LoggingSetup();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddMediatR(GetType().Assembly);
        }
    }
}