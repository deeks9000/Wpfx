using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Demo_13_EventAnimations;

public class MainWindow : Window
{
    private Brush? _textBrush;
    private ColorAnimation? _upTextColorAnimation;
    private ColorAnimation? _downTextColorAnimation;
    private DoubleAnimation? _upWidthDoubleAnimation;
    private DoubleAnimation? _downWidthDoubleAnimation;
    private DoubleAnimation? _upHeightDoubleAnimation;
    private DoubleAnimation? _downHeightDoubleAnimation;

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
        _textBrush = new SolidColorBrush(Colors.Black);

        _upTextColorAnimation = new ColorAnimation {
            To = Colors.DarkOrange,
            Duration = TimeSpan.FromMilliseconds(200)
        };

        _downTextColorAnimation = new ColorAnimation {
            To = Colors.Black,
            Duration = TimeSpan.FromMilliseconds(2000)
        };

        _upWidthDoubleAnimation = new DoubleAnimation {
            To = 400d,
            Duration = TimeSpan.FromMilliseconds(100)
        };

        _downWidthDoubleAnimation = new DoubleAnimation {
            To = 300d,
            Duration = TimeSpan.FromMilliseconds(200)
        };

        _upHeightDoubleAnimation = new DoubleAnimation {
            To = 80d,
            Duration = TimeSpan.FromMilliseconds(100)
        };

        _downHeightDoubleAnimation = new DoubleAnimation {
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
                        x.Foreground = _textBrush;

                        x.MouseEnter += (s,e) => {
                            x.BeginAnimation(Button.WidthProperty, _upWidthDoubleAnimation);
                            x.BeginAnimation(Button.HeightProperty, _upHeightDoubleAnimation);
                        };

                        x.MouseLeave += (s,e) => {
                            x.BeginAnimation(Button.WidthProperty, _downWidthDoubleAnimation);
                            x.BeginAnimation(Button.HeightProperty, _downHeightDoubleAnimation);
                            _textBrush?.BeginAnimation(SolidColorBrush.ColorProperty, _downTextColorAnimation);
                        };

                        x.Click += (s,e) => {
                            _textBrush?.BeginAnimation(SolidColorBrush.ColorProperty, _upTextColorAnimation);
                        };
                    }
                )
            ]
        );
    }
}
