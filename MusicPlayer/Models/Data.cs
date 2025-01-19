using System.Diagnostics;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic.Devices;
using MusicPlayer.Model;
using MusicPlayer.Utils;

public class AudioFile
{
    public string Name { get; internal set; }
    public string Extension { get; internal set; }
}

public class Playlist
{
    public string Name { get; internal set; }
    public List<AudioFile> AudioFiles { get; internal set; } = [];
}

public class Data
{
    private string[] _extensions = { ".mp3", ".wav", ".aiff" };
    public List<Playlist> Playlists { get; private set; } = [];

    public Data(string directoryPath)
    {
        string[] playlistPaths = Directory.GetDirectories(directoryPath);
        foreach (string playlistPath in playlistPaths)
        {
            DirectoryInfo playlistInfo = new DirectoryInfo(playlistPath);
            List<AudioFile> audioFiles = playlistInfo
                .GetFiles()
                .Where(file =>
                    _extensions.Any(extension => Path.GetExtension(file.Name) == extension)
                )
                .Select(file => new AudioFile
                {
                    Name = Path.GetFileNameWithoutExtension(file.Name),
                    Extension = Path.GetExtension(file.Name),
                })
                .ToList();
            Playlist playlist = new Playlist { Name = playlistInfo.Name, AudioFiles = audioFiles };
            Playlists.Add(playlist);
        }
    }

    public void AddAudioFiles(string playlistName, string[] audioFilePaths)
    {
        string destinationPlaylist = Metadata.absolutePath + "\\" + playlistName;
        var playlist = Playlists.Find(playlist => playlist.Name == playlistName);
        foreach (var audioFile in audioFilePaths)
        {
            string audioFileExtension = Path.GetExtension(audioFile);

            if (!_extensions.Any(extension => audioFileExtension == extension))
            {
                continue;
            }

            string audioFileName = Path.GetFileNameWithoutExtension(audioFile);
            string destinationAudioFile =
                destinationPlaylist + "\\" + audioFileName + audioFileExtension;

            if (File.Exists(destinationAudioFile))
            {
                continue;
            }

            File.Copy(audioFile, destinationAudioFile);

            playlist.AudioFiles.Add(
                new AudioFile { Name = audioFileName, Extension = audioFileExtension }
            );
        }
    }

    public static string? GetLyrics(MusicFile song)
    {
        string filePath = Metadata.absolutePath + "\\" + song.Playlist + "\\" + song.Title + ".txt";

        if (!File.Exists(filePath))
        {
            return null;
        }

        return File.ReadAllText(filePath);
    }

    public static void CreateAndOpenFile(MusicFile song)
    {
        string filePath = Metadata.absolutePath + "\\" + song.Playlist + "\\" + song.Title + ".txt";

        if (!File.Exists(filePath))
        {
            using (StreamWriter writer = new StreamWriter(filePath))
                ;
        }

        Process.Start("notepad.exe", filePath);
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
