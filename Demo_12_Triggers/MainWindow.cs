using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Demo_12_Triggers;

public class MainWindow : Window
{
    private SolidColorBrush animatedBrush = new SolidColorBrush();
    private ColorAnimation upColorAnimation = new ColorAnimation();
    private ColorAnimation downColorAnimation = new ColorAnimation();

    private SolidColorBrush textBrush = new SolidColorBrush();
    private ColorAnimation upTextColorAnimation = new ColorAnimation();
    private ColorAnimation downTextColorAnimation = new ColorAnimation();

    private DoubleAnimation upWidthDoubleAnimation = new DoubleAnimation();
    private DoubleAnimation downWidthDoubleAnimation = new DoubleAnimation();
    private DoubleAnimation upHeightDoubleAnimation = new DoubleAnimation();
    private DoubleAnimation downHeightDoubleAnimation = new DoubleAnimation();

    public MainWindow()
    {
        InitializeEventTriggerAnimations();
        InitializeEventAnimations();

        Title = "Demo 12 Triggers";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 600;
        Resources = BuildWindowResources();
        Content = Build();
    }

    private void InitializeEventTriggerAnimations()
    {
        NameScope.SetNameScope(this, new NameScope());
        RegisterName("AnimatedBrush", animatedBrush);

        animatedBrush.Color = Colors.Black;

        upColorAnimation.To = Colors.DarkOrange;
        upColorAnimation.Duration = TimeSpan.FromSeconds(1);
        Storyboard.SetTargetName(upColorAnimation, "AnimatedBrush");
        Storyboard.SetTargetProperty(upColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

        downColorAnimation.To = Colors.Black;
        downColorAnimation.Duration = TimeSpan.FromSeconds(1);
        Storyboard.SetTargetName(downColorAnimation, "AnimatedBrush");
        Storyboard.SetTargetProperty(downColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
    }

    private void InitializeEventAnimations()
    {
        textBrush.Color = Colors.Black;

        upTextColorAnimation.To = Colors.Magenta;
        upTextColorAnimation.Duration = TimeSpan.FromSeconds(1);

        downTextColorAnimation.To = Colors.Black;
        downTextColorAnimation.Duration = TimeSpan.FromSeconds(1);

        upWidthDoubleAnimation.To = 400d;
        upWidthDoubleAnimation.Duration = TimeSpan.FromSeconds(0.1);

        downWidthDoubleAnimation.To = 300d;
        downWidthDoubleAnimation.Duration = TimeSpan.FromSeconds(0.1);

        upHeightDoubleAnimation.To = 80d;
        upHeightDoubleAnimation.Duration = TimeSpan.FromSeconds(0.1);

        downHeightDoubleAnimation.To = 40d;
        downHeightDoubleAnimation.Duration = TimeSpan.FromSeconds(0.1);
    }

    private UIElement Build()
    {
        return StackPanelX(
            children: [
                TextBlockX(
                    configure: x => {
                        x.Margin = ThicknessX(10);
                        x.Text = "Button Style with Trigger:";
                    }
                ),
                ButtonX(
                    configure: x => {
                        x.Content = "Button Style with Trigger";
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(5);
                        x.Style = TryFindResource("ButtonTriggerStyle") as Style;
                    }
                ),
                TextBlockX(
                    configure: x => {
                        x.Margin = ThicknessX(10);
                        x.Text = "Button Style with MultiTrigger:";
                    }
                ),
                ButtonX(
                    configure: x => {
                        x.Content = "Button Style with MultiTrigger";
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(5);
                        x.Style = TryFindResource("ButtonMultiTriggerStyle") as Style;
                    }
                ),
                TextBlockX(
                    configure: x => {
                        x.Margin = ThicknessX(10);
                        x.Text = "Button Style with EventTrigger:";
                    }
                ),
                ButtonX(
                    configure: x => {
                        x.Content = "Button Style with EventTrigger";
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(5);
                        x.Style = TryFindResource("ButtonEventTriggerStyle") as Style;
                    }
                ),
                TextBlockX(
                    configure: x => {
                        x.Margin = ThicknessX(10);
                        x.Text = "Button animation with Triggers:";
                    }
                ),
                ButtonX(
                    configure: x => {
                        x.Content = "Click me, wait, then leave";
                        x.FontSize = 18;
                        x.FontWeight = FontWeights.Bold;
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(5);
                        x.Width = 300;
                        x.Height = 40;
                        x.Foreground = animatedBrush;

                        x.Triggers.Add(
                            EventTriggerX(
                                routedEvent: Button.ClickEvent,
                                actions: [
                                    BeginStoryboardX(
                                        StoryboardX(
                                            children: [
                                                DoubleAnimationX(duration: TimeSpan.FromSeconds(0.1), to: 400d, targetProperty: Button.WidthProperty),
                                                DoubleAnimationX(duration: TimeSpan.FromSeconds(0.1), to: 80d, targetProperty: Button.HeightProperty),
                                                upColorAnimation
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
                                                DoubleAnimationX(duration: TimeSpan.FromSeconds(0.1), to: 300d, targetProperty: Button.WidthProperty),
                                                DoubleAnimationX(duration: TimeSpan.FromSeconds(0.1), to: 40d, targetProperty: Button.HeightProperty),
                                                downColorAnimation
                                            ]
                                        )
                                    )
                                ]
                            )
                        );
                    }
                ),
                TextBlockX(
                    configure: x => {
                        x.Margin = ThicknessX(10);
                        x.Text = "Button animation with Event Handers:";
                    }
                ),
                ButtonX(
                    configure: x => {
                        x.Content = "Click me, wait, then leave";
                        x.FontSize = 18;
                        x.FontWeight = FontWeights.Bold;
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(5);
                        x.Width = 300;
                        x.Height = 40;
                        x.Foreground = textBrush;

                        x.Click += (s,e) => {
                            x.BeginAnimation(Button.WidthProperty, upWidthDoubleAnimation);
                            x.BeginAnimation(Button.HeightProperty, upHeightDoubleAnimation);
                            textBrush.BeginAnimation(SolidColorBrush.ColorProperty, upTextColorAnimation);
                        };

                        x.MouseLeave += (s,e) => {
                            x.BeginAnimation(Button.WidthProperty, downWidthDoubleAnimation);
                            x.BeginAnimation(Button.HeightProperty, downHeightDoubleAnimation);
                            textBrush.BeginAnimation(SolidColorBrush.ColorProperty, downTextColorAnimation);
                        };
                    }
                )
            ]
        );
    }
  
    public ResourceDictionary BuildWindowResources()
    {
        //------------------------------------------------------------
        // [1] ButtonTriggerStyle

        var buttonTriggerStyle = StyleX<Button>(
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
                        //SetterX(Control.BackgroundProperty, Brushes.Green),       // Doesn't work! BackgroundProperty is not template-bound to a standard Button ControlTemplate
                    ]
                ),
            ]
        );

