using System.Windows;

namespace Demo_07_Style;

public static class Program
{
    [STAThread]
    public static void Main()
    { 
        var app = new Application();

        app.Resources.MergedDictionaries.Add(ButtonStyles.Build());

        var win = new MainWindow();

        app.Run(win);
    }
}
