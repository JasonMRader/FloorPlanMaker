using FloorplanClassLibrary;
using FloorPlanMaker;
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

namespace FloorPlanMakerUI
{
    public partial class frmEditDiningAreas : Form
    {

        private DiningAreaCreationManager areaCreationManager = new DiningAreaCreationManager();
        private TableControl currentEmphasizedTableControl = null;
        private DrawingHandler drawingHandler;
        List<TableControl> emphasizedTablesList = new List<TableControl>();
        private bool isDragging = false;
        private System.Drawing.Point dragStartPoint;
        private Rectangle dragRectangle;
        private List<TableControl> allTableControls = new List<TableControl>();
        List<TableDataEditorControl> tableDataEditors = new List<TableDataEditorControl>();
        private TableEditorControl positionEditor;
        // TODO: tableChanges not consistant, sometimes changing dining areas will not allow changes to the next one (specifically moving tables? deleting?)
        // TODO: Moving tables not always saving
        // TODO: dont allow tables to stack on one another?
        public frmEditDiningAreas()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if the Tab key is pressed
            if (keyData == Keys.Tab)
            {
                MoveToNextControl();
                return true; // Indicate that you've handled this key press
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void ChangeDiningArea(Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                // Ensure the index stays within bounds
                if (cboDiningAreas.SelectedIndex > 0)
                {
                    cboDiningAreas.SelectedIndex--;
                }
                else
                {
                    cboDiningAreas.SelectedIndex = cboDiningAreas.Items.Count - 1;
                }

            }

            if (keyData == Keys.Down)
            {
                // Ensure the index stays within bounds
                if (cboDiningAreas.SelectedIndex < cboDiningAreas.Items.Count - 1)
                {
                    cboDiningAreas.SelectedIndex++;
                }
                else
                {
                    cboDiningAreas.SelectedIndex = 0;
                }

            }
        }
        private void MoveToNextControl()
        {
            if (rdoCoverView.Checked || rdoSalesView.Checked)
            {
                TableDataEditorControl focusedControl = GetFocusedTableDataEditorControl();

                var currentControlIndex = tableDataEditors.FindIndex(control => control == focusedControl);
                if (currentControlIndex >= 0 && currentControlIndex < tableDataEditors.Count - 1)
                {
                    var nextControl = tableDataEditors[currentControlIndex + 1];
                    nextControl.Focus();
                }
            }



        }
        private TableDataEditorControl GetFocusedTableDataEditorControl()
        {
            Control focused = this.ActiveControl;
            while (focused != null)
            {
                if (focused is TableDataEditorControl)
                {
                    return (TableDataEditorControl)focused;
                }
                focused = focused.Parent;
            }
            return null;
        }

        private void SetColors()
        {

            UITheme.FormatMainButton(btnQuickEdit);
            UITheme.FormatMainButton(cbViewMode);

            UITheme.FormatMainButton(btnSaveTable);
            UITheme.FormatMainButton(cbLockTables);

            UITheme.FormatMainButton(btnCreateNewDiningArea);
            UITheme.FormatMainButton(btnSaveDiningArea);
            UITheme.FormatMainButton(btnSaveDiningArea);

            // AppColors.FormatSecondColor(this);
            UITheme.FormatSecondColor(panel4);
            UITheme.FormatSecondColor(panel3);

            UITheme.FormatAccentColor(this);

            UITheme.FormatAccentColor(panel3);

            UITheme.FormatCanvasColor(pnlFloorPlan);

            UITheme.FormatCanvasColor(panel5);
            UITheme.FormatSecondColor(panel2);

            UITheme.FormatSecondColor(panel6);


        }
        private void frmEditDiningAreas_Load(object sender, EventArgs e)
        {
            SetColors();
            cboDiningAreas.DataSource = areaCreationManager.DiningAreas;
            cboDiningAreas.DisplayMember = "Name";
            cboDiningAreas.ValueMember = "ID";
        }

