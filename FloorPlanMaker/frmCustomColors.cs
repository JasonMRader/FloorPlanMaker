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
        private Dictionary<int, ColorPair> CustomColors = new Dictionary<int, ColorPair>();
        private ColorSelection colorSelection = new ColorSelection();
        private Dictionary<int, Label> labelDictionary = new Dictionary<int, Label>();
        public frmCustomColors()
        {
            InitializeComponent();
            panel1.Controls.Add(colorSelection);

        }

        private void frmCustomColors_Load(object sender, EventArgs e)
        {
            SectionColorManager.LoadColors();
            //customColors = SectionColorManager.SectionColors;
            colorDialog.CustomColors = customColors;
            PopulateMainColorControls();
            btnSave.BackColor = UITheme.YesColor;
            btnCancel.BackColor = UITheme.NoColor;
            colorSelection.ColorChanged += UpdateSectionColor;

        }

        private void UpdateSectionColor(int sectionNum, ColorPair colorPair)
        {
            Label label = labelDictionary[sectionNum];
            label.BackColor = colorPair.BackgroundColor;
            label.ForeColor = colorPair.FontColor;
            CustomColors[sectionNum] = colorPair;
        }

        private void PopulateMainColorControls()
        {
            for (int i = 1; i <= 15; i++) {

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
                CustomColors[i] = SectionColorManager.GetColorPair(i);
                lbl.Click += Label_Click;
                flowLayoutPanel1.Controls.Add(lbl);
                labelDictionary[i] = lbl;

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
            SectionColorManager.SaveCustomColorsToDatabase(CustomColors);
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {

        }
    }
}
