using System;
using System.Collections.Generic;
using System.Linq;
using de.netcrave._2CI.Shared;
using System.Diagnostics;

namespace de.netcrave._2CI.ffmpegBackend
{
    public class FFMpegBackend : IAudioBackend
    {
        public string FFMpegExe;

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Seek(double position)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