        private void cboDiningAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            allTableControls.Clear();
            areaCreationManager.SelectedTable = null;
            areaCreationManager.SelectedTables.Clear();
            areaCreationManager.DiningAreaSelected = (DiningArea?)cboDiningAreas.SelectedItem;
            txtDiningAreaName.Text = areaCreationManager.DiningAreaSelected.Name;
            pnlFloorPlan.Controls.Clear();
            foreach (Table table in areaCreationManager.DiningAreaSelected.Tables)
            {
                table.DiningArea = areaCreationManager.DiningAreaSelected;
                TableControl tableControl = TableControlFactory.CreateTableControl(table);
                //tableControl.TableClicked += Table_TableClicked;  // Uncomment if you want to attach event handler
                tableControl.TableClicked += ExistingTable_TableClicked;
                tableControl.BackColor = Color.LightGray;
                pnlFloorPlan.Controls.Add(tableControl);
                allTableControls.Add(tableControl);
            }
            if (rdoCoverView.Checked)
            {
                SetTableControlsToCoverData();
            }
            if (rdoEditPositions.Checked)
            {
                SetTableControlsToReposition();
            }
            if (rdoSalesView.Checked)
            {
                SetTableControlsToSalesData();
            }


        }
        private void ExistingTable_TableClicked(object sender, TableClickedEventArgs e)
        {

            TableControl clickedTableControl = sender as TableControl;
            Table clickedTable = clickedTableControl.Table;

            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {

                foreach (var emphasizedTable in emphasizedTablesList)
                {
                    if (emphasizedTable != clickedTableControl)
                    {
                        emphasizedTable.Controls.Remove(emphasizedTable.txtTableNumber);
                        emphasizedTable.BorderThickness = 1;
                        emphasizedTable.Invalidate();
                    }
                }
                emphasizedTablesList.Clear();
                areaCreationManager.SelectedTables.Clear();


                txtTableNumber.Enabled = true;
            }
            else
            {
                txtTableNumber.Enabled = false;
            }

            areaCreationManager.SelectedTable = clickedTable;
            currentEmphasizedTableControl = clickedTableControl;

            clickedTableControl.BorderThickness = 6;
            clickedTableControl.Invalidate();

            emphasizedTablesList.Add(clickedTableControl);

            areaCreationManager.SelectedTables.Add(clickedTable);
            txtTableNumber.Text = clickedTable.TableNumber;
            txtMaxCovers.Text = clickedTable.MaxCovers.ToString();
            txtAverageCovers.Text = clickedTable.AverageCovers.ToString();

            txtXco.Text = clickedTable.XCoordinate.ToString();
            txtYco.Text = clickedTable.YCoordinate.ToString();
            string tableNum = "";
            if (areaCreationManager.SelectedTables.Count > 1)
            {
                foreach (var table in areaCreationManager.SelectedTables)
                {
                    tableNum += table.TableNumber + " ";
                }
                txtTableNumber.Text = tableNum;
            }
            pnlFloorPlan.Controls.Remove(positionEditor);
            if (clickedTableControl.Moveable)
            {
                //positionEditor = TableEditorFactory.CreateEditor(clickedTableControl, pnlFloorPlan);
                positionEditor = new TableEditorControl(clickedTableControl, pnlFloorPlan, ExistingTable_TableClicked);
                clickedTableControl.AddTxtTableNumber();
                clickedTableControl.txtTableNumber.Focus();
                //editor.Location = new Point(clickedTableControl.Right + 20, clickedTableControl.Top);

                pnlFloorPlan.Controls.Add(positionEditor);
                positionEditor.BringToFront();
            }



            refreshSelectedTableNeighbors();


        }



