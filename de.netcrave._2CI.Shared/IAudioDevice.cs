using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.netcrave._2CI.Model
{
    public interface IAudioDevice
    {
        AudioDeviceCapabilities capabilities { get; set; }
    }

}
