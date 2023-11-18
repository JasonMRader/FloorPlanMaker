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
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            txtCovers = new TextBox();
            txtSales = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.sales;
            pictureBox2.Location = new Point(3, 29);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(25, 20);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Chair;
            pictureBox3.Location = new Point(3, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(25, 20);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // txtCovers
            // 
            txtCovers.BackColor = Color.Gainsboro;
            txtCovers.BorderStyle = BorderStyle.None;
            txtCovers.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtCovers.Location = new Point(34, 3);
            txtCovers.MaximumSize = new Size(50, 20);
            txtCovers.Name = "txtCovers";
            txtCovers.Size = new Size(40, 20);
            txtCovers.TabIndex = 1;
            // 
            // txtSales
            // 
            txtSales.BackColor = Color.Gainsboro;
            txtSales.BorderStyle = BorderStyle.None;
            txtSales.Font = new Font("Segoe UI Variable Display", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtSales.Location = new Point(34, 29);
            txtSales.MaximumSize = new Size(50, 20);
            txtSales.Name = "txtSales";
            txtSales.Size = new Size(40, 20);
            txtSales.TabIndex = 1;
            // 
            // TableDataEditorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonShadow;
            Controls.Add(txtSales);
            Controls.Add(txtCovers);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Name = "TableDataEditorControl";
            Size = new Size(80, 56);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private TextBox txtCovers;
        private TextBox txtSales;
    }
}
