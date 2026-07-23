using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_07_Style;

public static class ButtonStyles
{
    public static ResourceDictionary Build()
    {
        // ---- Default style ----
        var defaultButtonStyle = StyleX<Button>(
            setters: [
                SetterX(
                    property: Control.TemplateProperty,
                    value: ControlTemplateX<Button>(
                        visualTree: FrameworkElementFactoryX<Border>(
                            name: "PART_Border",
                            setters: [
                                SetterX(Border.BackgroundProperty, TemplateBindingX(Border.BackgroundProperty)),
                                SetterX(Border.CornerRadiusProperty, CornerRadiusX(4)),
                                SetterX(Border.RenderTransformOriginProperty, PointX(0.5, 0.5)),
                                SetterX(Border.SnapsToDevicePixelsProperty, true)
                            ],
                            children: [
                                FrameworkElementFactoryX<ContentPresenter>(
                                    setters: [
                                        SetterX(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                                        SetterX(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center),
                                        SetterX(ContentPresenter.MarginProperty, ThicknessX(10))
                                    ]
                                )
                            ]
                        ),
                        triggers: [
                            TriggerX(
                                property: UIElement.IsMouseOverProperty,
                                value: true,
                                setters: [
                                    SetterX(Border.BackgroundProperty, Brushes.LightGray, targetName: "PART_Border"),
                                    SetterX(FrameworkElement.RenderTransformProperty, ScaleTransformX(1,1), targetName: "PART_Border")
                                ]
                            ),
                            TriggerX(
                                property: Button.IsPressedProperty,
                                value: true,
                                setters: [
                                    SetterX(Border.BackgroundProperty, Brushes.LightGreen, targetName: "PART_Border"),
                                    SetterX(FrameworkElement.RenderTransformProperty, ScaleTransformX(0.96,0.96), targetName: "PART_Border")
                                ]
                            )
                        ]
                    )
                )
            ]
        );


        // ---- Named style 1 ----
        var primaryButtonStyle = StyleX<Button>(
            basedOn: defaultButtonStyle,
            setters: [
                SetterX(Control.BackgroundProperty, Brushes.Cyan),
                SetterX(Control.ForegroundProperty, Brushes.DarkBlue)
            ]
        );
        

        // ---- Resource Dictionary ----
        ResourceDictionary dict = new ResourceDictionary();
        dict.Add(typeof(Button), defaultButtonStyle);           // Key: Type
        dict.Add("PrimaryButton", primaryButtonStyle);          // Key: String

        return dict;
    }


    public static ResourceDictionary Build_Old()
    {        
        // ---- Default style ----

        // [1] Create the template
        var template = new ControlTemplate(typeof(Button));

        var border = new FrameworkElementFactory(typeof(Border));
        border.Name = "PART_Border";
        border.SetValue(Border.BackgroundProperty, Brushes.DarkOrange);
        border.SetValue(Border.BorderBrushProperty, Brushes.Black);
        border.SetValue(Border.BorderThicknessProperty, new Thickness(1));

        var contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
        contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
        contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
        contentPresenter.SetValue(ContentPresenter.MarginProperty, new Thickness(10));

        border.AppendChild(contentPresenter);
        template.VisualTree = border;

        // [2] Add triggers inside Template.Triggers

        // IsMouseOver trigger
        var mouseOverTrigger = new Trigger
        {
            Property = UIElement.IsMouseOverProperty,
            Value = true
        };
        mouseOverTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.Orange, "PART_Border"));
        template.Triggers.Add(mouseOverTrigger);

        // IsPressed trigger
        var pressedTrigger = new Trigger
        {
            Property = Button.IsPressedProperty,
            Value = true
        };
        pressedTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.LightGreen, "PART_Border"));
        template.Triggers.Add(pressedTrigger);

        // [3] Create style
        var defaultButtonStyle = new Style(typeof(Button));
        defaultButtonStyle.Setters.Add(new Setter(Control.TemplateProperty, template));

                
        // ---- Named style 1 ----
        var primaryButtonStyle = new Style(typeof(Button));
        primaryButtonStyle.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.RoyalBlue));
        primaryButtonStyle.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.White));
                            
        
        // ---- Resource Dictionary ----
        ResourceDictionary dict = new ResourceDictionary();
        dict.Add(typeof(Button), defaultButtonStyle);
        dict.Add("PrimaryButton", primaryButtonStyle);

        return dict;
    }
}