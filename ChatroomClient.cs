using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace ChatroomClient
{
    public partial class frmChatClient : Form
    {
        private SocketClient client = null;
        MessageTrans msgTrans = new MessageTrans();    //server 與 client、client 與 client 間的訊息接收與傳送 (json)
        
        public frmChatClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 中央Server連接&接收&傳送&錯誤訊息  觸發事件
        /// </summary>
        /// <param name="msg"></param>
        public void ServerMsg(string msg)
        {
            this.Invoke(new ShowServerMsgDe(ShowServerMsg), new object[] {msg});
        }
        delegate void ShowServerMsgDe(string msg);
        private void ShowServerMsg(string msg)   //收到server訊息並show出
        {
            try
            {
                string[] catchMsg = msgTrans.MessageReceive(msg, this.txtUserID.Text.ToString());
                if (catchMsg.Length == 3 && catchMsg[2] == "#msg#")   //回傳的是訊息
                {
                    this.txtServerLog.AppendText(catchMsg[0]);
                }
                else   //回傳的是清單
                {
                    this.cmbUserList.Items.AddRange(catchMsg);
                }
                ChangeBtnStatus();   //改變按鈕狀態
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 接收是否連接到中央Server
        /// </summary>
        /// <param name="isConnect"></param>
        private void IsConnectServer(bool isConnect)
        {
            if (isConnect)
            {
                string loginStr = this.txtUserID.Text;
                client.SendToServer(loginStr);   //傳送登錄命令
            }
        }

        /// <summary>
        /// 處理中央Server傳過來的訊息
        /// </summary>
        /// <param name="msg"></param>
        private void ServerSendMsg(string msg)
        {
            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client = new SocketClient(this.textBoxIP.Text, (int)this.numericPort.Value);   //初始化與中央Server連接物件
            client.ReceiveServerMsgDelegate += this.ServerMsg;                             //綁定事件，所有關於與中央Server的訊息
            client.IsConnectServerDelegate += this.IsConnectServer;                        //綁定事件，是否與中央Server連接
            client.ServerSendMsgDelegate += this.ServerSendMsg;
            client.StartConnect();                                                         //開始連接中央Server
            this.btnSendToServer.Enabled = true;
            this.cmbUserList.SelectedIndex = 0;
        }


        private void ChangeBtnStatus()
        {
            if (client.isConnect)
            {
                this.btnConnect.Enabled = false;
                this.btnSendToServer.Enabled = true;
            }
            else
            {
                this.btnConnect.Enabled = true;
                this.btnSendToServer.Enabled = false;
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.Disconnect();
                string msgSelf = string.Empty;
                msgSelf = DateTime.Now.ToString("HH:mm:ss") + " 您已離開聊天\r\n";
                this.txtServerLog.AppendText(msgSelf);
                this.cmbUserList.Items.Clear();
                this.cmbUserList.Items.Add("ALL");
                this.btnSendToServer.Enabled = false;
                //this.btnConnect.Enabled = true;
            }
        }

        private void btnSendToServer_Click(object sender, EventArgs e)
        {
            string combineMSG = msgTrans.MessageCombine("chat", this.txtUserID.Text.ToString(), this.cmbUserList.SelectedItem.ToString(), this.txtSendToClient.Text.ToString());
            string[] catchMsg = msgTrans.MessageReceive(combineMSG, this.txtUserID.Text.ToString());
            client.SendToServer(combineMSG);
            this.txtServerLog.AppendText(catchMsg[1]);
            this.txtSendToClient.Text = "";
        }

        private void frmChatClient_Load(object sender, EventArgs e)
        {
            this.btnSendToServer.Enabled = false;
        }
    }
}
