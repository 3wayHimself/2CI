using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using System.Reflection;
using de.netcrave._2CI.Model;

namespace de.netcrave._2CI.DB
{
    public class MusicDatabase
    {
        // note to self find dupes by similarity percentage and show in other window
        public DBFragmentIndex FragmentIndex = new DBFragmentIndex();
        private static MusicDatabase instance = new MusicDatabase();
        public static MusicDatabase _ { get { return instance; } }
        private MusicDatabase() { }

       
      
        /// <summary>
        /// Determines file type from file magic 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Assets.AssetFileType GetFileType(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                byte[] magic;
                foreach (var f in Assets.AssetFileMagic._.GetType().GetFields().Where(w => w.FieldType == typeof(byte[])))
                {
                    var cur = (byte[])f.GetValue(Assets.AssetFileMagic._);
                    magic = new byte[cur.Length];
                    fs.Read(magic, 0, cur.Length);

                    if (cur.SequenceEqual(magic))
                    {
                        return (Assets.AssetFileType)System.Enum.Parse(typeof(Assets.AssetFileType), f.Name);
                    }

                    fs.Seek(0, SeekOrigin.Begin);
                }
                return Assets.AssetFileType.UNKNOWN;
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
                        DBFragment fragment = new DBFragment(current);
                        ConcurrentQueue<string> ContentFiles = new ConcurrentQueue<string>(Directory.GetFiles(current));
                        ConcurrentQueue<IAssociatedFile> Pending = new ConcurrentQueue<IAssociatedFile>();
                        foreach (var f in ContentFiles)
                        {
                            var CurrentFileType = GetFileType(f);
                            switch (CurrentFileType)
                            {
                                case Assets.AssetFileType.UNKNOWN:
                                    continue;                                    
                                // Other Asset files
                                case Assets.AssetFileType.JPEGRAW:
                                case Assets.AssetFileType.JPEGEXIF:
                                case Assets.AssetFileType.JPEGJFIF:
                                case Assets.AssetFileType.PNG:
                                    if(fragment == null)
                                    {
                                        // Lets avoid commiting database fragments for files that aren't necesarrily associated with any music
                                        // later when (if) the fragment gets created it can revist the Skipped buffer
                                        Pending.Enqueue(new AssociatedImageFile(f, CurrentFileType, fragment));
                                        continue;
                                    }
                                    // Add these files which may likely be album art or playlists to the assets for the current fragment
                                    fragment.AssociatedFiles.Add(new AssociatedImageFile(f, CurrentFileType, fragment));
                                    break;
                                // Audio files
                                case Assets.AssetFileType.AAC:                                    
                                case Assets.AssetFileType.FLAC:                                    
                                case Assets.AssetFileType.MP3:                                
                                case Assets.AssetFileType.APE:
                                case Assets.AssetFileType.WAVRIFF:
                                case Assets.AssetFileType.WAVWAVE:
                                case Assets.AssetFileType.WMA:                                
                                    //fragment = (fragment != null) ? fragment : new DBFragment(current);
                                    fragment.AudioFiles.Add(new AudioFile(f, CurrentFileType, fragment));
                                    foreach(var s in Pending)
                                    {
                                        fragment.AssociatedFiles.Add(s);
                                    }
                                    break;
                            }
                        }
                        // We won't index / save fragments that don't have any music associated with them
                        if(fragment != null && fragment.AudioFiles.Count() > 0)
                        {
                            FragmentIndex.fragments.Add(fragment);
                        }
                        fragment = null;
                    }
                });
        }
      
    }
}
