using System.IO;

public class Data
{
    public class Playlist
    {
        public string Name { get; internal set; };
        public List<string> AudioNames { get; internal set; } = [];
    }

    public List<Playlist> FetchedPlaylists { get; private set; } = [];

    public Data(string directoryPath)
    {
        string[] formats = { ".mp3", ".wav", ".aiff" };
        string[] playlistPaths = Directory.GetDirectories(directoryPath);
        foreach (string playlistPath in playlistPaths)
        {
            DirectoryInfo playlistInfo = new DirectoryInfo(playlistPath);
            List<string> audioNames = playlistInfo
                .GetFiles()
                .Where(file =>
                    formats.Any(ext => file.Name.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                )
                .Select(file => file.Name)
                .ToList();
            Playlist playlist = new Playlist { Name = playlistInfo.Name, AudioNames = audioNames };
            FetchedPlaylists.Add(playlist);
        }
    }

    // For debuging
    public void Print()
    {
        foreach (var playlist in FetchedPlaylists)
        {
            Console.WriteLine(playlist.Name);
            foreach (var audioName in playlist.AudioNames)
            {
                Console.WriteLine("-- " + audioName);
            }
        }
    }
}
