using System;
using System.Collections.Generic;
using System.Linq;
using de.netcrave._2CI.DB;


namespace de.netcrave._2CI.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            MusicDatabase._.InitializeFromPath(@"G:\New folder (2)", true);
            MusicDatabase._.InitializeFromPath(@"H:\incoming3\files.sq10.net", true);        
            MusicDatabase._.InitializeFromPath(@"K:\finished bt downloads", true);        
            MusicDatabase._.InitializeFromPath(@"G:\Vyzo Music", true);
            MusicDatabase._.InitializeFromPath(@"K:\new bt downloads\Ministry Audiophile Vinyl 24-96 FLAC", true);

            Console.ReadKey();
        }
    }
}