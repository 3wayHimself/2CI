using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.netcrave._2CI.Model
{
    public class Assets
    {       
        /// <summary>
        /// Asset files applicable to this program, names should correspond directly to field names in AssetFileMagic class
        /// </summary>
        public enum AssetFileType
        {
            /// <summary>
            /// MP3 File with no ID3v2 tag or No ID3 tag at all
            /// </summary>
            MP3ID3v1ORNOTAG,
            /// <summary>
            /// MP3 File with ID3v2 tags
            /// </summary>
            MP3WITHID3v2,
            OGG,
            WMA,
            AAC,
            APE,
            FLAC,
            WAVRIFF,
            WAVWAVE,
            JPEGRAW,
            JPEGJFIF,
            JPEGEXIF,
            PNG,
            UNKNOWN,
        }
        /// <summary>
        /// magic signatures for relevant file types, field names should correspond directly to names in the AssetFileType enum
        /// </summary>
        public class AssetFileMagic
        {
            private static AssetFileMagic instance = new AssetFileMagic();
            private AssetFileMagic() { }
            public static AssetFileMagic _ { get { return instance; } }
            public readonly byte[] MP3ID3v1ORNOTAG = new byte[] { 0xFF, 0xFB }; // FMPEG-1 Layer 3 file without an ID3 tag or with an ID3v1 tag (which's appended at the end of the file)
            public readonly byte[] MP3WITHID3v2 = new byte[] { 0x49, 0x44, 0x33 };
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

    }
  
}
