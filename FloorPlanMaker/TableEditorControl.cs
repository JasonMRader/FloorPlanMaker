using FloorPlanMaker;
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
    public partial class TableEditorControl : UserControl
    {
        private TableControl? tableControl { get; set; }
        public TableEditorControl() { }
        public TableEditorControl(TableControl tableControl, Panel panel)
        {
            InitializeComponent();
            this.tableControl = tableControl;
            setDefaultLocation(panel);

        }
        private void setDefaultLocation(Panel panel)
        {
            this.Location = new Point(this.tableControl.Right + 10, this.tableControl.Top);
            yCoordinateAdjustment(panel.Height);
            xCoordinateAdjustment(panel.Width);
        }
        private void yCoordinateAdjustment(int panelHeight)
        {
            if(this.Top+this.Height > panelHeight)
            {
                this.Location = new Point(this.Left, panelHeight-this.Height); 
            }
        }
        private void xCoordinateAdjustment(int panelWidth)
        {
            if (this.Left + this.Width > panelWidth)
            {
                this.Location = new Point(this.tableControl.Left - this.Width - 10, this.Top);
            }
        }
        private void btnSmaller_Click(object sender, EventArgs e)
        {
            this.tableControl.Width -= 10;
            tableControl.Table.Width = tableControl.Height;
            this.tableControl.Height -= 10;
            tableControl.Table.Height = tableControl.Height;
            tableControl.Invalidate();
        }

        private void btnBigger_Click(object sender, EventArgs e)
        {
            this.tableControl.Width += 10;
            tableControl.Table.Width = tableControl.Height;
            this.tableControl.Height += 10;
            tableControl.Table.Height = tableControl.Height;
            tableControl.Invalidate();
        }

        private void btnNarrower_Click(object sender, EventArgs e)
        {
            this.tableControl.Width -= 10;
            tableControl.Table.Width = tableControl.Height;
            tableControl.Invalidate();
        }

        private void btnWider_Click(object sender, EventArgs e)
        {
            this.tableControl.Width += 10;
            tableControl.Table.Width = tableControl.Height;
            tableControl.Invalidate();
        }

        private void btnShorter_Click(object sender, EventArgs e)
        {
            this.tableControl.Height -= 10;
            tableControl.Table.Height = tableControl.Height;
            tableControl.Invalidate();
        }

        private void btnTaller_Click(object sender, EventArgs e)
        {
            this.tableControl.Height += 10;
            tableControl.Table.Height = tableControl.Height;
            tableControl.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
