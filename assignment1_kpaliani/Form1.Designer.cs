namespace assignment1_kpaliani
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
            this.label1 = new System.Windows.Forms.Label();
            this.listenOnPortButton = new System.Windows.Forms.Button();
            this.serverIPAddressText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listenOnPortText = new System.Windows.Forms.TextBox();
            this.printRTPHeaderCheckBox = new System.Windows.Forms.CheckBox();
            this.serverStatusText = new System.Windows.Forms.TextBox();
            this.clientRequestsText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Listen on Port";
            // 
            // listenOnPortButton
            // 
            this.listenOnPortButton.Location = new System.Drawing.Point(420, 76);
            this.listenOnPortButton.Name = "listenOnPortButton";
            this.listenOnPortButton.Size = new System.Drawing.Size(138, 69);
            this.listenOnPortButton.TabIndex = 1;
            this.listenOnPortButton.Text = "Listen";
            this.listenOnPortButton.UseVisualStyleBackColor = true;
            this.listenOnPortButton.Click += new System.EventHandler(this.listenOnPortButton_Click);
            // 
            // serverIPAddressText
            // 
            this.serverIPAddressText.Enabled = false;
            this.serverIPAddressText.Location = new System.Drawing.Point(285, 196);
            this.serverIPAddressText.Name = "serverIPAddressText";
            this.serverIPAddressText.Size = new System.Drawing.Size(300, 31);
            this.serverIPAddressText.TabIndex = 2;
            this.serverIPAddressText.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server IP Address";
            // 
            // listenOnPortText
            // 
            this.listenOnPortText.Location = new System.Drawing.Point(257, 97);
            this.listenOnPortText.Name = "listenOnPortText";
            this.listenOnPortText.Size = new System.Drawing.Size(100, 31);
            this.listenOnPortText.TabIndex = 4;
            this.listenOnPortText.Text = "3000";
            // 
            // printRTPHeaderCheckBox
            // 
            this.printRTPHeaderCheckBox.AutoSize = true;
            this.printRTPHeaderCheckBox.Location = new System.Drawing.Point(906, 97);
            this.printRTPHeaderCheckBox.Name = "printRTPHeaderCheckBox";
            this.printRTPHeaderCheckBox.Size = new System.Drawing.Size(212, 29);
            this.printRTPHeaderCheckBox.TabIndex = 6;
            this.printRTPHeaderCheckBox.Text = "Print RTP Header";
            this.printRTPHeaderCheckBox.UseVisualStyleBackColor = true;
            this.printRTPHeaderCheckBox.CheckedChanged += new System.EventHandler(this.printRTPHeaderCheckBox_CheckedChanged);
            // 
            // serverStatusText
            // 
            this.serverStatusText.Location = new System.Drawing.Point(75, 330);
            this.serverStatusText.Multiline = true;
            this.serverStatusText.Name = "serverStatusText";
            this.serverStatusText.Size = new System.Drawing.Size(1059, 350);
            this.serverStatusText.TabIndex = 7;
            // 
            // clientRequestsText
            // 
            this.clientRequestsText.Location = new System.Drawing.Point(75, 764);
            this.clientRequestsText.Multiline = true;
            this.clientRequestsText.Name = "clientRequestsText";
            this.clientRequestsText.Size = new System.Drawing.Size(1059, 350);
            this.clientRequestsText.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Server Status";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 723);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Client Requests";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 1147);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.clientRequestsText);
            this.Controls.Add(this.serverStatusText);
            this.Controls.Add(this.printRTPHeaderCheckBox);
            this.Controls.Add(this.listenOnPortText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverIPAddressText);
            this.Controls.Add(this.listenOnPortButton);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button listenOnPortButton;
        private System.Windows.Forms.TextBox serverIPAddressText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox listenOnPortText;
        private System.Windows.Forms.CheckBox printRTPHeaderCheckBox;
        private System.Windows.Forms.TextBox serverStatusText;
        private System.Windows.Forms.TextBox clientRequestsText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

