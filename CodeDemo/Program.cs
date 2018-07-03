using Framework.IO;

namespace CodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ca = FileStreamIO.Read("E:\\12345.txt", 9, 0, "GB2312");

            //FileStreamIO.Write("E:\\12345.txt", "mb", 2, "GB2312");

            var ji = StreamXXXIO.Read("E:\\12345.txt", "GB2312");

            StreamXXXIO.Write("E:\\12345.txt", "明明", "GB2312");

            //var i = TxtFileHelper.TxtSpecialLineRead("E:\\12345.txt", 5);
            //var j = TxtFileHelper.TxtSpecialLineRead("E:\\12345.txt", 4);
            //var k = TxtFileHelper.TxtSpecialLineRead("E:\\12345.txt", 10);
        }
    }
}
