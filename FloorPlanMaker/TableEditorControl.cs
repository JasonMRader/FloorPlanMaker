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
           
        }

        private void btnBigger_Click(object sender, EventArgs e)
        {

        }

        private void btnNarrower_Click(object sender, EventArgs e)
        {

        }

        private void btnWider_Click(object sender, EventArgs e)
        {

        }

        private void btnShorter_Click(object sender, EventArgs e)
        {
            this.tableControl.Height -= 5;
            tableControl.Table.Height = tableControl.Height;
            tableControl.Invalidate();
        }

        private void btnTaller_Click(object sender, EventArgs e)
        {

        }
    }
}
