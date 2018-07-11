using Framework.Http;
using Framework.IO;
using Framework.Log;
using Framework.Monitor;
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


            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            //var jj = WebRequestHelper.Get(@"http://192.168.1.150:5000/parse?q=价格怎么样&model=model_20180605-042101");
            //stopWatch.Stop();
            //TimeSpan ts = stopWatch.Elapsed;
            //var time = ts.Milliseconds;

            //System.Timers.Timer timer = new System.Timers.Timer();

            //timer.Interval = 500;

            //timer.Elapsed += delegate
            //{
            //    Console.WriteLine($"Timer Thread: {Thread.CurrentThread.ManagedThreadId}");

            //    Console.WriteLine($"Is Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}");

            //    Console.WriteLine("Timer Action");

            //    timer.Stop();
            //};

            //timer.Start();

            //Console.WriteLine("Main Action.");
            //Console.WriteLine($"Main Thread: {Thread.CurrentThread.ManagedThreadId}");

            //Console.ReadLine();

            //var jj = ComputerInfo.GetWin32ProcessorInfos();

            //ILogger logger = Logger.GetInstance(typeof(Program));

            //PerformanceCounterCollect collector = new PerformanceCounterCollect("Processor", "% Processor Time", "_Total");

            //PerformanceCounterCollect collector2 = new PerformanceCounterCollect("Process", "% Processor Time", "ConsoleApp5");

            //while (true)
            //{
            //    try
            //    {
            //        var result = collector2.Collect();
            //        logger.Info($"CPU Utilization -> {result / Environment.ProcessorCount}%");
            //        Thread.Sleep(1000);
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.Error("Error happened.", ex);
            //    }
            //}

            //SharpZipLibHelper.Decompress(@"E:\ziptest.zip", "E:\\test123");

            //SharpZipLibHelper.Decompress(@"C:\Users\13\Downloads\model_20180704-015925 (1).zip", "E:\\test123");

            //WebRequestHelper.DownloadFile(@"http://192.168.1.249:8083/models/房产/model_20180704-015925.zip", "E:\\123.zip");

            //string jj = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

            //CompressionHelper.Decompress(@"C:\Users\13\Downloads\model_20180704-015925.zip", "E:\\test123");

            //SharpZipLibHelper.Decompress(@"E:\123.zip", "E:\\test123");

            string filename = @"http://192.168.1.249:8083/models/房产/model_20180704-015925.zip";
            var arr = filename.Split('/');
            var project = arr[4].ToString();
            var model = arr[5].Substring(0, arr[5].Length - 4);
            filename = filename.Substring(filename.LastIndexOf(@"/") + 1);
        }
    }
}
