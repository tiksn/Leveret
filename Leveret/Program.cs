using Avalonia;
using Avalonia.ReactiveUI;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using TIKSN.Leveret.ViewModels;
using TIKSN.Leveret.Views;

namespace TIKSN.Leveret
{
    internal class Program
    {
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .UseTXFX()
                .LogToTrace();

        private static void Main(string[] args)
        {
            Console.Title = "Leveret";
            var configurationRootSetup = new ConfigurationRootSetup(args);
            var configurationRoot = configurationRootSetup.GetConfigurationRoot();
            var compositionRootSetup = new CompositionRootSetup(configurationRoot);
            var serviceProvider = compositionRootSetup.CreateServiceProvider();

            var parser = new Parser(settings =>
                         {
                             settings.AutoHelp = true;
                             settings.AutoVersion = true;
                             settings.IgnoreUnknownArguments = false;
                             settings.HelpWriter = Console.Out;
                         });
            parser.ParseArguments<CommandLineOptions>(args)
                .WithParsed(options =>
                {
                    var mainWindowViewModel = serviceProvider.GetRequiredService<MainWindowViewModel>();

                    if (options.File != null)
                    {
                        mainWindowViewModel.InputSourceCode = File.ReadAllText(options.File);
                    }

                    BuildAvaloniaApp().Start<MainWindow>(() => mainWindowViewModel);
                });
        }
    }
}