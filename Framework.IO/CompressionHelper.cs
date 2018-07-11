using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.IO
{
    public class CompressionHelper
    {
        public static void Compress(string sourceFile, string destinationFile)
        {
            if (File.Exists(sourceFile) == false)
            {
                throw new FileNotFoundException();
            }

            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (FileStream destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] buffer = new byte[sourceStream.Length];
                using (GZipStream compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true))
                {
                    compressedStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public static void Decompress(string sourceZipPath, string destDirPath)
        {
            ZipFile.ExtractToDirectory(sourceZipPath, destDirPath);
        }
    }
}
