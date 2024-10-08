namespace FloorPlanMakerUI
{
    partial class frmReservationView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dateTimePicker1 = new DateTimePicker();
            rdoAM = new RadioButton();
            rdoPM = new RadioButton();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            lbl4pm = new Label();
            lbl5pm = new Label();
            lbl6pm = new Label();
            lbl7pm = new Label();
            lbl8pm = new Label();
            lbl9pm = new Label();
            lbl10pm = new Label();
            lblReservationCount = new Label();
            lblCoverCount = new Label();
            btnGetReservations = new Button();
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(199, 10);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 0;
            // 
            // rdoAM
            // 
            rdoAM.AutoSize = true;
            rdoAM.Location = new Point(55, 12);
            rdoAM.Name = "rdoAM";
            rdoAM.Size = new Size(44, 19);
            rdoAM.TabIndex = 1;
            rdoAM.TabStop = true;
            rdoAM.Text = "AM";
            rdoAM.UseVisualStyleBackColor = true;
            // 
            // rdoPM
            // 
            rdoPM.AutoSize = true;
            rdoPM.Location = new Point(119, 12);
            rdoPM.Name = "rdoPM";
            rdoPM.Size = new Size(43, 19);
            rdoPM.TabIndex = 1;
            rdoPM.TabStop = true;
            rdoPM.Text = "PM";
            rdoPM.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 67);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 2;
            label1.Text = "Reservations";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(285, 67);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 2;
            label2.Text = "Covers";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(55, 157);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 2;
            label3.Text = "4pm";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(55, 204);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 2;
            label4.Text = "5pm";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(55, 246);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 2;
            label5.Text = "6pm";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(55, 286);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 2;
            label6.Text = "7pm";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(55, 328);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 2;
            label7.Text = "8pm";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(55, 366);
            label8.Name = "label8";
            label8.Size = new Size(31, 15);
            label8.TabIndex = 2;
            label8.Text = "9pm";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(55, 412);
            label9.Name = "label9";
            label9.Size = new Size(37, 15);
            label9.TabIndex = 2;
            label9.Text = "10pm";
            // 
            // lbl4pm
            // 
            lbl4pm.AutoSize = true;
            lbl4pm.Location = new Point(131, 157);
            lbl4pm.Name = "lbl4pm";
            lbl4pm.Size = new Size(13, 15);
            lbl4pm.TabIndex = 2;
            lbl4pm.Text = "0";
            // 
            // lbl5pm
            // 
            lbl5pm.AutoSize = true;
            lbl5pm.Location = new Point(131, 204);
            lbl5pm.Name = "lbl5pm";
            lbl5pm.Size = new Size(13, 15);
            lbl5pm.TabIndex = 2;
            lbl5pm.Text = "0";
            // 
            // lbl6pm
            // 
            lbl6pm.AutoSize = true;
            lbl6pm.Location = new Point(131, 246);
            lbl6pm.Name = "lbl6pm";
            lbl6pm.Size = new Size(13, 15);
            lbl6pm.TabIndex = 2;
            lbl6pm.Text = "0";
            // 
            // lbl7pm
            // 
            lbl7pm.AutoSize = true;
            lbl7pm.Location = new Point(131, 286);
            lbl7pm.Name = "lbl7pm";
            lbl7pm.Size = new Size(13, 15);
            lbl7pm.TabIndex = 2;
            lbl7pm.Text = "0";
            // 
            // lbl8pm
            // 
            lbl8pm.AutoSize = true;
            lbl8pm.Location = new Point(131, 328);
            lbl8pm.Name = "lbl8pm";
            lbl8pm.Size = new Size(13, 15);
            lbl8pm.TabIndex = 2;
            lbl8pm.Text = "0";
            // 
            // lbl9pm
            // 
            lbl9pm.AutoSize = true;
            lbl9pm.Location = new Point(131, 366);
            lbl9pm.Name = "lbl9pm";
            lbl9pm.Size = new Size(13, 15);
            lbl9pm.TabIndex = 2;
            lbl9pm.Text = "0";
            // 
            // lbl10pm
            // 
            lbl10pm.AutoSize = true;
            lbl10pm.Location = new Point(131, 412);
            lbl10pm.Name = "lbl10pm";
            lbl10pm.Size = new Size(13, 15);
            lbl10pm.TabIndex = 2;
            lbl10pm.Text = "0";
            // 
            // lblReservationCount
            // 
            lblReservationCount.AutoSize = true;
            lblReservationCount.Location = new Point(55, 94);
            lblReservationCount.Name = "lblReservationCount";
            lblReservationCount.Size = new Size(13, 15);
            lblReservationCount.TabIndex = 2;
            lblReservationCount.Text = "0";
            // 
            // lblCoverCount
            // 
            lblCoverCount.AutoSize = true;
            lblCoverCount.Location = new Point(285, 94);
            lblCoverCount.Name = "lblCoverCount";
            lblCoverCount.Size = new Size(13, 15);
            lblCoverCount.TabIndex = 2;
            lblCoverCount.Text = "0";
            // 
            // btnGetReservations
            // 
            btnGetReservations.Location = new Point(452, 10);
            btnGetReservations.Name = "btnGetReservations";
            btnGetReservations.Size = new Size(137, 23);
            btnGetReservations.TabIndex = 3;
            btnGetReservations.Text = "Get Reservations";
            btnGetReservations.UseVisualStyleBackColor = true;
            btnGetReservations.Click += btnGetReservations_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(388, 103);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(248, 349);
            listBox1.TabIndex = 4;
            // 
            // frmReservationView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(724, 531);
            Controls.Add(listBox1);
            Controls.Add(btnGetReservations);
            Controls.Add(lblCoverCount);
            Controls.Add(label2);
            Controls.Add(lbl10pm);
            Controls.Add(label9);
            Controls.Add(lbl9pm);
            Controls.Add(label8);
            Controls.Add(lbl8pm);
            Controls.Add(label7);
            Controls.Add(lbl7pm);
            Controls.Add(label6);
            Controls.Add(lbl6pm);
            Controls.Add(label5);
            Controls.Add(lbl5pm);
            Controls.Add(label4);
            Controls.Add(lbl4pm);
            Controls.Add(lblReservationCount);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(rdoPM);
            Controls.Add(rdoAM);
            Controls.Add(dateTimePicker1);
            Name = "frmReservationView";
            Text = "frmReservationView";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateTimePicker1;
        private RadioButton rdoAM;
        private RadioButton rdoPM;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lbl4pm;
        private Label lbl5pm;
        private Label lbl6pm;
        private Label lbl7pm;
        private Label lbl8pm;
        private Label lbl9pm;
        private Label lbl10pm;
        private Label lblReservationCount;
        private Label lblCoverCount;
        private Button btnGetReservations;
        private ListBox listBox1;
    }
}