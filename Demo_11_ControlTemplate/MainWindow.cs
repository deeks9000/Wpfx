using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Demo_11_ControlTemplate;

public class MainWindow : Window
{
    private int _count = 0;
    private TextBlock? _txtInfo;

    public MainWindow()
    {
        Title = "Demo 11 ControlTemplate";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 450;
        Content = Build();
    }

    public UIElement Build()
    {
        return StackPanelX(
            children: [
                ButtonX(
                    configure: x => {
                        x.Content = "Click me";
                        x.Width = 120;
                        x.Height = 40;
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(10);
                        x.HorizontalAlignment = HorizontalAlignment.Left;
                        x.Background = BrushFromStringX("#88DDFF");
                        x.BorderThickness = ThicknessX(0);
                        x.Cursor = Cursors.Hand;
                        x.Template = TryFindResource("AppButtonTemplate") as ControlTemplate ?? x.Template;

                        x.Click += (s,e) => {
                            _count++;
                            _txtInfo?.Text = $"Count: {_count}";
                        };
                    }
                ),
                TextBlockX(
                    configure: x => { 
                        _txtInfo = x; 
                        x.Text = "Count: 0"; 
                        x.Margin = new Thickness(10);
                    }
                ),
                ButtonX(
                    configure: x => {
                        x.Content = "Click me too";
                        x.Width = 120;
                        x.Height = 40;
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(10);
                        x.HorizontalAlignment = HorizontalAlignment.Left;
                        x.Background = BrushFromStringX("#88FFDD");
                        x.BorderThickness = ThicknessX(0,0,0,2);
                        x.Cursor = Cursors.Hand;
                        x.Template = BuildButtonTemplate();

                        x.Click += (s,e) => {
                            _count++;
                            _txtInfo?.Text = $"Count: {_count}";
                        };
                    }
                ),
            ]
        );
    }        

    public ControlTemplate BuildButtonTemplate()
    {
        var visualTree = FrameworkElementFactoryX<Border>(
            name: "PART_Border",
            setters: [
                SetterX(Border.PaddingProperty, TemplateBindingX(Control.PaddingProperty)),
                SetterX(Border.CornerRadiusProperty, CornerRadiusX(20)),
                SetterX(Border.SnapsToDevicePixelsProperty, true),
                SetterX(Border.BorderThicknessProperty, TemplateBindingX(Border.BorderThicknessProperty)),
                SetterX(Border.BorderBrushProperty, TemplateBindingX(Border.BorderBrushProperty)),
                SetterX(Border.BackgroundProperty, TemplateBindingX(Panel.BackgroundProperty)),
            ],
            children: [
                FrameworkElementFactoryX<ContentPresenter>(
                    name: "PART_Presenter",
                    setters: [
                        SetterX(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                        SetterX(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center),
                    ]
                )
            ]
        );

        return ControlTemplateX<Button>(
            visualTree: visualTree,
            triggers: [
                TriggerX(
                    property: UIElement.IsMouseOverProperty,
                    value: true,
                    setters: [
                        SetterX(Border.BackgroundProperty, BrushFromStringX("#33aa80"), targetName: "PART_Border"),
                        SetterX(Control.ForegroundProperty, Brushes.Black, targetName: "PART_Presenter"),
                    ]
                ),
                TriggerX(
                    property: Button.IsPressedProperty,
                    value: true,
                    setters: [
                        SetterX(Border.BackgroundProperty, BrushFromStringX("#116020"), targetName: "PART_Border"),
                        SetterX(Control.ForegroundProperty, Brushes.White, targetName: "PART_Presenter")
                    ]
                )
            ]
        );
    }
}
