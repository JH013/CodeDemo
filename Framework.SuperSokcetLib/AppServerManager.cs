using SuperSocket.SocketBase;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SuperSokcetLib
{
    public class AppServerManager
    {
        static AppServerManager()
        {
            serverManager = new AppServerManager();
            appServerDic = new Dictionary<int, AppServer>();
        }

        private static AppServerManager serverManager;
        private static Dictionary<int, AppServer> appServerDic;

        public static void StartNewServer(int port)
        {
            if (appServerDic.ContainsKey(port))
            {
                throw new Exception($"The server with port {port} has exist.");
            }

            AppServer appServer = new AppServer();

            if (appServer.Setup(port))
            {
                throw new Exception($"The server with port {port} setup failed.");
            }

            if (!appServer.Start())
            {
                throw new Exception($"The server with port {port} start failed.");
            }

            appServerDic.Add(port, appServer);
        }
    }
}
