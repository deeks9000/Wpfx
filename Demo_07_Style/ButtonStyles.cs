using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Demo_07_Style;

public static class ButtonStyles
{
    public static ResourceDictionary Build()
    {
        var visualTree = FrameworkElementFactoryX<Border>(
            name: "PART_Border",
            setters: [
                SetterX(Border.PaddingProperty, TemplateBindingX(Control.PaddingProperty)),
                SetterX(Border.BackgroundProperty, TemplateBindingX(Border.BackgroundProperty)),
                SetterX(Border.CornerRadiusProperty, TemplateBindingX(Border.CornerRadiusProperty)),
                SetterX(Border.RenderTransformOriginProperty, PointX(0.5, 0.5)),
                SetterX(Border.SnapsToDevicePixelsProperty, true)
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

        var template = ControlTemplateX<Button>(
            visualTree: visualTree,
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
        );

        var defaultButtonStyle = StyleX<Button>(
            setters: [
                SetterX(Border.CornerRadiusProperty, CornerRadiusX(4)),
                SetterX(Control.TemplateProperty, template),
            ]
        );

        var primaryButtonStyle = StyleX<Button>(
            basedOn: defaultButtonStyle,
            setters: [
                SetterX(Border.CornerRadiusProperty, CornerRadiusX(16)),
                SetterX(Control.CursorProperty, Cursors.Hand)
            ]
        );

        ResourceDictionary dict = new ResourceDictionary();
        dict.Add(typeof(Button), defaultButtonStyle);           // KEY: Type
        dict.Add("PrimaryButton", primaryButtonStyle);          // KEY: String

        return dict;
    }

    public static ResourceDictionary Build_Flat()
    {
        // ---- Visual Tree ----
        var visualTree = new FrameworkElementFactory(typeof(Border));
        visualTree.Name = "PART_Border";
        visualTree.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Border.BackgroundProperty));
        visualTree.SetValue(Border.CornerRadiusProperty, new TemplateBindingExtension(Border.CornerRadiusProperty));
        visualTree.SetValue(Border.BorderThicknessProperty, new Thickness(1));
        visualTree.SetValue(Border.RenderTransformOriginProperty, new Point(0.5, 0.5));

        var contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
        contentPresenter.Name = "PART_Presenter";
        contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
        contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
        contentPresenter.SetValue(ContentPresenter.MarginProperty, new TemplateBindingExtension(Control.PaddingProperty));
        
        visualTree.AppendChild(contentPresenter);


        // ---- ControlTemplate ----
        var template = new ControlTemplate(typeof(Button));
         
        // Trigger: IsMouseOver
        var mouseOverTrigger = new Trigger
        {
            Property = UIElement.IsMouseOverProperty,
            Value = true
        };
        mouseOverTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.LightGray, "PART_Border"));
        mouseOverTrigger.Setters.Add(new Setter(FrameworkElement.RenderTransformProperty, ScaleTransformX(1, 1), targetName: "PART_Border"));

        // Trigger: IsPressed
        var pressedTrigger = new Trigger
        {
            Property = Button.IsPressedProperty,
            Value = true
        };
        pressedTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.LightGreen, "PART_Border"));
        pressedTrigger.Setters.Add(new Setter(FrameworkElement.RenderTransformProperty, ScaleTransformX(0.96, 0.96), targetName: "PART_Border"));

        template.VisualTree = visualTree;
        template.Triggers.Add(mouseOverTrigger);
        template.Triggers.Add(pressedTrigger);


        // ---- Default Style ----
        var defaultButtonStyle = new Style(typeof(Button));
        defaultButtonStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(4)));
        defaultButtonStyle.Setters.Add(new Setter(Control.TemplateProperty, template));

        // ---- Named Style 1 ----
        var primaryButtonStyle = new Style(typeof(Button));
        primaryButtonStyle.BasedOn = defaultButtonStyle;
        primaryButtonStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(16)));
        primaryButtonStyle.Setters.Add(new Setter(Control.CursorProperty, Cursors.Hand));

        // ---- ResourceDictionary ----
        ResourceDictionary dict = new ResourceDictionary();
        dict.Add(typeof(Button), defaultButtonStyle);
        dict.Add("PrimaryButton", primaryButtonStyle);

        return dict;
    }
}
