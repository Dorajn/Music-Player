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
using System.Collections.ObjectModel;

namespace MusicPlayer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public ObservableCollection<LeafNode> LeafNodes { get; set; }
    public MainWindow()
    {
        InitializeComponent();

        var musicFiles = new List<MusicFile>
        {
            new MusicFile { Title = "Song A", Artist = "Artist 1", Duration = "3:45" },
            new MusicFile { Title = "Song B", Artist = "Artist 2", Duration = "4:20" },
            new MusicFile { Title = "Song C", Artist = "Artist 3", Duration = "2:30" }
        };

        MusicFilesList.ItemsSource = musicFiles;

        LeafNodes = new ObservableCollection<LeafNode>
        {
            new LeafNode { Header = "Song 1", Data = "song1.mp3" },
            new LeafNode { Header = "Song 2", Data = "song2.mp3" },
            new LeafNode { Header = "Song 3", Data = "song3.mp3" }
        };
        
        DataContext = this;
    }

    private void AddNewNode()
    {
        // Add a new parent node
        var newPlaylist = new LeafNode { Header = "New Playlist" };
        LeafNodes.Add(newPlaylist);
        
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
            // Playback logic here
        }
    }

    public class LeafNode
    {
        public string Header { get; set; } // Displayed text
        public ICommand ButtonCommand { get; set; } // Command for the button
        public string Data { get; set; } // Additional data, like file paths

        public LeafNode()
        {
            Header = string.Empty;
            Data = string.Empty;
            ButtonCommand = new RelayCommand(param => ExecuteCommand(param));
        }

        private void ExecuteCommand(object param)
        {
            MessageBox.Show($"Executing command for {param}");
            // Implement custom logic, e.g., playing a file
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
}
