using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic.Devices;
using MusicPlayer.Utils;

namespace MusicPlayer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<LeafNode> LeafNodes { get; set; }
    public static ObservableCollection<MusicFile> MusicFilesList { get; set; }
    private static AudioPlayerNAudio Player { get; set; }
    private static DispatcherTimer timer { get; set; }
    public static ObservableProperty<string> CurrentSongTitle { get; set; }
    public static ObservableProperty<string> CurrentSongArtist { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        GatherPaths();

        MusicFilesList = new ObservableCollection<MusicFile>();
        Player = new AudioPlayerNAudio();
        timer = new DispatcherTimer();
        SetDispacher();

        CurrentSongTitle = new ObservableProperty<string>();
        CurrentSongArtist = new ObservableProperty<string>();

        DataContext = this;
    }

    private void SetDispacher()
    {
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += ShowTrackPercentage;
        timer.Start();
    }

    //FOR DEBUG
    private void ShowTrackPercentage(object sender, EventArgs e)
    {
        Console.WriteLine(Player.GetSongPlaybackPercentage());
    }

    private void GatherPaths()
    {
        Data data = new Data(Metadata.absolutePath);
        data.Print();
        LeafNodes = new ObservableCollection<LeafNode>();
        foreach (var playlist in data.FetchedPlaylists)
        {
            LeafNodes.Add(new LeafNode(playlist.Name, playlist.AudioFiles));
        }
    }

    private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        float newVolume = ((float)slVolume.Value) / 100;
        Console.WriteLine(newVolume);
        Player.Volume(newVolume);
    }

    public class MusicFile
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Duration { get; set; }
        public string FilePath { get; set; }
        public ICommand PlayCommand { get; set; }

        public MusicFile()
        {
            Title = string.Empty;
            Artist = string.Empty;
            Duration = string.Empty;
            FilePath = string.Empty;
            PlayCommand = new RelayCommand(param => PlayMusic(param?.ToString() ?? string.Empty));
        }

        private void PlayMusic(string filePath)
        {
            Player.Play(filePath);
            CurrentSongTitle.Value = Title;
            CurrentSongArtist.Value = Artist;
        }
    }

    public class LeafNode
    {
        public string Header { get; set; } // Displayed text
        public ICommand ButtonCommand { get; set; } // Command for the button
        public List<AudioFile> AudioFiles { get; set; } // Additional data, like file paths

        public LeafNode(string header, List<AudioFile> audioFiles)
        {
            Header = header;
            AudioFiles = audioFiles;
            ButtonCommand = new RelayCommand(param => ExecuteCommand(param));
        }

        private void ExecuteCommand(object param)
        {
            MusicFilesList.Clear();
            foreach (AudioFile audioFile in AudioFiles)
            {
                MusicFile mf = new MusicFile();
                mf.FilePath =
                    Metadata.absolutePath
                    + "\\"
                    + Header
                    + "\\"
                    + audioFile.Name
                    + audioFile.Format;
                mf.Title = audioFile.Name;
                MusicFilesList.Add(mf);
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool>? _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter!);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value!;
            remove => CommandManager.RequerySuggested -= value!;
        }
    }

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
}
