using Test.Properties;

namespace Test
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
            SimulatorData = new SimListView.SimListView();
            server = new TextBox();
            port = new TextBox();
            PeriodicEvents = new System.Windows.Forms.Timer(components);
            EnableRedis = new CheckBox();
            loadData = new Button();
            openAircraftData = new OpenFileDialog();
            simulatorGroup = new GroupBox();
            redisGroup = new GroupBox();
            simManual = new RadioButton();
            simTest = new RadioButton();
            simData = new RadioButton();
            simulatorGroup.SuspendLayout();
            redisGroup.SuspendLayout();
            SuspendLayout();
            // 
            // SimulatorData
            // 
            SimulatorData.Alignment = ListViewAlignment.Default;
            SimulatorData.FullRowSelect = true;
            SimulatorData.GridLines = true;
            SimulatorData.Location = new Point(11, 12);
            SimulatorData.Margin = new Padding(3, 2, 3, 2);
            SimulatorData.Name = "SimulatorData";
            SimulatorData.Size = new Size(950, 500);
            SimulatorData.TabIndex = 0;
            SimulatorData.UseCompatibleStateImageBehavior = false;
            SimulatorData.View = View.Details;
            SimulatorData.SelectedIndexChanged += SimulatorData_SelectedIndexChanged;
            // 
            // server
            // 
            server.BorderStyle = BorderStyle.FixedSingle;
            server.Location = new Point(6, 26);
            server.Name = "server";
            server.Size = new Size(125, 27);
            server.TabIndex = 2;
            // 
            // port
            // 
            port.BorderStyle = BorderStyle.FixedSingle;
            port.Location = new Point(137, 26);
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
            EnableRedis.Enabled = false;
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
            simulatorGroup.Location = new Point(131, 524);
            simulatorGroup.Name = "simulatorGroup";
            simulatorGroup.Size = new Size(298, 119);
            simulatorGroup.TabIndex = 7;
            simulatorGroup.TabStop = false;
            simulatorGroup.Text = "Simulator";
            // 
            // redisGroup
            // 
            redisGroup.Controls.Add(server);
            redisGroup.Controls.Add(port);
            redisGroup.Controls.Add(EnableRedis);
            redisGroup.Location = new Point(458, 524);
            redisGroup.Name = "redisGroup";
            redisGroup.Size = new Size(341, 119);
            redisGroup.TabIndex = 8;
            redisGroup.TabStop = false;
            redisGroup.Text = "Redis";
            // 
            // simManual
            // 
            simManual.AutoSize = true;
            simManual.Location = new Point(6, 22);
            simManual.Name = "simManual";
            simManual.Size = new Size(79, 24);
            simManual.TabIndex = 0;
            simManual.TabStop = true;
            simManual.Text = "Manual";
            simManual.UseVisualStyleBackColor = true;
            simManual.Enabled = false;
            simManual.Checked = true;
            // 
            // simTest
            // 
            simTest.AutoSize = true;
            simTest.Location = new Point(6, 49);
            simTest.Name = "simTest";
            simTest.Size = new Size(92, 24);
            simTest.TabIndex = 1;
            simTest.TabStop = true;
            simTest.Text = "Test Data";
            simTest.UseVisualStyleBackColor = true;
            simTest.Enabled = false;
            // 
            // simData
            // 
            simData.AutoSize = true;
            simData.Location = new Point(6, 79);
            simData.Name = "simData";
            simData.Size = new Size(94, 24);
            simData.TabIndex = 2;
            simData.TabStop = true;
            simData.Text = "Simulator";
            simData.UseVisualStyleBackColor = true;
            simData.Enabled = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 653);
            Controls.Add(redisGroup);
            Controls.Add(simulatorGroup);
            Controls.Add(loadData);
            Controls.Add(SimulatorData);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Simulator Listener";
            simulatorGroup.ResumeLayout(false);
            simulatorGroup.PerformLayout();
            redisGroup.ResumeLayout(false);
            redisGroup.PerformLayout();
            ResumeLayout(false);
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
    }
}
