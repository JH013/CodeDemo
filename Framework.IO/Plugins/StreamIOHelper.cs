using System.IO;
using System.Text;

namespace Framework.IO
{
    /// <summary>
    /// StreamIO读写
    /// </summary>
    /// <remarks>
    /// 1. 用于文本的读写和保存，支持编码
    /// 2. StreamReader & StreamWriter
    /// </remarks>
    public static class StreamIOHelper
    {
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码方式：UTF-8，GB2312</param>
        /// <returns>读到的内容</returns>
        public static string Read(string filePath, string encoding = "UTF-8")
        {
            using (StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding(encoding)))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="filePath"><文件路径/param>
        /// <param name="content">写入的内容</param>
        /// <param name="encoding">编码方式：UTF-8，GB2312</param>
        public static void Write(string filePath, string content, string encoding = "UTF-8")
        {
            Stream stream = null;
            try
            {
                stream = new FileStream(filePath, FileMode.Append);
                using (StreamWriter writer = new StreamWriter(stream, Encoding.GetEncoding(encoding)))
                {
                    stream = null;
                    writer.Write(content);
                }
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }
    }
}
