using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;

namespace de.netcrave._2CI.DB
{
    /// <summary>
    /// these will all go into one single file
    /// </summary>
    class DBFragmentIndex
    {
        public ConcurrentBag<DBFragment> fragments = new ConcurrentBag<DBFragment>();
        public DBFragmentIndex()
        { 
        }        
    }
}
