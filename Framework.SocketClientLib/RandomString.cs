using System;
using System.Linq;

namespace Framework.SocketClientLib
{
    public class RandomString
    {
        private const string alphanum = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int len = 12;
        private static Random random;
        private static object _lock = new object();

        public static string Generate()
        {
            lock (_lock)
            {
                if (random == null)
                {
                    random = new Random();
                }

                char[] result = new char[len];
                var alphanumArr = alphanum.ToArray();
                for (int i = 0; i < len; ++i)
                {
                    result[i] = alphanumArr[random.Next(0, alphanum.Length - 1)];
                }

                return new string(result);
            }
        }
    }
}
