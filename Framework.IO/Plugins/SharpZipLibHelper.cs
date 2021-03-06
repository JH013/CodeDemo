﻿using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace Framework.IO
{
    /// <summary>
    /// zip文件解压缩
    /// </summary>
    public static class SharpZipLibHelper
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="sourceDirPath">源文件夹路径</param>
        /// <param name="destZipPath">目标文件路径</param>
        public static void Compress(string sourceDirPath, string destZipPath)
        {
            if (string.IsNullOrEmpty(sourceDirPath))
            {
                throw new FileNotFoundException("Source directory is null.");
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

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="sourceZipPath">源zip文件路径</param>
        /// <param name="destDirPath">目标文件夹路径</param>
        public static void Decompress(string sourceZipPath, string destDirPath)
        {
            if (string.IsNullOrEmpty(sourceZipPath))
            {
                throw new FileNotFoundException("Source zip file is null.");
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

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(destDirPath + theEntry.Name))
                        {

                            var size = 2048;
                            byte[] data = new byte[size];
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
    }
}
