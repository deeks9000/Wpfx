using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_03_GridSplitter;

public class MainWindow : Window
{
    public MainWindow()
    {
        Title = "Demo 03 GridSplitter";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 500;
        Content = Build();
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.AddColumnDefinitionX(GridUnitType.Star, 1);
                x.AddColumnDefinitionX(GridUnitType.Auto);
                x.AddColumnDefinitionX(GridUnitType.Star, 1);
            },
            children: [
                GridX(
                    configure: x => {
                        Grid.SetColumn(x, 0);
                        x.Background = Brushes.LightGreen;
                        x.AddRowDefinitionX(GridUnitType.Star, 1);
                        x.AddRowDefinitionX(GridUnitType.Auto);
                        x.AddRowDefinitionX(GridUnitType.Star, 1);
                    },
                    children: [
                        BorderX(
                            configure: x => {
                                Grid.SetRow(x, 0);
                                x.Background = Brushes.LightSkyBlue;
                                x.SnapsToDevicePixels = true;
                            },
                            child: TextBlockX(
                                configure: x => {
                                    x.HorizontalAlignment = HorizontalAlignment.Center;
                                    x.VerticalAlignment = VerticalAlignment.Center;
                                    x.Text = "Left-Top area";
                                }
                            )                            
                        ),
                        GridSplitterX(
                            configure: x => {
                                Grid.SetRow(x, 1);
                                x.Background = Brushes.White;
                                x.Height = 4;
                                x.HorizontalAlignment = HorizontalAlignment.Stretch;
                                x.VerticalAlignment = VerticalAlignment.Stretch;
                                x.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
                                x.SnapsToDevicePixels = true;

                                x.MouseEnter += (s,e) => {
                                    var gs = s as GridSplitter;
                                    gs?.Height = 6;
                                    gs?.Background = Brushes.CornflowerBlue;
                                };

                                x.MouseLeave += (s,e) => {
                                    var gs = s as GridSplitter;
                                    gs?.Height = 4;
                                    gs?.Background = Brushes.White;
                                };
                            }
                        ),
                        BorderX(
                            configure: x => {
                                Grid.SetRow(x, 2);
                                x.Background = Brushes.LightSkyBlue;
                                x.SnapsToDevicePixels = true;
                            },
                            child: TextBlockX(
                                configure: x => {
                                    x.HorizontalAlignment = HorizontalAlignment.Center;
                                    x.VerticalAlignment = VerticalAlignment.Center;
                                    x.Text = "Left-Bottom area";
                                }
                            )                            
                        ),
                    ]
                ),
                GridSplitterX(
                    configure: x => {
                        Grid.SetColumn(x, 1);
                        x.Background = Brushes.White;
                        x.Width = 4;
                        x.HorizontalAlignment = HorizontalAlignment.Stretch;
                        x.VerticalAlignment = VerticalAlignment.Stretch;
                        x.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
                        x.SnapsToDevicePixels = true;

                        x.MouseEnter += (s,e) => {
                            var gs = s as GridSplitter;
                            gs?.Width = 6;
                            gs?.Background = Brushes.CornflowerBlue;
                        };
                        
                        x.MouseLeave += (s,e) => {
                            var gs = s as GridSplitter;
                            gs?.Width = 4;
                            gs?.Background = Brushes.White;
                        };
                    }
                ),
                BorderX(
                    configure: x => {
                        Grid.SetColumn(x, 2);
                        x.Background = Brushes.LightBlue;
                        x.SnapsToDevicePixels = true;
                    },
                    child: TextBlockX(
                        configure: x => {
                            x.HorizontalAlignment = HorizontalAlignment.Center;
                            x.VerticalAlignment = VerticalAlignment.Center;
                            x.Text = "Main area";
                        }
                    )                    
                )
            ]
        );
    }
}
