using Demo_15_DataTemplate2.Converters;
using Demo_15_DataTemplate2.Models;
using Demo_15_DataTemplate2.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Demo_15_DataTemplate2;

public class MainWindow : Window
{
    private CatViewModel vm;

    public MainWindow(CatViewModel catViewModel)
    {
        vm = catViewModel;
        DataContext = vm;
        Title = "Demo 15 DataTemplate2";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 450;
        Content = Build();
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.AddColumn(GridUnitType.Pixel, 300);
                x.AddColumn(GridUnitType.Auto);
                x.AddColumn();
            },
            children: [
                GridX(
                    configure: x => {
                        Grid.SetColumn(x, 0);
                    },
                    children: [
                        ListBoxX(
                            configure: x => {
                                x.ItemsSource = vm.Cats;
                                x.ItemTemplate = BuildCatDataTemplate();
                                x.Margin = ThicknessX(0);
                                x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                                x.HorizontalAlignment = HorizontalAlignment.Stretch;
                                x.BorderThickness = ThicknessX(0);
                                x.SetBinding(ListBox.SelectedItemProperty, BindingX(nameof(vm.SelectedCat)));
                            }
                        )
                    ]
                ),
                BuildGridSplitter(column: 1),
                BuildCatDetails(column: 2)
            ]
        );
    }

    private UIElement BuildGridSplitter(int column)
    {
        return GridSplitterX(
            configure: x => {
                Grid.SetColumn(x, column);
                x.Background = Brushes.White;
                x.Width = 8;
                x.HorizontalAlignment = HorizontalAlignment.Stretch;
                x.VerticalAlignment = VerticalAlignment.Stretch;
                x.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
                x.SnapsToDevicePixels = true;

                x.MouseEnter += (s, e) => {
                    var gs = s as GridSplitter;
                    gs?.Background = Brushes.CornflowerBlue;
                };

                x.MouseLeave += (s, e) => {
                    var gs = s as GridSplitter;
                    gs?.Background = Brushes.White;
                };
            }
        );
    }

    private UIElement BuildCatDetails(int column)
    {
        return GridX(
            configure: x => {
                Grid.SetColumn(x, column);
                x.Background = Brushes.Gold;
            },
            children: [
                ImageX(
                    configure: x => {
                        x.HorizontalAlignment = HorizontalAlignment.Center;
                        x.VerticalAlignment = VerticalAlignment.Center;
                        x.Stretch = Stretch.Uniform;
                        x.SetBinding(Image.SourceProperty, BindingX(b => {
                            b.Path = PropertyPathX(PathStringX((CatViewModel vm) => vm.SelectedCat!.ImageUrl));
                            b.Converter = ImageCacheConverter.Instance;
                            b.Mode = BindingMode.OneWay;
                        }));
                    }
                )
            ]
        );
    }

    private DataTemplate BuildCatDataTemplate()
    {
        var visualTree = FrameworkElementFactoryX<Border>(
            setters: [
                SetterX(Border.MarginProperty, ThicknessX(2,5,2,5)),
                SetterX(Border.PaddingProperty, ThicknessX(10)),
                SetterX(Border.CornerRadiusProperty, CornerRadiusX(4)),
                SetterX(Border.BackgroundProperty, BindingX(b => {
                    b.Path = PropertyPathX(nameof(ListBoxItem.IsSelected));
                    b.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ListBoxItem), 1);
                    b.Converter = new IsSelectedBackgroundConverter();
                }))
            ],
            children: [
                FrameworkElementFactoryX<StackPanel>(
                    setters: [
                        SetterX(StackPanel.OrientationProperty, Orientation.Vertical)
                    ],
                    children: [
                        FrameworkElementFactoryX<TextBlock>(
                            setters: [
                                SetterX(TextBlock.FontWeightProperty, FontWeights.Bold),
                                SetterX(TextBlock.TextProperty, BindingX(b => {
                                    b.Path = new PropertyPath(nameof(Cat.Type));
                                    b.StringFormat = "Type: {0}";
                                }))
                            ]
                        ),
                        FrameworkElementFactoryX<TextBlock>(
                            setters: [
                                SetterX(TextBlock.FontSizeProperty, 12.0),
                                SetterX(TextBlock.ForegroundProperty, Brushes.Gray),
                                SetterX(TextBlock.TextProperty, BindingX(b => {
                                    b.Path = new PropertyPath(nameof(Cat.Name));
                                    b.StringFormat = "Name: {0}";
                                }))
                            ]
                        )
                    ]
                )
            ]
        );

        return DataTemplateX<Cat>(visualTree);
    }
}
