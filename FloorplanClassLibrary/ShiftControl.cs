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

            // Set images based on shift properties
            _picOutside = new PictureBox
            {
                Width = width,
                Height = (height / 3),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            _picClose = new PictureBox
            {
                Width = width,
                Height = (height / 3),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            _picTeam = new PictureBox
            {
                Width = width,
                Height = (height / 3),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // Add to Controls
            this.Controls.Add(_picOutside);
            this.Controls.Add(_picClose);
            this.Controls.Add(_picTeam);
        }

        public PictureBox PicOutside => _picOutside;
        public PictureBox PicClose => _picClose;
        public PictureBox PicTeam => _picTeam;
    }

}