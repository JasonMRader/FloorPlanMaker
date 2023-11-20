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
    public partial class TableEditorControl : UserControl
    {
        private TableControl? tableControl { get; set; }
        private Action<TableControl, TableClickedEventArgs> tableClickedHandler;
        public TableEditorControl() { }
        public TableEditorControl(TableControl tableControl, Panel panel)
        {
            InitializeComponent();
            this.tableControl = tableControl;
            setDefaultLocation(panel);

        }
        public TableEditorControl(TableControl tableControl, Panel panel,
    Action<TableControl, TableClickedEventArgs> tableClickedHandler) : this(tableControl, panel)
        {
            InitializeComponent();
            this.tableControl = tableControl;
            setDefaultLocation(panel);
            this.tableClickedHandler = tableClickedHandler;
        }
        private void setDefaultLocation(Panel panel)
        {
            this.Location = new Point(this.tableControl.Right + 10, this.tableControl.Top);
            yCoordinateAdjustment(panel.Height);
            xCoordinateAdjustment(panel.Width);
        }
        private void yCoordinateAdjustment(int panelHeight)
        {
            if (this.Top + this.Height > panelHeight)
            {
                this.Location = new Point(this.Left, panelHeight - this.Height);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tableControl != null && tableControl.Parent is Panel parentPanel)
            {
                SqliteDataAccess.DeleteTable(this.tableControl.Table);
                // Remove the tableControl from the parent Panel
                parentPanel.Controls.Remove(tableControl);

                // Optionally, you can also dispose the tableControl if it's no longer needed
                tableControl.Dispose();
                tableControl = null;

                // Refresh the parent Panel to update the UI
                parentPanel.Invalidate();
                this.Dispose();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            int currentTableNumber = int.Parse(this.tableControl.Table.TableNumber);
            int newTableNumber = currentTableNumber + 1;
            //TableControl clickedTable = (TableControl)sender;
            Table table = new Table()
            {
                Width = this.tableControl.Table.Width,
                Height = this.tableControl.Table.Height,
                //Left = new Random().Next(100, 300), // These are example values, replace with what you need
                //Top = new Random().Next(100, 300),
                //Moveable = true,
                Shape = this.tableControl.Table.Shape,
                TableNumber = newTableNumber.ToString(),
                MaxCovers = this.tableControl.Table.MaxCovers,
                AverageCovers = this.tableControl.Table.AverageCovers,
                YCoordinate = this.tableControl.Table.YCoordinate,
                XCoordinate = this.tableControl.Table.XCoordinate + this.tableControl.Table.Width + 5,
                DiningAreaId = this.tableControl.Table.DiningAreaId,
                DiningArea = this.tableControl.Table.DiningArea

            };
            table.ID = SqliteDataAccess.SaveTable(table);
            TableControl tableControl = TableControlFactory.CreateTableControl(table);
            tableControl.BackColor = this.tableControl.BackColor;
            tableControl.Moveable = true;
            tableControl.Tag = table;
            this.tableControl.Table.DiningArea.Tables.Add(table);
            tableControl.TableClicked += (sender, e) => tableClickedHandler((TableControl)sender, e);
            // Subscribe to the TableClicked event for the new table as well
            //table.TableClicked += Table_TableClicked;
           // tableControl.TableClicked += (sender, e) => tableClickedHandler?.Invoke((TableControl)sender, e);

            Parent.Controls.Add(tableControl);
            this.Dispose();
        }
    }
}