        private void SaveNewTableByTableControl(TableControl tableControl)
        {
            Table tableToSave = tableControl.Table;

            // Update table properties based on the tableControl properties.
            // This ensures any changes made in the UI are saved.
            tableToSave.TableNumber = tableControl.Table.TableNumber;
            tableToSave.DiningArea = areaCreationManager.DiningAreaSelected;
            tableToSave.Width = tableControl.Width;
            tableToSave.Height = tableControl.Height;
            tableToSave.XCoordinate = tableControl.Location.X;
            tableToSave.YCoordinate = tableControl.Location.Y;
            tableToSave.Shape = tableControl.Shape;

            SqliteDataAccess.SaveTable(tableToSave);

        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            areaCreationManager.DiningAreaSelected.Tables.Remove(areaCreationManager.SelectedTable);

            SqliteDataAccess.DeleteTable(areaCreationManager.SelectedTable);

            foreach (Control control in pnlFloorPlan.Controls)
            {
                if (control is TableControl && control.Tag == areaCreationManager.SelectedTable)
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
            areaCreationManager.SelectedTable = null;
        }

        private void btnCopyTable_Click(object sender, EventArgs e)
        {
            int currentTableNumber = int.Parse(areaCreationManager.SelectedTable.TableNumber);
            int newTableNumber = currentTableNumber + 1;
            //TableControl clickedTable = (TableControl)sender;
            Table table = new Table()
            {
                Width = areaCreationManager.SelectedTable.Width,
                Height = areaCreationManager.SelectedTable.Height,
                //Left = new Random().Next(100, 300), // These are example values, replace with what you need
                //Top = new Random().Next(100, 300),
                //Moveable = true,
                Shape = areaCreationManager.SelectedTable.Shape,
                TableNumber = newTableNumber.ToString(),
                MaxCovers = areaCreationManager.SelectedTable.MaxCovers,
                AverageCovers = areaCreationManager.SelectedTable.AverageCovers,
                YCoordinate = areaCreationManager.SelectedTable.YCoordinate,
                XCoordinate = areaCreationManager.SelectedTable.XCoordinate + areaCreationManager.SelectedTable.Width,
                DiningAreaId = areaCreationManager.SelectedTable.DiningAreaId,
                DiningArea = areaCreationManager.SelectedTable.DiningArea

            };
            table.ID = SqliteDataAccess.SaveTable(table);
            TableControl tableControl = TableControlFactory.CreateTableControl(table);
            tableControl.Moveable = true;
            tableControl.Tag = table;
            areaCreationManager.DiningAreaSelected.Tables.Add(table);
            tableControl.TableClicked += ExistingTable_TableClicked;
            // Subscribe to the TableClicked event for the new table as well
            //table.TableClicked += Table_TableClicked;

            pnlFloorPlan.Controls.Add(tableControl);
        }

        private void btnSaveTable_Click(object sender, EventArgs e)
        {
            if (areaCreationManager.SelectedTables.Count > 1)
            {
                foreach (TableControl tableControl in emphasizedTablesList)
                {
                    SqliteDataAccess.UpdateTable(tableControl.Table);
                }


            }
            if (areaCreationManager.SelectedTables.Count == 1)
            {
                SqliteDataAccess.UpdateTable(areaCreationManager.SelectedTable);
            }
        }


        private void RefreshTableControl(object sender, EventArgs e)
        {
            if (areaCreationManager.SelectedTables.Count > 1)
            {
                foreach (TableControl tableControl in emphasizedTablesList)
                {
                    SetTablePropertiesFromTxtBx(tableControl.Table, tableControl);
                }

            }
            if (areaCreationManager.SelectedTables.Count == 1)
            {
                SetTablePropertiesFromTxtBx(areaCreationManager.SelectedTable, currentEmphasizedTableControl);
                //SqliteDataAccess.UpdateTable(areaManager.SelectedTable);
            }



        }
        private void RefreshTableControl()
        {
            if (areaCreationManager.SelectedTables.Count > 1)
            {
                foreach (TableControl tableControl in emphasizedTablesList)
                {
                    SetTablePropertiesFromTxtBx(tableControl.Table, tableControl);
                }

            }
            if (areaCreationManager.SelectedTables.Count == 1)
            {
                SetTablePropertiesFromTxtBx(areaCreationManager.SelectedTable, currentEmphasizedTableControl);
                //SqliteDataAccess.UpdateTable(areaManager.SelectedTable);
            }



        }
        private void SetTablePropertiesFromTxtBx(Table table, TableControl tableControl)
        {
            if (!string.IsNullOrWhiteSpace(txtTableNumber.Text))
            {
                if (emphasizedTablesList.Count == 1)
                {
                    table.TableNumber = txtTableNumber.Text;
                }

            }

            if (int.TryParse(txtMaxCovers.Text, out int maxCovers))
            {
                table.MaxCovers = maxCovers;
            }

            if (float.TryParse(txtAverageCovers.Text, out float averageCovers))
            {
                table.AverageCovers = averageCovers;
            }


            if (int.TryParse(txtXco.Text, out int xCo))
            {
                table.XCoordinate = xCo;
            }
            if (int.TryParse(txtYco.Text, out int yCo))
            {
                table.YCoordinate = yCo;
            }

            // Assuming these other methods and properties do not require validation
            table.DiningArea = areaCreationManager.DiningAreaSelected;
            //table.XCoordinate = UpdateXCoordinateForTableControl(table);
            //table.XCoordinate = tableControl.Left;
            table.YCoordinate = UpdateYCoordinateForTableControl(table);
            //table.YCoordinate = tableControl.Top;
            tableControl.Table = table;
            TableControlFactory.RedrawTableControl(tableControl, pnlFloorPlan);
        }
        private void SetTableProperties(Table table, TableControl tableControl)
        {
            if (!string.IsNullOrWhiteSpace(txtTableNumber.Text))
            {
                if (emphasizedTablesList.Count == 1)
                {
                    table.TableNumber = txtTableNumber.Text;
                }

            }

            if (int.TryParse(txtMaxCovers.Text, out int maxCovers))
            {
                table.MaxCovers = maxCovers;
            }

            if (float.TryParse(txtAverageCovers.Text, out float averageCovers))
            {
                table.AverageCovers = averageCovers;
            }


            if (int.TryParse(txtXco.Text, out int xCo))
            {
                table.XCoordinate = xCo;
            }
            if (int.TryParse(txtYco.Text, out int yCo))
            {
                table.YCoordinate = yCo;
            }

            // Assuming these other methods and properties do not require validation
            table.DiningArea = areaCreationManager.DiningAreaSelected;
            //table.XCoordinate = UpdateXCoordinateForTableControl(table);
            //table.XCoordinate = tableControl.Left;
            table.YCoordinate = UpdateYCoordinateForTableControl(table);
            //table.YCoordinate = tableControl.Top;
            tableControl.Table = table;
            TableControlFactory.RedrawTableControl(tableControl, pnlFloorPlan);
        }
        private int UpdateYCoordinateForTableControl(Table table)
        {
            int yCoordinate = table.YCoordinate;
            foreach (Control control in pnlFloorPlan.Controls)
            {
                if (control is TableControl && control.Tag == areaCreationManager.SelectedTable)
                {
                    yCoordinate = control.Top;

                }
            }
            return yCoordinate;
        }
        private int UpdateXCoordinateForTableControl(Table table)
        {
            int xCoordinate = table.XCoordinate;
            foreach (Control control in pnlFloorPlan.Controls)
            {
                if (control is TableControl && control.Tag == areaCreationManager.SelectedTable)
                {
                    xCoordinate = control.Left;

                }
            }
            return xCoordinate;
        }
        private void btnCreateNewDiningArea_Click(object sender, EventArgs e)
        {
            txtDiningAreaName.Enabled = true;
            rbInside.Enabled = true;
            rbOutside.Enabled = true;
            cbTemporaryFloorplan.Enabled = true;
            btnSaveDiningArea.Enabled = true;
            txtDiningAreaName.Clear();
        }

        private void btnSaveDiningArea_Click(object sender, EventArgs e)
        {
            DiningArea area = new DiningArea(txtDiningAreaName.Text, rbInside.Checked);

            SqliteDataAccess.SaveDiningArea(area);

            txtDiningAreaName.Enabled = false;
            rbInside.Enabled = false;
            rbOutside.Enabled = false;
            cbTemporaryFloorplan.Enabled = false;
            btnSaveDiningArea.Enabled = false;
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

        private void btnQuickEdit_Click(object sender, EventArgs e)
        {
            //allTableControls.Clear();

            // pnlFloorPlan.Controls.Clear();
            foreach (TableControl tableControl in allTableControls)
            {
                TableDataEditorControl dataEditor = new TableDataEditorControl(tableControl);
                pnlFloorPlan.Controls.Add(dataEditor);
                dataEditor.BringToFront();

            }
        }

        private void cbViewMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbViewMode.Checked)
            {
                foreach (Control c in pnlFloorPlan.Controls)
                {
                    if (c is TableControl tableControl)
                    {
                        tableControl.CurrentDisplayMode = DisplayMode.AverageCovers;
                        tableControl.Invalidate();
                    }
                }
            }
            else
            {
                foreach (Control c in pnlFloorPlan.Controls)
                {
                    if (c is TableControl tableControl)
                    {
                        tableControl.CurrentDisplayMode = DisplayMode.TableNumber;
                        tableControl.Invalidate();
                    }
                }
            }
        }

