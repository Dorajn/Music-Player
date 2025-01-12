using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MusicPlayer.Utils;

public class AudioPlayerMedia : IAudioPlayer
{
    private MediaPlayer _player;
    private string? _currentAudioFilePath = null;

    public AudioPlayerMedia()
    {
        _player = new MediaPlayer();
    }

    public void Play(string filePath)
    {
        try
        {
            _player.Open(new Uri(filePath));
            _player.Play();
            _currentAudioFilePath = filePath;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while playing audio. \n" + ex.ToString());
            throw;
        }
    }

    public void Stop()
    {
        _player.Stop();
        _currentAudioFilePath = null;
    }

    public void Pause()
    {
        _player.Pause();
    }

    public void Resume()
    {
        if (_currentAudioFilePath != null)
        {
            Play(_currentAudioFilePath);
        }
    }

    public void Volume(float volume)
    {
        if (volume >= 0 && volume <= 1)
        {
            _player.Volume = volume;
        }
    }

}
