using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Documents.DocumentStructures;
using MusicPlayer.Utils;

namespace MusicPlayer;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        //To powinno byc wywylywane tylko RAZ przy instalacji lub RAZ przez was gdy uruchamiacie aplikacje
        //Ta metoda zapisuje sciezke do rejestru systemu
        Metadata.SaveMusicFolderPath(@"C:\Users\nier\Desktop\Music");

        Metadata.absolutePath = Metadata.GetMusicFolderPath();
    }
}