        private void rdoEditData_CheckedChanged(object sender, EventArgs e)
        {
            SetTableControlsToCoverData();

        }
        private void SetTableControlsToCoverData()
        {
            foreach (TableDataEditorControl editor in tableDataEditors)
            {
                pnlFloorPlan.Controls.Remove(editor);
            }
            tableDataEditors.Clear();
            if (rdoCoverView.Checked)
            {
                pnlFloorPlan.Controls.Remove(positionEditor);
                foreach (TableControl tableControl in allTableControls)
                {
                    TableDataEditorControl dataEditor = new TableDataEditorControl(tableControl);
                    tableControl.Controls.Remove(txtTableNumber);

                    dataEditor.SetToCoversOnly();
                    tableDataEditors.Add(dataEditor);
                    pnlFloorPlan.Controls.Add(dataEditor);
                    dataEditor.BringToFront();
                    tableControl.Invalidate();

                }
            }
            tableDataEditors = OrderEditorList();

        }
        private List<TableDataEditorControl> OrderEditorList()
        {
            var orderedList = tableDataEditors.OrderBy(t => t.TableNumber).ToList();
            return orderedList;

        }
        private void rdoSalesView_CheckedChanged(object sender, EventArgs e)
        {
            SetTableControlsToSalesData();
        }
        private void SetTableControlsToSalesData()
        {
            foreach (TableDataEditorControl editor in tableDataEditors)
            {
                pnlFloorPlan.Controls.Remove(editor);
            }
            tableDataEditors.Clear();
            if (rdoSalesView.Checked)
            {
                pnlFloorPlan.Controls.Remove(positionEditor);
                foreach (TableControl tableControl in allTableControls)
                {
                    TableDataEditorControl dataEditor = new TableDataEditorControl(tableControl);
                    tableControl.Controls.Remove(txtTableNumber);

                    dataEditor.SetToSalesOnly();
                    tableDataEditors.Add(dataEditor);
                    pnlFloorPlan.Controls.Add(dataEditor);
                    dataEditor.BringToFront();
                    tableControl.Invalidate();

                }
            }
        }

