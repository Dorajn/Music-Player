using System.Windows.Input;
using MusicPlayer.Utils;
using System.Collections.ObjectModel;
using MusicPlayer.Model;

namespace MusicPlayer.Services;

public class MusicPlayerMenager
{
    
    public static AudioPlayerNAudio Player = new AudioPlayerNAudio();
    public static ICommand PlayCommand { get; set; } = new RelayCommand(param => PlayMusic(GetIndex(param?.ToString() ?? string.Empty)));
    public static ICommand AddLyricsCommand { get; set; } = new RelayCommand(param => AddLyrics(GetIndex(param?.ToString() ?? string.Empty)));
    public static ICommand SkipForwardCommand { get; set; } = new RelayCommand(_ => SkipForward());
    public static ICommand SkipBackwardCommand { get; set; } = new RelayCommand(_ => SkipBackward());
    public static ICommand PauseResumeCommand { get; set; } = new RelayCommand(_ => PauseResume());
    public static ObservableCollection<MusicFile> MusicFilesList { get; set; }
    public static ObservableProperty<string> CurrentSongTitle { get; set; }
    public static ObservableProperty<string> CurrentSongArtist { get; set; }
    private static bool isPlaying { get; set; } = false;
    public static ObservableProperty<string> CurrentButtonSign { get; set; } 
    private static int CurrentSongIndex = 0;

    public MusicPlayerMenager()
    {
        MusicFilesList = new ObservableCollection<MusicFile>();
        CurrentSongTitle = new ObservableProperty<string>();
        CurrentSongArtist = new ObservableProperty<string>();
        CurrentButtonSign = new ObservableProperty<string>();
        CurrentButtonSign.Value = "▶";
    }
    private static void PlayMusic(int ind)
    {
        Player.Stop();
        MusicFile song = MusicFilesList[ind];

        CurrentSongTitle.Value = ShortenTitle(song.Title);
        CurrentSongArtist.Value = song.Artist;
        CurrentButtonSign.Value = "❚❚";
        CurrentSongIndex = ind;
        isPlaying = true;
        Player.Play(song.FilePath);
    }

    private static void AddLyrics(int ind)
    {
        MusicFile song = MusicFilesList[ind];
        Data.CreateAndOpenFile(song);
    }

    private static void SkipForward()
    {  
        if(MusicFilesList.Count != 0)
            PlayMusic((CurrentSongIndex + 1) % MusicFilesList.Count);
    }

    private static void SkipBackward()
    {
        if(MusicFilesList.Count != 0)
            PlayMusic(CurrentSongIndex == 0 ? MusicFilesList.Count - 1 : CurrentSongIndex - 1);
    }

    private static void PauseResume()
    {
        if(isPlaying)
        {
            Player.Pause();
            isPlaying = false;
            CurrentButtonSign.Value = "▶";
        }
        else
        {
            Player.Resume();
            isPlaying = true;
            CurrentButtonSign.Value = "❚❚";
        }
    }
    
    private static int GetIndex(string songTitle)
    {
        for(int i = 0; i < MusicFilesList.Count; i++)
        {
            if(MusicFilesList[i].Title == songTitle)
            {
                return i;
            }
        }
        return 0;
    }

    public static void CheckIfSongEnded(object sender, EventArgs e)
    {
        if (Player.GetSongPlaybackPercentage() >= 1)
        {
            SkipForward();
        }
    }

    private static string ShortenTitle(string text)
    {
        const int SIZE = 22;
        if (text.Length <= SIZE)
            return text;
        else
        {
            string beg = text.Substring(0, SIZE - 3);
            return beg + "...";
        }
    }
}
