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
        public event Action<int, ColorPair> ColorChanged;
        public ColorSelection()
        {
            InitializeComponent();
            PopulateDefaults();
        }
        private void PopulateDefaults()
        {

            for (int i = 1; i <= 15; i++) {


                Button btn = new Button() {
                    Text = i.ToString(),
                    Margin = new Padding(0),
                    Size = new Size(flowDefaults.Width / 15, flowDefaults.Height),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = UITheme.LargeFont,
                    Tag = i,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = SectionColorManager.GetDefaultColorPair(i).BackgroundColor,
                    ForeColor = SectionColorManager.GetDefaultColorPair(i).FontColor,
                    AllowDrop = true
                };



                btn.Click += defaultColor_Click;
                //this.Controls.Add(lbl1Before);
                //this.Controls.Add(lbl1After);
                flowDefaults.Controls.Add(btn);

            }



        }
        private void GetNeighboringSections()
        {
            int spots = 1;
            int previous = num - spots;
            int next = num + spots;
            int previous2 = num - 2;
            int next2 = num + 2;
            int previous3 = num - 3;
            int next3 = num + 3;
            if (previous < 1) {
                previous = 15 - spots + num;
            }
            if (next > 15) {
                next = num - 15 + spots;
            }
            if (previous2 < 1) {
                previous2 = 15 - 2 + num;
            }
            if (next2 > 15) {
                next2 = num - 15 + spots;
            }
            if (previous3 < 1) {
                previous3 = 15 - 3 + num;
            }
            if (next3 > 15) {
                next3 = num - 15 + 3;
            }

            lbl1Before.Text = previous.ToString();
            lbl1Before.Margin = new Padding(0);
            lbl1Before.TextAlign = ContentAlignment.MiddleCenter;
            lbl1Before.Font = UITheme.LargeFont;
            lbl1Before.BackColor = SectionColorManager.GetColorPair(previous).BackgroundColor;
            lbl1Before.ForeColor = SectionColorManager.GetColorPair(previous).FontColor;

            lbl1After.Text = next.ToString();
            lbl1After.Margin = new Padding(0);
            lbl1After.TextAlign = ContentAlignment.MiddleCenter;
            lbl1After.Font = UITheme.LargeFont;
            lbl1After.BackColor = SectionColorManager.GetColorPair(next).BackgroundColor;
            lbl1After.ForeColor = SectionColorManager.GetColorPair(next).FontColor;

            lbl2Before.Text = previous2.ToString();
            lbl2Before.Margin = new Padding(0);
            lbl2Before.TextAlign = ContentAlignment.MiddleCenter;
            lbl2Before.Font = UITheme.LargeFont;
            lbl2Before.BackColor = SectionColorManager.GetColorPair(previous2).BackgroundColor;
            lbl2Before.ForeColor = SectionColorManager.GetColorPair(previous2).FontColor;

            lbl2After.Text = next2.ToString();
            lbl2After.Margin = new Padding(0);
            lbl2After.TextAlign = ContentAlignment.MiddleCenter;
            lbl2After.Font = UITheme.LargeFont;
            lbl2After.BackColor = SectionColorManager.GetColorPair(next2).BackgroundColor;
            lbl2After.ForeColor = SectionColorManager.GetColorPair(next2).FontColor;

            lbl3Before.Text = previous3.ToString();
            lbl3Before.Margin = new Padding(0);
            lbl3Before.TextAlign = ContentAlignment.MiddleCenter;
            lbl3Before.Font = UITheme.LargeFont;
            lbl3Before.BackColor = SectionColorManager.GetColorPair(previous3).BackgroundColor;
            lbl3Before.ForeColor = SectionColorManager.GetColorPair(previous3).FontColor;

            lbl3After.Text = next3.ToString();
            lbl3After.Margin = new Padding(0);
            lbl3After.TextAlign = ContentAlignment.MiddleCenter;
            lbl3After.Font = UITheme.LargeFont;
            lbl3After.BackColor = SectionColorManager.GetColorPair(next3).BackgroundColor;
            lbl3After.ForeColor = SectionColorManager.GetColorPair(next3).FontColor;

        }
        private void defaultColor_Click(object? sender, EventArgs e)
        {
            Button button = sender as Button;
            int i = (int)button.Tag;
            SetColorsToSectionManagerColorPair(i, true);
            
            
        }
        
        public void SetSectionColorPair(int sectionNumber)
        {
            num = sectionNumber;
            SetColorsToSectionManagerColorPair(sectionNumber, false);
            lblSectionNumber.Text = $"Section #{sectionNumber}";
            GetNeighboringSections();

        }
        private void SetColorsToSectionManagerColorPair(int sectionNumber, bool isDefault)
        {
            if(!isDefault) {
                this.colorPair = SectionColorManager.GetColorPair(sectionNumber);
            }
            else {
                this.colorPair = SectionColorManager.GetDefaultColorPair(sectionNumber);
            }
            
            pnlMain.BackColor = colorPair.BackgroundColor;
            cbForeColor.BackColor = colorPair.BackgroundColor;
            tbR.Value = colorPair.BackgroundColor.R;
            R = colorPair.BackgroundColor.R;
            txtR.Text = colorPair.BackgroundColor.R.ToString();
            tbG.Value = colorPair.BackgroundColor.G;
            G = colorPair.BackgroundColor.G;
            txtG.Text = colorPair.BackgroundColor.G.ToString();
            tbB.Value = colorPair.BackgroundColor.B;
            B = colorPair.BackgroundColor.B;
            txtB.Text = colorPair.BackgroundColor.B.ToString();
            if (colorPair.FontColor.R == 0) {
                SetForeColorToBlack();
                cbForeColor.Checked = true;
            }
            else {
                SetForeColorToWhite();
                cbForeColor.Checked = false;
            }
        }

        private void tbR_Scroll(object sender, EventArgs e)
        {
            R = tbR.Value;
            SetBackColor();
            txtR.Text = R.ToString();
        }

        private void tbB_Scroll(object sender, EventArgs e)
        {
            B = tbB.Value;
            SetBackColor();
            txtB.Text = B.ToString();
        }

        private void tbG_Scroll(object sender, EventArgs e)
        {
            G = tbG.Value;
            SetBackColor();
            txtG.Text = G.ToString();
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
            //cbForeColor.BackColor = Color.White;
            cbForeColor.ForeColor = Color.White;
            cbForeColor.Text = "Change Section Text Color To Black";
            colorPair.FontColor = Color.White;
        }
        private void SetForeColorToBlack()
        {
            lblDefaults.ForeColor = Color.Black;
            lblSectionNumber.ForeColor = Color.Black;
            //cbForeColor.BackColor = Color.Black;
            cbForeColor.ForeColor = Color.Black;
            cbForeColor.Text = "Change Section Text Color To White";
            colorPair.FontColor = Color.Black;
        }

        private void ColorSelection_Load(object sender, EventArgs e)
        {

        }


        private void lblDefaults_Click(object sender, EventArgs e)
        {

        }

        private void txtR_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtR.Text)) {
                if (int.TryParse(txtR.Text, out int r)) {
                    if (r <= 255 && r >= 0) {
                        R = r;
                        tbR.Value = r;
                        SetBackColor();
                    }
                }
            }
        }
        private void SetBackColor()
        {
            colorPair.BackgroundColor = Color.FromArgb(R, G, B);
            pnlMain.BackColor = Color.FromArgb(R, G, B);
            cbForeColor.BackColor = Color.FromArgb(R, G, B);
        }
        private void txtG_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtG.Text)) {
                if (int.TryParse(txtG.Text, out int g)) {
                    if (g <= 255 && g >= 0) {
                        tbG.Value = g;
                        G = g;
                        SetBackColor();
                    }
                }
            }
        }

        private void txtB_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtB.Text)) {
                if (int.TryParse(txtB.Text, out int b)) {
                    if (b <= 255 && b >= 0) {
                        B = b;
                        tbB.Value = b;
                        SetBackColor();
                    }
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ColorChanged?.Invoke(num, colorPair);
        }
    }
}
