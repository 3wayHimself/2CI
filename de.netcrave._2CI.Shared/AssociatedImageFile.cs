using System;
using System.Collections.Generic;
using System.Text;


namespace de.netcrave._2CI.Model
{
    public class AssociatedImageFile : IAssociatedFile
    {
        public DBFragment ParentDBFragment;
        public string FileName;
        public Assets.AssetFileType FileType;
        public AssociatedImageFile(string file, Assets.AssetFileType file_type, DBFragment fragment)
        {
            FileName = file;
            ParentDBFragment = fragment;
            FileType = file_type;
        }
    }
}
