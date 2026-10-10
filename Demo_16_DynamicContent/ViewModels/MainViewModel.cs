using Demo_16_DynamicContent.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo_16_DynamicContent.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private ProtocolType _protocol = ProtocolType.Serial;

    private string _tcpHost = "127.0.0.1";
    private string _tcpPort = "5000";

    private string _udpHost = "127.0.0.1";
    private string _udpPort = "6000";

    private string _serialPort = "COM3";

    private string _bleMAC = "18:42:0D:AA:7B:FF";

    private string _message = string.Empty;
    private string _messageSent = string.Empty;
    private bool _isConnected;

    public MainViewModel()
    {
       
    }
       
    //------------------------------------------------------------------------------------------------------------
    // Properties
    
    public ProtocolType Protocol
    {
        get => _protocol;

        set
        {
            if (_protocol == value) return;
            _protocol = value;
            OnPropertyChanged();
        }
    }

    public string TcpHost
    {
        get => _tcpHost;

        set
        {
            if (_tcpHost == value) return;
            _tcpHost = value;
            OnPropertyChanged();
        }
    }

    public string TcpPort
    {
        get => _tcpPort;

        set
        {
            if (_tcpPort == value) return;
            _tcpPort = value;
            OnPropertyChanged();            
        }
    }

    public string UdpHost
    {
        get => _udpHost;

        set
        {
            if (_udpHost == value) return;
            _udpHost = value;
            OnPropertyChanged();
        }
    }

    public string UdpPort
    {
        get => _udpPort;

        set
        {
            if (_udpPort == value) return;
            _udpPort = value;
            OnPropertyChanged();
        }
    }

    public string SerialPort
    {
        get => _serialPort;

        set
        {
            if (_serialPort == value) return;
            _serialPort = value;
            OnPropertyChanged();
        }
    }

    public string BleMAC
    {
        get => _bleMAC;

        set
        {
            if (_bleMAC == value) return;
            _bleMAC = value;
            OnPropertyChanged();
        }
    }

    public bool IsConnected
    {
        get => _isConnected;
        
        private set
        {
            if (_isConnected == value) return;
            _isConnected = value;
            OnPropertyChanged();
        }
    }
                      
    public string Message
    {
        get => _message;

        set
        {
            if (_message == value) return;
            _message = value;
            OnPropertyChanged();
        }
    }

    //---------------------------------------------
    // COMMANDS

   
    //---------------------------------------------
    // COMMAND METHODS
      

    //------------------------------------------------------------------------------------------------------------
    // Events

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
