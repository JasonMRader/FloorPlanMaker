namespace FloorplanUserControlLibrary
{
    partial class DiningAreaInfoControl
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
            if (disposing && (components != null)) {
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
            lblDiningAreaName = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            listBox1 = new ListBox();
            textBox1 = new TextBox();
            btnAddNew = new Button();
            btnRemoveSelected = new Button();
            label7 = new Label();
            listBox2 = new ListBox();
            btnOpenManageTablesForm = new Button();
            SuspendLayout();
            // 
            // lblDiningAreaName
            // 
            lblDiningAreaName.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiningAreaName.Location = new Point(3, 10);
            lblDiningAreaName.Name = "lblDiningAreaName";
            lblDiningAreaName.Size = new Size(202, 47);
            lblDiningAreaName.TabIndex = 0;
            lblDiningAreaName.Text = "Outside Cocktail";
            lblDiningAreaName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 87);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 1;
            label1.Text = "Outside Section:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 102);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 1;
            label2.Text = "Cocktail Area:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 117);
            label3.Name = "label3";
            label3.Size = new Size(141, 15);
            label3.TabIndex = 1;
            label3.Text = "Exclude From Total Sales?";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 57);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 1;
            label4.Text = "Total Tables:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 72);
            label5.Name = "label5";
            label5.Size = new Size(100, 15);
            label5.TabIndex = 1;
            label5.Text = "Max Cover Count";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 191);
            label6.Name = "label6";
            label6.Size = new Size(150, 15);
            label6.TabIndex = 1;
            label6.Text = "Excluded Tables From Sales";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(13, 219);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(150, 94);
            listBox1.TabIndex = 2;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(13, 319);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 23);
            textBox1.TabIndex = 3;
            // 
            // btnAddNew
            // 
            btnAddNew.Location = new Point(13, 348);
            btnAddNew.Name = "btnAddNew";
            btnAddNew.Size = new Size(150, 23);
            btnAddNew.TabIndex = 4;
            btnAddNew.Text = "Add New";
            btnAddNew.UseVisualStyleBackColor = true;
            // 
            // btnRemoveSelected
            // 
            btnRemoveSelected.Location = new Point(13, 377);
            btnRemoveSelected.Name = "btnRemoveSelected";
            btnRemoveSelected.Size = new Size(150, 23);
            btnRemoveSelected.TabIndex = 4;
            btnRemoveSelected.Text = "Remove Selected";
            btnRemoveSelected.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(13, 449);
            label7.Name = "label7";
            label7.Size = new Size(183, 15);
            label7.TabIndex = 1;
            label7.Text = "Included Tables No Longer Active";
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(13, 480);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(150, 94);
            listBox2.TabIndex = 2;
            // 
            // btnOpenManageTablesForm
            // 
            btnOpenManageTablesForm.Location = new Point(13, 152);
            btnOpenManageTablesForm.Name = "btnOpenManageTablesForm";
            btnOpenManageTablesForm.Size = new Size(150, 23);
            btnOpenManageTablesForm.TabIndex = 5;
            btnOpenManageTablesForm.Text = "Manage Tables";
            btnOpenManageTablesForm.UseVisualStyleBackColor = true;
            btnOpenManageTablesForm.Click += btnOpenManageTablesForm_Click;
            // 
            // DiningAreaInfoControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnOpenManageTablesForm);
            Controls.Add(btnRemoveSelected);
            Controls.Add(btnAddNew);
            Controls.Add(textBox1);
            Controls.Add(listBox2);
            Controls.Add(listBox1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(lblDiningAreaName);
            Name = "DiningAreaInfoControl";
            Size = new Size(208, 950);
            Load += DiningAreaInfoControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDiningAreaName;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ListBox listBox1;
        private TextBox textBox1;
        private Button btnAddNew;
        private Button btnRemoveSelected;
        private Label label7;
        private ListBox listBox2;
        private Button btnOpenManageTablesForm;
    }
}
