using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_01_Simple;

public class MainWindow : Window
{
    private int _count = 0;
    private TextBlock? _txtInfo;

    public MainWindow()
    {
        Title = "Demo 01 Simple";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 600;
        Height = 400;
        Content = Build();
    }

    private UIElement Build()
    {
        var ui = StackPanelX(
            children: [
                ButtonX(
                    configure: x => {
                        x.Content = "Update";
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(10);
                        x.HorizontalAlignment = HorizontalAlignment.Left;
                        x.VerticalAlignment = VerticalAlignment.Top;

                        x.Click += (s,e) => {
                            _count++;
                            _txtInfo?.Text = $"Count: {_count}";
                        };
                    }
                ),               
                TextBlockX(
                    configure: x => {
                        _txtInfo = x;
                        x.Margin = ThicknessX(10);
                        x.Text = $"Count: {_count}";
                    }
                )
            ]
        );

        // Print the tree
        System.Diagnostics.Debug.WriteLine("####################");
        PrintVisualTree(ui);
        System.Diagnostics.Debug.WriteLine("####################");

        return ui;
    }

    private static void PrintVisualTree(DependencyObject obj, int indent = 0)
    {
        System.Diagnostics.Debug.WriteLine($"{new string(' ', indent)}{obj.GetType().Name}");

        int count = VisualTreeHelper.GetChildrenCount(obj);

        for (int i = 0; i < count; i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(obj, i);
            PrintVisualTree(child, indent + 2);
        }
    }
}
