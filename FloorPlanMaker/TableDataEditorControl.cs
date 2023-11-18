using FloorplanClassLibrary;
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
    public partial class TableDataEditorControl : UserControl
    {
        private TableControl? tableControl { get; set; }
        public TableDataEditorControl() { }
        public TableDataEditorControl(TableControl tableControl)
        {
            InitializeComponent();
            this.tableControl = tableControl;
            txtCovers.Text = tableControl.Table.MaxCovers.ToString();
            this.BackColor = tableControl.BackColor;
            txtSales.Text = Section.FormatAsCurrencyWithoutParentheses(tableControl.Table.AverageCovers);
            setStartLocation();
        }
        private void setStartLocation()
        {
            int xLocation = this.tableControl.Left + (this.tableControl.Width / 2 - this.Width / 2);
            int yLocation = this.tableControl.Top + (this.tableControl.Height / 2 - this.Height / 2);
            this.Location = new Point(xLocation, yLocation);
        }
    }
}
