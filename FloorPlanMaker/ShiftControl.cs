using FloorplanClassLibrary;
using FloorPlanMakerUI;
using System.Security.Cryptography.X509Certificates;

namespace FloorPlanMaker
{
    public class ShiftControl : FlowLayoutPanel
    {
        private PictureBox _picOutside;
        private PictureBox _picClose;
        private PictureBox _picTeam;
        public Shift Shift { get; set; }

        public ShiftControl(Shift shift, int width, int height)
        {
            this.Height = height;
            this.Width = width;
            this.BackColor = Color.LightGray;
            this.Tag = shift;
            this.Shift = shift;
            this.AutoSize = true;
            int picBoxWidth = (int)(width / 1.25);
            this.MaximumSize = new Size(width, height*2);
            this.Margin = new Padding(6,0,0,0);
            ShiftDayOfWeek = new Label
            {
                Text = shift.Date.ToString("ddd"),
                Width = width,
                Height = 12,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0)
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
        public void HideOutside()
        {
            this._picOutside.Dispose();
        }
        public void ShowClose()
        {
            _picClose = new PictureBox
            {
                Width = (int)(this.Width / 1.5),
                Height = (int)(this.Width / 2), 
                Margin = new Padding(6, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage,

            };
            if (this.Shift.IsCloser)
            {
                _picClose.Image = Resource1.ClsLetters;
            }
            else
            {
                _picClose.Image = Resource1.Cut;
            }
            this.Controls.Add(_picClose);
            //this.Invalidate();
            
        }
        public void ShowTeam()
        {
            _picTeam = new PictureBox
            {
                Width = (int)(this.Width / 1.5),
                Height = (int)(this.Width / 2),
                Margin = new Padding(6, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            if (this.Shift.IsTeamWait)
            {
                _picTeam.Image = Resource1.TeamWait;
            }
            else
            {
                _picTeam.Image= Resource1.Solo;
            }
            this.Controls.Add(_picTeam);
            //this.Invalidate();
        }
        public Label ShiftDayOfWeek { get ; set; }
        public PictureBox PicOutside => _picOutside;
        public PictureBox PicClose => _picClose;
        public PictureBox PicTeam => _picTeam;
    }

}