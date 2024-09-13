using FloorplanClassLibrary;
using FloorPlanMakerUI;
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
        private int num = 0;
        private ColorPair colorPair = new ColorPair();
        public ColorSelection()
        {
            InitializeComponent();
            PopulateDefaults();
        }
        private void PopulateDefaults()
        {

            for (int i = 1; i <= 15; i++) {
              

                Button lbl = new Button() {
                    Text = i.ToString(),
                    Margin = new Padding(0),
                    Size = new Size(flowDefaults.Width / 15, flowDefaults.Height),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = UITheme.LargeFont,
                    Tag = i,
                    BackColor = SectionColorManager.GetColorPair(i).BackgroundColor,
                    ForeColor = SectionColorManager.GetColorPair(i).FontColor,
                    AllowDrop = true
                };

               
                
                lbl.Click += Label_Click;
                //this.Controls.Add(lbl1Before);
                //this.Controls.Add(lbl1After);
                flowDefaults.Controls.Add(lbl);

            }
           


        }
        private void GetNeighboringSections()
        {
            int spots = 1;
            int previous = num - spots;
            int next = num + spots;
            if (previous < 1) {
                previous = 15 - spots + num;
            }
            if (next > 15) {
                next = num - 15 + spots;
            }
            lbl1Before.Text = previous.ToString();
            lbl1Before.Margin = new Padding(0);
            //Size = new Size((flowLayoutPanel1.Width / 5), (flowLayoutPanel1.Height / 20)),
            lbl1Before.TextAlign = ContentAlignment.MiddleCenter;
            lbl1Before.Font = UITheme.LargeFont;
            lbl1Before.BackColor = SectionColorManager.GetColorPair(previous).BackgroundColor;
            lbl1Before.ForeColor = SectionColorManager.GetColorPair(previous).FontColor;
            //lbl1Before.Dock = DockStyle.Left;
            // lbl1Before.AllowDrop = true;


            lbl1After.Text = next.ToString();
            lbl1After.Margin = new Padding(0);
            //Size = new Size((flowLayoutPanel1.Width / 5), (flowLayoutPanel1.Height / 20)),
            lbl1After.TextAlign = ContentAlignment.MiddleCenter;
            lbl1After.Font = UITheme.LargeFont;
            lbl1After.BackColor = SectionColorManager.GetColorPair(next).BackgroundColor;
            lbl1After.ForeColor = SectionColorManager.GetColorPair(next).FontColor;
            //lbl1After.Dock = DockStyle.Right;
            // lbl1After.AllowDrop = true;
        }
        private void Label_Click(object? sender, EventArgs e)
        {

        }

        public void SetSectionColorPair(int sectionNumber)
        {
            num = sectionNumber;
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
            GetNeighboringSections();

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

        private void ColorSelection_Load(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
