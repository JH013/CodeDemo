using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Framework.IO
{
    /// <summary>
    /// txt文件操作，适用小文件
    /// </summary>
    public class TxtFileHelper
    {
        /// <summary>
        /// 文本文件写入，文件不存在则创建，覆盖原文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">写入内容</param>
        public static void TxtWrite(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// txt文件追加,文件不存在则创建，换行使用Environment.NewLine或者\r\n
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">追加内容</param>
        public static void TxtAppend(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }

        /// <summary>
        /// txt文件读取
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string TxtRead(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// txt文件单行写入，文件不存在则创建
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">写入内容</param>
        /// <param name="append">是否追加</param>
        public static void TxtSingleLineWrite(string filePath, string content, bool append = false)
        {
            using (StreamWriter writer = new StreamWriter(filePath, append))
            {
                writer.WriteLine(content);
            }
        }

        /// <summary>
        /// txt文件读取单行读取，即读取首行
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <returns></returns>
        public static string TxtSingleLineRead(string filePath)
        {
            using (StreamReader reader = File.OpenText(filePath))
            {
                return reader.ReadLine();
            }
        }

        /// <summary>
        /// txt文件读取指定行数据
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <param name="lineIndex">行号，从0开始</param>
        /// <returns></returns>
        public static string TxtSpecialLineRead(string filePath, int lineIndex)
        {
            string result = string.Empty;
            using (StreamReader reader = File.OpenText(filePath))
            {
                int index = 0;
                while (reader.Peek() != -1)
                {
                    string temp = reader.ReadLine();
                    if (lineIndex == index)
                    {
                        result = temp;
                        break;
                    }
                    index++;
                }
                return result;
            }
        }

        /// <summary>
        /// txt文件移除指定行
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lineIndex">行号，从0开始</param>
        public static void TxtSpecialLineRemove(string filePath, int lineIndex)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList(); ;
            lines.RemoveAt(lineIndex);
            File.WriteAllLines(filePath, lines.ToArray());
        }

        /// <summary>
        /// txt文件指定行插入数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lineIndex">行号。从0开始</param>
        /// <param name="content">插入内容</param>
        public static void TxtSpecialLineInsert(string filePath, int lineIndex, string content)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            if (lineIndex <= lines.Count - 1)
            {
                lines.Insert(lineIndex, content);
            }
            else
            {
                for (int i = lines.Count; i < lineIndex; i++)
                {
                    lines.Add(string.Empty);
                }
                lines.Add(content);
            }
            File.WriteAllLines(filePath, lines.ToArray());
        }

        /// <summary>
        /// txt文件修改指定行数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lineIndex">行号，从0开始</param>
        /// <param name="content">修改内容</param>
        public static void TxtSpecialLineModify(string filePath, int lineIndex, string content)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            if (lineIndex <= lines.Count - 1)
            {
                lines[lineIndex] = content;
            }
            else
            {
                for (int i = lines.Count; i < lineIndex; i++)
                {
                    lines.Add(string.Empty);
                }
                lines.Add(content);
            }
            File.WriteAllLines(filePath, lines.ToArray());
        }
    }
}
