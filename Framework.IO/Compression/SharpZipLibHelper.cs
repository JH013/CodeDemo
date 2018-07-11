using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace Framework.IO
{
    public class SharpZipLibHelper
    {
        public static void Compress(string sourceDirPath, string destZipPath)
        {
            if (string.IsNullOrEmpty(sourceDirPath))
            {
                throw new Exception("Source directory is null.");
            }
            if (!Directory.Exists(sourceDirPath))
            {
                throw new FileNotFoundException("Source directory not exist.", sourceDirPath);
            }

            if (string.IsNullOrEmpty(destZipPath))
            {
                if (sourceDirPath.EndsWith("//"))
                {
                    sourceDirPath = sourceDirPath.Substring(0, sourceDirPath.Length - 1);
                }
                destZipPath = sourceDirPath + ".zip";
            }

            try
            {
                string[] filenames = Directory.GetFiles(sourceDirPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(destZipPath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void Decompress(string sourceZipPath, string destDirPath)
        {
            if (string.IsNullOrEmpty(sourceZipPath))
            {
                throw new Exception("Source zip file is null.");
            }
            if (!File.Exists(sourceZipPath))
            {
                throw new FileNotFoundException("Source zip file not exist.", sourceZipPath);
            }
            if (!destDirPath.EndsWith("//"))
            {
                destDirPath += "//";
            }

            if (!Directory.Exists(destDirPath))
            {
                Directory.CreateDirectory(destDirPath);
            }

            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(sourceZipPath)))
                {

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(destDirPath + directoryName);
                        }
                        if (!directoryName.EndsWith("//"))
                        {
                            directoryName += "//";
                        }
                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(destDirPath + theEntry.Name))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
