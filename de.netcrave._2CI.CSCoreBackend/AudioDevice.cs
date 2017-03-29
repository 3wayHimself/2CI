using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de.netcrave._2CI.Model;
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using CSCore.Streams;

namespace de.netcrave._2CI.CSCoreBackend
{
    class AudioDevice : IAudioDevice
    {
        public AudioDeviceCapabilities capabilities { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MMDevice mm_device;               
        
        public AudioDevice(MMDevice m)
        {
            mm_device = m;
        }
        public AudioDevice()
        {

        }
    }
}
