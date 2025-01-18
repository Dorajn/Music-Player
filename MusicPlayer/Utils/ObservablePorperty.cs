using System.ComponentModel;

namespace MusicPlayer.Utils;

public class ObservableProperty<T> : INotifyPropertyChanged
{
    private T _value { get; set; }

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged(nameof(Value));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}