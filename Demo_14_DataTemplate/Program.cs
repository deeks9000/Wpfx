using Demo_14_DataTemplate.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Demo_14_DataTemplate;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var services = new ServiceCollection();
        services.AddSingleton<PersonViewModel>();
        services.AddSingleton<MainWindow>();

        var sp = services.BuildServiceProvider();

        var app = new Application();

        var win = sp.GetRequiredService<MainWindow>();

        app.Run(win);
    }   
}
