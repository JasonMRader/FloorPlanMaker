namespace FloorplanUserControlLibrary
{
    partial class ServerSelectionPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMain = new Panel();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.AutoSize = true;
            pnlMain.BackColor = Color.WhiteSmoke;
            pnlMain.Dock = DockStyle.Top;
            pnlMain.Location = new Point(3, 3);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(0, 0);
            pnlMain.TabIndex = 2;
            // 
            // ServerSelectionPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.ActiveCaption;
            Controls.Add(pnlMain);
            Name = "ServerSelectionPanel";
            Padding = new Padding(3);
            Size = new Size(6, 6);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlMain;
    }
}