        //------------------------------------------------------------
        // [2] ButtonMultiTriggerStyle

        var buttonMultiTriggerStyle = StyleX<Button>(
            setters: [
                SetterX(Control.FontFamilyProperty, new FontFamily("Times New Roman")),
            ],
            triggers: [
                MultiTriggerX(
                    conditions: [
                        ConditionX(Button.IsPressedProperty, true),
                        ConditionX(Control.IsMouseOverProperty, true),
                    ],
                    setters: [
                        SetterX(Control.ForegroundProperty, Brushes.OrangeRed),
                        SetterX(Control.FontSizeProperty, 18d),
                    ]
                ),
            ]
        );

        //------------------------------------------------------------
        // [3] ButtonEventTriggerStyle

        var buttonEventTriggerStyle = StyleX<Button>(
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
                                    DoubleAnimationX(duration: TimeSpan.FromSeconds(0.1), targetProperty: Control.FontSizeProperty)
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

        //------------------------------------------------------------

        ResourceDictionary dict = new ResourceDictionary();
        dict.Add("ButtonTriggerStyle", buttonTriggerStyle);
        dict.Add("ButtonMultiTriggerStyle", buttonMultiTriggerStyle);
        dict.Add("ButtonEventTriggerStyle", buttonEventTriggerStyle);

        return dict;
    }
}
