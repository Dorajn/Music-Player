using System.Windows.Input;
using MusicPlayer.Utils;

namespace MusicPlayer.Services;

public class MusicPlayerMenager
{
    
    public static AudioPlayerNAudio Player = new AudioPlayerNAudio();
    public static ICommand PlayCommand { get; set; } = new RelayCommand(param => PlayMusic(param?.ToString() ?? string.Empty));
    
    private static void PlayMusic(string filePath)
    {
        Player.Play(filePath);
        // window.Player.Play(filePath);
        // window.CurrentSongTitle.Value = Title;
        // window.CurrentSongArtist.Value = Artist;
        
        
        //TUTAJ COŚ JEST NIE TAK 
        //JAK POŁĄCZYĆ TO ŻEBY PUSZCZAŁO MUZYKĘ??? CHATGTPT POMÓŻ
    }
    
}