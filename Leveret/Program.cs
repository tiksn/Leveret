using Avalonia;
using Avalonia.Logging.Serilog;
using Microsoft.Extensions.DependencyInjection;
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
                .LogToDebug();

        private static void Main(string[] args)
        {
            var configurationRootSetup = new ConfigurationRootSetup(args);
            var configurationRoot = configurationRootSetup.GetConfigurationRoot();
            var compositionRootSetup = new CompositionRootSetup(configurationRoot);
            var serviceProvider = compositionRootSetup.CreateServiceProvider();
            BuildAvaloniaApp().Start<MainWindow>(() => serviceProvider.GetRequiredService<MainWindowViewModel>());
        }
    }
}