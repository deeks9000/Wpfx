using System.Windows;
using System.Windows.Controls;

namespace Demo_07_Style;

public class MainWindow : Window
{
    private int _count = 0;
    private TextBlock? _txtInfo;

    public MainWindow()
    {
        Title = "Demo 07 Style";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 600;
        Height = 400;
        Content = Build();
    }

    public UIElement Build()
    {
        return StackPanelX(
            children: [
                ButtonX(
                    configure: x => {
                        x.Content = "Click me with Default Button style";
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
                        x.Text = $"Count: {_count}";
                        x.Margin = ThicknessX(10);
                    }
                ),
                ButtonX(
                    configure: x => {
                        x.Style = Application.Current.TryFindResource("PrimaryButton") as Style;
                        x.Content = "Click me with Primary Button style";
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
            ]
        );
    }
}
