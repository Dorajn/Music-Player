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
using MusicPlayer.Utils;


namespace MusicPlayer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static List<MusicFile> temp = new List<MusicFile>();
    public ObservableCollection<LeafNode> LeafNodes { get; set; }
    public static ObservableCollection<MusicFile> MusicFilesList { get; set; }
    
    public MainWindow()
    {
        InitializeComponent();
        GatherPaths();
        
        MusicFilesList = new ObservableCollection<MusicFile>();
        
        DataContext = this;
    }

    private void GatherPaths()
    {
        Data data = new Data(Metadata.absolutePath);
        LeafNodes = new ObservableCollection<LeafNode>();
        foreach (var playlist in data.FetchedData)
        {
            LeafNodes.Add(new LeafNode(playlist.Item1, playlist.Item2));
        }
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
            filePath = "C:\\Users\\derqu\\OneDrive\\Pulpit\\example.mp3";
            AudioPlayerMedia player = new AudioPlayerMedia();
            player.Play(filePath);
        }
    }

    public class LeafNode
    {
        public string Header { get; set; } // Displayed text
        public ICommand ButtonCommand { get; set; } // Command for the button
        public List<string> Data { get; set; } // Additional data, like file paths

        public LeafNode(string header, List<string> songs)
        {
            Header = header;
            Data = new List<string>(songs);
            ButtonCommand = new RelayCommand(param => ExecuteCommand(param));
        }

        private void ExecuteCommand(object param)
        {
            Console.WriteLine("dupa");
            List<MusicFile> musicFiles = new List<MusicFile>();
            foreach (var data in Data)
            {
                MusicFile mf = new MusicFile();
                mf.FilePath = Metadata.absolutePath + "\\" + Header + "\\" + data;
                Console.WriteLine(mf.FilePath);
                mf.Title = data;
                musicFiles.Add(mf);
            }

            // foreach (var mf in musicFiles)
            // {
            //     Console.WriteLine(mf.Title);
            // }

            MusicFilesList = new ObservableCollection<MusicFile>(musicFiles);

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
