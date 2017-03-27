using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace de.netcrave._2CI.DB
{
    public class DBFragment
    {
        public string RootDirectory;

        public ConcurrentBag<AudioFile> AudioFiles = new ConcurrentBag<AudioFile>();
        public ConcurrentBag<IAssociatedFile> AssociatedFiles = new ConcurrentBag<IAssociatedFile>();

        public DBFragment(string dir)
        {
            RootDirectory = dir;
        }
    }
}