        private void rdoEditPositions_CheckedChanged(object sender, EventArgs e)
        {
            SetTableControlsToReposition();
        }
        private void SetTableControlsToReposition()
        {
            if (rdoEditPositions.Checked)
            {
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
                foreach (Control control in pnlFloorPlan.Controls)
                {
                    if (control is TableControl tableControl)
                    {
                        tableControl.Moveable = false;
                    }
                }
            }
        }
        private void rdoDefaultView_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDefaultView.Checked)
            {

            }
        }

        private void picAddSquare_Click(object sender, EventArgs e)
        {
            TableControl table = new TableControl()
            {
                Width = 100,
                Height = 100,
                Left = new Random().Next(100, 300),
                Top = new Random().Next(100, 300),
                Moveable = true,
                Shape = Table.TableShape.Square,
                Location = new System.Drawing.Point(300, 400)
            };
            table.TableClicked += ExistingTable_TableClicked;

            //table.TableClicked += Table_TableClicked;
            SaveNewTableByTableControl(table);
            pnlFloorPlan.Controls.Add(table);
            areaCreationManager.SelectedTable = table.Table;
        }

        private void picAddDiamond_Click(object sender, EventArgs e)
        {
            TableControl table = new TableControl()
            {
                Width = 100,
                Height = 100,
                Left = new Random().Next(100, 300),
                Top = new Random().Next(100, 300),
                Moveable = true,
                Shape = Table.TableShape.Diamond,
                Location = new System.Drawing.Point(300, 400)
            };
            table.TableClicked += ExistingTable_TableClicked;

            //table.TableClicked += Table_TableClicked;
            SaveNewTableByTableControl(table);
            pnlFloorPlan.Controls.Add(table);
        }

        private void picAddCircle_Click(object sender, EventArgs e)
        {
            TableControl table = new TableControl()
            {
                Width = 100,
                Height = 100,
                Left = new Random().Next(100, 300),
                Top = new Random().Next(100, 300),
                Moveable = true,
                Shape = Table.TableShape.Circle,
                Location = new System.Drawing.Point(300, 400)
            };
            table.TableClicked += ExistingTable_TableClicked;

            //table.TableClicked += Table_TableClicked;
            SaveNewTableByTableControl(table);
            pnlFloorPlan.Controls.Add(table);
        }

        private void pnlFloorPlan_Click(object sender, EventArgs e)
        {
            foreach (var emphasizedTable in emphasizedTablesList)
            {

                emphasizedTable.Controls.Remove(emphasizedTable.txtTableNumber);
                emphasizedTable.BorderThickness = 1;
                emphasizedTable.Invalidate();

            }
            emphasizedTablesList.Clear();
            areaCreationManager.SelectedTables.Clear();
            areaCreationManager.SelectedTable = null;
            currentEmphasizedTableControl = null;
            pnlFloorPlan.Controls.Remove(positionEditor);
        }


        TableGrid grid = new TableGrid();
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                MakeNeighborControlsVisible();
                grid = new TableGrid(areaCreationManager.DiningAreaSelected.Tables);
                grid.FindTableTopBottomNeighbors();
                grid.FindTableNeighbors();
                grid.SetTableBoarderMidPoints();
                ToggleViewAllBorders();

                List<string> testing = grid.GetTestData();

            }
            else
            {
                MakeNeighborControlsInvisible();
                pnlFloorPlan.BackgroundImage = null;
            }
        }
        private void refreshSelectedTableNeighbors()
        {
            if (checkBox1.Checked)
            {
                lblSelectedTableNumber.Text = areaCreationManager.SelectedTable.TableNumber;
                List<string> tableNumbers = grid.GetNeighborNames(areaCreationManager.SelectedTable.TableNumber);
                foreach (TableControl tableControl in pnlFloorPlan.Controls)
                {
                    if (tableNumbers.Contains(tableControl.Table.TableNumber))
                    {
                        tableControl.BackColor = UITheme.CautionColor;
                    }
                    else
                    {
                        tableControl.BackColor = Color.LightGray;
                    }
                }
                ToggleViewAllBorders();

                List<Neighbor> neighbors = grid.GetNeighbors(areaCreationManager.SelectedTable.TableNumber);
                lbTableNeighbors.Items.Clear();

                List<string> displayableNeighbors = grid.GetDisplayableNeighbors(neighbors, areaCreationManager.SelectedTable);
                foreach (var neighborString in displayableNeighbors)
                {
                    lbTableNeighbors.Items.Add(neighborString);
                }
                txtMidPoint.Text = "";
                txtStart.Text = "";
                txtEnd.Text = "";

            }

        }
        private void MakeNeighborControlsInvisible()
        {
            lblSelectedTableNumber.Visible = false;
            lblSelectTable.Visible = false;
            lbTableNeighbors.Visible = false;
            btnRemoveNeighbor.Visible = false;
            btnAddRightLeftNeighbor.Visible = false;
            lblPairData.Visible = false;
            lblEndPoint.Visible = false;
            lblMidPoint.Visible = false;
            lblStartPoint.Visible = false;
            txtMidPoint.Visible = false;
            txtStart.Visible = false;
            txtEnd.Visible = false;
            cbOnlyShowThisTableLines.Visible = false;
        }
        private void MakeNeighborControlsVisible()
        {
            lblSelectedTableNumber.Visible = true;
            lblSelectTable.Visible = true;
            lbTableNeighbors.Visible = true;
            btnAddRightLeftNeighbor.Visible = true;
            btnRemoveNeighbor.Visible = true;
            lblPairData.Visible = true;
            lblEndPoint.Visible = true;
            lblMidPoint.Visible = true;
            lblStartPoint.Visible = true;
            txtMidPoint.Visible = true;
            txtStart.Visible = true;
            txtEnd.Visible = true;
            cbOnlyShowThisTableLines.Visible = true;
        }

        private void lbTableNeighbors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTableNeighbors.SelectedIndex != -1)
            {
                string selectedString = lbTableNeighbors.SelectedItem.ToString();
                if (grid.NeighborMapping.TryGetValue(selectedString, out Neighbor selectedNeighbor))
                {
                    txtMidPoint.Text = selectedNeighbor.MidPoint.ToString();
                    txtStart.Text = selectedNeighbor.Start.ToString();
                    txtEnd.Text = selectedNeighbor.End.ToString();

                }
            }
            else
            {
                txtMidPoint.Text = "";
                txtStart.Text = "";
                txtEnd.Text = "";
            }
        }

        private void ckBxOnlyShowThisTableLines_CheckedChanged(object sender, EventArgs e)
        {
            ToggleViewAllBorders();
        }
        private void ToggleViewAllBorders()
        {
            if (cbOnlyShowThisTableLines.Checked)
            {
                if (areaCreationManager.SelectedTable == null)
                {
                    return;
                }
                SectionLineDrawer edgeDrawer = new SectionLineDrawer(3f);
                List<Edge> edges = grid.GetNeighborEdgesForOneTable(areaCreationManager.SelectedTable.TableNumber);
                Bitmap edgesBitmap = edgeDrawer.CreateEdgeBitmap(pnlFloorPlan.Size, edges);
                pnlFloorPlan.BackgroundImage = edgesBitmap;
            }
            else
            {
                SectionLineDrawer edgeDrawer = new SectionLineDrawer(3f);
                //Bitmap edgesBitmap = edgeDrawer.CreateEdgeBitmap(pnlFloorPlan.Size, grid.GetAllTableBoarders());
                List<Edge> edges = grid.GetNeighborEdges();
                Bitmap edgesBitmap = edgeDrawer.CreateEdgeBitmap(pnlFloorPlan.Size, edges);
                pnlFloorPlan.BackgroundImage = edgesBitmap;
            }
        }

        private void btnRemoveNeighbor_Click(object sender, EventArgs e)
        {
            string selectedString = lbTableNeighbors.SelectedItem.ToString();
            if (grid.NeighborMapping.TryGetValue(selectedString, out Neighbor selectedNeighbor))
            {
                string pairKey = grid.overriddenPairs.GetPairKey(selectedNeighbor.table1, selectedNeighbor.table2);
                grid.overriddenPairs.ignorePairs.Add(pairKey);
                ToggleViewAllBorders();

            }
        }

        private void btnAddNewRightLeftNeighbor_Click(object sender, EventArgs e)
        {
            grid.ManuallyCreateRLNeighbor(areaCreationManager.SelectedTable.TableNumber, txtAddNewNeighbor.Text);
            ToggleViewAllBorders();
        }

        private void btnChangeNeighborEdge_Click(object sender, EventArgs e)
        {

        }

        private void btnAddTopBottomNeighbor_Click(object sender, EventArgs e)
        {

        }
    }
}
