using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Demo_06_TwoWayMVVM;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _message = string.Empty;
    
    public MainViewModel()
    {
        Message = "Message text";

        ClearMessage = new AsyncCommand(
            execute: async () => await ClearMessageAsync(),
            onError: ex => System.Diagnostics.Debug.WriteLine($"Command error: {ex.Message}")
        );

        DefaultMessage = new AsyncCommand(
            execute: async () => await DefaultMessageAsync(),
            onError: ex => System.Diagnostics.Debug.WriteLine($"Command error: {ex.Message}")
        );
    }

    //---------------------------------------------
    // PROPERTIES

    public string Message
    {
        get => _message;

        set
        {
            if (_message != value)
            {
                _message = value;
                OnPropertyChanged();
            }
        }
    }

    //---------------------------------------------
    // COMMANDS

    public ICommand ClearMessage { get; }

    public ICommand DefaultMessage { get; }

    private async Task ClearMessageAsync()
    {
        await Task.CompletedTask;

        Message = string.Empty;
    }

    private async Task DefaultMessageAsync()
    {
        await Task.CompletedTask;

        Message = "Hello Universe!";
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }    
}
