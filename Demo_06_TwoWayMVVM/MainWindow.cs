using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Demo_06_TwoWayMVVM;

public class MainWindow : Window
{
    private MainViewModel vm;
    private TextBox? _textBox1;
    private TextBox? _textBox2;
    private Brush? _backgroundBrush;
    private ColorAnimation? _upColorAnimation;
    private ColorAnimation? _downColorAnimation;

    public MainWindow(MainViewModel mainViewModel)
    {
        _backgroundBrush = new SolidColorBrush(Colors.White);

        vm = mainViewModel;
        DataContext = vm;
        Title = "Demo 06 TwoWay MVVM";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 500;
        Content = Build();

        Loaded += async (s, e) => {
            _textBox1?.CaretIndex = _textBox1.Text.Length;
            _textBox1?.Focus();

            _upColorAnimation = new ColorAnimation
            {
                To = Colors.RoyalBlue,
                BeginTime = TimeSpan.FromMilliseconds(300),
                Duration = TimeSpan.FromMilliseconds(100)
            };

            _downColorAnimation = new ColorAnimation
            {
                To = Colors.White,
                Duration = TimeSpan.FromMilliseconds(100)
            };
        };
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.AddRow();
                x.AddRow(GridUnitType.Auto);
            },
            children: [
                GridX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.AddRow();
                        x.AddRow(GridUnitType.Auto);
                        x.AddRow();
                    },
                    children: [
                        BorderX(
                            configure: x => {
                                Grid.SetRow(x, 0);
                                x.Background = Brushes.White;
                                x.SnapsToDevicePixels = true;
                                x.BorderThickness = ThicknessX(0, 1, 0, 1);
                                x.BorderBrush = Brushes.LightGray;
                            },
                            child: TextBoxX(
                                configure: x => {
                                    _textBox1 = x;
                                    x.BorderThickness = ThicknessX(0);
                                    x.Background = Brushes.Transparent;
                                    x.FontSize = 14;
                                    x.TextWrapping = TextWrapping.Wrap;
                                    x.AcceptsReturn = true;
                                    x.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                                    x.HorizontalAlignment = HorizontalAlignment.Stretch;
                                    x.VerticalAlignment = VerticalAlignment.Stretch;
                                    x.Margin = ThicknessX(0);
                                    x.SetBinding(TextBox.TextProperty, BindingX(b => {
                                        b.Path = PropertyPathX(nameof(vm.Text));
                                        b.Mode = BindingMode.TwoWay;
                                        b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                                    }));
                                }
                            )
                        ),
                        BuildRowGridSplitter(row: 1),
                        BorderX(
                            configure: x => {
                                Grid.SetRow(x, 2);
                                x.Background = Brushes.LemonChiffon;
                                x.SnapsToDevicePixels = true;
                                x.BorderThickness = ThicknessX(0, 1, 0, 1);
                                x.BorderBrush = Brushes.LightGray;
                            },
                            child: TextBoxX(
                                configure: x => {
                                    _textBox2 = x;
                                    x.BorderThickness = ThicknessX(0);
                                    x.Background = Brushes.Transparent;
                                    x.IsReadOnly = true;
                                    x.TextWrapping = TextWrapping.Wrap;
                                    x.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                                    x.FontSize = 28;
                                    x.HorizontalAlignment = HorizontalAlignment.Stretch;
                                    x.VerticalAlignment = VerticalAlignment.Stretch;
                                    x.SetBinding(TextBox.TextProperty, BindingX(nameof(vm.Text)));
                                }
                            )
                        ),
                    ]
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.Background = BrushX(242,242,242);
                        x.SnapsToDevicePixels = true;
                    },
                    child: StackPanelX(
                        configure: x => {
                            x.Orientation = Orientation.Horizontal;
                        },
                        children: [
                            ButtonX(
                                configure: x => {
                                    x.Content = "Clear text";
                                    x.Margin = ThicknessX(10);
                                    x.Padding = ThicknessX(5);
                                    x.HorizontalAlignment = HorizontalAlignment.Left;
                                    x.VerticalAlignment = VerticalAlignment.Center;
                                    x.SetBinding(Button.CommandProperty, BindingX(nameof(vm.ClearTextCommand)));
                                }
                            ),
                            ButtonX(
                                configure: x => {
                                    x.Content = "Default text";
                                    x.Margin = ThicknessX(0,10,10,10);
                                    x.Padding = ThicknessX(5);
                                    x.HorizontalAlignment = HorizontalAlignment.Left;
                                    x.VerticalAlignment = VerticalAlignment.Center;
                                    x.SetBinding(Button.CommandProperty, BindingX(nameof(vm.DefaultTextCommand)));
                                }
                            )
                        ]
                    )
                )
            ]
        );
    }

    private UIElement BuildRowGridSplitter(int row)
    {
        return GridSplitterX(
            configure: x => {
                Grid.SetRow(x, row);
                x.Background = _backgroundBrush;
                x.Height = 4;
                x.HorizontalAlignment = HorizontalAlignment.Stretch;
                x.VerticalAlignment = VerticalAlignment.Stretch;
                x.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
                x.SnapsToDevicePixels = true;
                               
                x.MouseEnter += (s, e) =>
                {
                    _backgroundBrush?.BeginAnimation(SolidColorBrush.ColorProperty, _upColorAnimation);
                };

                x.MouseLeave += (s, e) =>
                {
                    _backgroundBrush?.BeginAnimation(SolidColorBrush.ColorProperty, _downColorAnimation);
                };
            }
        );
    }
}
