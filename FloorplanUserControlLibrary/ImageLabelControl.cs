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
        public void SetProperties(Image image,  string toolTip, int width)
        {
            pbImage.Image = image;
            //lblText.Text = text;
            //this.Height = height;
            this.Width = width;
            GetLocations();
            SetTooltip(toolTip);
        }
        public void SetProperties(Image image, string toolTip, string text)
        {
            pbImage.Image = image;
            lblText.Text = text;
            //this.Height = height;
            
            GetLocations();
            SetTooltip(toolTip);
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
        public void UseLargeFont()
        {
            lblText.Font = UITheme.LargeFont;
        }
        public void SetFontSize(float fontSize)
        {
            lblText.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
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
        public void SetSizeAndLeftMostImage(int width, int height)
        {
            this.Size = new Size(width, height);
            pbImage.Height = this.Size.Height;
            pbImage.Width = this.Size.Height;
            pbImage.Location = new Point(0, 0);

            pbImage.BackColor = Color.White;
            lblText.AutoSize = false;
            lblText.Location = new Point(pbImage.Right + 3, 0);
            lblText.Size = new Size(this.Width - pbImage.Width, this.Size.Height);
            lblText.TextAlign = ContentAlignment.MiddleLeft;
            
        }

        private void ImageLabelControl_Load(object sender, EventArgs e)
        {

        }
    }
}
