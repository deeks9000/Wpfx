using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Demo_12_Triggers;

public class MainWindow : Window
{
    private SolidColorBrush animatedBrush = new SolidColorBrush(Colors.Black);
    private ColorAnimation upColorAnimation = new ColorAnimation();
    private ColorAnimation downColorAnimation = new ColorAnimation();

    public MainWindow()
    {
        InitializeEventTriggerAnimations();

        Title = "Demo 12 Triggers";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 600;
        Content = Build();
    }

    private void InitializeEventTriggerAnimations()
    {
        NameScope.SetNameScope(this, new NameScope());
        RegisterName("AnimatedBrush", animatedBrush);

        upColorAnimation.To = Colors.DarkOrange;
        upColorAnimation.Duration = TimeSpan.FromMilliseconds(200);
        Storyboard.SetTargetName(upColorAnimation, "AnimatedBrush");
        Storyboard.SetTargetProperty(upColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

        downColorAnimation.To = Colors.Black;
        downColorAnimation.Duration = TimeSpan.FromMilliseconds(1000);
        Storyboard.SetTargetName(downColorAnimation, "AnimatedBrush");
        Storyboard.SetTargetProperty(downColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.AddRow();
                x.AddRow();
                x.AddRow();
                x.AddRow();
            },
            children: [
                ButtonX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.Content = "Button with Trigger Style";
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(5);
                        x.HorizontalAlignment = HorizontalAlignment.Center;
                        x.VerticalAlignment = VerticalAlignment.Center;
                        x.Style = BuildButtonTriggerStyle();
                    }
                ),
                ButtonX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.Content = "Button with MultiTrigger Style";
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(5);
                        x.HorizontalAlignment = HorizontalAlignment.Center;
                        x.VerticalAlignment = VerticalAlignment.Center;
                        x.Style = BuildButtonMultiTriggerStyle();
                    }
                ),
                ButtonX(
                    configure: x => {
                        Grid.SetRow(x, 2);
                        x.Content = "Button with EventTrigger Style";
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(5);
                        x.HorizontalAlignment = HorizontalAlignment.Center;
                        x.VerticalAlignment = VerticalAlignment.Center;
                        x.Style = BuildButtonEventTriggerStyle();
                    }
                ),
                ButtonX(
                    configure: x => {
                        Grid.SetRow(x, 3);
                        x.Content = "Click me, wait, then leave";
                        x.FontSize = 18;
                        x.FontWeight = FontWeights.Bold;
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(5);
                        x.HorizontalAlignment = HorizontalAlignment.Center;
                        x.VerticalAlignment = VerticalAlignment.Center;
                        x.Width = 300;
                        x.Height = 40;
                        x.Foreground = animatedBrush;

                        x.Triggers.Add(
                            EventTriggerX(
                                routedEvent: Mouse.MouseEnterEvent,
                                actions: [
                                    BeginStoryboardX(
                                        StoryboardX(
                                            children: [
                                                DoubleAnimationX(duration: TimeSpan.FromSeconds(0.1), to: 400d, targetProperty: Button.WidthProperty),
                                                DoubleAnimationX(duration: TimeSpan.FromSeconds(0.1), to: 80d, targetProperty: Button.HeightProperty)
                                            ]
                                        )
                                    )
                                ]
                            )
                        );

                        x.Triggers.Add(
                            EventTriggerX(
                                routedEvent: Mouse.MouseLeaveEvent,
                                actions: [
                                    BeginStoryboardX(
                                        StoryboardX(
                                            children: [
                                                DoubleAnimationX(duration: TimeSpan.FromMilliseconds(200), to: 300d, targetProperty: Button.WidthProperty),
                                                DoubleAnimationX(duration: TimeSpan.FromMilliseconds(200), to: 40d, targetProperty: Button.HeightProperty),
                                                downColorAnimation
                                            ]
                                        )
                                    )
                                ]
                            )
                        );

                        x.Triggers.Add(
                            EventTriggerX(
                                routedEvent: Button.ClickEvent,
                                actions: [
                                    BeginStoryboardX(
                                        StoryboardX(
                                            children: [
                                                upColorAnimation
                                            ]
                                        )
                                    )
                                ]
                            )
                        );
                    }
                )
            ]
        );
    }

    public Style BuildButtonTriggerStyle()
    {
        return StyleX<Button>(
            setters: [
                SetterX(Control.FontFamilyProperty, new FontFamily("Times New Roman")),
                SetterX(Control.FontSizeProperty, 18d),
            ],
            triggers: [
                TriggerX(
                    property: Control.IsFocusedProperty,
                    value: true,
                    setters: [
                        SetterX(Control.ForegroundProperty, Brushes.DarkRed),
                    ]
                ),
                TriggerX(
                    property: Control.IsMouseOverProperty,
                    value: true,
                    setters: [
                        SetterX(Control.ForegroundProperty, Brushes.Green),
                        SetterX(Control.FontWeightProperty, FontWeights.Bold),
                    ]
                ),
                TriggerX(
                    property: Button.IsPressedProperty,
                    value: true,
                    setters: [
                        SetterX(Control.ForegroundProperty, Brushes.OrangeRed),
                    ]
                ),
            ]
        );
    }

    public Style BuildButtonMultiTriggerStyle()
    {
        return StyleX<Button>(
            setters: [
                SetterX(Control.FontFamilyProperty, new FontFamily("Times New Roman")),
                SetterX(Control.FontSizeProperty, 14d),
            ],
            triggers: [
                MultiTriggerX(
                    conditions: [
                        ConditionX(Button.IsPressedProperty, true),
                        ConditionX(Control.IsMouseOverProperty, true),
                    ],
                    setters: [
                        SetterX(Control.ForegroundProperty, Brushes.OrangeRed),
                        SetterX(Control.FontSizeProperty, 16d),
                        SetterX(Control.FontWeightProperty, FontWeights.Bold),
                    ]
                ),
            ]
        );
    }

    public Style BuildButtonEventTriggerStyle()
    {
        return StyleX<Button>(
            setters: [
                SetterX(Control.FontFamilyProperty, new FontFamily("Times New Roman")),
                SetterX(Control.FontSizeProperty, 18d),
                SetterX(Control.FontWeightProperty, FontWeights.Bold),
            ],
            triggers: [
                EventTriggerX(
                    routedEvent: Mouse.MouseEnterEvent,
                    actions: [
                        BeginStoryboardX(
                            StoryboardX(
                                children: [
                                    DoubleAnimationX(duration: TimeSpan.FromSeconds(0.1), to: 24d, targetProperty: Control.FontSizeProperty)
                                ]
                            )
                        )
                    ]
                ),
                EventTriggerX(
                    routedEvent: Mouse.MouseLeaveEvent,
                    actions: [
                        BeginStoryboardX(
                            StoryboardX(
                                children: [
                                    DoubleAnimationX(duration: TimeSpan.FromMilliseconds(200), targetProperty: Control.FontSizeProperty)
                                ]
                            )
                        )
                    ]
                ),
                EventTriggerX(
                    routedEvent: Button.ClickEvent,
                    actions: [
                        BeginStoryboardX(
                            StoryboardX(
                                children: [
                                    DoubleAnimationX(duration: TimeSpan.FromSeconds(0.1), to: 28d, targetProperty: Control.FontSizeProperty)
                                ]
                            )
                        )
                    ]
                )
            ]
        );
    }
}
