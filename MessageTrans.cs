using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChatroomClient
{
    /// <summary>
    /// 使用 JSON  來實現 server 與 client、client 與 client 間的訊息接收與傳送
    /// 將傳送與接收的訊息類型、時間、來自誰傳的、傳給誰、訊息內容，組合成 JSON，並回傳 JSON 格式的字串
    /// </summary>
    public class MessageTrans
    {
        private JObject jMessage;                          //負責訊息組合的 JObject
        private string OnlineUsers = "";                   //記錄client線上使用者名單
        private string returnMsg = string.Empty;           //回傳的訊息

        public MessageTrans() { }

        /// <summary>
        /// 將訊息組合成json格式並回傳
        /// </summary>
        /// <param name="type">訊息類型</param>
        /// <param name="from">來自誰傳的</param>
        /// <param name="to">傳給誰</param>
        /// <param name="msg">訊息內容</param>
        /// <returns></returns>
        public string MessageCombine(string type, string from, string to, string msg)
        {
            jMessage = new JObject();
            jMessage.Add(new JProperty("type", type));
            jMessage.Add(new JProperty("time", DateTime.Now.ToString("HH:mm:ss")));
            jMessage.Add(new JProperty("from", from));
            jMessage.Add(new JProperty("to", to));
            jMessage.Add(new JProperty("message", msg));

            returnMsg = JsonConvert.SerializeObject(jMessage, Formatting.None);
            return returnMsg;
        }

        /// <summary>
        /// 接收 json格式字串，拆解後回傳訊息
        /// </summary>
        /// <param name="msg">json訊息</param>
        /// <param name="userID">目前登入的使用者</param>
        public string[] MessageReceive(string msg, string userID)
        {
            jMessage = JObject.Parse(msg);                       //拆解傳過來的 json 訊息
            string type = jMessage["type"].ToString();           //訊息類型
            string time = jMessage["time"].ToString();           //時間戳記
            string from = jMessage["from"].ToString();           //來自誰傳的
            string user = jMessage["to"].ToString();             //傳給誰
            string message = jMessage["message"].ToString();     //傳遞的訊息
            string[] returnMsgAry = new string[3] {"", "", ""} ; //[0]:傳給server的訊息   [1]:顯示在client端畫面的訊息   [2]:回傳的是訊息，分類用
            string[] returnUserList;                             //回傳給client增加線上使用者的清單

            if (type == "list")      //這次傳來的是使用者清單
            {
                returnUserList = OnlineUserList(user, userID);
                return returnUserList;
            }
            else
            {
                switch (type)
                {
                    case "connect":  //連線時的訊息
                        returnMsgAry[0] = time + " " + from + message + "\r\n";
                        break;
                    case "login":    //登入時的訊息
                        returnMsgAry[0] = time + " " + user + message + "\r\n";
                        break;
                    case "logout":   //登出時的訊息
                        returnMsgAry[0] = time + " " + user + message + "\r\n";
                        returnMsgAry[1] = time + " 您已離開聊天\r\n";
                        break;
                    case "chat":     //聊天時的訊息
                        if (user == "ALL")
                        {
                            returnMsgAry[0] = time + " " + from + " 對大家說: " + message + "\r\n";
                        }
                        else
                        {
                            returnMsgAry[0] = time + " " + from + " 悄悄對你說: " + message + "\r\n";
                            returnMsgAry[1] = time + " 你對: " + user + " 悄悄地說: " + message + "\r\n";
                        }
                        break;
                    case "repeat":    //重複登入的訊息
                        returnMsgAry[0] = time + " " + message + "\r\n";
                        break;
                    default:
                        break;
                }
                returnMsgAry[2] = "#msg#";   //代表回傳的是訊息，而不是清單
                return returnMsgAry;
            }
        }


        public string[] OnlineUserList(string user, string userID)
        {
            string[] allUser;                                    //將使用者清單轉存在陣列，比對用
            int userLength = 0;                                  //使用者清單陣列陣列長度
            int k = 0;                                           //index

            allUser = user.Split('#');                           //將清單以#分割，分別存在陣列中
            userLength = allUser.Length - 1;                     //陣列長度最後一個是空白，-1 是把空白拿掉
            string[] toCombo = new string[userLength - 1];       //回傳使用者清單給combobox，-1是扣除自己

            for (int i = 0; i < userLength; i++)
            {
                int idx = OnlineUsers.IndexOf(allUser[i]);
                if (allUser[i] != userID && OnlineUsers.IndexOf(allUser[i]) == -1)    //線上使用者清單中不與目前client端登入的使用者(自己)相同時，才增加線上使用者清單
                {
                    toCombo[k] = allUser[i];
                    OnlineUsers += allUser[i] + "#";   //記錄client自己的清單
                    k++;
                }
            }
            return toCombo;
        }
    }
}
