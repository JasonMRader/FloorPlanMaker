using FloorplanClassLibrary;
using System.Security.Cryptography.X509Certificates;

namespace FloorPlanMaker
{
    public class ShiftControl : FlowLayoutPanel
    {
        private PictureBox _picOutside;
        private PictureBox _picClose;
        private PictureBox _picTeam;

        public ShiftControl(Shift shift, int width, int height)
        {
            this.Height = height;
            this.Width = width;
            this.BackColor = Color.LightGray;
            this.Tag = shift;
            this.AutoSize = true;
            int picBoxWidth = (int)(width / 1.25);
            this.MaximumSize = new Size(width, height*2);
            ShiftDayOfWeek = new Label
            {
                Text = shift.Date.ToString("ddd"),
                Width = width,
                TextAlign = ContentAlignment.MiddleCenter
            };
            // Set images based on shift properties
            _picOutside = new PictureBox
            {
                Width = (int)(width / 1.25),
                Height = (height / 3) - (ShiftDayOfWeek.Height / 3),
                Padding = new Padding(4, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            _picClose = new PictureBox
            {
                Width = (int)(width / 1.25),
                Height = (height / 3) - (ShiftDayOfWeek.Height / 3),
                Padding = new Padding(4, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            _picTeam = new PictureBox
            {
                Width = (int)(width / 1.25),
                Height = (height / 3) - (ShiftDayOfWeek.Height / 3),
                Padding = new Padding(4, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // Add to Controls
            this.Controls.Add(ShiftDayOfWeek);
            this.Controls.Add(_picOutside);
            //this.Controls.Add(_picClose);
            //this.Controls.Add(_picTeam);
        }
        public Label ShiftDayOfWeek { get ; set; }
        public PictureBox PicOutside => _picOutside;
        public PictureBox PicClose => _picClose;
        public PictureBox PicTeam => _picTeam;
    }

}