using System.Windows;

namespace Demo_01_Simple;

public static class Program
{
    [STAThread]
    public static void Main()
    { 
        var app = new Application();

        var win = new MainWindow();

        app.Run(win);
    }
}
