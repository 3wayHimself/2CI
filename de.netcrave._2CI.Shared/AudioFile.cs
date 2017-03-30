using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using de.netcrave._2CI.Model;
using System.Data.Linq.Mapping;
using System.ComponentModel;


namespace de.netcrave._2CI.Model
{
    public class AudioFile
    {
        private Guid _Id = System.Guid.NewGuid();
        private string _FileName = "";
        private string _Header = "";
        private string _Title = "";
        private string _Artist = "";
        private string _Album = "";
        private string _Year = "";
        private string _Comment = "";
        private int _Track = 0;
        private string _Genre = "";
        private int _Samplerate = 0;
        private int _Channels = 0;
        private int _Length = 0;
        private Assets.AssetFileType _FileType = Assets.AssetFileType.UNKNOWN;
        private DBFragment _ParentDBFragment;

        public Guid Id {
            get { return _Id; }
            set { _Id = value; } }
        public string FileName { get { return _FileName; } set { _FileName = value; } }
        public string Header { get { return _Header; } set { _Header = value; } }
        public string Title { get { return _Title; } set { _Title = value; } }
        public string Artist { get { return _Artist; } set { _Artist = value; } }
        public string Album { get { return _Album; } set { _Album = value; } }
        public string Year { get { return _Year; } set { _Year = value; } }
        public string Comment { get { return _Comment; } set { _Comment = value; } }
        public int Track { get { return _Track; } set { _Track = value; } }
        public string Genre { get { return _Genre; } set { _Genre = value; } }
        public int Samplerate { get { return _Samplerate; } set { _Samplerate = value; } }
        public int Channels { get { return _Channels; } set { _Channels = value; } }
        public int Length { get { return _Length; } set { _Length = value; } }
        public Assets.AssetFileType FileType { get { return _FileType; } set { _FileType = value; } }
        public DBFragment ParentDBFragment { get { return _ParentDBFragment; } set { _ParentDBFragment = value; } }

        public AudioFile(string file, Assets.AssetFileType ft, DBFragment parent)
        {
            FileName = file;
            FileType = ft;
            ParentDBFragment = parent;
        }          
    }
}
