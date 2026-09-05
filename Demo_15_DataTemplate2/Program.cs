using Demo_15_DataTemplate2.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Demo_15_DataTemplate2;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var services = new ServiceCollection();
        services.AddSingleton<CatViewModel>();
        services.AddSingleton<MainWindow>();

        var sp = services.BuildServiceProvider();

        var app = new Application();

        //var win = new MainWindow();
        var win = sp.GetRequiredService<MainWindow>();

        app.Run(win);
    }   
}
