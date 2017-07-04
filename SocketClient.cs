using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace ChatroomClient
{
    /// <summary>
    /// 聊天室的Client端
    /// </summary>

    public class SocketClient
    {
        private string IP;                             //中央Server端IP
        private int PORT;                              //中央Server端Port號
        private int BUFFER_SIZE = 8192;                //接收資料的緩衝區大小 (連線預設)
        public bool isConnect = false;                //是否已成功連上Server
        private SocketAsyncEventArgs receiveArg = new SocketAsyncEventArgs();   //負責連接與接收的通訊端作業
        private SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();      //負責傳送的通訊端作業
        public bool isSocketClose;                     //Socket是否已關閉
        private string packetBuffer = string.Empty;    //未完整接收的封包內容
        MessageTrans msgTrans = new MessageTrans();    //server 與 client、client 與 client 間的訊息接收與傳送 (json)

        //System.Threading.Timer heart;                  //心跳包


        //委派事件
        public delegate void receiveServerMsgDelegate(string msg);       //委派，訊息丟出
        public event receiveServerMsgDelegate ReceiveServerMsgDelegate;
        
        public delegate void serverSendMsgDelegate(string msg);          //委派，將Server傳送的訊息丟出
        public event serverSendMsgDelegate ServerSendMsgDelegate;  
        
        public delegate void isConnectServerDelegate(bool isConnect);    //委派，是否連上Server訊息丟出
        public event isConnectServerDelegate IsConnectServerDelegate;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="ip">中央Server IP</param>
        /// <param name="port">中央Server Port</param>
        /// <param name="aApName"></param>
        public SocketClient(string ip, int port)
        {
            IP = ip;
            PORT = port;
            //this.heart = new System.Threading.Timer(new System.Threading.TimerCallback(Heart), null, -1, -1);
        }


        public void StartConnect()
        {
            isSocketClose = false;
            DnsEndPoint hostEntry = new DnsEndPoint(IP, PORT);                                            //設定中央Server的ip與port
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);    //初始化Socket物件
            receiveArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketEventArg_Completed);     //綁定事件(連接&接收)
            sendArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketEventArg_Completed);        //綁定事件(傳送)
            receiveArg.RemoteEndPoint = hostEntry;
            receiveArg.AcceptSocket = sock;
            sock.ConnectAsync(receiveArg);                                                                //連接
        }

        /// <summary>
        /// 傳送訊息給中央Server
        /// </summary>
        /// <param name="msg">訊息</param>
        public void SendToServer(string msg)
        {
            try
            {
                if (sendArg.SocketError == SocketError.Success)
                {
                    //ReceiveServerMsgDelegate(msg);
                    msg = msg + (char)0xF2;     //訊息結束固定加結束符號

                    byte[] buffer = Encoding.UTF8.GetBytes(msg);

                    SocketAsyncEventArgs sendEvent = new SocketAsyncEventArgs();
                    sendEvent.Completed += new EventHandler<SocketAsyncEventArgs>(SocketEventArg_Completed);

                    sendEvent.SetBuffer(buffer, 0, buffer.Length);
                    Socket sock = receiveArg.AcceptSocket;
                    sendEvent.AcceptSocket = sock;

                    bool willRaiseEvent = sock.SendAsync(sendEvent);
                    if (!willRaiseEvent)
                    {
                        ProcessSend();
                    }
                    buffer = null;
                }
                else
                {
                    ProcessError("傳送訊息給中央Server時", sendArg.SocketError);
                    IsConnectServerDelegate(false);   //將是否連上中央server訊息丟出
                }
            }
            catch (Exception e)
            {
                //ProcessError("Send Message Error", sendArg.SocketError);
                ReceiveServerMsgDelegate(msg);
            }
        }

        private void SocketEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect();
                    break;
                case SocketAsyncOperation.Receive:
                    ProcessReceive();
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend();
                    break;
                default:
                    throw new Exception("無效的處理事件");
            }
        }

        //連接完成時(等待接收)
        private void ProcessConnect()
        {
            if (receiveArg.SocketError == SocketError.Success)
            {
                string msg = msgTrans.MessageCombine("connect", "<Server>", "self", "連線建立成功");
                ReceiveServerMsgDelegate(msg);

                Socket sock = receiveArg.AcceptSocket;
                byte[] buffer = new byte[BUFFER_SIZE];
                receiveArg.SetBuffer(buffer, 0, buffer.Length);

                bool willRaiseEvent = sock.ReceiveAsync(receiveArg);
                if (!willRaiseEvent)
                {
                    ProcessReceive();
                }
                isConnect = true;
            }
            else
            {
                ProcessError("連接完成時", receiveArg.SocketError);
                isConnect = false;
                isSocketClose = true;
            }
            IsConnectServerDelegate(isConnect);   //委派，將是否連上Server訊息丟出
        }

        //從中央Server接收資料
        private void ProcessReceive()
        {
            if (receiveArg.SocketError == SocketError.Success)
            {
                //若接收訊息長度為0，結束連接
                if (receiveArg.BytesTransferred != 0)
                {
                    Socket sock = receiveArg.AcceptSocket;
                    byte[] buffer = new byte[receiveArg.BytesTransferred];
                    Buffer.BlockCopy(receiveArg.Buffer, 0, buffer, 0, buffer.Length);
                    string msg = Encoding.UTF8.GetString(buffer);

                    string[] msgs = msg.Split((char)0xF2);
                    int lastIdx = msgs.Length - 1;
                    for (int i = 0; i <= lastIdx; i++)
                    {
                        if (i == lastIdx)
                        {
                            packetBuffer = msgs[lastIdx];     //切割最後一個封包為不完整訊息，固定暫存
                            continue;
                        }
                        else if (i == 0)
                        {
                            msg = packetBuffer + msgs[i];     //切割第一個封包，固定組合暫存內容(前一則訊息過長則不為空)
                        }
                        else
                        {
                            msg = msgs[i];
                        }
                        ReceiveServerMsgDelegate(msg);    //委派，將接收的訊息丟出
                        ServerSendMsgDelegate(msg);                                                                        //委派，將接收的指令丟出
                    }
                    if (!isSocketClose)
                    {
                        bool willRaiseEvent = sock.ReceiveAsync(receiveArg);
                        if (!willRaiseEvent)
                        {
                            ProcessReceive();
                        }
                    }
                }
                else
                {
                    if (!isSocketClose)
                    {
                        Disconnect();
                        //this.heart.Change(-1, -1);   //心跳包計算時間
                    }
                    ProcessError("從中央Server接收資料時", receiveArg.SocketError);
                }
            }
            else
            {
                if (!isSocketClose)
                {
                    isSocketClose = true;
                    Disconnect();
                    //this.heart.Change(-1, -1);   //心跳包計算時間
                }
                ProcessError("從中央Server接收資料時", receiveArg.SocketError);
            }
        }

        //傳送完成時
        private void ProcessSend()
        {
            if (sendArg.SocketError != SocketError.Success)
            {
                ProcessError("傳送完成時", sendArg.SocketError);
            }
        }

        //SocketError錯誤事件處理
        private void ProcessError(string process, SocketError error)
        {
            string msg = "";
            switch (error)
            {
                case SocketError.ConnectionReset:
                    msg = string.Format("{0}: 中央伺服器斷線", process);
                    break;
                case SocketError.ConnectionRefused:
                    msg = string.Format("{0}: 中央伺服器拒絕連接，可能是伺服器未開啟", process);
                    break;
                case SocketError.TimedOut:
                    msg = string.Format("{0}: 連接逾時，或連接的中央伺服器無法回應", process);
                    break;
                case SocketError.OperationAborted:
                    msg = string.Format("{0}: 中央Server回傳【客戶端離線】", process);
                    break;
                case SocketError.Success:
                    msg = string.Format("{0}: 中央伺服器斷線", process);
                    break;
                default:
                    msg = string.Format("{0}: SocketError --- {1}", process, error);
                    break;
            }
            string catchMsg = msgTrans.MessageCombine("connect", "<Server>", "self", msg);
            ReceiveServerMsgDelegate(catchMsg);
            isConnect = false;
        }

        /// <summary>
        /// 與中央Server斷開連接
        /// </summary>
        public void Disconnect()
        {
            if (receiveArg.SocketError == SocketError.Success)
            {
                Socket sock = receiveArg.AcceptSocket;
                try
                {
                    sock.Shutdown(SocketShutdown.Both);
                }
                catch (Exception) { }

                sock.Close();
                sock.Dispose();
                sock = null;
                GC.Collect(0);
                isSocketClose = true;    //Socket已關閉
                isConnect = false;
            }
            else
            {
                ProcessError("與中央Server斷開連接時", receiveArg.SocketError);
            }
            IsConnectServerDelegate(false);     //委派，將是否連上Server訊息丟出
        }

        //心跳包 timer
        private void Heart(object state)
        {
            Disconnect();
        }
    }
}
