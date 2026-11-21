using Demo_06_TwoWayMVVM.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Demo_06_TwoWayMVVM;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _text = string.Empty;
    
    public MainViewModel()
    {
        Text = "The quick brown fox jumps over the lazy dog.";

        ClearTextCommand = new AsyncCommand(
            execute: async () => await ClearTextAsync(),
            onError: ex => System.Diagnostics.Debug.WriteLine($"Command error: {ex.Message}")
        );

        DefaultTextCommand = new AsyncCommand(
            execute: async () => await DefaultTextAsync(),
            onError: ex => System.Diagnostics.Debug.WriteLine($"Command error: {ex.Message}")
        );
    }

    //---------------------------------------------
    // PROPERTIES

    public string Text
    {
        get => _text;

        set
        {
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged();
            }
        }
    }

    //---------------------------------------------
    // COMMANDS

    public ICommand ClearTextCommand { get; }

    public ICommand DefaultTextCommand { get; }

    private async Task ClearTextAsync()
    {
        await Task.CompletedTask;

        Text = string.Empty;
    }

    private async Task DefaultTextAsync()
    {
        await Task.CompletedTask;

        Text = LoremIpsum.GetText();
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
