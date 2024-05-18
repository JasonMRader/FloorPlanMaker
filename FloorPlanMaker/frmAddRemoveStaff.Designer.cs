namespace FloorPlanMakerUI
{
    partial class frmAddRemoveStaff
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
            lbServers = new ListBox();
            btnAddNewServer = new Button();
            textBox1 = new TextBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // lbServers
            // 
            lbServers.FormattingEnabled = true;
            lbServers.ItemHeight = 15;
            lbServers.Location = new Point(12, 42);
            lbServers.Name = "lbServers";
            lbServers.Size = new Size(247, 349);
            lbServers.TabIndex = 0;
            // 
            // btnAddNewServer
            // 
            btnAddNewServer.Location = new Point(12, 426);
            btnAddNewServer.Name = "btnAddNewServer";
            btnAddNewServer.Size = new Size(247, 23);
            btnAddNewServer.TabIndex = 1;
            btnAddNewServer.Text = "Add New Server";
            btnAddNewServer.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 397);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(247, 23);
            textBox1.TabIndex = 2;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(12, 12);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(90, 19);
            radioButton1.TabIndex = 3;
            radioButton1.TabStop = true;
            radioButton1.Text = "Show Active";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(127, 12);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(104, 19);
            radioButton2.TabIndex = 3;
            radioButton2.Text = "Show Archived";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(358, 42);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(247, 23);
            textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(358, 71);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(247, 23);
            textBox3.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(306, 45);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 4;
            label1.Text = "Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(265, 74);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 4;
            label2.Text = "Display Name:";
            // 
            // button1
            // 
            button1.Location = new Point(358, 100);
            button1.Name = "button1";
            button1.Size = new Size(247, 23);
            button1.TabIndex = 5;
            button1.Text = "Save Changes";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(358, 129);
            button2.Name = "button2";
            button2.Size = new Size(247, 23);
            button2.TabIndex = 5;
            button2.Text = "Archive This Server";
            button2.UseVisualStyleBackColor = true;
            // 
            // frmAddRemoveStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(659, 497);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(btnAddNewServer);
            Controls.Add(lbServers);
            Name = "frmAddRemoveStaff";
            Text = "frmAddRemoveStaff";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lbServers;
        private Button btnAddNewServer;
        private TextBox textBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
    }
}