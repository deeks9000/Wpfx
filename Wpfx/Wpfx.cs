using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace UserExtensions;

public static class Wpfx
{
    public static Grid GridX(Action<Grid>? configure = null, UIElement[]? children = null)
    {
        var grid = new Grid();

        var items = children ?? Array.Empty<UIElement>();

        for (int i = 0; i < items.Length; i++)
        {
            var child = items[i];

            if (child == null)
                continue;

            var hasExplicitRow = child.ReadLocalValue(Grid.RowProperty) != DependencyProperty.UnsetValue;

            var hasExplicitColumn = child.ReadLocalValue(Grid.ColumnProperty) != DependencyProperty.UnsetValue;

            if (!hasExplicitRow && !hasExplicitColumn)
            {
                Grid.SetRow(child, i);
            }

            grid.Children.Add(child);
        }

        configure?.Invoke(grid);

        return grid;
    }

    public static RowDefinition RowDefinitionX(GridUnitType type, double height = 1)
    {
        return new RowDefinition
        {
            Height = GridLengthX(height, type)
        };
    }

    public static ColumnDefinition ColumnDefinitionX(GridUnitType type, double width = 1)
    {
        return new ColumnDefinition
        {
            Width = GridLengthX(width, type)
        };
    }

    public static GridLength GridLengthX(double value, GridUnitType type)
    {
        return type switch
        {
            GridUnitType.Auto => GridLength.Auto,
            GridUnitType.Pixel => new GridLength(value, GridUnitType.Pixel),
            GridUnitType.Star => new GridLength(value, GridUnitType.Star),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }

    public static StackPanel StackPanelX(Action<StackPanel>? configure = null, UIElement[]? children = null)
    {
        var stackPanel = new StackPanel();

        foreach (var child in children ?? Array.Empty<UIElement>())
        {
            stackPanel.Children.Add(child);
        }

        configure?.Invoke(stackPanel);

        return stackPanel;
    }

    public static TextBox TextBoxX(Action<TextBox>? configure = null)
    {
        var textBox = new TextBox();
        configure?.Invoke(textBox);
        return textBox;
    }

    public static TextBlock TextBlockX(Action<TextBlock>? configure = null)
    {
        var textBlock = new TextBlock();
        configure?.Invoke(textBlock);
        return textBlock;
    }
      
    public static Button ButtonX(Action<Button>? configure = null)
    {
        var button = new Button();
        configure?.Invoke(button);
        return button;
    }

    public static Border BorderX(Action<Border>? configure = null, UIElement[]? child = null)
    {
        if (child != null && child.Length > 1)
            throw new ArgumentException("BorderX accepts at most one child. Use a Panel for multiple children.");

        var border = new Border 
        {
            Child = (child == null) ? null : child[0]
        };
        
        configure?.Invoke(border);
        return border;
    }

    public static Viewbox ViewboxX(Action<Viewbox>? configure = null, UIElement[]? child = null)
    {
        if (child != null && child.Length > 1)
            throw new ArgumentException("ViewboxX accepts at most one child.");

        var viewbox = new Viewbox
        {
            Child = (child == null) ? null : child[0]
        };

        configure?.Invoke(viewbox);
        return viewbox;
    }

    public static CheckBox CheckBoxX(Action<CheckBox>? configure = null)
    {
        var checkBox = new CheckBox();
        configure?.Invoke(checkBox);
        return checkBox;
    }

    public static ComboBox ComboBoxX(Action<ComboBox>? configure = null)
    {
        var comboBox = new ComboBox();
        configure?.Invoke(comboBox);
        return comboBox;
    }

    public static Slider SliderX(Action<Slider>? configure = null)
    {
        var slider = new Slider();
        configure?.Invoke(slider);
        return slider;
    }

    public static GridSplitter GridSplitterX(Action<GridSplitter>? configure = null)
    {
        var gridSplitter = new GridSplitter();
        configure?.Invoke(gridSplitter);
        return gridSplitter;
    }

    public static Thickness ThicknessX(double length)
    {
        return new Thickness(length);
    }

    public static Thickness ThicknessX(double left, double top, double right, double bottom)
    {
        return new Thickness(left, top, right, bottom);
    }

    public static Brush? BrushFromStringX(string str)
    {
        var obj = ColorConverter.ConvertFromString(str);
        if (obj is Color color)
            return new SolidColorBrush(color);
        return null;
    }

    public static Binding BindingX(string path, Action<Binding>? configure = null)
    {
        var binding = new Binding(path);
        configure?.Invoke(binding);
        return binding;
    }

    //------------------------------------------------------------
    // STYLE

    public static Style StyleX<T>(Style? basedOn = null, Setter[]? setters = null) where T : Control
    {
        var style = new Style(typeof(T), basedOn);

        if (setters != null)
        {
            foreach (var s in setters)
                style.Setters.Add(
                    s.TargetName == null
                        ? new Setter(s.Property, s.Value)
                        : new Setter(s.Property, s.Value, s.TargetName)
                );
        }

        return style;
    }
        
    public static Setter SetterX(DependencyProperty property, object value, string? targetName = null)
    {
        var setter = new Setter(property, value, targetName);

        return setter;
    }

    public static Trigger TriggerX(DependencyProperty property, object value, Setter[]? setters = null)
    {
        var trigger = new Trigger 
        { 
            Property = property,
            Value = value 
        };

        if (setters != null)
        {
            foreach (var s in setters)
                trigger.Setters.Add(
                    s.TargetName == null
                        ? new Setter(s.Property, s.Value)
                        : new Setter(s.Property, s.Value, s.TargetName)
                );
        }

        return trigger;
    }

    public static ControlTemplate ControlTemplateX<T>(FrameworkElementFactory visualTree, Trigger[]? triggers = null) where T : Control
    {
        var template = new ControlTemplate(typeof(T));
        template.VisualTree = visualTree;

        if (triggers != null)
        {
            foreach (var t in triggers)
                template.Triggers.Add(t);
        }

        return template;
    }

    public static FrameworkElementFactory FrameworkElementFactoryX<T>(string? name = null, Setter[]? setters = null, FrameworkElementFactory[]? children = null) where T : FrameworkElement
    {
        var factory = new FrameworkElementFactory(typeof(T));

        if (name != null)
            factory.Name = name;

        if (setters != null)
        {
            foreach (var s in setters)
                factory.SetValue(s.Property, s.Value);
        }

        if (children != null)
        {
            foreach (var c in children)
                factory.AppendChild(c);
        }

        return factory;
    }
}
