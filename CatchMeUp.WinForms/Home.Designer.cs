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
            this.startServerButton = new System.Windows.Forms.Button();
            this.startClientButton = new System.Windows.Forms.Button();
            this.joinNewServerButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGame = new System.Windows.Forms.TabPage();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.groupBoxLobby = new System.Windows.Forms.GroupBox();
            this.listBoxLobby = new System.Windows.Forms.ListBox();
            this.groupBoxJoinGame = new System.Windows.Forms.GroupBox();
            this.buttonListenBroadcast = new System.Windows.Forms.Button();
            this.buttonJoinGame = new System.Windows.Forms.Button();
            this.groupBoxCreateGame = new System.Windows.Forms.GroupBox();
            this.buttonCreateGame = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxSessionName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMaxPlayers = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFieldWIdth = new System.Windows.Forms.TextBox();
            this.textBoxFieldHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageGame.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.groupBoxLobby.SuspendLayout();
            this.groupBoxJoinGame.SuspendLayout();
            this.groupBoxCreateGame.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGame);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(982, 477);
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
            this.tabPageGame.Size = new System.Drawing.Size(974, 451);
            this.tabPageGame.TabIndex = 2;
            this.tabPageGame.Text = "Game";
            this.tabPageGame.UseVisualStyleBackColor = true;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.textBox1);
            this.groupBoxSettings.Controls.Add(this.label5);
            this.groupBoxSettings.Location = new System.Drawing.Point(4, 4);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(190, 444);
            this.groupBoxSettings.TabIndex = 3;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // groupBoxLobby
            // 
            this.groupBoxLobby.Controls.Add(this.listBoxLobby);
            this.groupBoxLobby.Location = new System.Drawing.Point(566, 4);
            this.groupBoxLobby.Name = "groupBoxLobby";
            this.groupBoxLobby.Size = new System.Drawing.Size(405, 444);
            this.groupBoxLobby.TabIndex = 2;
            this.groupBoxLobby.TabStop = false;
            this.groupBoxLobby.Text = "Lobby";
            // 
            // listBoxLobby
            // 
            this.listBoxLobby.FormattingEnabled = true;
            this.listBoxLobby.Location = new System.Drawing.Point(7, 20);
            this.listBoxLobby.Name = "listBoxLobby";
            this.listBoxLobby.Size = new System.Drawing.Size(392, 420);
            this.listBoxLobby.TabIndex = 0;
            // 
            // groupBoxJoinGame
            // 
            this.groupBoxJoinGame.Controls.Add(this.buttonListenBroadcast);
            this.groupBoxJoinGame.Controls.Add(this.buttonJoinGame);
            this.groupBoxJoinGame.Location = new System.Drawing.Point(379, 4);
            this.groupBoxJoinGame.Name = "groupBoxJoinGame";
            this.groupBoxJoinGame.Size = new System.Drawing.Size(181, 444);
            this.groupBoxJoinGame.TabIndex = 1;
            this.groupBoxJoinGame.TabStop = false;
            this.groupBoxJoinGame.Text = "Join game";
            // 
            // buttonListenBroadcast
            // 
            this.buttonListenBroadcast.Location = new System.Drawing.Point(6, 415);
            this.buttonListenBroadcast.Name = "buttonListenBroadcast";
            this.buttonListenBroadcast.Size = new System.Drawing.Size(75, 23);
            this.buttonListenBroadcast.TabIndex = 1;
            this.buttonListenBroadcast.Text = "Connect";
            this.buttonListenBroadcast.UseVisualStyleBackColor = true;
            this.buttonListenBroadcast.Click += new System.EventHandler(this.buttonListenBroadcast_Click);
            // 
            // buttonJoinGame
            // 
            this.buttonJoinGame.Location = new System.Drawing.Point(100, 415);
            this.buttonJoinGame.Name = "buttonJoinGame";
            this.buttonJoinGame.Size = new System.Drawing.Size(75, 23);
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
            this.groupBoxCreateGame.Controls.Add(this.label2);
            this.groupBoxCreateGame.Controls.Add(this.textBoxMaxPlayers);
            this.groupBoxCreateGame.Controls.Add(this.label1);
            this.groupBoxCreateGame.Controls.Add(this.textBoxSessionName);
            this.groupBoxCreateGame.Controls.Add(this.buttonCreateGame);
            this.groupBoxCreateGame.Location = new System.Drawing.Point(200, 4);
            this.groupBoxCreateGame.Name = "groupBoxCreateGame";
            this.groupBoxCreateGame.Size = new System.Drawing.Size(173, 444);
            this.groupBoxCreateGame.TabIndex = 0;
            this.groupBoxCreateGame.TabStop = false;
            this.groupBoxCreateGame.Text = "Create game";
            // 
            // buttonCreateGame
            // 
            this.buttonCreateGame.Location = new System.Drawing.Point(92, 415);
            this.buttonCreateGame.Name = "buttonCreateGame";
            this.buttonCreateGame.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateGame.TabIndex = 0;
            this.buttonCreateGame.Text = "Create";
            this.buttonCreateGame.UseVisualStyleBackColor = true;
            this.buttonCreateGame.Click += new System.EventHandler(this.buttonCreateGame_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(974, 451);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(579, 285);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.startServerButton);
            this.tabPage2.Controls.Add(this.startClientButton);
            this.tabPage2.Controls.Add(this.joinNewServerButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(974, 451);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxSessionName
            // 
            this.textBoxSessionName.Location = new System.Drawing.Point(82, 20);
            this.textBoxSessionName.Name = "textBoxSessionName";
            this.textBoxSessionName.Size = new System.Drawing.Size(85, 20);
            this.textBoxSessionName.TabIndex = 1;
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
            // textBoxMaxPlayers
            // 
            this.textBoxMaxPlayers.Location = new System.Drawing.Point(82, 47);
            this.textBoxMaxPlayers.Name = "textBoxMaxPlayers";
            this.textBoxMaxPlayers.Size = new System.Drawing.Size(84, 20);
            this.textBoxMaxPlayers.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Max players:";
            // 
            // textBoxFieldWIdth
            // 
            this.textBoxFieldWIdth.Location = new System.Drawing.Point(82, 74);
            this.textBoxFieldWIdth.Name = "textBoxFieldWIdth";
            this.textBoxFieldWIdth.Size = new System.Drawing.Size(83, 20);
            this.textBoxFieldWIdth.TabIndex = 5;
            // 
            // textBoxFieldHeight
            // 
            this.textBoxFieldHeight.Location = new System.Drawing.Point(82, 101);
            this.textBoxFieldHeight.Name = "textBoxFieldHeight";
            this.textBoxFieldHeight.Size = new System.Drawing.Size(82, 20);
            this.textBoxFieldHeight.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Field width:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Field height:";
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(84, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 501);
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
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startServerButton;
        private System.Windows.Forms.Button startClientButton;
        private System.Windows.Forms.Button joinNewServerButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPageGame;
        private System.Windows.Forms.GroupBox groupBoxCreateGame;
        private System.Windows.Forms.GroupBox groupBoxLobby;
        private System.Windows.Forms.GroupBox groupBoxJoinGame;
        private System.Windows.Forms.Button buttonJoinGame;
        private System.Windows.Forms.Button buttonCreateGame;
        private System.Windows.Forms.Button buttonListenBroadcast;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.ListBox listBoxLobby;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMaxPlayers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSessionName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFieldHeight;
        private System.Windows.Forms.TextBox textBoxFieldWIdth;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
    }
}