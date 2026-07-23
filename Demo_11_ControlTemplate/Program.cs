using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_11_ControlTemplate;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var app = new Application();

        app.Resources = BuildAppResourceDictionary();

        var win = new MainWindow();

        app.Run(win);
    }

    public static ResourceDictionary BuildAppResourceDictionary()
    {
        var btnTemplate = ControlTemplateX<Button>(
            visualTree: FrameworkElementFactoryX<Border>(
                name: "PART_Border",
                setters: [
                    SetterX(Border.CornerRadiusProperty, CornerRadiusX(4)),
                    SetterX(Border.SnapsToDevicePixelsProperty, true),
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
                        SetterX(Border.BackgroundProperty, BrushFromStringX("#66BBEE"), targetName: "PART_Border"),
                        SetterX(Control.ForegroundProperty, Brushes.Black, targetName: "PART_Presenter")
                    ]
                ),
                TriggerX(
                    property: Button.IsPressedProperty, 
                    value: true, 
                    setters: [
                        SetterX(Border.BackgroundProperty, Brushes.Black, targetName: "PART_Border"),                        
                        SetterX(Control.ForegroundProperty, Brushes.White, targetName: "PART_Presenter")
                    ]
                )
            ]
        );

        ResourceDictionary dict = new ResourceDictionary();
        dict.Add("ButtonControlTemplate", btnTemplate);

        return dict;
    }
}
