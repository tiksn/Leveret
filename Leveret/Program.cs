using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using TIKSN.Leveret.ViewModels;
using TIKSN.Leveret.Views;

namespace TIKSN.Leveret
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel());
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
    }
}
