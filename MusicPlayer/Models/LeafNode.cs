using System.Collections.ObjectModel;
using System.Windows.Input;
using MusicPlayer.Utils;

namespace MusicPlayer.Model;

public class LeafNode
{
    public string PlaylistName { get; set; }
    public List<string> Songs { get; set; }
    public ICommand ButtonCommand { get; set; }
    
    public LeafNode(string playlistName, List<string> songs, ObservableCollection<MusicFile> musicFilesList)
    {
        PlaylistName = playlistName;
        Songs = new List<string>(songs);
        ButtonCommand = new RelayCommand(param => ExecuteCommand(musicFilesList));
    }

    private void ExecuteCommand(ObservableCollection<MusicFile> musicFilesList)
    {
        musicFilesList.Clear();
        foreach (var song in Songs)
        {
            MusicFile mf = new MusicFile();
            mf.FilePath = Metadata.absolutePath + "\\" + PlaylistName + "\\" + song;
            mf.Title = song; 
            musicFilesList.Add(mf);
        }
    }
}