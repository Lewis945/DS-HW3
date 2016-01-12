namespace CatchMeUp.WinForms
{
    partial class Home
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGame = new System.Windows.Forms.TabPage();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.radioButtonHunted = new System.Windows.Forms.RadioButton();
            this.radioButtonHunter = new System.Windows.Forms.RadioButton();
            this.textBoxPlayerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBoxLobby = new System.Windows.Forms.GroupBox();
            this.listBoxLobby = new System.Windows.Forms.ListBox();
            this.groupBoxJoinGame = new System.Windows.Forms.GroupBox();
            this.buttonListenBroadcast = new System.Windows.Forms.Button();
            this.buttonJoinGame = new System.Windows.Forms.Button();
            this.groupBoxCreateGame = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFieldHeight = new System.Windows.Forms.TextBox();
            this.textBoxFieldWIdth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSessionName = new System.Windows.Forms.TextBox();
            this.buttonCreateGame = new System.Windows.Forms.Button();
            this.buttonStopBroadcast = new System.Windows.Forms.Button();
            this.buttonStopMulticast = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageGame.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.groupBoxLobby.SuspendLayout();
            this.groupBoxJoinGame.SuspendLayout();
            this.groupBoxCreateGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGame);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(895, 176);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPageGame
            // 
            this.tabPageGame.Controls.Add(this.groupBoxSettings);
            this.tabPageGame.Controls.Add(this.groupBoxLobby);
            this.tabPageGame.Controls.Add(this.groupBoxJoinGame);
            this.tabPageGame.Controls.Add(this.groupBoxCreateGame);
            this.tabPageGame.Location = new System.Drawing.Point(4, 22);
            this.tabPageGame.Name = "tabPageGame";
            this.tabPageGame.Size = new System.Drawing.Size(887, 150);
            this.tabPageGame.TabIndex = 2;
            this.tabPageGame.Text = "Game";
            this.tabPageGame.UseVisualStyleBackColor = true;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.buttonStopMulticast);
            this.groupBoxSettings.Controls.Add(this.buttonStopBroadcast);
            this.groupBoxSettings.Controls.Add(this.radioButtonHunted);
            this.groupBoxSettings.Controls.Add(this.radioButtonHunter);
            this.groupBoxSettings.Controls.Add(this.textBoxPlayerName);
            this.groupBoxSettings.Controls.Add(this.label5);
            this.groupBoxSettings.Location = new System.Drawing.Point(4, 4);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(190, 140);
            this.groupBoxSettings.TabIndex = 3;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // radioButtonHunted
            // 
            this.radioButtonHunted.AutoSize = true;
            this.radioButtonHunted.Location = new System.Drawing.Point(84, 70);
            this.radioButtonHunted.Name = "radioButtonHunted";
            this.radioButtonHunted.Size = new System.Drawing.Size(60, 17);
            this.radioButtonHunted.TabIndex = 3;
            this.radioButtonHunted.TabStop = true;
            this.radioButtonHunted.Text = "Hunted";
            this.radioButtonHunted.UseVisualStyleBackColor = true;
            // 
            // radioButtonHunter
            // 
            this.radioButtonHunter.AutoSize = true;
            this.radioButtonHunter.Checked = true;
            this.radioButtonHunter.Location = new System.Drawing.Point(84, 47);
            this.radioButtonHunter.Name = "radioButtonHunter";
            this.radioButtonHunter.Size = new System.Drawing.Size(57, 17);
            this.radioButtonHunter.TabIndex = 2;
            this.radioButtonHunter.TabStop = true;
            this.radioButtonHunter.Text = "Hunter";
            this.radioButtonHunter.UseVisualStyleBackColor = true;
            // 
            // textBoxPlayerName
            // 
            this.textBoxPlayerName.Location = new System.Drawing.Point(84, 19);
            this.textBoxPlayerName.Name = "textBoxPlayerName";
            this.textBoxPlayerName.Size = new System.Drawing.Size(100, 20);
            this.textBoxPlayerName.TabIndex = 1;
            this.textBoxPlayerName.Text = "Player";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Player name:";
            // 
            // groupBoxLobby
            // 
            this.groupBoxLobby.Controls.Add(this.listBoxLobby);
            this.groupBoxLobby.Location = new System.Drawing.Point(474, 4);
            this.groupBoxLobby.Name = "groupBoxLobby";
            this.groupBoxLobby.Size = new System.Drawing.Size(405, 140);
            this.groupBoxLobby.TabIndex = 2;
            this.groupBoxLobby.TabStop = false;
            this.groupBoxLobby.Text = "Lobby";
            // 
            // listBoxLobby
            // 
            this.listBoxLobby.FormattingEnabled = true;
            this.listBoxLobby.Location = new System.Drawing.Point(7, 20);
            this.listBoxLobby.Name = "listBoxLobby";
            this.listBoxLobby.Size = new System.Drawing.Size(392, 108);
            this.listBoxLobby.TabIndex = 0;
            // 
            // groupBoxJoinGame
            // 
            this.groupBoxJoinGame.Controls.Add(this.buttonListenBroadcast);
            this.groupBoxJoinGame.Controls.Add(this.buttonJoinGame);
            this.groupBoxJoinGame.Location = new System.Drawing.Point(379, 4);
            this.groupBoxJoinGame.Name = "groupBoxJoinGame";
            this.groupBoxJoinGame.Size = new System.Drawing.Size(89, 140);
            this.groupBoxJoinGame.TabIndex = 1;
            this.groupBoxJoinGame.TabStop = false;
            this.groupBoxJoinGame.Text = "Join game";
            // 
            // buttonListenBroadcast
            // 
            this.buttonListenBroadcast.Location = new System.Drawing.Point(6, 23);
            this.buttonListenBroadcast.Name = "buttonListenBroadcast";
            this.buttonListenBroadcast.Size = new System.Drawing.Size(77, 43);
            this.buttonListenBroadcast.TabIndex = 1;
            this.buttonListenBroadcast.Text = "Connect";
            this.buttonListenBroadcast.UseVisualStyleBackColor = true;
            this.buttonListenBroadcast.Click += new System.EventHandler(this.buttonListenBroadcast_Click);
            // 
            // buttonJoinGame
            // 
            this.buttonJoinGame.Location = new System.Drawing.Point(6, 85);
            this.buttonJoinGame.Name = "buttonJoinGame";
            this.buttonJoinGame.Size = new System.Drawing.Size(77, 43);
            this.buttonJoinGame.TabIndex = 0;
            this.buttonJoinGame.Text = "Join";
            this.buttonJoinGame.UseVisualStyleBackColor = true;
            this.buttonJoinGame.Click += new System.EventHandler(this.buttonJoinGame_Click);
            // 
            // groupBoxCreateGame
            // 
            this.groupBoxCreateGame.Controls.Add(this.label4);
            this.groupBoxCreateGame.Controls.Add(this.label3);
            this.groupBoxCreateGame.Controls.Add(this.textBoxFieldHeight);
            this.groupBoxCreateGame.Controls.Add(this.textBoxFieldWIdth);
            this.groupBoxCreateGame.Controls.Add(this.label1);
            this.groupBoxCreateGame.Controls.Add(this.textBoxSessionName);
            this.groupBoxCreateGame.Controls.Add(this.buttonCreateGame);
            this.groupBoxCreateGame.Location = new System.Drawing.Point(200, 4);
            this.groupBoxCreateGame.Name = "groupBoxCreateGame";
            this.groupBoxCreateGame.Size = new System.Drawing.Size(173, 140);
            this.groupBoxCreateGame.TabIndex = 0;
            this.groupBoxCreateGame.TabStop = false;
            this.groupBoxCreateGame.Text = "Create game";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Field height:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Field width:";
            // 
            // textBoxFieldHeight
            // 
            this.textBoxFieldHeight.Location = new System.Drawing.Point(82, 73);
            this.textBoxFieldHeight.Name = "textBoxFieldHeight";
            this.textBoxFieldHeight.Size = new System.Drawing.Size(82, 20);
            this.textBoxFieldHeight.TabIndex = 6;
            this.textBoxFieldHeight.Text = "10";
            // 
            // textBoxFieldWIdth
            // 
            this.textBoxFieldWIdth.Location = new System.Drawing.Point(82, 46);
            this.textBoxFieldWIdth.Name = "textBoxFieldWIdth";
            this.textBoxFieldWIdth.Size = new System.Drawing.Size(83, 20);
            this.textBoxFieldWIdth.TabIndex = 5;
            this.textBoxFieldWIdth.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Session name:";
            // 
            // textBoxSessionName
            // 
            this.textBoxSessionName.Location = new System.Drawing.Point(82, 20);
            this.textBoxSessionName.Name = "textBoxSessionName";
            this.textBoxSessionName.Size = new System.Drawing.Size(85, 20);
            this.textBoxSessionName.TabIndex = 1;
            this.textBoxSessionName.Text = "Player session";
            // 
            // buttonCreateGame
            // 
            this.buttonCreateGame.Location = new System.Drawing.Point(92, 105);
            this.buttonCreateGame.Name = "buttonCreateGame";
            this.buttonCreateGame.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateGame.TabIndex = 0;
            this.buttonCreateGame.Text = "Create";
            this.buttonCreateGame.UseVisualStyleBackColor = true;
            this.buttonCreateGame.Click += new System.EventHandler(this.buttonCreateGame_Click);
            // 
            // buttonStopBroadcast
            // 
            this.buttonStopBroadcast.Location = new System.Drawing.Point(7, 111);
            this.buttonStopBroadcast.Name = "buttonStopBroadcast";
            this.buttonStopBroadcast.Size = new System.Drawing.Size(75, 23);
            this.buttonStopBroadcast.TabIndex = 4;
            this.buttonStopBroadcast.Text = "Stop b";
            this.buttonStopBroadcast.UseVisualStyleBackColor = true;
            this.buttonStopBroadcast.Click += new System.EventHandler(this.buttonStopBroadcast_Click);
            // 
            // buttonStopMulticast
            // 
            this.buttonStopMulticast.Location = new System.Drawing.Point(88, 111);
            this.buttonStopMulticast.Name = "buttonStopMulticast";
            this.buttonStopMulticast.Size = new System.Drawing.Size(75, 23);
            this.buttonStopMulticast.TabIndex = 5;
            this.buttonStopMulticast.Text = "Stop m";
            this.buttonStopMulticast.UseVisualStyleBackColor = true;
            this.buttonStopMulticast.Click += new System.EventHandler(this.buttonStopMulticast_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 197);
            this.Controls.Add(this.tabControl1);
            this.Name = "Home";
            this.Text = "Home";
            this.tabControl1.ResumeLayout(false);
            this.tabPageGame.ResumeLayout(false);
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.groupBoxLobby.ResumeLayout(false);
            this.groupBoxJoinGame.ResumeLayout(false);
            this.groupBoxCreateGame.ResumeLayout(false);
            this.groupBoxCreateGame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGame;
        private System.Windows.Forms.GroupBox groupBoxCreateGame;
        private System.Windows.Forms.GroupBox groupBoxLobby;
        private System.Windows.Forms.GroupBox groupBoxJoinGame;
        private System.Windows.Forms.Button buttonJoinGame;
        private System.Windows.Forms.Button buttonCreateGame;
        private System.Windows.Forms.Button buttonListenBroadcast;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.ListBox listBoxLobby;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSessionName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFieldHeight;
        private System.Windows.Forms.TextBox textBoxFieldWIdth;
        private System.Windows.Forms.TextBox textBoxPlayerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButtonHunted;
        private System.Windows.Forms.RadioButton radioButtonHunter;
        private System.Windows.Forms.Button buttonStopMulticast;
        private System.Windows.Forms.Button buttonStopBroadcast;
    }
}