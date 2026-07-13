using System.Windows;

namespace Demo_08_FluentTheme;

public static class Program
{
    [STAThread]
    public static void Main()
    { 
        var app = new Application();

        app.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/PresentationFramework.Fluent;component/Themes/Fluent.xaml", UriKind.Absolute)
        });

        var win = new MainWindow();

        app.Run(win);
    }
}
