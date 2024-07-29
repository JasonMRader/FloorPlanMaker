namespace FloorplanUserControlLibrary
{
    partial class HourlyWeatherDisplay
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
            lblTime = new Label();
            lblFeelsLikeHi = new Label();
            lblFeelsLikeLow = new Label();
            lblChanceOfRain = new Label();
            lblWindAvg = new Label();
            lblRainAmount = new Label();
            lblWindMax = new Label();
            SuspendLayout();
            // 
            // lblTime
            // 
            lblTime.BackColor = SystemColors.ControlDarkDark;
            lblTime.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblTime.ForeColor = Color.White;
            lblTime.Location = new Point(0, 0);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(125, 21);
            lblTime.TabIndex = 0;
            lblTime.Text = "11:00 AM";
            lblTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFeelsLikeHi
            // 
            lblFeelsLikeHi.BackColor = Color.FromArgb(224, 224, 224);
            lblFeelsLikeHi.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblFeelsLikeHi.Location = new Point(1, 21);
            lblFeelsLikeHi.Margin = new Padding(1);
            lblFeelsLikeHi.Name = "lblFeelsLikeHi";
            lblFeelsLikeHi.Size = new Size(60, 23);
            lblFeelsLikeHi.TabIndex = 1;
            lblFeelsLikeHi.Text = "87°";
            lblFeelsLikeHi.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFeelsLikeLow
            // 
            lblFeelsLikeLow.BackColor = Color.FromArgb(224, 224, 224);
            lblFeelsLikeLow.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblFeelsLikeLow.Location = new Point(63, 21);
            lblFeelsLikeLow.Margin = new Padding(1);
            lblFeelsLikeLow.Name = "lblFeelsLikeLow";
            lblFeelsLikeLow.Size = new Size(60, 23);
            lblFeelsLikeLow.TabIndex = 1;
            lblFeelsLikeLow.Text = "67°";
            lblFeelsLikeLow.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblChanceOfRain
            // 
            lblChanceOfRain.BackColor = Color.FromArgb(224, 224, 224);
            lblChanceOfRain.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblChanceOfRain.Location = new Point(1, 46);
            lblChanceOfRain.Margin = new Padding(1);
            lblChanceOfRain.Name = "lblChanceOfRain";
            lblChanceOfRain.Size = new Size(60, 23);
            lblChanceOfRain.TabIndex = 1;
            lblChanceOfRain.Text = "40%";
            lblChanceOfRain.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWindAvg
            // 
            lblWindAvg.BackColor = Color.FromArgb(224, 224, 224);
            lblWindAvg.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblWindAvg.Location = new Point(1, 71);
            lblWindAvg.Margin = new Padding(1);
            lblWindAvg.Name = "lblWindAvg";
            lblWindAvg.Size = new Size(60, 23);
            lblWindAvg.TabIndex = 1;
            lblWindAvg.Text = "3 MPH";
            lblWindAvg.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblRainAmount
            // 
            lblRainAmount.BackColor = Color.FromArgb(224, 224, 224);
            lblRainAmount.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblRainAmount.Location = new Point(63, 46);
            lblRainAmount.Margin = new Padding(1);
            lblRainAmount.Name = "lblRainAmount";
            lblRainAmount.Size = new Size(60, 23);
            lblRainAmount.TabIndex = 1;
            lblRainAmount.Text = ".01\"";
            lblRainAmount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWindMax
            // 
            lblWindMax.BackColor = Color.FromArgb(224, 224, 224);
            lblWindMax.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblWindMax.Location = new Point(63, 71);
            lblWindMax.Margin = new Padding(1);
            lblWindMax.Name = "lblWindMax";
            lblWindMax.Size = new Size(60, 23);
            lblWindMax.TabIndex = 1;
            lblWindMax.Text = "8 MPH";
            lblWindMax.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HourlyWeatherDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblRainAmount);
            Controls.Add(lblFeelsLikeLow);
            Controls.Add(lblWindMax);
            Controls.Add(lblWindAvg);
            Controls.Add(lblChanceOfRain);
            Controls.Add(lblFeelsLikeHi);
            Controls.Add(lblTime);
            Name = "HourlyWeatherDisplay";
            Size = new Size(125, 100);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTime;
        private Label lblFeelsLikeHi;
        private Label lblFeelsLikeLow;
        private Label lblChanceOfRain;
        private Label lblWindAvg;
        private Label lblRainAmount;
        private Label lblWindMax;
    }
}
