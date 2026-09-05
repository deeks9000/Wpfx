using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Demo_13_EventAnimations;

public class MainWindow : Window
{
    private Brush? textBrush;
    private ColorAnimation? upTextColorAnimation;
    private ColorAnimation? downTextColorAnimation;
    private DoubleAnimation? upWidthDoubleAnimation;
    private DoubleAnimation? downWidthDoubleAnimation;
    private DoubleAnimation? upHeightDoubleAnimation;
    private DoubleAnimation? downHeightDoubleAnimation;

    public MainWindow()
    {
        InitializeEventAnimations();

        Title = "Demo 13 EventAnimations";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 600;
        Content = Build();
    }

    private void InitializeEventAnimations()
    {
        textBrush = new SolidColorBrush(Colors.Black);

        upTextColorAnimation = new ColorAnimation {
            To = Colors.DarkOrange,
            Duration = TimeSpan.FromMilliseconds(200)
        };

        downTextColorAnimation = new ColorAnimation {
            To = Colors.Black,
            Duration = TimeSpan.FromMilliseconds(2000)
        };

        upWidthDoubleAnimation = new DoubleAnimation {
            To = 400d,
            Duration = TimeSpan.FromMilliseconds(100)
        };

        downWidthDoubleAnimation = new DoubleAnimation {
            To = 300d,
            Duration = TimeSpan.FromMilliseconds(200)
        };

        upHeightDoubleAnimation = new DoubleAnimation {
            To = 80d,
            Duration = TimeSpan.FromMilliseconds(100)
        };

        downHeightDoubleAnimation = new DoubleAnimation {
            To = 40d,
            Duration = TimeSpan.FromMilliseconds(200)
        };
    }

    private UIElement Build()
    {
        return GridX(
            children: [
                ButtonX(
                    configure: x => {
                        x.Content = "Click me, wait, then leave";
                        x.FontSize = 18;
                        x.FontWeight = FontWeights.Bold;
                        x.Margin = ThicknessX(10);
                        x.Padding = ThicknessX(5);
                        x.HorizontalAlignment = HorizontalAlignment.Center;
                        x.VerticalAlignment = VerticalAlignment.Center;
                        x.Width = 300;
                        x.Height = 40;
                        x.Foreground = textBrush;

                        x.MouseEnter += (s,e) => {
                            x.BeginAnimation(Button.WidthProperty, upWidthDoubleAnimation);
                            x.BeginAnimation(Button.HeightProperty, upHeightDoubleAnimation);
                        };

                        x.MouseLeave += (s,e) => {
                            x.BeginAnimation(Button.WidthProperty, downWidthDoubleAnimation);
                            x.BeginAnimation(Button.HeightProperty, downHeightDoubleAnimation);
                            textBrush?.BeginAnimation(SolidColorBrush.ColorProperty, downTextColorAnimation);
                        };

                        x.Click += (s,e) => {
                            textBrush?.BeginAnimation(SolidColorBrush.ColorProperty, upTextColorAnimation);
                        };
                    }
                )
            ]
        );
    }
}
