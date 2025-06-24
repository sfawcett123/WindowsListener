using Broadcast.Properties;

namespace Broadcast
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private SimListener.Connect SimListener = null;
        public SimListView.SimListView SimulatorData;
        public SimRedis.SimRedis SimRedis = null;
        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            SimulatorData = new SimListView.SimListView();
            server = new TextBox();
            port = new TextBox();
            PeriodicEvents = new System.Windows.Forms.Timer(components);
            EnableRedis = new CheckBox();
            loadData = new Button();
            openAircraftData = new OpenFileDialog();
            simulatorGroup = new GroupBox();
            simData = new RadioButton();
            simTest = new RadioButton();
            simManual = new RadioButton();
            redisGroup = new GroupBox();
            statusStrip1 = new StatusStrip();
            lastMessage = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            btnPurge = new Button();
            simulatorGroup.SuspendLayout();
            redisGroup.SuspendLayout();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // SimulatorData
            // 
            SimulatorData.Alignment = ListViewAlignment.Default;
            SimulatorData.FullRowSelect = true;
            SimulatorData.GridLines = true;
            SimulatorData.Location = new Point(11, 33);
            SimulatorData.Name = "SimulatorData";
            SimulatorData.Size = new Size(950, 472);
            SimulatorData.TabIndex = 0;
            SimulatorData.UseCompatibleStateImageBehavior = false;
            SimulatorData.View = View.Details;
            // 
            // server
            // 
            server.BorderStyle = BorderStyle.FixedSingle;
            server.Location = new Point(6, 27);
            server.Name = "server";
            server.Size = new Size(125, 27);
            server.TabIndex = 2;
            // 
            // port
            // 
            port.BorderStyle = BorderStyle.FixedSingle;
            port.Location = new Point(137, 27);
            port.Name = "port";
            port.Size = new Size(68, 27);
            port.TabIndex = 3;
            // 
            // PeriodicEvents
            // 
            PeriodicEvents.Enabled = true;
            PeriodicEvents.Interval = 5000;
            PeriodicEvents.Tick += PeriodicEvents_Tick;
            // 
            // EnableRedis
            // 
            EnableRedis.AutoSize = true;
            EnableRedis.Location = new Point(6, 57);
            EnableRedis.Name = "EnableRedis";
            EnableRedis.Size = new Size(67, 24);
            EnableRedis.TabIndex = 5;
            EnableRedis.Text = "Redis";
            EnableRedis.UseVisualStyleBackColor = true;
            EnableRedis.CheckedChanged += OnEnableRedis;
            // 
            // loadData
            // 
            loadData.Location = new Point(13, 524);
            loadData.Margin = new Padding(3, 4, 3, 4);
            loadData.Name = "loadData";
            loadData.Size = new Size(86, 31);
            loadData.TabIndex = 6;
            loadData.Text = "Load";
            loadData.UseVisualStyleBackColor = true;
            loadData.Click += OnLoadDataButton;
            // 
            // openAircraftData
            // 
            openAircraftData.DefaultExt = "yaml";
            openAircraftData.FileName = "aircraftdata";
            openAircraftData.Filter = "yaml (*.yaml, *.yml)|*.yaml;*.yml";
            openAircraftData.Title = "Open Aircarft Data YAML";
            // 
            // simulatorGroup
            // 
            simulatorGroup.Controls.Add(simData);
            simulatorGroup.Controls.Add(simTest);
            simulatorGroup.Controls.Add(simManual);
            simulatorGroup.Location = new Point(131, 517);
            simulatorGroup.Name = "simulatorGroup";
            simulatorGroup.Size = new Size(298, 111);
            simulatorGroup.TabIndex = 7;
            simulatorGroup.TabStop = false;
            simulatorGroup.Text = "Simulator";
            // 
            // simData
            // 
            simData.AutoSize = true;
            simData.Enabled = false;
            simData.Location = new Point(6, 79);
            simData.Name = "simData";
            simData.Size = new Size(94, 24);
            simData.TabIndex = 2;
            simData.TabStop = true;
            simData.Text = "Simulator";
            simData.UseVisualStyleBackColor = true;
            // 
            // simTest
            // 
            simTest.AutoSize = true;
            simTest.Enabled = false;
            simTest.Location = new Point(6, 49);
            simTest.Name = "simTest";
            simTest.Size = new Size(92, 24);
            simTest.TabIndex = 1;
            simTest.TabStop = true;
            simTest.Text = "Test Data";
            simTest.UseVisualStyleBackColor = true;
            simTest.CheckedChanged += simTest_CheckedChanged;
            // 
            // simManual
            // 
            simManual.AutoSize = true;
            simManual.Checked = true;
            simManual.Enabled = false;
            simManual.Location = new Point(6, 21);
            simManual.Name = "simManual";
            simManual.Size = new Size(79, 24);
            simManual.TabIndex = 0;
            simManual.TabStop = true;
            simManual.Text = "Manual";
            simManual.UseVisualStyleBackColor = true;
            // 
            // redisGroup
            // 
            redisGroup.Controls.Add(btnPurge);
            redisGroup.Controls.Add(server);
            redisGroup.Controls.Add(port);
            redisGroup.Controls.Add(EnableRedis);
            redisGroup.Location = new Point(458, 519);
            redisGroup.Name = "redisGroup";
            redisGroup.Size = new Size(341, 109);
            redisGroup.TabIndex = 8;
            redisGroup.TabStop = false;
            redisGroup.Text = "Redis";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lastMessage });
            statusStrip1.Location = new Point(0, 651);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(982, 22);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // lastMessage
            // 
            lastMessage.DisplayStyle = ToolStripItemDisplayStyle.Text;
            lastMessage.Name = "lastMessage";
            lastMessage.Size = new Size(0, 16);
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(6, 3, 0, 3);
            menuStrip1.Size = new Size(982, 30);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.ShortcutKeyDisplayString = "H";
            toolStripMenuItem1.Size = new Size(55, 24);
            toolStripMenuItem1.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(133, 26);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // btnPurge
            // 
            btnPurge.Location = new Point(241, 72);
            btnPurge.Name = "btnPurge";
            btnPurge.Size = new Size(94, 29);
            btnPurge.TabIndex = 6;
            btnPurge.Text = "Purge";
            btnPurge.UseVisualStyleBackColor = true;
            btnPurge.MouseClick += onPurgeRedis;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 673);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(redisGroup);
            Controls.Add(simulatorGroup);
            Controls.Add(loadData);
            Controls.Add(SimulatorData);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Simulator Listener";
            Load += loadForm;
            simulatorGroup.ResumeLayout(false);
            simulatorGroup.PerformLayout();
            redisGroup.ResumeLayout(false);
            redisGroup.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox server;
        private TextBox port;
        private System.Windows.Forms.Timer PeriodicEvents;
        private CheckBox EnableRedis;
        private Button loadData;
        private OpenFileDialog openAircraftData;
        private GroupBox simulatorGroup;
        private GroupBox redisGroup;
        private RadioButton simData;
        private RadioButton simTest;
        private RadioButton simManual;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lastMessage;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private Button btnPurge;
    }
}
