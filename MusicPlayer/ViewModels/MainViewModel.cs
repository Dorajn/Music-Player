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
    public MusicPlayerMenager MusicPlayerMenager { get; set; }
    public Data Data { get; set; }

    private DispatcherTimer _timer;

    public MainViewModel()
    {
        Data = new Data(Metadata.absolutePath);
        LeafNodes = new ObservableCollection<LeafNode>();
        MusicPlayerMenager = new MusicPlayerMenager();

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
            LeafNodes.Add(new LeafNode(playlist.Name, playlist.AudioFiles, MusicPlayerMenager.MusicFilesList));
        }
    }


    public void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e, float slVolume)
    {
        float newVolume = slVolume / 100;
        Console.WriteLine(newVolume);
        MusicPlayerMenager.Player.Volume(newVolume);
    }

}
