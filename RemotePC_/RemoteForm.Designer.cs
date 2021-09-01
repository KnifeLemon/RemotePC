namespace RemotePC_
{
    partial class RemoteForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbGround = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGround)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pbGround);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(842, 506);
            this.panel1.TabIndex = 0;
            // 
            // pbGround
            // 
            this.pbGround.Location = new System.Drawing.Point(0, 0);
            this.pbGround.Name = "pbGround";
            this.pbGround.Size = new System.Drawing.Size(842, 506);
            this.pbGround.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbGround.TabIndex = 1;
            this.pbGround.TabStop = false;
            this.pbGround.MouseEnter += new System.EventHandler(this.pbGround_MouseEnter);
            this.pbGround.MouseLeave += new System.EventHandler(this.pbGround_MouseLeave);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // RemoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 506);
            this.Controls.Add(this.panel1);
            this.Name = "RemoteForm";
            this.Text = "RemoteForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RemoteForm_FormClosed);
            this.Load += new System.EventHandler(this.RemoteForm_Load);
            this.Shown += new System.EventHandler(this.RemoteForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGround)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbGround;
        private System.Windows.Forms.Timer timer1;

    }
}