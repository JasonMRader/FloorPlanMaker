using FloorplanClassLibrary;

namespace FloorPlanMaker
{
    public partial class Form1 : Form
    {
        //List<DiningArea> areaList = new List<DiningArea>();
        DiningAreaManager areaManager = new DiningAreaManager();
        private int LastTableNumberSelected;
        private TableControl currentEmphasizedTable = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            cboDiningAreas.DataSource = areaManager.DiningAreas;
            cboDiningAreas.DisplayMember = "Name";
            cboDiningAreas.ValueMember = "ID";

            TableControl circleTable = new TableControl();
            circleTable.Location = new Point(70, 50);
            circleTable.Size = new Size(100, 100);
            circleTable.Moveable = false;
            circleTable.TableClicked += AddTable_TableClicked;

            pnlAddTables.Controls.Add(circleTable);

            TableControl diamondTable = new TableControl();
            diamondTable.Location = new Point(70, 175);
            diamondTable.Size = new Size(100, 100);
            diamondTable.Moveable = false;
            diamondTable.TableClicked += AddTable_TableClicked;
            diamondTable.Shape = Table.TableShape.Diamond;
            pnlAddTables.Controls.Add(diamondTable);

            TableControl squareTable = new TableControl();
            squareTable.Location = new Point(70, 300);
            squareTable.Size = new Size(100, 100);
            squareTable.Moveable = false;
            squareTable.TableClicked += AddTable_TableClicked;
            squareTable.Shape = Table.TableShape.Square;
            pnlAddTables.Controls.Add(squareTable);
        }
        private void AddTable_TableClicked(object sender, TableClickedEventArgs e)
        {
            TableControl clickedTable = (TableControl)sender;
            TableControl table = new TableControl()
            {
                Width = 100,
                Height = 100,
                Left = new Random().Next(100, 300), // These are example values, replace with what you need
                Top = new Random().Next(100, 300),
                Moveable = true,
                Shape = clickedTable.Shape,
                Location = new Point(300, 400)
            };
            table.TableClicked += ExistingTable_TableClicked;
            // Subscribe to the TableClicked event for the new table as well
            //table.TableClicked += Table_TableClicked;

            pnlFloorPlan.Controls.Add(table);
        }
        private void ExistingTable_TableClicked(object sender, TableClickedEventArgs e)
        {
            Table clickedTable = e.ClickedTable;
            TableControl clickedTableControl = sender as TableControl;
            if (currentEmphasizedTable != null && currentEmphasizedTable != clickedTableControl)
            {
                currentEmphasizedTable.BorderThickness = 1;
                currentEmphasizedTable.Invalidate();  // Request a redraw
            }
            txtTableNumber.Text = clickedTable.TableNumber;
            txtMaxCovers.Text = clickedTable.MaxCovers.ToString();
            txtAverageCovers.Text = clickedTable.AverageCovers.ToString();
            txtHeight.Text = clickedTable.Height.ToString();
            txtWidth.Text = clickedTable.Width.ToString();
            areaManager.SelectedTable = clickedTable;

            clickedTableControl.BorderThickness = 3;
            clickedTableControl.Invalidate(); // Request a redraw

            // Update the current emphasized table.
            currentEmphasizedTable = clickedTableControl;
            //if (!e.IsMoveable)
            //{

            //    txtTableNumber.Text = clickedTable.TableNumber;
            //    txtMaxCovers.Text = clickedTable.MaxCovers.ToString();
            //    txtAverageCovers.Text = clickedTable.AverageCovers.ToString();
            //    txtHeight.Text = clickedTable.Height.ToString();
            //    txtWidth.Text = clickedTable.Width.ToString();
            //    areaManager.SelectedTable = clickedTable;

            //    clickedTableControl.BorderThickness = 3;
            //    clickedTableControl.Invalidate(); // Request a redraw

            //    // Update the current emphasized table.
            //    currentEmphasizedTable = clickedTableControl;

            //}
            //else
            //{
            //    // Handle the table movement logic if it's movable
            //}
        }

