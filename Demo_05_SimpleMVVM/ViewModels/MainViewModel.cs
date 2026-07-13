using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Demo_05_SimpleMVVM;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private int _counter = 0;
    private string _message = string.Empty;
    
    public MainViewModel()
    {
        Message = $"Button clicked {_counter} times";

        UpdateMessage = new AsyncCommand(
            execute: async () => await UpdateMessageAsync(),
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

    public ICommand UpdateMessage { get; }

    private async Task UpdateMessageAsync()
    {
        await Task.CompletedTask;

        _counter += 1;

        Message = $"Button clicked {_counter} times";
    }


    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }    
}
