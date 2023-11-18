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
        public TableEditorControl(TableControl tableControl)
        {
            InitializeComponent();
            this.tableControl = tableControl;

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
