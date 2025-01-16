using System.Windows.Input;
using MusicPlayer.Services;
using MusicPlayer.Utils;

namespace MusicPlayer.Model;

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
        //PlayCommand = new RelayCommand(param => PlayMusic(param?.ToString() ?? string.Empty));
    }

    // private void PlayMusic(string filePath)
    // {
    //     MusicPlayerMenager.Player.Play(filePath);
    //     // window.Player.Play(filePath);
    //     // window.CurrentSongTitle.Value = Title;
    //     // window.CurrentSongArtist.Value = Artist;
    // }
}