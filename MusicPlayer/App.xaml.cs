using MusicPlayer.Utils;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MusicPlayer;

public partial class App : Application
{

    ////uncomment to see how audio player works
    //protected override void OnStartup(StartupEventArgs e)
    //{
    //    base.OnStartup(e);

    //    AudioPlayerMedia player = new AudioPlayerMedia();
    //    //AudioPlayerNAudio player = new AudioPlayerNAudio();
    //    string path = "sciezka";

    //    player.Play(path);
    //    player.Volume(0.5f);
    //}

}