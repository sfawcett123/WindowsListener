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
            simulatorOn = new CheckBox();
            EnableRedis = new CheckBox();
            loadData = new Button();
            openAircraftData = new OpenFileDialog();
            SuspendLayout();
            // 
            // SimulatorData
            // 
            SimulatorData.Alignment = ListViewAlignment.Default;
            SimulatorData.FullRowSelect = true;
            SimulatorData.GridLines = true;
            SimulatorData.Location = new Point(10, 9);
            SimulatorData.Margin = new Padding(3, 2, 3, 2);
            SimulatorData.Name = "SimulatorData";
            SimulatorData.Size = new Size(500, 400);
            SimulatorData.TabIndex = 0;
            SimulatorData.UseCompatibleStateImageBehavior = false;
            SimulatorData.View = View.Details;
            // 
            // server
            // 
            server.Location = new Point(514, 9);
            server.Margin = new Padding(3, 2, 3, 2);
            server.Name = "server";
            server.Size = new Size(110, 23);
            server.TabIndex = 2;
            // 
            // port
            // 
            port.Location = new Point(629, 9);
            port.Margin = new Padding(3, 2, 3, 2);
            port.Name = "port";
            port.Size = new Size(60, 23);
            port.TabIndex = 3;
            // 
            // PeriodicEvents
            // 
            PeriodicEvents.Enabled = true;
            PeriodicEvents.Interval = 5000;
            PeriodicEvents.Tick += PeriodicEvents_Tick;
            // 
            // simulatorOn
            // 
            simulatorOn.AutoSize = true;
            simulatorOn.Location = new Point(514, 79);
            simulatorOn.Margin = new Padding(3, 2, 3, 2);
            simulatorOn.Name = "simulatorOn";
            simulatorOn.Size = new Size(77, 19);
            simulatorOn.TabIndex = 4;
            simulatorOn.Text = "Simulator";
            simulatorOn.UseVisualStyleBackColor = true;
            simulatorOn.CheckedChanged += EnableSimData;
            // 
            // EnableRedis
            // 
            EnableRedis.AutoSize = true;
            EnableRedis.Location = new Point(515, 58);
            EnableRedis.Margin = new Padding(3, 2, 3, 2);
            EnableRedis.Name = "EnableRedis";
            EnableRedis.Size = new Size(54, 19);
            EnableRedis.TabIndex = 5;
            EnableRedis.Text = "Redis";
            EnableRedis.UseVisualStyleBackColor = true;
            EnableRedis.CheckedChanged += OnEnableRedis;
            // 
            // loadData
            // 
            loadData.Location = new Point(11, 417);
            loadData.Name = "loadData";
            loadData.Size = new Size(75, 23);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 450);
            Controls.Add(loadData);
            Controls.Add(EnableRedis);
            Controls.Add(simulatorOn);
            Controls.Add(port);
            Controls.Add(server);
            Controls.Add(SimulatorData);
            Name = "Form1";
            Text = "Simulator Listener";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox server;
        private TextBox port;
        private System.Windows.Forms.Timer PeriodicEvents;
        private CheckBox simulatorOn;
        private CheckBox EnableRedis;
        private Button loadData;
        private OpenFileDialog openAircraftData;
    }
}
