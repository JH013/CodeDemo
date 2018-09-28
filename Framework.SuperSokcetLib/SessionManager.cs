using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;

namespace Framework.SuperSokcetLib
{
    public class SessionManager
    {
        static SessionManager()
        {
            manager = new SessionManager();
            sessionDic = new Dictionary<string, AppSession>();
        }

        private static SessionManager manager;
        private static Dictionary<string, AppSession> sessionDic;

        public static void Add(AppSession session)
        {
            if (sessionDic.ContainsKey(session.SessionID))
            {
                throw new Exception($"The sission with id {session.SessionID} has exist.");
            }
            sessionDic.Add(session.SessionID, session);
        }

        public static void Remove(string sessionID)
        {
            if (sessionDic.ContainsKey(sessionID))
            {
                sessionDic.Remove(sessionID);
            }
        }
    }
}
