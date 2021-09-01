namespace RemotePC_
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefreshPassword = new System.Windows.Forms.Button();
            this.txtMyPW = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMyID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbRemotePC = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtRemotePW = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRemoteID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gbRemotePC.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefreshPassword);
            this.groupBox1.Controls.Add(this.txtMyPW);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMyID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.groupBox1.Size = new System.Drawing.Size(242, 139);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "My Computer";
            // 
            // btnRefreshPassword
            // 
            this.btnRefreshPassword.Location = new System.Drawing.Point(164, 101);
            this.btnRefreshPassword.Name = "btnRefreshPassword";
            this.btnRefreshPassword.Size = new System.Drawing.Size(54, 25);
            this.btnRefreshPassword.TabIndex = 4;
            this.btnRefreshPassword.Text = "Renew";
            this.btnRefreshPassword.UseVisualStyleBackColor = true;
            this.btnRefreshPassword.Click += new System.EventHandler(this.btnRefreshPassword_Click);
            // 
            // txtMyPW
            // 
            this.txtMyPW.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMyPW.Font = new System.Drawing.Font("Gulim", 15F);
            this.txtMyPW.Location = new System.Drawing.Point(25, 101);
            this.txtMyPW.Margin = new System.Windows.Forms.Padding(10);
            this.txtMyPW.Name = "txtMyPW";
            this.txtMyPW.ReadOnly = true;
            this.txtMyPW.Size = new System.Drawing.Size(135, 23);
            this.txtMyPW.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Connection PW";
            // 
            // txtMyID
            // 
            this.txtMyID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMyID.Font = new System.Drawing.Font("Gulim", 15F);
            this.txtMyID.Location = new System.Drawing.Point(25, 46);
            this.txtMyID.Margin = new System.Windows.Forms.Padding(10);
            this.txtMyID.Name = "txtMyID";
            this.txtMyID.ReadOnly = true;
            this.txtMyID.Size = new System.Drawing.Size(193, 23);
            this.txtMyID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection ID";
            // 
            // gbRemotePC
            // 
            this.gbRemotePC.Controls.Add(this.txtRemotePW);
            this.gbRemotePC.Controls.Add(this.label3);
            this.gbRemotePC.Controls.Add(this.txtRemoteID);
            this.gbRemotePC.Controls.Add(this.label4);
            this.gbRemotePC.Location = new System.Drawing.Point(12, 157);
            this.gbRemotePC.Name = "gbRemotePC";
            this.gbRemotePC.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.gbRemotePC.Size = new System.Drawing.Size(242, 140);
            this.gbRemotePC.TabIndex = 4;
            this.gbRemotePC.TabStop = false;
            this.gbRemotePC.Text = "Remote PC";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 303);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(242, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtRemotePW
            // 
            this.txtRemotePW.Font = new System.Drawing.Font("Gulim", 15F);
            this.txtRemotePW.Location = new System.Drawing.Point(25, 101);
            this.txtRemotePW.Margin = new System.Windows.Forms.Padding(10);
            this.txtRemotePW.Name = "txtRemotePW";
            this.txtRemotePW.Size = new System.Drawing.Size(192, 30);
            this.txtRemotePW.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Target PW";
            // 
            // txtRemoteID
            // 
            this.txtRemoteID.Font = new System.Drawing.Font("Gulim", 15F);
            this.txtRemoteID.Location = new System.Drawing.Point(25, 46);
            this.txtRemoteID.Margin = new System.Windows.Forms.Padding(10);
            this.txtRemoteID.Name = "txtRemoteID";
            this.txtRemoteID.Size = new System.Drawing.Size(193, 30);
            this.txtRemoteID.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Target ID";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 338);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.gbRemotePC);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RemotePC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbRemotePC.ResumeLayout(false);
            this.gbRemotePC.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMyPW;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMyID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbRemotePC;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtRemotePW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRemoteID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRefreshPassword;
    }
}

