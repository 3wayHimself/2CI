using System;
using System.Collections.Generic;
using System.Linq;


namespace de.netcrave._2CI.Shared
{
    public interface IAudioBackend
    {
        void Play();
        void Pause();
        void Stop();
        void Seek(double position);        

    }
}
