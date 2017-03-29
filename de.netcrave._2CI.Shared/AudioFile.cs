using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using de.netcrave._2CI.Model;

namespace de.netcrave._2CI.Model
{
    public class AudioFile
    {
        public string FileName;
        public string Header;
        public string Title;
        public string Artist;
        public string Album;
        public string Year;
        public string Comment;
        public int Track;
        public string Genre;
        public int Samplerate;
        public int Channels;
        public int Length;
        public Assets.AssetFileType FileType;
        public DBFragment ParentDBFragment;

        
        public AudioFile(string file, Assets.AssetFileType ft, DBFragment parent)
        {

        }
          
    }
}
