namespace FloorPlanMakerUI
{
    partial class frmSpecialDates
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
            dtpEventDate = new DateTimePicker();
            txtEventName = new TextBox();
            cboType = new ComboBox();
            btnCreateEvent = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cbIgnoreSales = new CheckBox();
            label4 = new Label();
            label5 = new Label();
            lbPastEvents = new ListBox();
            lbUpcomingEvents = new ListBox();
            btnDeleteEvent = new Button();
            SuspendLayout();
            // 
            // dtpEventDate
            // 
            dtpEventDate.Location = new Point(18, 63);
            dtpEventDate.Name = "dtpEventDate";
            dtpEventDate.Size = new Size(200, 23);
            dtpEventDate.TabIndex = 0;
            // 
            // txtEventName
            // 
            txtEventName.Location = new Point(245, 63);
            txtEventName.Name = "txtEventName";
            txtEventName.Size = new Size(159, 23);
            txtEventName.TabIndex = 1;
            // 
            // cboType
            // 
            cboType.FormattingEnabled = true;
            cboType.Location = new Point(18, 107);
            cboType.Name = "cboType";
            cboType.Size = new Size(200, 23);
            cboType.TabIndex = 2;
            // 
            // btnCreateEvent
            // 
            btnCreateEvent.Location = new Point(18, 136);
            btnCreateEvent.Name = "btnCreateEvent";
            btnCreateEvent.Size = new Size(386, 37);
            btnCreateEvent.TabIndex = 3;
            btnCreateEvent.Text = "Create Event";
            btnCreateEvent.UseVisualStyleBackColor = true;
            btnCreateEvent.Click += btnCreateEvent_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 45);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 4;
            label1.Text = "Date";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(245, 45);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 4;
            label2.Text = "Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 89);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 4;
            label3.Text = "Type";
            // 
            // cbIgnoreSales
            // 
            cbIgnoreSales.AutoSize = true;
            cbIgnoreSales.Location = new Point(245, 111);
            cbIgnoreSales.Name = "cbIgnoreSales";
            cbIgnoreSales.Size = new Size(153, 19);
            cbIgnoreSales.TabIndex = 5;
            cbIgnoreSales.Text = "Ignore Sales From Stats?";
            cbIgnoreSales.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 179);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 4;
            label4.Text = "Past Events";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(218, 179);
            label5.Name = "label5";
            label5.Size = new Size(100, 15);
            label5.TabIndex = 4;
            label5.Text = "Upcoming Events";
            // 
            // lbPastEvents
            // 
            lbPastEvents.FormattingEnabled = true;
            lbPastEvents.ItemHeight = 15;
            lbPastEvents.Location = new Point(18, 197);
            lbPastEvents.Name = "lbPastEvents";
            lbPastEvents.Size = new Size(177, 199);
            lbPastEvents.TabIndex = 6;
            // 
            // lbUpcomingEvents
            // 
            lbUpcomingEvents.FormattingEnabled = true;
            lbUpcomingEvents.ItemHeight = 15;
            lbUpcomingEvents.Location = new Point(218, 197);
            lbUpcomingEvents.Name = "lbUpcomingEvents";
            lbUpcomingEvents.Size = new Size(177, 199);
            lbUpcomingEvents.TabIndex = 6;
            // 
            // btnDeleteEvent
            // 
            btnDeleteEvent.Location = new Point(18, 415);
            btnDeleteEvent.Name = "btnDeleteEvent";
            btnDeleteEvent.Size = new Size(386, 23);
            btnDeleteEvent.TabIndex = 7;
            btnDeleteEvent.Text = "Delete Selected";
            btnDeleteEvent.UseVisualStyleBackColor = true;
            // 
            // frmSpecialDates
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(438, 450);
            Controls.Add(btnDeleteEvent);
            Controls.Add(lbUpcomingEvents);
            Controls.Add(lbPastEvents);
            Controls.Add(cbIgnoreSales);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCreateEvent);
            Controls.Add(cboType);
            Controls.Add(txtEventName);
            Controls.Add(dtpEventDate);
            Name = "frmSpecialDates";
            Text = "frmSpecialDates";
            Load += frmSpecialDates_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpEventDate;
        private TextBox txtEventName;
        private ComboBox cboType;
        private Button btnCreateEvent;
        private Label label1;
        private Label label2;
        private Label label3;
        private CheckBox cbIgnoreSales;
        private Label label4;
        private Label label5;
        private ListBox lbPastEvents;
        private ListBox lbUpcomingEvents;
        private Button btnDeleteEvent;
    }
}