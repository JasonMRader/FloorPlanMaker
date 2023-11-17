using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class ImageLabelControl : UserControl
    {
        public ImageLabelControl(Image image, string text, int width, int height)
        {
            
            InitializeComponent();
            this.Height = height;
            this.Width = width;
            this.BackColor = UITheme.SecondColor;
            lblText.Text = text;
            lblText.Font = UITheme.MainFont;
            pbImage.Image = image;
            GetLocations();

        }
        private void GetLocations()
        {
            lblText.Location = new Point(this.Width/2, (this.Height - lblText.Height)/2);

            pbImage.Height = lblText.Height;
            pbImage.Width = pbImage.Height;

            pbImage.Location = new Point((this.Width/2) - pbImage.Width, lblText.Location.Y);
        }
    }
}
