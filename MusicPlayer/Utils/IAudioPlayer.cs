using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Utils;

public interface IAudioPlayer
{
    void Play(string filePath);
    void Pause();
    void Stop();
    void Resume();
    void Volume(float volume);
}
