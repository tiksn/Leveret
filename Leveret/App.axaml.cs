using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TIKSN.DependencyInjection;
using TIKSN.Leveret.Interpretation.Abstractions;
using TIKSN.Leveret.Interpretation.MondConcretion;
using TIKSN.Leveret.Interpretation.MondConcretion.Factories;
using TIKSN.Leveret.ViewModels;
using TIKSN.Leveret.Views;

namespace TIKSN.Leveret;

public partial class App : Application
{
    private IHost host;

    public App()
    {
        host = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<IInterpretationService, InterpretationService>();
                services.AddSingleton<IMondStateFactory, MondStateFactory>();
                services.AddFrameworkPlatform();
                services.AddMediatR(config =>
                {
                    config.RegisterServicesFromAssembly(this.GetType().Assembly);
                    config.RegisterServicesFromAssembly(typeof(GlobalVariable).Assembly);
                    config.RegisterServicesFromAssembly(typeof(InterpretationService).Assembly);
                });
            })
            .Build();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = host.Services.GetRequiredService<MainWindow>();
            desktop.MainWindow.DataContext = host.Services.GetRequiredService<MainWindowViewModel>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}