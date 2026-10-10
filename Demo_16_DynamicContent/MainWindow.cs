using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using Demo_16_DynamicContent.Models;
using Demo_16_DynamicContent.ViewModels;

namespace Demo_16_DynamicContent;

public class MainWindow : Window
{
    private MainViewModel vm;
    private ContentControl? _slot;
    private UIElement? _tcpLayout;
    private UIElement? _udpLayout;
    private UIElement? _serialLayout;
    private UIElement? _bluetoothLayout;

    public MainWindow(MainViewModel mainViewModel)
    {
        vm = mainViewModel;
        DataContext = vm;
        Title = "Demo 16 Dynamic Content";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 500;
        Content = Build();
       
        vm.PropertyChanged += ViewModel_PropertyChanged;

        vm.Protocol = ProtocolType.Tcp;
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.AddRow(GridUnitType.Pixel, 34);
                x.AddRow();
            },
            children: [
                GridX(
                    configure: x => {
                        Grid.SetRow(x, 0); // ROW0: View selection controls
                        x.AddColumn(GridUnitType.Auto);
                        x.AddColumn(GridUnitType.Pixel, 100);
                        x.Background = Brushes.LemonChiffon;
                    },
                    children: [
                        LabelX(
                            configure: x => {
                                Grid.SetColumn(x, 0);
                                x.Margin = ThicknessX(5);
                                x.FontFamily = FontFamilyX("Consolas");
                                x.VerticalContentAlignment = VerticalAlignment.Center;
                                x.Content = "Protocol:";
                            }
                        ),
                        ComboBoxX(
                            configure: x => {
                                Grid.SetColumn(x, 1);
                                x.Margin = ThicknessX(5);
                                x.ItemsSource = Enum.GetValues<ProtocolType>();
                                x.SetBinding(ComboBox.SelectedItemProperty, BindingX(b => {
                                    b.Path = PropertyPathX(nameof(vm.Protocol));
                                    b.Mode = BindingMode.TwoWay;
                                }));
                            }
                        )
                    ]
                ),
                ContentControlX(
                    configure: x => {
                        Grid.SetRow(x, 1); // ROW1: Dynamic content
                        _slot = x;
                    }
                )
            ]
        );
    }   

    private UIElement BuildTcpLayout()
    {
        return GridX(
            configure: x => {
                x.AddRow(GridUnitType.Pixel, 80);
                x.AddRow();
            },
            children: [
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.Background = BrushX(242, 242, 242);
                    },
                    child: TextBlockX(
                        configure: x => {
                            x.Text = "TCP communications";
                            x.Margin = ThicknessX(10);
                            x.FontFamily = FontFamilyX("Consolas");
                            x.FontSize = 24;
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.Background = BrushX(222, 222, 222);
                    },
                    child: StackPanelX(
                        configure: x => {
                            Grid.SetColumn(x, 0);
                            x.Orientation = Orientation.Horizontal;
                            x.VerticalAlignment = VerticalAlignment.Top;
                        },
                        children: [
                            LabelX(
                                configure: x => {
                                    x.Content = "Host:";
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                }
                            ),
                            TextBoxX(
                                configure: x => {
                                    x.Margin = ThicknessX(5);
                                    x.FontFamily = FontFamilyX("Consolas");
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                    x.Width = 80;
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.CharacterCasing = CharacterCasing.Upper;
                                    x.SetBinding(TextBox.TextProperty, BindingX(nameof(vm.TcpHost)));
                                }
                            ),
                            LabelX(
                                configure: x => {
                                    x.Content = "Port:";
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                }
                            ),
                            TextBoxX(
                                configure: x => {
                                    x.Margin = ThicknessX(5);
                                    x.FontFamily = FontFamilyX("Consolas");
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                    x.Width = 80;
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.CharacterCasing = CharacterCasing.Upper;
                                    x.SetBinding(TextBox.TextProperty, BindingX(nameof(vm.TcpPort)));

                                    x.PreviewTextInput += (s, e) => {
                                        e.Handled = e.Text.Any(c => c < '0' || c > '9');
                                    };
                                }
                            ),
                            ButtonX(
                                configure: x => {
                                    x.Content = "Start";
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.Padding = ThicknessX(10, 0, 10, 0);
                                }
                            ),
                            ButtonX(
                                configure: x => {
                                    x.Content = "Stop";
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.Padding = ThicknessX(10, 0, 10, 0);
                                }
                            )
                        ]
                    )
                )
            ]
        );
    }

    private UIElement BuildUdpLayout()
    {
        return GridX(
            configure: x => {
                x.AddRow(GridUnitType.Pixel, 80);
                x.AddRow();
            },
            children: [
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.Background = Brushes.LightCoral;
                    },
                    child: TextBlockX(
                        configure: x => {
                            x.Text = "UDP communications";
                            x.Margin = ThicknessX(10);
                            x.FontFamily = FontFamilyX("Consolas");
                            x.FontSize = 24;
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.Background = Brushes.Coral;
                    },
                    child: StackPanelX(
                        configure: x => {
                            Grid.SetColumn(x, 0);
                            x.Orientation = Orientation.Horizontal;
                            x.VerticalAlignment = VerticalAlignment.Top;
                        },
                        children: [
                            LabelX(
                                configure: x => {
                                    x.Content = "Host:";
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                }
                            ),
                            TextBoxX(
                                configure: x => {
                                    x.Margin = ThicknessX(5);
                                    x.FontFamily = FontFamilyX("Consolas");
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                    x.Width = 80;
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.CharacterCasing = CharacterCasing.Upper;
                                    x.SetBinding(TextBox.TextProperty, BindingX(nameof(vm.UdpHost)));
                                }
                            ),
                            LabelX(
                                configure: x => {
                                    x.Content = "Port:";
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                }
                            ),
                            TextBoxX(
                                configure: x => {
                                    x.Margin = ThicknessX(5);
                                    x.FontFamily = FontFamilyX("Consolas");
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                    x.Width = 80;
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.CharacterCasing = CharacterCasing.Upper;
                                    x.SetBinding(TextBox.TextProperty, BindingX(nameof(vm.UdpPort)));

                                    x.PreviewTextInput += (s, e) => {
                                        e.Handled = e.Text.Any(c => c < '0' || c > '9');
                                    };
                                }
                            ),
                            ButtonX(
                                configure: x => {
                                    x.Content = "Start";
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.Padding = ThicknessX(10, 0, 10, 0);
                                }
                            ),
                            ButtonX(
                                configure: x => {
                                    x.Content = "Stop";
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.Padding = ThicknessX(10, 0, 10, 0);
                                }
                            )
                        ]
                    )
                )
            ]
        );
    }
       
    private UIElement BuildSerialLayout()
    {
        return GridX(
            configure: x => {
                x.AddRow(GridUnitType.Pixel, 80);
                x.AddRow();
            },
            children: [
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.Background = Brushes.LightGreen;
                    },
                    child: TextBlockX(
                        configure: x => {
                            x.Text = "Serial communications";
                            x.Margin = ThicknessX(10);
                            x.FontFamily = FontFamilyX("Consolas");
                            x.FontSize = 24;
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.Background = Brushes.LimeGreen;
                    },
                    child: StackPanelX(
                        configure: x => {
                            Grid.SetColumn(x, 0);
                            x.Orientation = Orientation.Horizontal;
                            x.VerticalAlignment = VerticalAlignment.Top;
                        },
                        children: [
                            LabelX(
                                configure: x => {
                                    x.Content = "Serial Port:";
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                }
                            ),
                            TextBoxX(
                                configure: x => {
                                    x.Margin = ThicknessX(5);
                                    x.FontFamily = FontFamilyX("Consolas");
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                    x.Width = 80;
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.CharacterCasing = CharacterCasing.Upper;
                                    x.SetBinding(TextBox.TextProperty, BindingX(b => {
                                        b.Path = PropertyPathX(nameof(vm.SerialPort));
                                        b.Mode = BindingMode.TwoWay;
                                        b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                                    }));
                                }
                            ),
                            ButtonX(
                                configure: x => {
                                    x.Content = "Start";
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.Padding = ThicknessX(10, 0, 10, 0);
                                }
                            ),
                            ButtonX(
                                configure: x => {
                                    x.Content = "Stop";
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.Padding = ThicknessX(10, 0, 10, 0);
                                }
                            ),
                        ]
                    )
                )
            ]
        );
    }

    private UIElement BuildBluetoothLayout()
    {
        return GridX(
            configure: x => {
                x.AddRow(GridUnitType.Pixel, 80);
                x.AddRow();
            },
            children: [
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.Background = Brushes.LightBlue;
                    },
                    child: TextBlockX(
                        configure: x => {
                            x.Text = "Bluetooth communications";
                            x.Margin = ThicknessX(10);
                            x.FontFamily = FontFamilyX("Consolas");
                            x.FontSize = 24;
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.Background = Brushes.SkyBlue;
                    },
                    child: StackPanelX(
                        configure: x => {
                            Grid.SetColumn(x, 0);
                            x.Orientation = Orientation.Horizontal;
                            x.VerticalAlignment = VerticalAlignment.Top;
                        },
                        children: [
                            LabelX(
                                configure: x => {
                                    x.Content = "MAC:";
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                }
                            ),
                            TextBoxX(
                                configure: x => {
                                    x.Margin = ThicknessX(5);
                                    x.FontFamily = FontFamilyX("Consolas");
                                    x.VerticalContentAlignment = VerticalAlignment.Center;
                                    x.Width = 140;
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.CharacterCasing = CharacterCasing.Upper;
                                    x.SetBinding(TextBox.TextProperty, BindingX(nameof(vm.BleMAC)));
                                }
                            ),
                            ButtonX(
                                configure: x => {
                                    x.Content = "Start";
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.Padding = ThicknessX(10, 0, 10, 0);
                                }
                            ),
                            ButtonX(
                                configure: x => {
                                    x.Content = "Stop";
                                    x.Margin = ThicknessX(5, 5, 0, 5);
                                    x.Padding = ThicknessX(10, 0, 10, 0);
                                }
                            )
                        ]
                    )
                )
            ]
        );
    }
   
    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(vm.Protocol):
                ProtocolType protocol = vm.Protocol;
                Dispatcher.InvokeAsync(() => UpdateUI_ProtocolContent(protocol));
                break;
        }
    }

    //--------------------------------------------------------------------------------------------------------
    // UI Updates
   
    private void UpdateUI_ProtocolContent(ProtocolType protocol)
    {
        switch (protocol)
        {
            case ProtocolType.Tcp:
                _slot?.Content = _tcpLayout ??= BuildTcpLayout();
                break;

            case ProtocolType.Udp:
                _slot?.Content = _udpLayout ??= BuildUdpLayout();
                break;

            case ProtocolType.Serial:
                _slot?.Content = _serialLayout ??= BuildSerialLayout();
                break;

            case ProtocolType.Bluetooth:
                _slot?.Content = _bluetoothLayout ??= BuildBluetoothLayout();
                break;

            default:
                _slot?.Content = _tcpLayout ??= BuildTcpLayout();
                break;
        }
    }
}
