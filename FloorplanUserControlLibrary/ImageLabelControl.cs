using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FloorPlanMakerUI
{
    public partial class ImageLabelControl : UserControl
    {
        private ToolTip toolTip = new ToolTip();
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
            toolTip = new ToolTip();

        }
        public void SetTooltip(string tooltipText)
        {
            toolTip.SetToolTip(this.lblText, tooltipText);
            toolTip.SetToolTip(this.pbImage, tooltipText);
            toolTip.SetToolTip(this, tooltipText);
        }
        public ImageLabelControl()
        {
            InitializeComponent();
        }
        private void GetLocations2()
        {
            lblText.Location = new Point(this.Width / 2, (this.Height - lblText.Height) / 2);

            pbImage.Height = lblText.Height;
            pbImage.Width = pbImage.Height;

            pbImage.Location = new Point((this.Width / 2) - pbImage.Width, lblText.Location.Y);
        }
        public void UpdateText(string text)
        {
            lblText.Text = text;
            GetLocations();
        }
        private void GetLocations()
        {
            pbImage.Height = lblText.Height;
            pbImage.Width = pbImage.Height;
            int controlsWidth = lblText.Width + pbImage.Width;
            int controlFirstX = this.Width / 2 - controlsWidth / 2;
            pbImage.Location = new Point(controlFirstX, (this.Height - pbImage.Height) / 2);
            lblText.Location = new Point(pbImage.Right, (this.Height - lblText.Height) / 2);




        }
        public override string ToString()
        {
            return lblText.Text;
        }

        private void ImageLabelControl_Load(object sender, EventArgs e)
        {

        }
    }
}
