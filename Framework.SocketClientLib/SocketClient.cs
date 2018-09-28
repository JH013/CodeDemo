using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SocketClientLib
{
    /// <summary>
    /// Socket客户端
    /// </summary>
    public class SocketClient
    {
        public SocketClient(string ip, int port)
        {
            _ip = ip;
            _port = port;
            callbackDic = new ConcurrentDictionary<string, Action<SocketResponse>>();
        }

        public Action<SocketClient> HandleClientStarted { get; set; }
        public Action<SocketRequest> HandleRequest { get; set; }
        public Action<SocketClient> HandleClientClose { get; set; }


        private Socket _socket;
        private string _ip;
        private int _port;
        private bool _isRec = true;
        private Encoding encoding = Encoding.UTF8;

        private ConcurrentDictionary<string, Action<SocketResponse>> callbackDic;

        public void Start()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse(_ip);
            IPEndPoint endpoint = new IPEndPoint(address, _port);
            _socket.BeginConnect(endpoint, asyncResult =>
            {
                _socket.EndConnect(asyncResult);
                this.StartRecMsg();
                this.HandleClientStarted?.Invoke(this);
            }, null);
        }

        public void Send(SocketRequest request, Action<SocketResponse> callback)
        {
            this.Send(JsonConvert.SerializeObject(request));

            callbackDic.TryAdd(request.Transcation, callback);
        }

        public void Send(SocketResponse response)
        {
            this.Send(JsonConvert.SerializeObject(response));
        }

        public void Close()
        {
            _isRec = false;
            _socket.Disconnect(false);
            HandleClientClose?.Invoke(this);
        }

        private void Send(string msg)
        {
            byte[] bytes = encoding.GetBytes($"X {msg}\r\n");
            _socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, asyncResult =>
            {
                int length = _socket.EndSend(asyncResult);
            }, null);
        }

        //private void SendLarge(string msg)
        //{
        //    Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    //IPAddress ip = new IPAddress;


        //    byte[] fileName = Encoding.UTF8.GetBytes(fName); //file name
        //    byte[] fileData = File.ReadAllBytes(textBox1.Text); //file
        //    byte[] fileNameLen = BitConverter.GetBytes(fileName.Length); //lenght of file name
        //    clientData = new byte[4 + fileName.Length + fileData.Length];

        //    fileNameLen.CopyTo(clientData, 0);
        //    fileName.CopyTo(clientData, 4);
        //    fileData.CopyTo(clientData, 4 + fileName.Length);

        //    textBox2.AppendText("Preparing File To Send");
        //    clientSock.Connect("127.0.0.1", 9050); //target machine's ip address and the port number
        //    clientSock.Send(clientData);
        //    //clientSock.
        //    clientSock.Close();
        //}

        private bool IsSocketConnected()
        {
            bool part1 = _socket.Poll(1000, SelectMode.SelectRead);
            bool part2 = (_socket.Available == 0);
            if (part1 && part2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void StartRecMsg()
        {
            byte[] container = new byte[1024 * 1024 * 2];
            _socket.BeginReceive(container, 0, container.Length, SocketFlags.None, asyncResult =>
            {
                int length = _socket.EndReceive(asyncResult);
                if (length > 0 && _isRec && IsSocketConnected())
                {
                    this.StartRecMsg();
                }

                if (length > 0)
                {
                    byte[] recBytes = new byte[length];
                    Array.Copy(container, 0, recBytes, 0, length);
                    this.ProcessRecevie(recBytes);
                }
                else
                {
                    this.Close();
                }
            }, null);
        }

        private Task ProcessRecevie(byte[] recBytes)
        {
            return Task.Run(() =>
            {
                try
                {
                    var receviced = encoding.GetString(recBytes);
                    receviced = receviced.Replace("\r\n", "");
                    JObject recevicedObj = JObject.Parse(receviced);
                    var transcation = recevicedObj["transcation"].ToString();

                    if (!string.IsNullOrEmpty(transcation))
                    {
                        if (this.callbackDic.ContainsKey(transcation))
                        {
                            Action<SocketResponse> callbackFunc;
                            this.callbackDic.TryRemove(transcation, out callbackFunc);
                            try
                            {
                                var response = JsonConvert.DeserializeObject<SocketResponse>(receviced);

                                callbackFunc?.Invoke(response);
                            }
                            catch (Exception ex)
                            {

                                callbackFunc?.Invoke(new SocketResponse {
                                    Transcation = transcation,
                                    Message = "ack",
                                    Status = ResponseStatus.Failed,
                                    Error = ex.ToString()
                                });
                            }
                        }
                        else
                        {
                            var request = JsonConvert.DeserializeObject<SocketRequest>(receviced);
                            this.HandleRequest?.Invoke(request);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log
                }
            });
        }
    }
}