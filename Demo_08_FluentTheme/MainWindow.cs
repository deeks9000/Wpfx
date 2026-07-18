using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Demo_08_FluentTheme;

public class MainWindow : Window
{
    private bool _isDarkMode = false;
    private TextBlock? _txtInfo;

    public MainWindow()
    {
        Title = "Demo 08 Fluent Theme";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 600;
        Height = 400;
        Content = Build();
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.AddRowDefinitionX(GridUnitType.Pixel, 100);
                x.AddRowDefinitionX(GridUnitType.Star, 1);
                x.AddRowDefinitionX(GridUnitType.Star, 1);
            },
            children: [
                ButtonX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.Content = "Switch to Dark Mode";
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(10);
                        x.HorizontalAlignment = HorizontalAlignment.Center;
                        x.VerticalAlignment = VerticalAlignment.Center;

                        x.Click += (s,e) => {
                            if (_isDarkMode == false)
                            { 
                                #pragma warning disable WPF0001
                                this.ThemeMode = ThemeMode.Dark;

                                _isDarkMode = true;
                                x.Content = "Switch to Light Mode";
                            }
                            else
                            {
                                #pragma warning disable WPF0001
                                this.ThemeMode = ThemeMode.Light;

                                 _isDarkMode = false;
                                x.Content = "Switch to Dark Mode";
                            }
                        };
                    }
                ),
                SliderX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.HorizontalAlignment = HorizontalAlignment.Center;
                        x.VerticalAlignment = VerticalAlignment.Center;
                        x.Width = 400;
                        x.Maximum = 100;
                        x.Minimum = 0;
                        x.Interval = 10;
                        x.IsSnapToTickEnabled = true;
                        x.TickPlacement = TickPlacement.BottomRight;
                        x.TickFrequency = 10;

                        x.ValueChanged += (s,e) => {
                            _txtInfo?.Text = $"Value: {e.NewValue}";
                        };
                    }
                ),
                TextBlockX(
                    configure: x => {
                        Grid.SetRow(x, 2);
                        _txtInfo = x;
                        x.HorizontalAlignment = HorizontalAlignment.Center;
                        x.VerticalAlignment = VerticalAlignment.Center;
                        x.Margin = ThicknessX(10);
                        x.Text = "Value: 0";
                    }
                )
            ]
        );
    }    
}
