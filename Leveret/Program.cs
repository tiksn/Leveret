using Avalonia;
using Avalonia.Logging.Serilog;
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
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel());
        }
    }
}