using Avalonia;
using Avalonia.ReactiveUI;

namespace TIKSN.Leveret;

class Program
{
    public static void Main(string[] args)
    {
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        var app = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .UseReactiveUI();

        return app;
    }
}
