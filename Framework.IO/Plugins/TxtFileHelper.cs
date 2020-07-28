using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Framework.IO
{
    /// <summary>
    /// txt文件读写
    /// </summary>
    /// <remarks>
    /// 1. 适用于小文件
    /// </remarks>
    public static class TxtFileHelper
    {
        #region 读取

        /// <summary>
        /// 读取全部
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>读取的内容</returns>
        public static string ReadAll(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// 读取一行
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>读取的内容</returns>
        public static string ReadSingleLine(string filePath)
        {
            using (StreamReader reader = File.OpenText(filePath))
            {
                return reader.ReadLine();
            }
        }

        /// <summary>
        /// 读取指定行
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <param name="lineIndex">行号，从0开始</param>
        /// <returns>读取的内容</returns>
        public static string ReadSpecialLine(string filePath, int lineIndex)
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

        #endregion

        #region 写入

        /// <summary>
        /// 全部写入
        /// </summary>
        /// <remarks>
        /// 1. 文件不存在则创建
        /// 2. 覆盖原文件内容
        /// </remarks>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">写入的内容</param>
        public static void WriteAll(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// 单行写入
        /// </summary>
        /// <remarks>
        /// 1. 文件不存在则创建
        /// </remarks>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">写入的内容</param>
        /// <param name="append">是否追加</param>
        public static void WriteSingleLine(string filePath, string content, bool append = false)
        {
            using (StreamWriter writer = new StreamWriter(filePath, append))
            {
                writer.WriteLine(content);
            }
        }

        /// <summary>
        /// 全部追加
        /// </summary>
        /// <remarks>
        /// 1. 文件不存在则创建
        /// 2. 换行使用Environment.NewLine或者\r\n
        /// </remarks>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">追加的内容</param>
        public static void AppendAll(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }

        /// <summary>
        /// 指定行追加
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lineIndex">行号。从0开始</param>
        /// <param name="content">追加的内容</param>
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

        #endregion

        #region 删除

        /// <summary>
        /// 删除指定行
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lineIndex">行号，从0开始</param>
        public static void RemoveSpecialLine(string filePath, int lineIndex)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            lines.RemoveAt(lineIndex);
            File.WriteAllLines(filePath, lines.ToArray());
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改指定行
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lineIndex">行号，从0开始</param>
        /// <param name="content">修改的内容</param>
        public static void ModifySpecialLine(string filePath, int lineIndex, string content)
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

        #endregion
    }
}
