using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
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
    public partial class frmCustomColors : Form
    {
        private Dictionary<int, ColorPair> Colors = new Dictionary<int, ColorPair>();
        private ColorSelection colorSelection = new ColorSelection();
        public frmCustomColors()
        {
            InitializeComponent();
            panel1.Controls.Add(colorSelection);

        }

        private void frmCustomColors_Load(object sender, EventArgs e)
        {
            SectionColorManager.LoadColors();
            Colors = SectionColorManager.SectionColors;
            colorDialog.CustomColors = customColors;
            PopulateMainColorControls();
            btnSave.BackColor = UITheme.YesColor;
            btnCancel.BackColor = UITheme.NoColor;

        }

        private void PopulateMainColorControls()
        {
            for (int i = 1; i <= 15; i++) {
                int spots = 2;
                int previous = i - spots;
                int next = i + spots;
                if (previous < 1) {
                    previous = Colors.Count - spots + i;
                }
                if (next > Colors.Count) {
                    next = i - Colors.Count + spots;
                }

                Label lbl = new Label() {
                    Text = i.ToString(),
                    Margin = new Padding(0),
                    Size = new Size(flowLayoutPanel1.Width / 15, flowLayoutPanel1.Height),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = UITheme.LargeFont,
                    Tag = i,
                    BackColor = SectionColorManager.GetColorPair(i).BackgroundColor,
                    ForeColor = SectionColorManager.GetColorPair(i).FontColor,
                    AllowDrop = true
                };
                //Label num = new Label() {
                //    Text = i.ToString(),
                //    Margin = new Padding(0),
                //    Size = new Size(flowNumber.Width, (flowNumber.Height / 20)),
                //    TextAlign = ContentAlignment.MiddleCenter,
                //    Font = UITheme.MainFont,
                //    //BackColor = BackColor(i),
                //    //ForeColor = FontColor(i),
                //    AllowDrop = true
                //};
                //Label lblPrevious = new Label() {
                //    Text = previous.ToString(),
                //    Margin = new Padding(0),
                //    Size = new Size((flowLayoutPanel1.Width / 5), (flowLayoutPanel1.Height / 20)),
                //    TextAlign = ContentAlignment.MiddleCenter,
                //    Font = UITheme.LargeFont,
                //    BackColor = BackColor(previous),
                //    ForeColor = FontColor(previous),
                //    Dock = DockStyle.Left,
                //    AllowDrop = true
                //};
                //Label lblNext = new Label() {
                //    Text = next.ToString(),
                //    Margin = new Padding(0),
                //    Size = new Size((flowLayoutPanel1.Width / 5), (flowLayoutPanel1.Height / 20)),
                //    TextAlign = ContentAlignment.MiddleCenter,
                //    Font = UITheme.LargeFont,
                //    BackColor = BackColor(next),
                //    ForeColor = FontColor(next),
                //    Dock = DockStyle.Right,
                //    AllowDrop = true
                //};

                //lbl.MouseDown += Label_MouseDown;
                //lbl.DragEnter += Label_DragEnter;
                //lbl.DragDrop += Label_DragDrop;
                lbl.Click += Label_Click;
                //lbl.Controls.Add(lblPrevious);
                //lbl.Controls.Add(lblNext);
                flowLayoutPanel1.Controls.Add(lbl);
                //flowNumber.Controls.Add(num);
            }


        }

        private void Label_Click(object? sender, EventArgs e)
        {
            Label label = (Label)sender;
            int colorPair = (int)label.Tag;
            colorSelection.SetSectionColorPair(colorPair);
            

            //// Show a prompt to indicate selecting BackColor
            //MessageBox.Show("Please select BackColor", "Color Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //colorDialog.FullOpen = true; // Shows the custom colors area
            //colorDialog.Color = label.BackColor; // Set the initial color to the label's current BackColor

            //if (colorDialog.ShowDialog() == DialogResult.OK) {
            //    // Set the selected BackColor
            //    label.BackColor = colorDialog.Color;
            //}

            //// Show a prompt to indicate selecting ForeColor
            //MessageBox.Show("Please select ForeColor", "Color Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //colorDialog.Color = label.ForeColor; // Set the initial color to the label's current ForeColor

            //if (colorDialog.ShowDialog() == DialogResult.OK) {
            //    // Set the selected ForeColor
            //    label.ForeColor = colorDialog.Color;
            //}
        }

        private void Label_MouseDown(object sender, MouseEventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null) {
                lbl.DoDragDrop(lbl, DragDropEffects.Move);
            }
        }

        private void Label_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Label))) {
                e.Effect = DragDropEffects.Move;
            }
            else {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Label_DragDrop(object sender, DragEventArgs e)
        {
            Label sourceLabel = (Label)e.Data.GetData(typeof(Label));
            Label targetLabel = (Label)sender;

            if (sourceLabel != null && targetLabel != null && sourceLabel != targetLabel) {
                int sourceIndex = flowLayoutPanel1.Controls.IndexOf(sourceLabel);
                int targetIndex = flowLayoutPanel1.Controls.IndexOf(targetLabel);

                flowLayoutPanel1.Controls.SetChildIndex(sourceLabel, targetIndex);
                flowLayoutPanel1.Controls.SetChildIndex(targetLabel, sourceIndex);

                flowLayoutPanel1.Invalidate(); // Refresh the layout
            }
        }


        int[] customColors = new int[]
        {
            ColorTranslator.ToOle(Color.FromArgb(17, 100, 184)),
            ColorTranslator.ToOle(Color.FromArgb(105, 209, 0)),
            ColorTranslator.ToOle(Color.FromArgb(176, 46, 12)),
            ColorTranslator.ToOle(Color.FromArgb(103, 178, 216)),
            ColorTranslator.ToOle(Color.FromArgb(240, 246, 0)),
            ColorTranslator.ToOle(Color.FromArgb(70, 17, 122)),
            ColorTranslator.ToOle(Color.FromArgb(65, 234, 212)),
            ColorTranslator.ToOle(Color.FromArgb(244, 192, 149)),
            ColorTranslator.ToOle(Color.FromArgb(130, 9, 29)),
            ColorTranslator.ToOle(Color.FromArgb(194, 178, 180)),
            ColorTranslator.ToOle(Color.FromArgb(7, 79, 87)),
            ColorTranslator.ToOle(Color.FromArgb(250, 127, 127)),
            ColorTranslator.ToOle(Color.FromArgb(84, 92, 82)),
            ColorTranslator.ToOle(Color.FromArgb(180, 134, 159)),
            ColorTranslator.ToOle(Color.LightGray),
            ColorTranslator.ToOle(Color.DarkGray)
        };

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {

        }
    }
}
