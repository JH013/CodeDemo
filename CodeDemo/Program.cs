using Framework.Http;
using Framework.IO;
using System;
using System.Diagnostics;
using System.Threading;

namespace CodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ca = FileStreamIO.Read("E:\\12345.txt", 9, 0, "GB2312");

            //FileStreamIO.Write("E:\\12345.txt", "mb", 2, "GB2312");

            //var ji = StreamXXXIO.Read("E:\\12345.txt", "GB2312");

            //StreamXXXIO.Write("E:\\12345.txt", "明明", "GB2312");

            //var i = TxtFileHelper.TxtSpecialLineRead("E:\\12345.txt", 5);
            //var j = TxtFileHelper.TxtSpecialLineRead("E:\\12345.txt", 4);
            //var k = TxtFileHelper.TxtSpecialLineRead("E:\\12345.txt", 10);

            //var jj2 = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");

            //var start = DateTime.Now;
            //Thread.Sleep(3000);
            //var end = DateTime.Now;
            //TimeSpan duration = end - start;
            //var jj = duration.Ticks;
            //if (duration > new TimeSpan(40000000))
            //{

            //}


            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var jj = WebRequestHelper.Get(@"http://192.168.1.150:5000/parse?q=价格怎么样&model=model_20180605-042101");
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            var time = ts.Milliseconds;
        }
    }
}
