namespace FloorPlanMakerUI
{
    partial class frmSalesStats
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
            dgvDiningAreas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvDiningAreas).BeginInit();
            SuspendLayout();
            // 
            // dgvDiningAreas
            // 
            dgvDiningAreas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDiningAreas.Location = new Point(12, 138);
            dgvDiningAreas.Name = "dgvDiningAreas";
            dgvDiningAreas.RowTemplate.Height = 25;
            dgvDiningAreas.Size = new Size(892, 352);
            dgvDiningAreas.TabIndex = 0;
            // 
            // frmSalesStats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(916, 566);
            Controls.Add(dgvDiningAreas);
            Name = "frmSalesStats";
            Text = "frmSalesStats";
            Load += frmSalesStats_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDiningAreas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvDiningAreas;
    }
}