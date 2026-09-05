using Demo_14_DataTemplate.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo_14_DataTemplate.ViewModels;

public class PersonViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private Person? _selectedPerson;

    public PersonViewModel()
    {
        BuildPersons();
    }

    private void BuildPersons()
    {
        Person[] personArray = new[] {
            new Person { FirstName = "Ellen", LastName="Ripley", Email = "ellen@weyland-yutani.com", Code = "180924609", Department="Nostromo" },
            new Person { FirstName = "Bruce", LastName="Wayne", Email = "bruce@wayne-industries.com", Code = "Batman", Department="Gotham City" },
            new Person { FirstName = "Luke", LastName="Skywalker", Email = "luke@force.net", Code = "R2-D2", Department="Jedi" },
            new Person { FirstName = "Clark", LastName="Kent", Email = "clark@dailyplanet.com", Code = "Krypton", Department="Justice" },
            new Person { FirstName = "Harry", LastName="Potter", Email = "harry@owlmail.net", Code = "9 3/4", Department="Hogwarts" },
            new Person { FirstName = "Albus", LastName="Dumbledore", Email = "albus@owlmail.net", Code = "Phoenix", Department="Hogwarts" },
            new Person { FirstName = "Wednesday", LastName="Addams", Email = "wednesday@spooky.com", Code = "Thing", Department="Gothic" },
            new Person { FirstName = "Henry", LastName="Jones", Email = "indy@marshal.org", Code = "Indianna", Department="Archaeology" },
            new Person { FirstName = "Charles", LastName="Xavier", Email = "charles@x.com", Code = "X", Department="Mutation" },
            new Person { FirstName = "Peter", LastName="Venkman", Email = "peter@ghostbusters.com", Code = "PK", Department="Paranormal" },
            new Person { FirstName = "Harley", LastName="Quinn", Email = "harley@squad.net", Code = "DC", Department="Comics" },
            new Person { FirstName = "Will", LastName="Turner", Email = "will@digitalocean.com", Code = "Bootstrap", Department="Port Royal" },
            new Person { FirstName = "Sherlock", LastName="Holmes", Email = "sherlock@holmes.com", Code = "221B", Department="Detective Consultancy" },
            new Person { FirstName = "James", LastName="Bond", Email = "james@mi6.gov.uk", Code = "007", Department="Secret Service" },
            new Person { FirstName = "Sarah", LastName="Connor", Email = "sarah@sky.net", Code = "CSM-101", Department="Resistance" },
            new Person { FirstName = "Dolores", LastName="Umbridge", Email = "dolores@ministryofmagic.net", Code = "Pink", Department="Dark Arts" },
            new Person { FirstName = "Jean-Baptiste", LastName="Zorg", Email = "jb@zorg.com", Code = "5", Department="Chaos" },
            new Person { FirstName = "Tasha", LastName="Yar", Email = "tasha@enterprise.net", Code = "NCC-1701D", Department="Data" },
            new Person { FirstName = "Jyn", LastName="Erso", Email = "jyn@rebel-alliance.org", Code = "R1", Department="Rebels" },
            new Person { FirstName = "Kevin", LastName="Flynn", Email = "flynn@tron.net", Code = "MCP", Department="Software" },
            new Person { FirstName = "Ryland", LastName="Grace", Email = "ryland@nasa.gov", Code = "Rocky", Department="Tau Ceti" },
            new Person { FirstName = "Pamela", LastName="Isley", Email = "pamela@ivy.net", Code = "Ivy", Department="Gotham City" },
            new Person { FirstName = "Marty", LastName="McFly", Email = "marty@outatime.net", Code = "88", Department="Hilldale" },
            new Person { FirstName = "Dave", LastName="Lister", Email = "dave@jupitermining.com", Code = "Lager", Department="Red Dwarf" },
            new Person { FirstName = "Otto", LastName="Octavius", Email = "doc@octopus.org", Code = "8", Department="Science" },
            new Person { FirstName = "Dennis", LastName="Nedry", Email = "dennis@ingen.com", Code = "DNA", Department="Software" },
        };

        Persons = new ObservableCollection<Person>(personArray.OrderBy(p => p.LastName));
    }

    //---------------------------------------------
    // PROPERTIES

    public ObservableCollection<Person>? Persons { get; private set; }

    public Person? SelectedPerson
    {
        get
        {
            return _selectedPerson;
        }
        set
        {
            if (_selectedPerson != value)
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }
    }

    void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
