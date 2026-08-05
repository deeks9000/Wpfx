using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Demo_06_TwoWayMVVM;

public class MainWindow : Window
{
    private MainViewModel vm;
    private TextBox? _textBox;

    public MainWindow(MainViewModel mainViewModel)
    {
        vm = mainViewModel;
        DataContext = vm;
        Title = "Demo 06 TwoWay MVVM";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 500;
        Content = Build();

        Loaded += async (s, e) => {
            _textBox?.CaretIndex = _textBox.Text.Length;
            _textBox?.Focus();
        };
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.AddRow(GridUnitType.Pixel, 100);
                x.AddRow();
                x.AddRow(GridUnitType.Auto);
            },
            children: [
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.Background = Brushes.White;
                    },
                    child: TextBoxX(
                        configure: x => {
                            _textBox = x;
                            x.Background = Brushes.Transparent;
                            x.FontSize = 14;
                            x.TextWrapping = TextWrapping.Wrap;
                            x.AcceptsReturn = true;
                            x.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                            x.HorizontalAlignment = HorizontalAlignment.Stretch;
                            x.VerticalAlignment = VerticalAlignment.Stretch;
                            x.Margin = ThicknessX(0);
                            x.SetBinding(TextBox.TextProperty, BindingX(b => {
                                b.Path = PropertyPathX(nameof(vm.Message));
                                b.Mode = BindingMode.TwoWay;
                                b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            }));
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.Background = Brushes.LemonChiffon;
                    },
                    child: TextBoxX(
                        configure: x => {
                            x.Background = Brushes.Transparent;
                            x.IsReadOnly = true;
                            x.TextWrapping = TextWrapping.Wrap;
                            x.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                            x.BorderThickness = ThicknessX(0);
                            x.FontSize = 28;
                            x.HorizontalAlignment = HorizontalAlignment.Stretch;
                            x.VerticalAlignment = VerticalAlignment.Stretch;
                            x.SetBinding(TextBox.TextProperty, BindingX(nameof(vm.Message)));
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 2);
                        x.Background = Brushes.LightGray;
                    },
                    child: StackPanelX(
                        configure: x => {
                            x.Orientation = Orientation.Horizontal;
                        },
                        children: [
                            ButtonX(
                                configure: x => {
                                    x.Content = "Clear message";
                                    x.Margin = ThicknessX(10);
                                    x.Padding = ThicknessX(10);
                                    x.HorizontalAlignment = HorizontalAlignment.Left;
                                    x.VerticalAlignment = VerticalAlignment.Center;
                                    x.SetBinding(Button.CommandProperty, BindingX(nameof(vm.ClearMessage)));
                                }
                            ),
                            ButtonX(
                                configure: x => {
                                    x.Content = "Default message";
                                    x.Margin = ThicknessX(10);
                                    x.Padding = ThicknessX(10);
                                    x.HorizontalAlignment = HorizontalAlignment.Left;
                                    x.VerticalAlignment = VerticalAlignment.Center;
                                    x.SetBinding(Button.CommandProperty, BindingX(nameof(vm.DefaultMessage)));
                                }
                            )
                        ]
                    )
                )
            ]
        );
    }
}
