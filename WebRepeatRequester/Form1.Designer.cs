namespace WebRepeatRequester
{
    partial class Form1
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
            this.txtURI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPostData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserAgent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReferrer = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.delayNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.timeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.onceBtn = new System.Windows.Forms.Button();
            this.headersBtn = new System.Windows.Forms.Button();
            this.matchSettingsBtn = new System.Windows.Forms.Button();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.delayNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // txtURI
            // 
            this.txtURI.Location = new System.Drawing.Point(12, 29);
            this.txtURI.Name = "txtURI";
            this.txtURI.Size = new System.Drawing.Size(412, 20);
            this.txtURI.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "URI:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "POST Data (leave blank for GET request):";
            // 
            // txtPostData
            // 
            this.txtPostData.Location = new System.Drawing.Point(12, 68);
            this.txtPostData.Name = "txtPostData";
            this.txtPostData.Size = new System.Drawing.Size(412, 20);
            this.txtPostData.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "User Agent (for alternate, separate with \'#\'):";
            // 
            // txtUserAgent
            // 
            this.txtUserAgent.Location = new System.Drawing.Point(12, 107);
            this.txtUserAgent.Name = "txtUserAgent";
            this.txtUserAgent.Size = new System.Drawing.Size(412, 20);
            this.txtUserAgent.TabIndex = 4;
            this.txtUserAgent.Text = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2" +
    "228.0 Safari/537.36";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Referrer:";
            // 
            // txtReferrer
            // 
            this.txtReferrer.Location = new System.Drawing.Point(12, 146);
            this.txtReferrer.Name = "txtReferrer";
            this.txtReferrer.Size = new System.Drawing.Size(412, 20);
            this.txtReferrer.TabIndex = 6;
            this.txtReferrer.Text = "https://www.google.com/";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(349, 251);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 8;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // delayNumericUpDown
            // 
            this.delayNumericUpDown.Location = new System.Drawing.Point(129, 172);
            this.delayNumericUpDown.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.delayNumericUpDown.Name = "delayNumericUpDown";
            this.delayNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.delayNumericUpDown.TabIndex = 12;
            this.delayNumericUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Delay (ms):";
            // 
            // timeoutNumericUpDown
            // 
            this.timeoutNumericUpDown.Location = new System.Drawing.Point(129, 198);
            this.timeoutNumericUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.timeoutNumericUpDown.Name = "timeoutNumericUpDown";
            this.timeoutNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.timeoutNumericUpDown.TabIndex = 14;
            this.timeoutNumericUpDown.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Timeout (ms):";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(430, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(385, 264);
            this.listBox1.TabIndex = 15;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // onceBtn
            // 
            this.onceBtn.Location = new System.Drawing.Point(349, 222);
            this.onceBtn.Name = "onceBtn";
            this.onceBtn.Size = new System.Drawing.Size(75, 23);
            this.onceBtn.TabIndex = 16;
            this.onceBtn.Text = "Once";
            this.onceBtn.UseVisualStyleBackColor = true;
            this.onceBtn.Click += new System.EventHandler(this.onceBtn_Click);
            // 
            // headersBtn
            // 
            this.headersBtn.Location = new System.Drawing.Point(129, 224);
            this.headersBtn.Name = "headersBtn";
            this.headersBtn.Size = new System.Drawing.Size(133, 23);
            this.headersBtn.TabIndex = 17;
            this.headersBtn.Text = "Edit Request Headers";
            this.headersBtn.UseVisualStyleBackColor = true;
            this.headersBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // matchSettingsBtn
            // 
            this.matchSettingsBtn.Location = new System.Drawing.Point(16, 222);
            this.matchSettingsBtn.Name = "matchSettingsBtn";
            this.matchSettingsBtn.Size = new System.Drawing.Size(107, 23);
            this.matchSettingsBtn.TabIndex = 18;
            this.matchSettingsBtn.Text = "Edit Match Settings";
            this.matchSettingsBtn.UseVisualStyleBackColor = true;
            this.matchSettingsBtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // chkSSL
            // 
            this.chkSSL.AutoSize = true;
            this.chkSSL.Location = new System.Drawing.Point(255, 173);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(116, 17);
            this.chkSSL.TabIndex = 19;
            this.chkSSL.Text = "Accept invalid SSL";
            this.chkSSL.UseVisualStyleBackColor = true;
            this.chkSSL.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "View Matches";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 286);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkSSL);
            this.Controls.Add(this.matchSettingsBtn);
            this.Controls.Add(this.headersBtn);
            this.Controls.Add(this.onceBtn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.timeoutNumericUpDown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.delayNumericUpDown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtReferrer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUserAgent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPostData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtURI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Web Repeat Requester";
            ((System.ComponentModel.ISupportInitialize)(this.delayNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtURI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPostData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserAgent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReferrer;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.NumericUpDown delayNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown timeoutNumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button onceBtn;
        private System.Windows.Forms.Button headersBtn;
        private System.Windows.Forms.Button matchSettingsBtn;
        private System.Windows.Forms.CheckBox chkSSL;
        private System.Windows.Forms.Button button1;
    }
}

