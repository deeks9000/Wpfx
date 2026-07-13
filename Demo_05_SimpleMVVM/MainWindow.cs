using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_05_SimpleMVVM;

public class MainWindow : Window
{
    private MainViewModel vm;

    public MainWindow(MainViewModel mainViewModel)
    {
        vm = mainViewModel;

        DataContext = vm;
        Title = "Demo 05 Simple MVVM";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 500;
        Content = Build();
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.RowDefinitions.Add(RowDefinitionX(GridUnitType.Auto));
                x.RowDefinitions.Add(RowDefinitionX(GridUnitType.Star, 1));
            },
            children: [
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.Background = Brushes.LightGray;
                    },
                    child: [
                        ButtonX(
                            configure: x => {
                                x.Content = "Update message";
                                x.Margin = ThicknessX(10);
                                x.Padding = ThicknessX(10);
                                x.HorizontalAlignment = HorizontalAlignment.Left;
                                x.VerticalAlignment = VerticalAlignment.Center;
                                x.SetBinding(Button.CommandProperty, BindingX(nameof(vm.UpdateMessage)));
                            }
                        )
                    ]
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.Background = Brushes.LemonChiffon;
                    },
                    child: [
                        TextBoxX(
                            configure: x => {
                                x.Background = Brushes.Transparent;
                                x.IsReadOnly = true;
                                x.TextWrapping = TextWrapping.Wrap;
                                x.BorderThickness = ThicknessX(0);
                                x.FontSize = 24;
                                x.Margin = ThicknessX(10, 10, 10, 10);
                                x.HorizontalAlignment = HorizontalAlignment.Left;
                                x.VerticalAlignment = VerticalAlignment.Top;
                                x.SetBinding(TextBox.TextProperty, BindingX(nameof(vm.Message)));
                            }
                        )
                    ]
                )
            ]
        );
    }
}
