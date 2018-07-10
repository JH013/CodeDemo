using log4net;
using System;

namespace Framework.Log
{
    public class Log
    {
        private Log()
        {

        }

        public static readonly ILog loginfo = LogManager.GetLogger("loginfo");

        public static readonly ILog logerror = LogManager.GetLogger("logerror");


        public static void Info(string message)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(message);
            }
        }

        public static void Warn(string info)
        {
            if (loginfo.IsWarnEnabled)
            {
                loginfo.Warn(info);
            }
        }

        public static void Error(string message, Exception ex)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(message, ex);
            }
        }
    }
}
