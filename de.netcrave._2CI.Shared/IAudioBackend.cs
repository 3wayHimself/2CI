using System;
using System.Collections.Generic;
using System.Linq;

namespace de.netcrave._2CI.Model
{
    public interface IAudioBackend
    {
        IAudioDevice device { get; }
        void Play();
        void Play(AudioFile af);
        void Pause();
        void Stop();
        void Seek(double position);
        IAudioDevice[] GetAudioDevices();
        void SelectAudioDevice(IAudioDevice device);

    }
}
