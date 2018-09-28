using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SuperSokcetLib
{
    public class Server
    {
        public Server(int port)
        {
            serverPort = port;
            appServer = new AppServer();
            appServer.NewSessionConnected += new SessionHandler<AppSession>(NewSessionConnected);
            appServer.SessionClosed += NewSessionClosed;
            appServer.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(NewRequestReceived);
        }

        private int serverPort;
        private AppServer appServer;

        public void Start()
        {
            if (!appServer.Setup(serverPort))
            {
                throw new Exception($"The server with port {serverPort} setup failed.");
            }

            if (!appServer.Start())
            {
                throw new Exception($"The server with port {serverPort} start failed.");
            }
        }

        public IEnumerable<AppSession> GetAllSessions()
        {
            return appServer.GetAllSessions();
        }

        public void Send(string sessionId, string message)
        {
            var session = appServer.GetSessionByID(sessionId);
            if (session == null)
            {
                throw new Exception($"The session {sessionId} is closed.");
            }
            session.Send(message);
        }

        private void NewSessionConnected(AppSession session)
        {
        }

        private void NewSessionClosed(AppSession session, CloseReason aaa)
        {
        }

        private void NewRequestReceived(AppSession session, StringRequestInfo requestInfo)
        {
        }
    }
}
