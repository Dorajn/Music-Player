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
        const string path = "C:\\Users\\derqu\\OneDrive\\Pulpit\\folder";
        Data data = new Data(path);
        data.Print();
        Metadata.absolutePath = path;
        
    }

}