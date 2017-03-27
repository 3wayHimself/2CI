using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using System.Reflection;


namespace de.netcrave._2CI.DB
{
    public class MusicDatabase
    {
        DBFragmentIndex FragmentIndex = new DBFragmentIndex();
        private static MusicDatabase instance = new MusicDatabase();
        public static MusicDatabase _ { get { return instance; } }
        private MusicDatabase() { }

        /// <summary>
        /// Asset files applicable to this program, names should correspond directly to field names in AssetFileMagic class
        /// </summary>
        public enum AssetFileType
        {
            MP3 = 1,
            OGG = 2,
            WMA = 3,
            AAC = 4,
            APE = 5,
            FLAC = 6,
            WAVRIFF = 7,
            WAVWAVE = 8,
            JPEGRAW = 9,
            JPEGJFIF = 10,
            JPEGEXIF = 11,
            PNG = 12,
            UNKNOWN = 13,

        }

        /// <summary>
        /// magic signatures for relevant file types, field names should correspond directly to names in the AssetFileType enum
        /// </summary>
        class AssetFileMagic
        {
            private static AssetFileMagic instance = new AssetFileMagic();
            private AssetFileMagic() { }
            public static AssetFileMagic _ { get { return instance; } }
            public readonly byte[] MP3 = new byte[] { 0xFF, 0xFB };
            public readonly byte[] OGG = new byte[] { 0x4F, 0x67, 0x67, 0x53 };
            public readonly byte[] WMA = new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C };
            public readonly byte[] AAC = new byte[] { 0xFF, 0xF1 };
            //public readonly byte[] APE = new byte[] { };
            public readonly byte[] FLAC = new byte[] { 0x66, 0x4C, 0x61, 0x43 };
            public readonly byte[] WAVRIFF = new byte[] { 0x52, 0x49, 0x46, 0x46 };
            public readonly byte[] WAVWAVE = new byte[] { 0x57, 0x41, 0x56, 0x45 };
            public readonly byte[] JPEGRAW = new byte[] { 0xFF, 0xD8, 0xFF, 0xDB };
            public readonly byte[] JPEGJFIF = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 };
            public readonly byte[] JPEGEXIF = new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 };
            public readonly byte[] PNG = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        }
      
        /// <summary>
        /// Determines file type from file magic 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static AssetFileType GetFileType(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                byte[] magic;
                foreach (var f in AssetFileMagic._.GetType().GetFields().Where(w => w.FieldType == typeof(byte[])))
                {
                    var cur = (byte[])f.GetValue(AssetFileMagic._);
                    magic = new byte[cur.Length];
                    fs.Read(magic, 0, cur.Length);

                    if (cur.SequenceEqual(magic))
                    {
                        return (AssetFileType)System.Enum.Parse(typeof(AssetFileType), f.Name);
                    }

                    fs.Seek(0, SeekOrigin.Begin);
                }
                return AssetFileType.UNKNOWN;
            }
        }

        /// <summary>
        /// Initialize music collection database from path, intended to be called multiple times
        /// </summary>
        /// <param name="path"></param>
        /// <param name="rebuild"></param>
        public void InitializeFromPath(string path, bool rebuild)
        {
            ConcurrentQueue<string> directories = new ConcurrentQueue<string>(Directory.GetDirectories(path));
           
            Parallel.ForEach(directories, (current) =>
                {
                    Directory.GetDirectories(current).ToList().ForEach(f => directories.Enqueue(f));

                    var frag = Directory.GetFiles(current, ".2CIFragment");
                    if(frag.Length > 0)
                    {
                        throw new NotImplementedException("load fragment and index if not indexed");
                    }
                    else if(rebuild == true)
                    {
                        // create a fragment for each sub directory
                        DBFragment fragment = null;
                        ConcurrentQueue<string> ContentFiles = new ConcurrentQueue<string>(Directory.GetFiles(current).AsEnumerable());
                        ConcurrentQueue<string> Skipped = new ConcurrentQueue<string>();
                        foreach (var f in ContentFiles)
                        {
                            var CurrentFileType = GetFileType(f);
                            switch (CurrentFileType)
                            {
                                case AssetFileType.UNKNOWN:
                                    continue;                                    
                                // Other Asset files
                                case AssetFileType.JPEGRAW:
                                case AssetFileType.JPEGEXIF:
                                case AssetFileType.JPEGJFIF:
                                case AssetFileType.PNG:
                                    if(fragment == null)
                                    {
                                        // Lets avoid creating database fragments for files that aren't necesarrily associated with any music
                                        // later when (if) the fragment gets created it can revist the Skipped buffer
                                        Skipped.Enqueue(f);
                                        continue;
                                    }
                                    // Add these files which may likely be album art or playlists to the assets for the current fragment
                                    fragment.AssociatedFiles.Add(new AssociatedImageFile(f, CurrentFileType, fragment));
                                    break;
                                // Audio files
                                case AssetFileType.AAC:                                    
                                case AssetFileType.FLAC:                                    
                                case AssetFileType.MP3:                                
                                case AssetFileType.APE:
                                case AssetFileType.WAVRIFF:
                                case AssetFileType.WAVWAVE:
                                case AssetFileType.WMA:                                
                                    fragment = (fragment != null) ? fragment : new DBFragment(current);
                                    fragment.AudioFiles.Add(new AudioFile(f, CurrentFileType, fragment));
                                    break;
                            }
                        }
                        if(fragment != null)
                        {
                            FragmentIndex.fragments.Add(fragment);
                        }
                        fragment = null;
                    }
                });
        }
      
    }
}