        private void cbDesignMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDesignMode.Checked)
            {
                cbDesignMode.Text = "Create Sections";
            }
            else
            {
                cbDesignMode.Text = "Edit Dining Area";
            }
        }

        private void btnSaveDiningArea_Click(object sender, EventArgs e)
        {

            DiningArea area = new DiningArea(txtDiningAreaName.Text, rbInside.Checked);

            SqliteDataAccess.SaveDiningArea(area);
        }

        private void btnSaveTables_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.DeleteTablesByDiningArea(areaManager.DiningAreaSelected);
            foreach (Control control in pnlFloorPlan.Controls)
            {
                if (control is TableControl tableControl)
                {
                    Table tableToSave = tableControl.Table;

                    // Update table properties based on the tableControl properties.
                    // This ensures any changes made in the UI are saved.
                    tableToSave.TableNumber = tableControl.Table.TableNumber;
                    tableToSave.DiningArea = areaManager.DiningAreaSelected;
                    tableToSave.Width = tableControl.Width;
                    tableToSave.Height = tableControl.Height;
                    tableToSave.XCoordinate = tableControl.Location.X;
                    tableToSave.YCoordinate = tableControl.Location.Y;
                    tableToSave.Shape = tableControl.Shape;

                    SqliteDataAccess.SaveTable(tableToSave);
                }
            }
        }

        private void cboDiningAreas_SelectedIndexChanged(object sender, EventArgs e)
        {

            areaManager.DiningAreaSelected = (DiningArea?)cboDiningAreas.SelectedItem;
            txtDiningAreaName.Text = areaManager.DiningAreaSelected.Name;
            pnlFloorPlan.Controls.Clear();
            foreach (Table table in areaManager.DiningAreaSelected.Tables)
            {
                table.DiningArea = areaManager.DiningAreaSelected;
                TableControl tableControl = TableControlFactory.CreateTableControl(table);
                //tableControl.TableClicked += Table_TableClicked;  // Uncomment if you want to attach event handler
                tableControl.TableClicked += ExistingTable_TableClicked;
                pnlFloorPlan.Controls.Add(tableControl);
            }
        }



        private void cbLockTables_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbLockTables.Checked)
            {
                cbLockTables.Text = "Lock Tables";
                foreach (Control control in pnlFloorPlan.Controls)
                {
                    if (control is TableControl tableControl)
                    {
                        tableControl.Moveable = true;
                    }
                }
            }
            else
            {
                cbLockTables.Text = "Unlock Tables";
                foreach (Control control in pnlFloorPlan.Controls)
                {
                    if (control is TableControl tableControl)
                    {
                        tableControl.Moveable = false;
                    }
                }
            }
        }

        private void btnSaveTable_Click(object sender, EventArgs e)
        {
            areaManager.SelectedTable.TableNumber = txtTableNumber.Text;
            areaManager.SelectedTable.MaxCovers = Int32.Parse(txtMaxCovers.Text);
            areaManager.SelectedTable.AverageCovers = float.Parse(txtAverageCovers.Text);
            areaManager.SelectedTable.Height = Int32.Parse(txtHeight.Text);
            areaManager.SelectedTable.Width = Int32.Parse(txtWidth.Text);
            areaManager.SelectedTable.DiningArea = areaManager.DiningAreaSelected;
            
            
            SqliteDataAccess.UpdateTable(areaManager.SelectedTable);
            areaManager.DiningAreaSelected.Tables.Add(areaManager.SelectedTable);
            //areaManager.DiningAreaSelected.Tables = SqliteDataAccess.LoadTables
            pnlFloorPlan.Controls.Clear();
            foreach (Table table in areaManager.DiningAreaSelected.Tables)
            {
                TableControl tableControl = TableControlFactory.CreateTableControl(table);
                tableControl.Moveable = false;
                tableControl.TableClicked += ExistingTable_TableClicked;
                pnlFloorPlan.Controls.Add(tableControl);
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            areaManager.DiningAreaSelected.Tables.Remove(areaManager.SelectedTable);

            SqliteDataAccess.DeleteTable(areaManager.SelectedTable);

            foreach (Control control in pnlFloorPlan.Controls)
            {
                if (control is TableControl && control.Tag == areaManager.SelectedTable)
                {
                    pnlFloorPlan.Controls.Remove(control);
                    break;
                }
            }
            //foreach (Table table in areaManager.DiningAreaSelected.Tables)
            //{
            //    TableControl tableControl = TableControlFactory.CreateTableControl(table);
            //    //tableControl.Moveable = false;
            //    tableControl.TableClicked += ExistingTable_TableClicked;
            //    pnlFloorPlan.Controls.Add(tableControl);
            //}
            areaManager.SelectedTable = null;
        }

        private void btnCopyTable_Click(object sender, EventArgs e)
        {
            int currentTableNumber = int.Parse(areaManager.SelectedTable.TableNumber);
            int newTableNumber = currentTableNumber + 1;
            //TableControl clickedTable = (TableControl)sender;
            Table table = new Table()
            {
                Width = areaManager.SelectedTable.Width,
                Height = areaManager.SelectedTable.Height,
                //Left = new Random().Next(100, 300), // These are example values, replace with what you need
                //Top = new Random().Next(100, 300),
                //Moveable = true,
                Shape = areaManager.SelectedTable.Shape,
                TableNumber = newTableNumber.ToString(),
                MaxCovers = areaManager.SelectedTable.MaxCovers,
                AverageCovers = areaManager.SelectedTable.AverageCovers,
                YCoordinate = areaManager.SelectedTable.YCoordinate,
                XCoordinate = areaManager.SelectedTable.XCoordinate + areaManager.SelectedTable.Width, 
                DiningAreaId = areaManager.SelectedTable.DiningAreaId,
                DiningArea = areaManager.SelectedTable.DiningArea
               
            };
            table.ID = SqliteDataAccess.SaveTable(table);
            TableControl tableControl = TableControlFactory.CreateTableControl(table);
            tableControl.Tag = table;
            tableControl.TableClicked += ExistingTable_TableClicked;
            // Subscribe to the TableClicked event for the new table as well
            //table.TableClicked += Table_TableClicked;

            pnlFloorPlan.Controls.Add(tableControl);
        }
    }
}