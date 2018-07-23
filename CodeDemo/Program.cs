using Framework.Log;
using Framework.PythonInteractive;
using System;
using System.Collections.Generic;

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

            ILogger logger = Logger.GetInstance(typeof(Program));

            //PerformanceCounterCollect collector = new PerformanceCounterCollect("Processor", "% Processor Time", "_Total");

            //PerformanceCounterCollect collector2 = new PerformanceCounterCollect("Process", "% Processor Time", "ConsoleApp5");

            //PerformanceCounterCollect collector3 = new PerformanceCounterCollect("Process", "Working Set - Private", "ConsoleApp5");

            //ProcessInfo processInfo = new ProcessInfo("ConsoleApp5");

            //while (true)
            //{
            //    try
            //    {
            //        logger.Info($"CPU Utilization -> {processInfo.CpuUtilization}% --- Memory Usage -> {processInfo.MemoryUsage} MB --- Thread Count -> {processInfo.ThreadCount}");
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

            //string filename = @"http://192.168.1.249:8083/models/房产/model_20180704-015925.zip";
            //var arr = filename.Split('/');
            //var project = arr[4].ToString();
            //var model = arr[5].Substring(0, arr[5].Length - 4);
            //filename = filename.Substring(filename.LastIndexOf(@"/") + 1);

            //var result = dosplit(@"123\n\n45\n678\n\n52\n\n64\n\n", @"\n\n");

            //IronPythonHelper.RunFunc("test.py", "say_hello");

            //ProcessContainer container = new ProcessContainer();

            //container.Add(new Process());

            //var jj = container[1];

            var jj = IronPythonHelper.RunFunc();
            Console.WriteLine(jj.ToString());
            Console.ReadKey();
        }


        public static string[] DoSplit(string text, string sep)
        {
            var result = new List<string>();
            RecurrenceSplit(text, sep, result);
            return result.ToArray();
        }

        public static void RecurrenceSplit(string text, string sep, List<string> result)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            int sepIndex = text.IndexOf(sep);
            if (sepIndex == -1)
            {
                result.Add(text);
            }
            else
            {
                int tempLength = sepIndex + sep.Length;
                var temp = text.Substring(0, tempLength);
                result.Add(temp);

                RecurrenceSplit(text.Substring(tempLength, text.Length - tempLength), sep, result);
            }

        }


        public static string[] dosplit(string text, string sep)
        {
            List<string> result = new List<string>();
            char[] textArr = text.ToCharArray();
            int index = 0;
            string tmp = text;
            while (index < text.Length)
            {
                int id = tmp.IndexOf(sep);
                if (id == -1)
                {
                    result.Add(tmp.Substring(0));
                    break;
                }
                else
                {
                    id += sep.Length;
                    result.Add(tmp.Substring(0, id));
                    index += id;
                    tmp = tmp.Substring(id);
                }
            }
            return result.ToArray();
        }


        //public static string[] StringSplit(string text, string separator)
        //{
        //    List<string> result = new List<string>();
        //    char[] textArr = text.ToCharArray();
        //    int index = 0;
        //    for (int i = 0; i <= textArr.Length - 1;)
        //    {
        //        if (char.Equals(textArr[i], separator.First()))
        //        {
        //            var seLength = GetLength(textArr, i, separator);
        //            var length = i - index + seLength;
        //            result.Add(text.Substring(index, length));
        //            i = index += length;
        //        }
        //        else
        //        {
        //            i++;
        //        }
        //    }
        //    if (index < textArr.Length - 1)
        //    {
        //        result.Add(text.Substring(index, text.Length - index));
        //    }
        //    return result.ToArray();
        //}

        //public static int GetLength(char[] textArr, int index, string separator)
        //{
        //    int length = separator.Length;
        //    while (index + length <= textArr.Length)
        //    {
        //        if (index + length > textArr.Length - 1)
        //        {
        //            break;
        //        }
        //        if (char.Equals(textArr[index + length], separator.First()))
        //        {
        //            length += separator.Length;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    return length;
        //}
    }
}
