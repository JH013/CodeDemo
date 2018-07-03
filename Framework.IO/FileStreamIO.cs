using System.IO;
using System.Text;

namespace Framework.IO
{
    /* 特点：1. FileStream表示在磁盘或网络路径上指向文件的流
     *      2. FileStream操作的是字节数据（byte）
     *      3. 默认编码是UTF-8，不能指定编码格式
     *      4. 如果读取中文的文本可能会乱码，中文必须使用System.Text.Encoding.GetEncoding("GB2312").GetChars()进行转码
     *      5. FileStream是一个较底层的类，只能简单地读文件到而缓冲区
     *      6. FileStream可以指定FileMode、FileAccess、FileShare、FileOptions等各种文件访问控制权限、共享权限等
     *      7. 提供异步读写
     * 用途：
     *      1. 用于数据传输
     *      2. 随机访问文件中间某点的数据 
     */
    public class FileStreamIO
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码方式，UTF-8，GB2312</param>
        /// <returns></returns>
        public static string Read(string filePath, string encoding = "UTF-8")
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return Encoding.GetEncoding(encoding).GetString(buffer);
            }
        }

        /// <summary>
        /// 文件写入，追加
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">写入内容</param>
        /// <param name="encoding">编码方式，UTF-8，GB2312</param>
        public static void Write(string filePath, string content, string encoding = "UTF-8")
        {
            byte[] buffer = Encoding.GetEncoding(encoding).GetBytes(content);
            using (FileStream fs = new FileStream(filePath, FileMode.Append))
            {
                fs.Write(buffer, 0, buffer.Length);
            };
        }

        /// <summary>
        /// 读取指定区间的数据，中文 1个字符 = 2个字节
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="start">起始位置，默认为0</param>
        /// <param name="end">结束位置，如果为0则默认读到末尾</param>
        /// <param name="encoding">编码方式，UTF-8，GB2312</param>
        /// <returns></returns>
        public static string Read(string filePath, int start = 0, int end = 0, string encoding = "UTF-8")
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                int length = end == 0 ? (int)fs.Length - start : end - start + 1;
                byte[] buffer = new byte[length];
                fs.Seek(start, SeekOrigin.Begin);
                fs.Read(buffer, 0, length);
                return Encoding.GetEncoding(encoding).GetString(buffer);
            }

        }

        /// <summary>
        /// 指定位置文件写入，覆盖
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">写入内容</param>
        /// <param name="seekIndex">位置索引</param>
        /// <param name="encoding">编码方式，UTF-8，GB2312</param>
        public static void Write(string filePath, string content, int seekIndex = 0, string encoding = "UTF-8")
        {
            byte[] buffer = Encoding.GetEncoding(encoding).GetBytes(content);
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                //等同于fs.Position = seekIndex;
                fs.Seek(seekIndex, SeekOrigin.Begin);
                fs.Write(buffer, 0, buffer.Length);
            };
        }
    }
}
