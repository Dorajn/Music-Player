using MusicPlayer.Utils;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Documents.DocumentStructures;

namespace MusicPlayer;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        //To powinno byc wywylywane tylko RAZ przy instalacji lub RAZ przez was gdy uruchamiacie aplikacje
        //Ta metoda zapisuje sciezke do rejestru systemu
        //Metadata.SaveMusicFolderPath("C:\\Users\\derqu\\OneDrive\\Pulpit\\folder");

        Metadata.absolutePath = Metadata.GetMusicFolderPath();
    }

}