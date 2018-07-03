using System.IO;
using System.Text;

namespace Framework.IO
{
    /// <summary>
    /// StreamReader & StreamWriter
    /// 用于文本的读写和保存，支持编码
    /// </summary>
    public class StreamXXXIO
    {
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">入内容</param>
        /// <param name="encoding">编码方式，UTF-8，GB2312</param>
        /// <returns></returns>
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
        /// <param name="content">写入内容</param>
        /// <param name="encoding">编码方式，UTF-8，GB2312</param>
        public static void Write(string filePath, string content, string encoding = "UTF-8")
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Append))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.GetEncoding(encoding)))
                {
                    writer.Write(content);
                }
            }
        }
    }
}
