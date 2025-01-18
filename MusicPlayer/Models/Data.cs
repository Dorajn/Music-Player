using System.IO;
using MusicPlayer.Utils;

public class AudioFile
{
    public string Name { get; internal set; }
    public string Format { get; internal set; }
}

public class Playlist
{
    public string Name { get; internal set; }
    public List<AudioFile> AudioFiles { get; internal set; } = [];
}

public class Data
{
    public List<Playlist> Playlists { get; private set; } = [];

    public Data(string directoryPath)
    {
        string[] formats = { ".mp3", ".wav", ".aiff" };
        string[] playlistPaths = Directory.GetDirectories(directoryPath);
        foreach (string playlistPath in playlistPaths)
        {
            DirectoryInfo playlistInfo = new DirectoryInfo(playlistPath);
            List<AudioFile> audioFiles = playlistInfo
                .GetFiles()
                .Where(file => formats.Any(format => Path.GetExtension(file.Name) == format))
                .Select(file => new AudioFile
                {
                    Name = Path.GetFileNameWithoutExtension(file.Name),
                    Format = Path.GetExtension(file.Name),
                })
                .ToList();
            Playlist playlist = new Playlist { Name = playlistInfo.Name, AudioFiles = audioFiles };
            Playlists.Add(playlist);
        }
    }

    public static string? GetLyrics(string playlistName, string audioFileName)
    {
        string filePath =
            Metadata.absolutePath + "\\" + playlistName + "\\" + audioFileName + ".txt";

        if (!File.Exists(filePath))
        {
            return null;
        }

        return File.ReadAllText(filePath);
    }

    // For debuging
    public void Print()
    {
        foreach (var playlist in Playlists)
        {
            Console.WriteLine(playlist.Name);
            foreach (var audioFile in playlist.AudioFiles)
            {
                Console.WriteLine("-- " + audioFile.Name);
            }
        }
    }
}
