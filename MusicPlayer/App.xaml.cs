using MusicPlayer.Utils;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Documents.DocumentStructures;

namespace MusicPlayer;

public partial class App : Application
{

    // //uncomment to see how audio player works
    // protected override void OnStartup(StartupEventArgs e)
    // {
    //     base.OnStartup(e);
    //
    //     AudioPlayerMedia player = new AudioPlayerMedia();
    //     //AudioPlayerNAudio player = new AudioPlayerNAudio();
    //     string path = "sciezka";
    //
    //     player.Play(path);
    //     player.Volume(0.5f);
    // }



    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        const string path = "C:\\Users\\derqu\\OneDrive\\Pulpit\\folder";
        Data data = new Data(path);
        data.Print();
        Metadata.absolutePath = path;

        // AudioPlayerNAudio player = new AudioPlayerNAudio();
        // // player.Play(path + "\\playlist1\\example.mp3");
        // player.GetTotalSongTime(path + "\\playlist1\\example.mp3");
    }

}