using System.Windows;
using System.Windows.Controls;

namespace Demo_04_ComboBox;

public class MainWindow : Window
{
    private ComboBox? _combo;
    private TextBlock? _txtInfo;

    public MainWindow()
    {
        Title = "Demo 04 ComboBox";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 600;
        Height = 400;
        Content = Build();
    }

    private UIElement Build()
    {
        return StackPanelX(
            children: [
                TextBlockX(
                    configure: x => {
                        _txtInfo = x;
                        x.Margin = ThicknessX(10);
                        x.Text = $"Selected: {_combo?.SelectedItem}";
                    }
                ),
                ComboBoxX(
                    configure: x => {
                        _combo = x;
                        x.Margin = ThicknessX(10);
                        x.HorizontalAlignment = HorizontalAlignment.Left;
                        x.Width = 200;

                        x.SelectionChanged += (s,e) => {
                            _txtInfo?.Text = $"Selected: {_combo?.SelectedItem}";
                        };

                        x.Items.Add("Apple");
                        x.Items.Add("Banana");
                        x.Items.Add("Cherry");
                        x.SelectedIndex = 0;
                    }
                )
            ]
        );
    }
}
