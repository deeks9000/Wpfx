using Demo_14_DataTemplate.Converters;
using Demo_14_DataTemplate.Models;
using Demo_14_DataTemplate.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Demo_14_DataTemplate;

public class MainWindow : Window
{
    private PersonViewModel vm;

    public MainWindow(PersonViewModel personViewModel)
    {
        vm = personViewModel;
        DataContext = vm;
        Title = "Demo 14 DataTemplate";
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
                                x.ItemsSource = vm.Persons;
                                x.ItemTemplate = BuildPersonDataTemplate();
                                x.Margin = ThicknessX(0);
                                x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                                x.HorizontalAlignment = HorizontalAlignment.Stretch;
                                x.BorderThickness = ThicknessX(0);
                                x.SetBinding(ListBox.SelectedItemProperty, BindingX(nameof(vm.SelectedPerson)));
                            }
                        )
                    ]
                ),
                BuildGridSplitter(column: 1),
                BuildPersonDetails(column: 2)
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

    private UIElement BuildPersonDetails(int column)
    {
        return GridX(
            configure: x => {
                Grid.SetColumn(x, column);
                x.Background = Brushes.LightSkyBlue;
                x.AddRow(GridUnitType.Auto);
                x.AddRow(GridUnitType.Auto);
                x.AddRow(GridUnitType.Auto);
                x.AddRow(GridUnitType.Auto);
                x.AddRow(GridUnitType.Auto);
                x.AddRow();
                x.AddColumn(GridUnitType.Auto);
                x.AddColumn();
            },
            children: [
                LabelX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        Grid.SetColumn(x, 0);
                        x.Content = "First Name:";
                    }
                ),
                TextBoxX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        Grid.SetColumn(x, 1);
                        x.Margin = ThicknessX(5);
                        x.SetBinding(TextBox.TextProperty, BindingX(PathStringX((PersonViewModel vm) => vm.SelectedPerson!.FirstName)));
                    }
                ),

                LabelX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        Grid.SetColumn(x, 0);
                        x.Content = "Last Name:";
                    }
                ),
                TextBoxX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        Grid.SetColumn(x, 1);
                        x.Margin = ThicknessX(5);
                        x.SetBinding(TextBox.TextProperty, BindingX(PathStringX((PersonViewModel vm) => vm.SelectedPerson!.LastName)));
                    }
                ),

                LabelX(
                    configure: x => {
                        Grid.SetRow(x, 2);
                        Grid.SetColumn(x, 0);
                        x.Content = "Email:";
                    }
                ),
                TextBoxX(
                    configure: x => {
                        Grid.SetRow(x, 2);
                        Grid.SetColumn(x, 1);
                        x.Margin = ThicknessX(5);
                        x.SetBinding(TextBox.TextProperty, BindingX(PathStringX((PersonViewModel vm) => vm.SelectedPerson!.Email)));
                    }
                ),

                LabelX(
                    configure: x => {
                        Grid.SetRow(x, 3);
                        Grid.SetColumn(x, 0);
                        x.Content = "Code:";
                    }
                ),
                TextBoxX(
                    configure: x => {
                        Grid.SetRow(x, 3);
                        Grid.SetColumn(x, 1);
                        x.Margin = ThicknessX(5);
                        x.SetBinding(TextBox.TextProperty, BindingX(PathStringX((PersonViewModel vm) => vm.SelectedPerson!.Code)));
                    }
                ),

                LabelX(
                    configure: x => {
                        Grid.SetRow(x, 4);
                        Grid.SetColumn(x, 0);
                        x.Content = "Department:";
                    }
                ),
                TextBoxX(
                    configure: x => {
                        Grid.SetRow(x, 4);
                        Grid.SetColumn(x, 1);
                        x.Margin = ThicknessX(5);
                        x.SetBinding(TextBox.TextProperty, BindingX(PathStringX((PersonViewModel vm) => vm.SelectedPerson!.Department)));
                    }
                )
            ]
        );
    }
     
    private DataTemplate BuildPersonDataTemplate()
    {
        var visualTree = FrameworkElementFactoryX<Border>(
            setters: [
                SetterX(Border.MarginProperty, ThicknessX(2,5,2,5)),
                SetterX(Border.PaddingProperty, ThicknessX(10)),
                SetterX(Border.CornerRadiusProperty, CornerRadiusX(4)),

                SetterX(Border.BackgroundProperty, BindingX(b => {
                    b.Path = new PropertyPath(nameof(ListBoxItem.IsSelected));
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
                                SetterX(TextBlock.TextProperty, MultiBindingX(mb => {
                                    mb.Bindings.Add(BindingX(nameof(Person.FirstName)));
                                    mb.Bindings.Add(BindingX(nameof(Person.LastName)));
                                    mb.StringFormat = "{1}, {0}";
                                }))
                            ]
                        ),
                        FrameworkElementFactoryX<TextBlock>(
                            setters: [
                                SetterX(TextBlock.FontSizeProperty, 12.0),
                                SetterX(TextBlock.ForegroundProperty, Brushes.Gray),
                                SetterX(TextBlock.TextProperty, BindingX(nameof(Person.Email)))
                            ]
                        )
                    ]
                )
            ]
        );

        return DataTemplateX<Person>(visualTree);
    }
}
