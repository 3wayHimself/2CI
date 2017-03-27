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
            Console.ReadKey();
        }
    }
}