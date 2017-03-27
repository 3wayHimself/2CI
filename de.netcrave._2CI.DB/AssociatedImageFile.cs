using System;
using System.Collections.Generic;
using System.Text;

namespace de.netcrave._2CI.DB
{
    class AssociatedImageFile : IAssociatedFile
    {
        public DBFragment ParentDBFragment;
        public string FileName;
        public MusicDatabase.AssetFileType FileType;
        public AssociatedImageFile(string file, MusicDatabase.AssetFileType file_type, DBFragment fragment)
        {
            FileName = file;
            ParentDBFragment = fragment;
            FileType = file_type;
        }
    }
}
