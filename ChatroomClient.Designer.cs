namespace ChatroomClient
{
    partial class frmChatClient
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtServerLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSendToClient = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.numericPort = new System.Windows.Forms.NumericUpDown();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.btnSendToServer = new System.Windows.Forms.Button();
            this.cmbUserList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericPort)).BeginInit();
            this.SuspendLayout();
            // 
            // txtServerLog
            // 
            this.txtServerLog.Location = new System.Drawing.Point(340, 26);
            this.txtServerLog.MaxLength = 65536;
            this.txtServerLog.Multiline = true;
            this.txtServerLog.Name = "txtServerLog";
            this.txtServerLog.ReadOnly = true;
            this.txtServerLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtServerLog.Size = new System.Drawing.Size(370, 455);
            this.txtServerLog.TabIndex = 7;
            this.txtServerLog.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(338, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "聊天內容：";
            // 
            // txtSendToClient
            // 
            this.txtSendToClient.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSendToClient.Location = new System.Drawing.Point(12, 339);
            this.txtSendToClient.MaxLength = 65536;
            this.txtSendToClient.Multiline = true;
            this.txtSendToClient.Name = "txtSendToClient";
            this.txtSendToClient.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSendToClient.Size = new System.Drawing.Size(303, 98);
            this.txtSendToClient.TabIndex = 23;
            this.txtSendToClient.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "傳送訊息:";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(208, 75);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 18;
            this.btnStop.Text = "斷開";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(208, 47);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 17;
            this.btnConnect.Text = "連接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // numericPort
            // 
            this.numericPort.Location = new System.Drawing.Point(102, 77);
            this.numericPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericPort.Name = "numericPort";
            this.numericPort.Size = new System.Drawing.Size(100, 21);
            this.numericPort.TabIndex = 16;
            this.numericPort.Value = new decimal(new int[] {
            7101,
            0,
            0,
            0});
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(102, 49);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(100, 21);
            this.textBoxIP.TabIndex = 15;
            this.textBoxIP.Text = "127.0.0.1";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(10, 79);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(83, 12);
            this.lblPort.TabIndex = 14;
            this.lblPort.Text = "Server Port：";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(22, 52);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(71, 12);
            this.lblIP.TabIndex = 13;
            this.lblIP.Text = "Server IP：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "暱稱：";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(102, 23);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(100, 21);
            this.txtUserID.TabIndex = 25;
            this.txtUserID.Text = "testUser1";
            // 
            // btnSendToServer
            // 
            this.btnSendToServer.Location = new System.Drawing.Point(240, 443);
            this.btnSendToServer.Name = "btnSendToServer";
            this.btnSendToServer.Size = new System.Drawing.Size(75, 23);
            this.btnSendToServer.TabIndex = 26;
            this.btnSendToServer.Text = "傳送訊息";
            this.btnSendToServer.UseVisualStyleBackColor = true;
            this.btnSendToServer.Click += new System.EventHandler(this.btnSendToServer_Click);
            // 
            // cmbUserList
            // 
            this.cmbUserList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserList.FormattingEnabled = true;
            this.cmbUserList.Items.AddRange(new object[] {
            "ALL"});
            this.cmbUserList.Location = new System.Drawing.Point(112, 444);
            this.cmbUserList.Name = "cmbUserList";
            this.cmbUserList.Size = new System.Drawing.Size(121, 20);
            this.cmbUserList.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 447);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "線上使用者清單:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 467);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "若要私密聊天，請挑選一位對象";
            // 
            // frmChatClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 487);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbUserList);
            this.Controls.Add(this.btnSendToServer);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSendToClient);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.numericPort);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServerLog);
            this.Name = "frmChatClient";
            this.Text = "ChatroomClient";
            this.Load += new System.EventHandler(this.frmChatClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServerLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSendToClient;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.NumericUpDown numericPort;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Button btnSendToServer;
        private System.Windows.Forms.ComboBox cmbUserList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

