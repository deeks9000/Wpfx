using System.Windows;
using System.Windows.Controls;

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
        return StackPanelX(
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
    }    
}
