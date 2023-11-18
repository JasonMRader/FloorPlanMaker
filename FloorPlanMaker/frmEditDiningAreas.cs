﻿using FloorplanClassLibrary;
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
        public frmEditDiningAreas()
        {
            InitializeComponent();
        }
        private void SetColors()
        {

            UITheme.FormatMainButton(btnQuickEdit);
            UITheme.FormatMainButton(cbViewMode);
            UITheme.FormatMainButton(btnCopyTable);
            UITheme.FormatMainButton(btnSaveTable);
            UITheme.FormatMainButton(cbLockTables);
            UITheme.FormatMainButton(btnMoreHeight);
            UITheme.FormatMainButton(btnMoreWidth);
            UITheme.FormatMainButton(btnLessHeight);
            UITheme.FormatMainButton(btnLessWidth);
            UITheme.FormatMainButton(btnDeleteTable);
            UITheme.FormatMainButton(btnCreateNewDiningArea);
            UITheme.FormatMainButton(btnSaveDiningArea);
            UITheme.FormatMainButton(btnSaveDiningArea);

            // AppColors.FormatSecondColor(this);
            UITheme.FormatSecondColor(panel4);
            UITheme.FormatSecondColor(panel3);

            UITheme.FormatAccentColor(this);

            UITheme.FormatAccentColor(panel3);

            UITheme.FormatCanvasColor(pnlFloorPlan);

            UITheme.FormatSecondColor(panel5);
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


        }
        private void ExistingTable_TableClicked(object sender, TableClickedEventArgs e)
        {

            TableControl clickedTableControl = sender as TableControl;
            Table clickedTable = clickedTableControl.Table;

            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                txtTableNumber.Clear();
                txtMaxCovers.Clear();
                txtAverageCovers.Clear();
                txtHeight.Clear();
                txtWidth.Clear();
                txtXco.Clear();
                txtYco.Clear();
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
            txtHeight.Text = clickedTable.Height.ToString();
            txtWidth.Text = clickedTable.Width.ToString();
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
                positionEditor = TableEditorFactory.CreateEditor(clickedTableControl, pnlFloorPlan);
                clickedTableControl.AddTxtTableNumber();
                clickedTableControl.txtTableNumber.Focus();
                //editor.Location = new Point(clickedTableControl.Right + 20, clickedTableControl.Top);

                pnlFloorPlan.Controls.Add(positionEditor);
                positionEditor.BringToFront();
            }
            
            //if (currentEmphasizedTable != null && currentEmphasizedTable != clickedTableControl)
            //{
            //    currentEmphasizedTable.BorderThickness = 1;
            //    currentEmphasizedTable.Invalidate();  // Request a redraw
            //}




        }

        private void btnAddSquare_Click(object sender, EventArgs e)
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
            SaveTableByTableControl(table);
            pnlFloorPlan.Controls.Add(table);
            areaCreationManager.SelectedTable = table.Table;
        }

        private void btnAddDiamond_Click(object sender, EventArgs e)
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
            SaveTableByTableControl(table);
            pnlFloorPlan.Controls.Add(table);
        }

        private void btnAddCircle_Click(object sender, EventArgs e)
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
            SaveTableByTableControl(table);
            pnlFloorPlan.Controls.Add(table);
        }
        private void SaveTableByTableControl(TableControl tableControl)
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

        private void btnMoreHeight_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtHeight.Text, out int height) && height >= 25 && height <= 390)
            {
                height = height + 10;
                txtHeight.Text = height.ToString();
                RefreshTableControl();

            }
        }

        private void btnLessHeight_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtHeight.Text, out int height) && height >= 35 && height <= 400)
            {
                height = height - 10;
                txtHeight.Text = height.ToString();
                RefreshTableControl();

            }
        }

        private void btnMoreWidth_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtWidth.Text, out int width) && width >= 25 && width <= 390)
            {
                width = width + 10;
                txtWidth.Text = width.ToString();
                RefreshTableControl();

            }
        }

        private void btnLessWidth_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtWidth.Text, out int width) && width >= 35 && width <= 400)
            {
                width = width - 10;
                txtWidth.Text = width.ToString();
                RefreshTableControl();

            }
        }
        private void RefreshTableControl(object sender, EventArgs e)
        {
            if (areaCreationManager.SelectedTables.Count > 1)
            {
                foreach (TableControl tableControl in emphasizedTablesList)
                {
                    SetTableProperties(tableControl.Table, tableControl);
                }

            }
            if (areaCreationManager.SelectedTables.Count == 1)
            {
                SetTableProperties(areaCreationManager.SelectedTable, currentEmphasizedTableControl);
                //SqliteDataAccess.UpdateTable(areaManager.SelectedTable);
            }



        }
        private void RefreshTableControl()
        {
            if (areaCreationManager.SelectedTables.Count > 1)
            {
                foreach (TableControl tableControl in emphasizedTablesList)
                {
                    SetTableProperties(tableControl.Table, tableControl);
                }

            }
            if (areaCreationManager.SelectedTables.Count == 1)
            {
                SetTableProperties(areaCreationManager.SelectedTable, currentEmphasizedTableControl);
                //SqliteDataAccess.UpdateTable(areaManager.SelectedTable);
            }



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

            if (int.TryParse(txtHeight.Text, out int height) && height >= 25 && height <= 400)
            {
                table.Height = height;
                tableControl.Height = height;  // Update control height
            }

            if (int.TryParse(txtWidth.Text, out int width) && width >= 25 && width <= 400)
            {
                table.Width = width;
                tableControl.Width = width;  // Update control width
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
            
            foreach (TableDataEditorControl editor in tableDataEditors)
            {                
                pnlFloorPlan.Controls.Remove(editor);                
            }
            tableDataEditors.Clear();
            if (rdoEditData.Checked)
            {
                pnlFloorPlan.Controls.Remove(positionEditor);
                foreach (TableControl tableControl in allTableControls)
                {
                    TableDataEditorControl dataEditor = new TableDataEditorControl(tableControl);
                    tableDataEditors.Add(dataEditor);
                    pnlFloorPlan.Controls.Add(dataEditor);
                    dataEditor.BringToFront();

                }
            }
        }

        private void rdoEditPositions_CheckedChanged(object sender, EventArgs e)
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
    }
}
