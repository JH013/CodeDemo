using Framework.Http;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Framework.RedisLib;
using System.Threading.Tasks;
using Framework.IO;

namespace CodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var redisClient = new RedisHelper(0);
            ////消息入列
            //redisClient.ListRightPush("TaskMessage2", new TaskMessage { MerchantId = 1, TaskId = 1 });
            ////redisClient.ListRightPush("TaskMessage", new TaskMessage { MerchantId = 2, TaskId = 2 });
            ////redisClient.ListRightPush("TaskMessage", new TaskMessage { MerchantId = 3, TaskId = 3 });
            ////redisClient.ListRightPush("TaskMessage", new TaskMessage { MerchantId = 4, TaskId = 4 });

            //////var jj = MessageQueue.CurrentChatModels;

            ////var jj = redisClient.ListLeftPop<ChatModels>("Test");

            //var jj = DateTime.Now.ToString("yyyy");

            //var aa = 10000001 % 20;


            //var count = redisClient.ListLength("TaskMessage");


            //SharpZipLibHelper.Decompress(@"D:\wz_workspace\document\其它\indx.zip", @"D:\wz_workspace\document");

            SharpZipLibHelper.Compress(@"D:\wz_workspace\document\test", @"D:\wz_workspace\document\test.zip");

            Console.ReadLine();
        }
    }
}
