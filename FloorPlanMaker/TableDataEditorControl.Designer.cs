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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            txtNumber = new TextBox();
            txtCovers = new TextBox();
            txtSales = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Number;
            pictureBox1.Location = new Point(3, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(25, 20);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.sales;
            pictureBox2.Location = new Point(3, 57);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(25, 20);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Chair;
            pictureBox3.Location = new Point(3, 31);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(25, 20);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // txtNumber
            // 
            txtNumber.BackColor = Color.Gainsboro;
            txtNumber.BorderStyle = BorderStyle.None;
            txtNumber.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtNumber.Location = new Point(34, 5);
            txtNumber.MaximumSize = new Size(50, 20);
            txtNumber.Name = "txtNumber";
            txtNumber.Size = new Size(40, 20);
            txtNumber.TabIndex = 1;
            // 
            // txtCovers
            // 
            txtCovers.BackColor = Color.Gainsboro;
            txtCovers.BorderStyle = BorderStyle.None;
            txtCovers.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtCovers.Location = new Point(34, 31);
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
            txtSales.Location = new Point(34, 57);
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
            Controls.Add(txtNumber);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "TableDataEditorControl";
            Size = new Size(86, 85);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private TextBox txtNumber;
        private TextBox txtCovers;
        private TextBox txtSales;
    }
}
