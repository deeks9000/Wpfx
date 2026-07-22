using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_02_Grid;

public class MainWindow : Window
{
    private bool _state = false;
    private Border? _border;

    public MainWindow()
    {
        Title = "Demo 02 Grid";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 500;
        Content = Build();
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.AddColumnDefinitionX();
                x.AddColumnDefinitionX();
                x.AddColumnDefinitionX();
            },
            children: [
                BorderX(
                    configure: x => {
                        Grid.SetColumn(x, 0);
                        x.Background = Brushes.Coral;
                    }
                ),
                BorderX(
                    configure: x => {
                        Grid.SetColumn(x, 1);
                        x.Background = Brushes.White;
                        _border = x;
                    },
                    child: ButtonX(
                        configure: x => {
                            x.Content = "CLICK ME";
                            x.Margin = ThicknessX(10);
                            x.Padding = ThicknessX(10);
                            x.HorizontalAlignment = HorizontalAlignment.Center;
                            x.VerticalAlignment = VerticalAlignment.Center;

                            x.Click += (s,e) => {
                                if (_state)
                                {
                                        _state = false;
                                    _border?.Background = Brushes.White;
                                }
                                else
                                {
                                    _state = true;
                                    _border?.Background = Brushes.Gold;
                                }
                            };
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetColumn(x, 2);
                        x.Background = Brushes.SkyBlue;
                    }
                )
            ]
        );
    }
}
