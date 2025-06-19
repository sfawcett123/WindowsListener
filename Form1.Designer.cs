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
            SimulatorData.Size = new Size(571, 532);
            SimulatorData.TabIndex = 0;
            SimulatorData.UseCompatibleStateImageBehavior = false;
            SimulatorData.View = View.Details;
            // 
            // server
            // 
            server.Location = new Point(588, 12);
            server.Name = "server";
            server.Size = new Size(125, 27);
            server.TabIndex = 2;
            server.Text = "controller.local";
            // 
            // port
            // 
            port.Location = new Point(719, 12);
            port.Name = "port";
            port.Size = new Size(68, 27);
            port.TabIndex = 3;
            port.Text = "6379";
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
            simulatorOn.Location = new Point(588, 105);
            simulatorOn.Name = "simulatorOn";
            simulatorOn.Size = new Size(95, 24);
            simulatorOn.TabIndex = 4;
            simulatorOn.Text = "Simulator";
            simulatorOn.UseVisualStyleBackColor = true;
            simulatorOn.CheckedChanged += EnableSimData;
            // 
            // EnableRedis
            // 
            EnableRedis.AutoSize = true;
            EnableRedis.Location = new Point(589, 78);
            EnableRedis.Name = "EnableRedis";
            EnableRedis.Size = new Size(67, 24);
            EnableRedis.TabIndex = 5;
            EnableRedis.Text = "Redis";
            EnableRedis.UseVisualStyleBackColor = true;
            EnableRedis.CheckedChanged += OnEnableRedis;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Controls.Add(EnableRedis);
            Controls.Add(simulatorOn);
            Controls.Add(port);
            Controls.Add(server);
            Controls.Add(SimulatorData);
            Margin = new Padding(3, 4, 3, 4);
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
    }
}
