using Demo_15_DataTemplate2.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo_15_DataTemplate2.ViewModels;

public class CatViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private Cat? _selectedCat;
    private bool _isImageLoading = false;

    public CatViewModel()
    {
        BuildCats();
    }

    private void BuildCats()
    {
        List<Cat> catList = new List<Cat>();

        catList.Add(new Cat
        {
            Type = "British Shorthair",
            Name = "Tabby",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/british_shorthair.jpg"
        });

        catList.Add(new Cat
        {
            Type = "Persian",
            Name = "Fluffy",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/persian.jpg"
        });

        catList.Add(new Cat
        {
            Type = "Egyptian Mau",
            Name = "Maui",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/egyptian_mau.jpg"
        });

        catList.Add(new Cat
        {
            Type = "Serval",
            Name = "Chloe",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/serval.jpg"
        });

        catList.Add(new Cat
        {
            Type = "Japanese Bobtail",
            Name = "Bobby",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/japanese_bobtail.jpg"
        });

        catList.Add(new Cat
        {
            Type = "Siberian",
            Name = "Ivan",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/siberian.jpg"
        });

        catList.Add(new Cat
        {
            Type = "Cheetah",
            Name = "Spotty",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/cheetah.jpg"
        });

        catList.Add(new Cat
        {
            Type = "Burmese",
            Name = "Geisha",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/burmese.jpg"
        });        

        catList.Add(new Cat
        {
            Type = "Siamese",
            Name = "Sammy",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/siamese.jpg"
        });

        catList.Add(new Cat
        {
            Type = "Norwegian Forest",
            Name = "Norbert",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/norwegian_forest.jpg"
        });

        catList.Add(new Cat
        {
            Type = "Tiger",
            Name = "Khan",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/tiger.jpg"
        });

        catList.Add(new Cat
        {
            Type = "Domestic Shorthair",
            Name = "Socks",
            ImageUrl = "https://raw.githubusercontent.com/deeks9000/app-assets/main/cats/domestic_shorthair.jpg"
        });
          
        Cats = new ObservableCollection<Cat>(catList);
    }

    //---------------------------------------------
    // PROPERTIES

    public ObservableCollection<Cat>? Cats { get; private set; }

    public Cat? SelectedCat
    {
        get
        {
            return _selectedCat;
        }
        set
        {
            if (_selectedCat != value)
            {
                _selectedCat = value;

                IsImageLoading = true;

                OnPropertyChanged();
            }
        }
    }

    public bool IsImageLoading
    {
        get
        {
            return _isImageLoading;
        }
        set
        {
            if (_isImageLoading != value)
            {
                _isImageLoading = value;
                OnPropertyChanged();
            }
        }
    }

    void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
