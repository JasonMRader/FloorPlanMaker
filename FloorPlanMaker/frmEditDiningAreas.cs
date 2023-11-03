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
    public partial class frmEditDiningAreas : Form
    {
        private bool isDraggingForm = false;
        private System.Drawing.Point lastLocation;
        public frmEditDiningAreas()
        {
            InitializeComponent();
        }

        private void frmEditDiningAreas_Load(object sender, EventArgs e)
        {

        }
        private void frmEditDiningAreas_MouseDown(object sender, MouseEventArgs e)
        {
            isDraggingForm = true;
            lastLocation = e.Location;
        }

        private void frmEditDiningAreas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingForm)
            {
                this.Location = new System.Drawing.Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void frmEditDiningAreas_MouseUp(object sender, MouseEventArgs e)
        {
            isDraggingForm = false;
        }
    }
}
