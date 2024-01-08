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
        private bool isSalesOnly = false;
        private bool isCoversOnly = false;
        public int TableNumber
        {
            get
            {
                if (int.TryParse(this.tableControl.Table.TableNumber, out int tableNumber))
                {
                    return tableNumber;
                }
                else { return -1; }
            }
        }
        public TableDataEditorControl() { }
        public TableDataEditorControl(TableControl tableControl)
        {
            InitializeComponent();
            this.tableControl = tableControl;
            txtCovers.Text = tableControl.Table.MaxCovers.ToString();
            this.BackColor = tableControl.BackColor;
            txtSales.Text = Section.FormatAsCurrencyWithoutParentheses(tableControl.Table.AverageSales);
            SetToCoversOnly();
            setStartLocation();
        }
        private void setStartLocation()
        {
            int xLocation = this.tableControl.Left + (this.tableControl.Width / 2 - this.Width / 2);
            int yLocation = this.tableControl.Top + (this.tableControl.Height / 2 - this.Height / 2);
            this.Location = new Point(xLocation, yLocation);
        }
        public void SetToCoversOnly()
        {
            this.Controls.Clear();
            this.Size = new Size(65, 20);
            picCovers.Location = new Point(0, 0);
            txtCovers.Location = new Point(25, 0);
            this.Controls.Add(picCovers);
            this.Controls.Add(txtCovers);
            setStartLocation();
            this.Invalidate();
            isCoversOnly = true;
        }
        public void SetToSalesOnly()
        {
            this.Controls.Clear();
            this.Size = new Size(65, 20);
            picSales.Location = new Point(0, 0);
            txtSales.Location = new Point(25, 0);
            this.Controls.Add(picSales);
            this.Controls.Add(txtSales);
            setStartLocation();
            this.Invalidate();
            isSalesOnly = true;
        }
        public void SetToBoth()
        {
            this.Controls.Clear();
            this.Size = new Size(65, 40);
            picCovers.Location = new Point(0, 0);
            picSales.Location = new Point(0, 20);
            txtCovers.Location = new Point(25, 0);
            txtSales.Location = new Point(25, 20);
            this.Controls.Add(picCovers);
            this.Controls.Add(txtCovers);
            this.Controls.Add(picSales);
            this.Controls.Add(txtSales);
            setStartLocation();
            this.Invalidate();
            isSalesOnly = false;
            isCoversOnly = false;

        }
        private void TableDataEditorControl_Load(object sender, EventArgs e)
        {

        }

        private void txtCovers_TextChanged(object sender, EventArgs e)
        {
            int newMaxCovers = -1;
            if (int.TryParse(txtCovers.Text, out int maxCovers))
            {
                newMaxCovers = maxCovers;
            }
            if (newMaxCovers > -1 && newMaxCovers != this.tableControl.Table.MaxCovers)
            {
                this.tableControl.Table.MaxCovers = newMaxCovers;
                SqliteDataAccess.UpdateTable(this.tableControl.Table);
            }
        }

        private void txtSales_TextChanged(object sender, EventArgs e)
        {
            int newAvgSales = -1;
            if (int.TryParse(txtSales.Text, out int avgSales))
            {
                newAvgSales = avgSales;
            }
            if (newAvgSales > -1 && newAvgSales != this.tableControl.Table.AverageSales)
            {
                this.tableControl.Table.AverageSales = newAvgSales;
                SqliteDataAccess.UpdateTable(this.tableControl.Table);
            }
        }

        private void TableDataEditorControl_Enter(object sender, EventArgs e)
        {
            if(isCoversOnly)
            {
                this.txtCovers.Focus();
            }
            if (isSalesOnly)
            {
                this.txtSales.Focus();
            }
            
        }
    }
}
