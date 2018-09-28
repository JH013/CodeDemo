using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.RedisLib
{
    public class Maaaa
    {
        static RedisStackExchangeHelper _redis = new RedisStackExchangeHelper();

        public static async Task Pub()
        {
            Console.WriteLine("请输入要发布向哪个通道？");
            var channel = Console.ReadLine();

            await Task.Delay(10);
            for (int i = 0; i < 10; i++)
            {
                await _redis.PublishAsync(channel, i.ToString());
            }

        }

        public static async Task Sub()
        {
            Console.WriteLine("请输入您要订阅哪个通道的信息？");
            var channelKey = Console.ReadLine();
            await _redis.SubscribeAsync(channelKey, (channel, message) =>
            {
                Console.WriteLine("接受到发布的内容为：" + message);
            });
            Console.WriteLine("您订阅的通道为：<< " + channelKey + " >> ! 请耐心等待消息的到来！！");
        }
    }
}
