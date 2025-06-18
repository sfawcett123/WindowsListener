namespace Test
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private SimListener.Connect SimListener = null;

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
            SimulatorData = new ListView();
            SuspendLayout();
            // 
            // SimulatorData
            // 
            SimulatorData.Alignment = ListViewAlignment.Default;
            SimulatorData.View = View.Details;
            SimulatorData.FullRowSelect = true;
            SimulatorData.GridLines = true;
            SimulatorData.Location = new Point(10, 9);
            SimulatorData.Margin = new Padding(3, 2, 3, 2);
            SimulatorData.Name = "SimulatorData";
            SimulatorData.Size = new Size(500, 400);
            SimulatorData.TabIndex = 0;
            SimulatorData.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 450);
            Controls.Add(SimulatorData);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Simulator Listener";
            ResumeLayout(false);
        }

        #endregion

        public ListView SimulatorData;
    }
}
