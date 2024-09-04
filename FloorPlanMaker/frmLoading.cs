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
    public partial class frmLoading : Form
    {
        public enum GifType
        {
            Assign,
            BluePrint,
            Rocket,
            Drawing,
            Time,
            Stats

        }
        public frmLoading(string display)
        {
            InitializeComponent();
            this.display = display;
        }
        public frmLoading(GifType gifType)
        {
            InitializeComponent();
            SetGif(gifType);
        }

        private void SetGif(GifType gifType)
        {
            if (gifType == GifType.Assign) {
                pictureBox1.Image = Properties.Resources.assign;
            }
            else if (gifType == GifType.Stats) {
                pictureBox1.Image = Properties.Resources.statistics;
            }
            else if (gifType == GifType.BluePrint) {
                pictureBox1.Image = Properties.Resources.blueprint;
            }
            else if(gifType == GifType.Rocket) {
                pictureBox1.Image = Properties.Resources.rocket;
            }
            else if(gifType == GifType.Drawing) {
                pictureBox1.Image = Properties.Resources.drawing;
            }
            else if (gifType == GifType.Time) {
                pictureBox1.Image = Properties.Resources.sleep;
            }
        }

        string display;
        private int dotCount = 1;

        private void timer_Tick(object sender, EventArgs e)
        {
            // Update the text based on the dotCount
            //switch (dotCount)
            //{
            //    case 1:
            //        label1.Text = display + ".";
            //        dotCount++;
            //        break;
            //    case 2:
            //        label1.Text = display + "..";
            //        dotCount++;
            //        break;
            //    case 3:
            //        label1.Text = display + "...";
            //        dotCount++;  // Reset to start cycle over
            //        break;
            //    case 4:
            //        label1.Text = display + "....";
            //        dotCount++;
            //        break;
            //    case 5:
            //        label1.Text = display + ".....";
            //        dotCount = 1;
            //        break;
            //}
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void FormLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
