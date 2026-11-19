using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TrpoLab3.Lib;
using TrpoLab4.WpfApp.ViewModels;
using TrpoLab4.WpfApp.Views;

namespace TrpoLab4.WpfApp;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        services.AddSingleton<ConeCalculator>();
        services.AddSingleton<ConeViewModel>();
        services.AddSingleton<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();
        _serviceProvider.GetRequiredService<MainWindow>().Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();
        base.OnExit(e);
    }
}
