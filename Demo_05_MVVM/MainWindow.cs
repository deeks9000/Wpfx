using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_05_MVVM;

public class MainWindow : Window
{
    private MainViewModel vm;

    public MainWindow(MainViewModel mainViewModel)
    {
        vm = mainViewModel;
        DataContext = vm;
        Title = "Demo 05 MVVM";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 900;
        Height = 600;
        Content = Build();
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.AddRow(GridUnitType.Auto);
                x.AddRow(GridUnitType.Auto);
                x.AddRow(GridUnitType.Auto);
                x.AddRow(GridUnitType.Auto);
                x.AddRow();
            },
            children: [
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.Background = Brushes.LightGray;
                    },
                    child: ButtonX(
                        configure: x => {
                            x.Content = "Update message";
                            x.Margin = ThicknessX(10);
                            x.Padding = ThicknessX(10);
                            x.HorizontalAlignment = HorizontalAlignment.Left;
                            x.VerticalAlignment = VerticalAlignment.Center;
                            x.SetBinding(Button.CommandProperty, BindingX(nameof(vm.UpdateMessage)));
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.Background = Brushes.Gold;
                    },
                    child: TextBoxX(
                        configure: x => {
                            x.Background = Brushes.Transparent;
                            x.IsReadOnly = true;
                            x.TextWrapping = TextWrapping.Wrap;
                            x.BorderThickness = ThicknessX(0);
                            x.FontSize = 24;
                            x.Margin = ThicknessX(10);
                            x.HorizontalAlignment = HorizontalAlignment.Left;
                            x.VerticalAlignment = VerticalAlignment.Center;
                            x.SetBinding(TextBox.TextProperty, BindingX(nameof(vm.Message)));
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 2);
                        x.Background = Brushes.LightBlue;
                    },
                    child: TextBoxX(
                        configure: x => {
                            x.Background = Brushes.Transparent;
                            x.IsReadOnly = true;
                            x.TextWrapping = TextWrapping.Wrap;
                            x.BorderThickness = ThicknessX(0);
                            x.FontSize = 24;
                            x.Margin = ThicknessX(10);
                            x.HorizontalAlignment = HorizontalAlignment.Left;
                            x.VerticalAlignment = VerticalAlignment.Center;
                            x.SetBinding(TextBox.TextProperty, MultiBindingX(mb => {
                                mb.Bindings.Add(BindingX(nameof(vm.FirstName)));
                                mb.Bindings.Add(BindingX(nameof(vm.MiddleName)));
                                mb.Bindings.Add(BindingX(nameof(vm.LastName)));
                                mb.StringFormat = "First: {0}, Middle: {1}, Last: {2}";
                            }));
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 3);
                        x.Background = Brushes.LightYellow;
                        x.BorderBrush = Brushes.Gold;
                        x.BorderThickness= ThicknessX(4);
                    },
                    child: TextBoxX(
                        configure: x => {
                            x.Background = Brushes.Transparent;
                            x.Foreground = Brushes.DarkOrange;
                            x.FontWeight = FontWeights.SemiBold;
                            x.IsReadOnly = true;
                            x.TextWrapping = TextWrapping.Wrap;
                            x.BorderThickness = ThicknessX(0);
                            x.FontSize = 24;
                            x.Margin = ThicknessX(10);
                            x.HorizontalAlignment = HorizontalAlignment.Left;
                            x.VerticalAlignment = VerticalAlignment.Center;
                            x.SetBinding(TextBox.TextProperty, BindingX(b => {
                                b.Path = PropertyPathX(PathStringX((MainViewModel vm) => vm.SelectedCat!.Type));
                                b.StringFormat = "Type: {0}";
                            }));
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 4);
                        x.Background = Brushes.Azure;
                    },
                    child: ImageX(
                        configure: x => {
                            x.HorizontalAlignment = HorizontalAlignment.Center;
                            x.VerticalAlignment = VerticalAlignment.Center;
                            x.Stretch = Stretch.Uniform;
                            x.SetBinding(Image.SourceProperty, BindingX(PathStringX((MainViewModel vm) => vm.SelectedCat!.ImageUrl)));
                        }
                    )
                )
            ]
        );
    }
}
