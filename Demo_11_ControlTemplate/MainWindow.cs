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
        Resources = BuildLocalResourceDictionary();
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
                        x.HorizontalAlignment = HorizontalAlignment.Left;
                        x.Foreground = Brushes.Black;
                        x.Background = BrushFromStringX("#88DDFF");                        
                        x.BorderThickness = ThicknessX(0);

                        if (TryFindResource("ButtonControlTemplate") is ControlTemplate btnTemplate)
                            x.Template = btnTemplate;

                        x.MouseEnter += (s,e) => {
                            Mouse.OverrideCursor = Cursors.Hand;
                        };

                        x.MouseLeave += (s,e) => {
                            Mouse.OverrideCursor = null;
                        };

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
                        x.HorizontalAlignment = HorizontalAlignment.Left;
                        x.Foreground = Brushes.Black;
                        x.Background = BrushFromStringX("#88FFDD");
                        x.BorderThickness = ThicknessX(0,0,0,2);

                        if (TryFindResource("LocalButtonControlTemplate") is ControlTemplate btnTemplate)
                            x.Template = btnTemplate;

                        x.MouseEnter += (s,e) => {
                            Mouse.OverrideCursor = Cursors.Hand;
                        };

                        x.MouseLeave += (s,e) => {
                            Mouse.OverrideCursor = null;
                        };

                        x.Click += (s,e) => {
                            _count++;
                            _txtInfo?.Text = $"Count: {_count}";
                        };
                    }
                ),
            ]
        );
    }

    public static ResourceDictionary BuildLocalResourceDictionary()
    {
        var btnTemplate = ControlTemplateX<Button>(
            visualTree: FrameworkElementFactoryX<Border>(
                name: "PART_Border",
                setters: [
                    SetterX(Border.CornerRadiusProperty, CornerRadiusX(8)),
                    SetterX(Border.SnapsToDevicePixelsProperty, true),
                    // TemplateBindings map values from the templated control into the visual tree
                    SetterX(Border.PaddingProperty, TemplateBindingX(Control.PaddingProperty)),
                    SetterX(Border.BorderThicknessProperty, TemplateBindingX(Border.BorderThicknessProperty)),
                    SetterX(Border.BorderBrushProperty, TemplateBindingX(Border.BorderBrushProperty)),
                    SetterX(Border.BackgroundProperty, TemplateBindingX(Panel.BackgroundProperty)),
                ],
                children: [
                    FrameworkElementFactoryX<ContentPresenter>(
                        name: "PART_Presenter",
                        setters: [
                            SetterX(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                            SetterX(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center)
                        ]
                    )
                ]
            ),
            triggers: [
                TriggerX(
                    property: UIElement.IsMouseOverProperty,
                    value: true,
                    setters: [
                        SetterX(Border.BackgroundProperty, BrushFromStringX("#33aa80"), targetName: "PART_Border"),
                        SetterX(Control.ForegroundProperty, Brushes.Black, targetName: "PART_Presenter")
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

        ResourceDictionary dict = new ResourceDictionary();
        dict.Add("LocalButtonControlTemplate", btnTemplate);

        return dict;
    }
}
