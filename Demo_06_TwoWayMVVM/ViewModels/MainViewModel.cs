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

    private async Task ClearMessageAsync()
    {
        await Task.CompletedTask;

        Message = string.Empty;
    }


    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }    
}
