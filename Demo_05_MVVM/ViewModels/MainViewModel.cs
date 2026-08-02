using Demo_05_MVVM.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Demo_05_MVVM;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private int _counter = 0;
    private string _message = string.Empty;
    private string _firstName = string.Empty;
    private string _middleName = string.Empty;
    private string _lastName = string.Empty;
    private Cat? _selectedCat;

    public MainViewModel()
    {
        Message = $"The button has not been clicked";

        FirstName = "AMAZE";
        MiddleName = "AMAZE";
        LastName = "AMAZE";

        SelectedCat = new Cat
        {
            Type = "British Shorthair",
            Name = "Tabby",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/british_shorthair.jpg"
        };

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

    public string FirstName
    {
        get => _firstName;

        set
        {
            if (_firstName != value)
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
    }

    public string MiddleName
    {
        get => _middleName;

        set
        {
            if (_middleName != value)
            {
                _middleName = value;
                OnPropertyChanged();
            }
        }
    }

    public string LastName
    {
        get => _lastName;

        set
        {
            if (_lastName != value)
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
    }

    public Cat? SelectedCat
    {
        get => _selectedCat;

        set
        {
            if (_selectedCat != value && value != null)
            {
                _selectedCat = value;
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

        Message = _counter > 1
            ? $"The button was clicked {_counter} times"
            : "The button was clicked";
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }    
}
