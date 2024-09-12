using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorplanUserControlLibrary
{
    public partial class ColorSelection : UserControl
    {
        private int R = 0;
        private int G = 0;
        private int B = 0;
        private ColorPair colorPair = new ColorPair();
        public ColorSelection()
        {
            InitializeComponent();
        }
        public void SetSectionColorPair(int sectionNumber)
        {
            this.colorPair = SectionColorManager.GetColorPair(sectionNumber);
            this.BackColor = colorPair.BackgroundColor;
            tbR.Value = colorPair.BackgroundColor.R;
            R = colorPair.BackgroundColor.R;
            tbG.Value = colorPair.BackgroundColor.G;
            G = colorPair.BackgroundColor.G;
            tbB.Value = colorPair.BackgroundColor.B;
            B = colorPair.BackgroundColor.G;
            if (colorPair.FontColor.R == 0) {
                cbForeColor.Checked = true;
            }
            else {
                cbForeColor.Checked = false;
            }
            lblSectionNumber.Text = $"Section #{sectionNumber}";

        }

        private void tbR_Scroll(object sender, EventArgs e)
        {
            R = tbR.Value;
            this.BackColor = Color.FromArgb(R, G, B);
        }

        private void tbB_Scroll(object sender, EventArgs e)
        {
            B = tbB.Value;
            this.BackColor = Color.FromArgb(R, G, B);
        }

        private void tbG_Scroll(object sender, EventArgs e)
        {
            G = tbG.Value;
            this.BackColor = Color.FromArgb(R, G, B);
        }

        private void cbForeColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbForeColor.Checked) {
                SetForeColorToBlack();
            }
            else {
                SetForeColorToWhite();
            }

        }
        private void SetForeColorToWhite()
        {

            lblDefaults.ForeColor = Color.White;
            lblSectionNumber.ForeColor = Color.White;
            cbForeColor.BackColor = Color.White;
            cbForeColor.ForeColor = Color.Black;
        }
        private void SetForeColorToBlack()
        {
            lblDefaults.ForeColor = Color.Black;
            lblSectionNumber.ForeColor = Color.Black;
            cbForeColor.BackColor = Color.Black;
            cbForeColor.ForeColor = Color.White;
        }
    }
}
