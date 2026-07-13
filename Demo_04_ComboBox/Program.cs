using System.Windows;

namespace Demo_04_ComboBox;

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
