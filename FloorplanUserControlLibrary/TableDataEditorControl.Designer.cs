using FloorplanUserControlLibrary.Properties;
namespace FloorPlanMakerUI
{
    partial class TableDataEditorControl
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
            picSales = new PictureBox();
            picCovers = new PictureBox();
            txtCovers = new TextBox();
            txtSales = new TextBox();
            ((System.ComponentModel.ISupportInitialize)picSales).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCovers).BeginInit();
            SuspendLayout();
            // 
            // picSales
            // 
            picSales.Image = Resources.sales;
            picSales.Location = new Point(0, 20);
            picSales.Name = "picSales";
            picSales.Size = new Size(25, 20);
            picSales.SizeMode = PictureBoxSizeMode.Zoom;
            picSales.TabIndex = 0;
            picSales.TabStop = false;
            // 
            // picCovers
            // 
            picCovers.Image = Resources.Chair;
            picCovers.Location = new Point(0, 0);
            picCovers.Name = "picCovers";
            picCovers.Size = new Size(25, 20);
            picCovers.SizeMode = PictureBoxSizeMode.Zoom;
            picCovers.TabIndex = 0;
            picCovers.TabStop = false;
            // 
            // txtCovers
            // 
            txtCovers.BackColor = Color.Gainsboro;
            txtCovers.BorderStyle = BorderStyle.None;
            txtCovers.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtCovers.Location = new Point(25, 0);
            txtCovers.MaximumSize = new Size(50, 20);
            txtCovers.Name = "txtCovers";
            txtCovers.Size = new Size(40, 20);
            txtCovers.TabIndex = 1;
            txtCovers.TextAlign = HorizontalAlignment.Center;
            txtCovers.TextChanged += txtCovers_TextChanged;
            // 
            // txtSales
            // 
            txtSales.BackColor = Color.Gainsboro;
            txtSales.BorderStyle = BorderStyle.None;
            txtSales.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtSales.Location = new Point(25, 20);
            txtSales.MaximumSize = new Size(50, 20);
            txtSales.Name = "txtSales";
            txtSales.Size = new Size(40, 17);
            txtSales.TabIndex = 1;
            txtSales.TextChanged += txtSales_TextChanged;
            // 
            // TableDataEditorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonShadow;
            Controls.Add(txtSales);
            Controls.Add(txtCovers);
            Controls.Add(picCovers);
            Controls.Add(picSales);
            Name = "TableDataEditorControl";
            Size = new Size(65, 20);
            Load += TableDataEditorControl_Load;
            Enter += TableDataEditorControl_Enter;
            ((System.ComponentModel.ISupportInitialize)picSales).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCovers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox picSales;
        private PictureBox picCovers;
        private TextBox txtCovers;
        private TextBox txtSales;
    }
}
