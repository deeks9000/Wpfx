using Demo_05_MVVM.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Demo_05_MVVM;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private int _count = 0;
    private int _nameIndex = 0;

    private string _text = string.Empty;
    private string _message = string.Empty;
    private string _firstName = string.Empty;
    private string _middleName = string.Empty;
    private string _lastName = string.Empty;
    private Cat? _selectedCat;

    public MainViewModel()
    {
        Message = $"The button has not been clicked";

        FirstName = "Amaze";
        MiddleName = "Amaze";
        LastName = "Amaze";

        SelectedCat = new Cat
        {
            Type = "British Shorthair",
            Name = "Tabby",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/british_shorthair.jpg"
        };

        UpdateCountCommand = new AsyncCommand(
            execute: async () => await UpdateCountAsync(),
            onError: ex => System.Diagnostics.Debug.WriteLine($"Command error: {ex.Message}")
        );

        AddTextCommand = new AsyncCommand(
            execute: async () => await AddTextAsync(),
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

    public ICommand UpdateCountCommand { get; }

    public ICommand AddTextCommand { get; }


    //---------------------------------------------
    // COMMAND Methods

    private async Task UpdateCountAsync()
    {
        await Task.CompletedTask;

        _count += 1;

        Message = _count > 1
            ? $"The button was clicked {_count} times"
            : "The button was clicked";
    }

    private async Task AddTextAsync()
    {
        await Task.CompletedTask;

        string txt = Text;

        int residue = _nameIndex % 3;

        if (residue == 0)
        {
            FirstName = txt;
            _nameIndex++;
        }
        else if (residue == 1)
        { 
            MiddleName = txt;
            _nameIndex++;
        }
        else // residue = 2, or other
        {
            LastName = txt;
            _nameIndex = 0;
        }        

        Text = string.Empty;
    }

    //---------------------------------------------
    // Events

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }    
}
