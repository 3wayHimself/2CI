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
    public class Backend : IAudioBackend
    {
        private readonly MusicPlayer _musicPlayer = new MusicPlayer();
        private AudioDevice _device;
        public IAudioDevice device { get { return _device; } }

        public IAudioDevice[] GetAudioDevices()
        {
            List<IAudioDevice> devices = new List<IAudioDevice>();
            using (var mmdeviceEnumerator = new MMDeviceEnumerator())
            {
                using (
                    var mmdeviceCollection = mmdeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
                {
                    foreach (var device in mmdeviceCollection)
                    {
                        devices.Add(new AudioDevice(device));
                    }
                }
            }
            return devices.ToArray();
        }

        public void SelectAudioDevice(IAudioDevice device)
        {
            throw new NotImplementedException();
        }
        public Backend()
        {
            
        }

        public void Pause()
        {
            _musicPlayer.Pause();
        }

        public void Play()
        {
            _musicPlayer.Play();
        }

        public void Play(AudioFile af)
        {
            _musicPlayer.Open(af.FileName, _device.mm_device);
            _musicPlayer.Play();
        }

        public void Seek(long position)
        {
            _musicPlayer.Position = new TimeSpan(position);
        }

        public void Seek(double position)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            _musicPlayer.Stop();
        }
    }
}
