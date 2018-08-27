using Framework.Http;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Thread> pool = new List<Thread>();

            //for (int i = 0; i <= 100; i++)
            //{
            //    pool.Add(new Thread(TestHttp));
            //}

            //foreach (var thread in pool)
            //{
            //    thread.Start();
            //}

            //var result = WebRequestHelper.Post("http://localhost:8005/api/Test/PostMethod", "{\"Text\":\"price\",\"Type\":\"xdianhu_house\"}", "application/json");


            string str = "我不是那么需要呀";
            Regex reg = new Regex(".*不.*需要.*");

            MatchCollection mc = reg.Matches(str);

            foreach(var m in mc)
            {

            }



            Console.ReadKey();
        }

        public static void TestHttp()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = WebRequestHelper.Get("http://localhost:8005/api/Test/GetMethod");
            stopwatch.Stop();
            Console.WriteLine($"result->{result},pid->{Thread.CurrentThread.ManagedThreadId}, timeout->{stopwatch.ElapsedMilliseconds}");
        }
    }
}
