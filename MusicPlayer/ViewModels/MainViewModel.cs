using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using MusicPlayer.Model;
using MusicPlayer.Services;
using MusicPlayer.Utils;

namespace MusicPlayer.ViewModels;

public class MainViewModel
{
    public ObservableCollection<LeafNode> LeafNodes { get; set; }
    public ObservableCollection<MusicFile> MusicFilesList { get; set; }
    public ObservableProperty<string> CurrentSongTitle { get; set; }
    public ObservableProperty<string> CurrentSongArtist { get; set; }
    public MusicPlayerMenager MusicPlayerMenager { get; set; }
    public Data Data { get; set; }

    private DispatcherTimer _timer;

    public MainViewModel()
    {
        Data = new Data(Metadata.absolutePath);
        LeafNodes = new ObservableCollection<LeafNode>();
        MusicFilesList = new ObservableCollection<MusicFile>();
        MusicPlayerMenager = new MusicPlayerMenager();
        CurrentSongTitle = new ObservableProperty<string>();
        CurrentSongArtist = new ObservableProperty<string>();

        _timer = new DispatcherTimer();
        GatherPaths();
        SetDispatcher();
    }

    private void SetDispatcher()
    {
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += ShowTrackPercentage;
        _timer.Start();
    }

    private void ShowTrackPercentage(object sender, EventArgs e)
    {
        Console.WriteLine(MusicPlayerMenager.Player.GetSongPlaybackPercentage());
    }

    private void GatherPaths()
    {
        foreach (var playlist in Data.Playlists)
        {
            LeafNodes.Add(new LeafNode(playlist.Name, playlist.AudioFiles, MusicFilesList));
        }
    }

    // private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    // {
    //     float newVolume = ((float)slVolume.Value) / 100;
    //     Console.WriteLine(newVolume);
    //     //Player.Volume(newVolume);
    // }
}
