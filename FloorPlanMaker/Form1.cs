
using FloorplanClassLibrary;
using System.Drawing.Drawing2D;
//using static System.Collections.Specialized.BitVector32;

namespace FloorPlanMaker
{
    public partial class Form1 : Form
    {
        //List<DiningArea> areaList = new List<DiningArea>();
        DiningAreaCreationManager areaCreationManager = new DiningAreaCreationManager();
        EmployeeManager employeeManager = new EmployeeManager();
        private ShiftManager shiftManager;
        private int LastTableNumberSelected;
        private TableControl currentEmphasizedTableControl = null;
        private DrawingHandler drawingHandler;
        List<TableControl> emphasizedTablesList = new List<TableControl>();
        private bool isDragging = false;
        private Point dragStartPoint;
        private Rectangle dragRectangle;
        private List<TableControl> allTableControls = new List<TableControl>();
        private int currentFocusedSectionIndex = 0;


        public Form1()
        {
            InitializeComponent();
            drawingHandler = new DrawingHandler(pnlFloorPlan);
            shiftManager = new ShiftManager();
            shiftManager.ServersNotOnShift = SqliteDataAccess.LoadServers();
            this.KeyDown += pnlFloorPlan_KeyDown;
            pnlFloorPlan.MouseDown += pnlFloorplan_MouseDown;
            pnlFloorPlan.MouseUp += pnlFloorplan_MouseUp;
            pnlFloorPlan.MouseMove += pnlFloorplan_MouseMove;
            pnlFloorPlan.Paint += PnlFloorplan_Paint;

            //pnlFloorPlan.KeyPreview = true;
        }
        protected override bool ProcessTabKey(bool forward)
        {
            if (forward)
            {
                // Move forward in sections
                currentFocusedSectionIndex++;
                if (currentFocusedSectionIndex >= flowSectionSelect.Controls.Count)
                    currentFocusedSectionIndex = 0;
            }
            else
            {
                // Move backward in sections
                currentFocusedSectionIndex--;
                if (currentFocusedSectionIndex < 0)
                    currentFocusedSectionIndex = flowSectionSelect.Controls.Count - 1;
            }

            Panel sectionPanel = (Panel)flowSectionSelect.Controls[currentFocusedSectionIndex];
            CheckBox sectionCheckBox = (CheckBox)sectionPanel.Controls[0];
            sectionCheckBox.Focus();
            sectionCheckBox.Checked = true;
            return true; // Indicate that the key press was handled
        }


        private void PnlFloorplan_Paint(object sender, PaintEventArgs e)
        {
            if (isDragging)
            {
                using (Pen dragPen = new Pen(Color.Black) { DashStyle = DashStyle.Dash }) // example styling
                {
                    e.Graphics.DrawRectangle(dragPen, dragRectangle);
                }
            }
        }

        private void pnlFloorplan_MouseDown(object sender, MouseEventArgs e)
        {
            if (!cbLockNodes.Checked)
            {
                if (rdoSections.Checked)
                {
                    isDragging = true;
                    dragStartPoint = e.Location;
                }
            }
        }
        private void pnlFloorplan_MouseUp(object sender, MouseEventArgs e)
        {
            if (!cbLockNodes.Checked)
            {
                if (isDragging)
                {
                    isDragging = false;

                    // Define the drag rectangle based on the start and end points
                    dragRectangle = new Rectangle(
                        Math.Min(dragStartPoint.X, e.X),
                        Math.Min(dragStartPoint.Y, e.Y),
                        Math.Abs(dragStartPoint.X - e.X),
                        Math.Abs(dragStartPoint.Y - e.Y)
                    );

                    SelectTablesInDragRectangle();
                    pnlFloorPlan.Invalidate();
                }
            }
        }
        private void pnlFloorplan_MouseMove(object sender, MouseEventArgs e)
        {
            if (!cbLockNodes.Checked)
            {
                if (isDragging)
                {
                    dragRectangle = new Rectangle(
                        Math.Min(dragStartPoint.X, e.X),
                        Math.Min(dragStartPoint.Y, e.Y),
                        Math.Abs(dragStartPoint.X - e.X),
                        Math.Abs(dragStartPoint.Y - e.Y)
                    );

                    pnlFloorPlan.Invalidate(); // Redraw the form
                }
            }
        }
        private void SelectTablesInDragRectangle()
        {
            foreach (var tableControl in allTableControls)
            {
                if (tableControl.Parent == pnlFloorPlan)
                {
                    Rectangle tableRect = new Rectangle(tableControl.Location, tableControl.Size);
                    if (dragRectangle.IntersectsWith(tableRect))
                    {
                        tableControl.IsSelected = true;
                        tableControl.BackColor = shiftManager.SectionSelected.Color; // Or any other color indicating selection

                        if (shiftManager.SelectedFloorplan.Sections != null)
                        {
                            var targetSection = shiftManager.SelectedFloorplan.Sections.FirstOrDefault(sec => sec.Equals(shiftManager.SectionSelected));
                            if (targetSection != null)
                            {
                                targetSection.Tables.Add(tableControl.Table);
                                tableControl.Section = targetSection;
                                UpdateSectionLabels(targetSection, targetSection.MaxCovers, targetSection.AverageCovers);
                            }
                            //shiftManager.SectionSelected.Tables.Add(tableControl.Table);
                            //UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
                        }
                    }
                    else
                    {
                        tableControl.IsSelected = false;
                    }
                    tableControl.Invalidate(); // Request a redraw 
                }
            }
        }

        private void pnlFloorPlan_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                drawingHandler.UndoLastPoint();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Floorplan> floorplans = SqliteDataAccess.LoadFloorplanList();
            cboDiningAreas.DataSource = areaCreationManager.DiningAreas;
            cboDiningAreas.DisplayMember = "Name";
            cboDiningAreas.ValueMember = "ID";

            CreateTableControlsToAdd();


        }
        private void CreateTableControlsToAdd()
        {
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
            SaveTableByTableControl(table);
            pnlFloorPlan.Controls.Add(table);
        }

        private void ExistingTable_TableClicked(object sender, TableClickedEventArgs e)
        {
            Table clickedTable = e.ClickedTable;
            TableControl clickedTableControl = sender as TableControl;
            if (e.MouseButton == MouseButtons.Right && clickedTableControl.Section != null)
            {
                Section sectionEdited = (Section)clickedTableControl.Section;
                //ShiftManager.SectionSelected.Tables.Remove(clickedTable); // Remove the table from the section

                clickedTableControl.Section.Tables.Remove(clickedTable);
                clickedTableControl.Section = null;
                clickedTableControl.BackColor = pnlFloorPlan.BackColor;  // Restore the original color
                clickedTableControl.Invalidate();
                UpdateSectionLabels(sectionEdited, sectionEdited.MaxCovers, sectionEdited.AverageCovers);
                //if (ShiftManager.SectionSelected != null && ShiftManager.SectionSelected.Tables.Contains(clickedTable))
                //{
                //    ShiftManager.SectionSelected.Tables.Remove(clickedTable);
                //    clickedTableControl.BackColor = Color.Transparent;  
                //    clickedTableControl.Invalidate();
                //}
                return;
            }
            if (rdoSections.Checked)
            {
                if (shiftManager.SectionSelected != null)
                {
                    shiftManager.SectionSelected.Tables.Add(clickedTable);
                    clickedTableControl.Section = shiftManager.SectionSelected;
                    // 2. Fill the table control with the FloorplanManager.SectionSelected.Color
                    clickedTableControl.BackColor = shiftManager.SectionSelected.Color;

                    // Optionally, you can invalidate the control to request a redraw if needed.
                    clickedTableControl.Invalidate();
                    UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
                }

            }
            else
            {
                if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
                {
                    txtTableNumber.Clear();
                    txtMaxCovers.Clear();
                    txtAverageCovers.Clear();
                    txtHeight.Clear();
                    txtWidth.Clear();
                    foreach (var emphasizedTable in emphasizedTablesList)
                    {
                        if (emphasizedTable != clickedTableControl)
                        {
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

                clickedTableControl.BorderThickness = 3;
                clickedTableControl.Invalidate();

                emphasizedTablesList.Add(clickedTableControl);

                areaCreationManager.SelectedTables.Add(clickedTable);
                txtTableNumber.Text = clickedTable.TableNumber;
                txtMaxCovers.Text = clickedTable.MaxCovers.ToString();
                txtAverageCovers.Text = clickedTable.AverageCovers.ToString();
                txtHeight.Text = clickedTable.Height.ToString();
                txtWidth.Text = clickedTable.Width.ToString();
                string tableNum = "";
                if (areaCreationManager.SelectedTables.Count > 1)
                {
                    foreach (var table in areaCreationManager.SelectedTables)
                    {
                        tableNum += table.TableNumber + " ";
                    }
                    txtTableNumber.Text = tableNum;
                }

                //if (currentEmphasizedTable != null && currentEmphasizedTable != clickedTableControl)
                //{
                //    currentEmphasizedTable.BorderThickness = 1;
                //    currentEmphasizedTable.Invalidate();  // Request a redraw
                //}

            }


        }



        private void btnSaveDiningArea_Click(object sender, EventArgs e)
        {

            DiningArea area = new DiningArea(txtDiningAreaName.Text, rbInside.Checked);

            SqliteDataAccess.SaveDiningArea(area);
        }

        private void btnSaveTables_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.DeleteTablesByDiningArea(areaCreationManager.DiningAreaSelected);
            foreach (Control control in pnlFloorPlan.Controls)
            {
                if (control is TableControl tableControl)
                {
                    SaveTableByTableControl(tableControl);
                }
            }
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
                pnlFloorPlan.Controls.Add(tableControl);
                allTableControls.Add(tableControl);
            }
            lblPanel2Text.Text = areaCreationManager.DiningAreaSelected.Name;
            this.shiftManager.SelectedDiningArea = areaCreationManager.DiningAreaSelected;
            if (shiftManager.Floorplans.Count > 0)
            {
                UpdateFloorplan();
            }

            RefreshTemplateList(shiftManager.SelectedDiningArea);



        }
        private void RefreshTemplateList(DiningArea dining)
        {
            cboFloorplanTemplates.Items.Clear();
            cboFloorplanTemplates.DisplayMember = "Name";
            List<FloorplanTemplate> templates = SqliteDataAccess.LoadTemplatesByDiningArea(dining);
            foreach (FloorplanTemplate template in templates)
            {
                cboFloorplanTemplates.Items.Add(template);
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
        private void btnLessHeight_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtHeight.Text, out int height) && height >= 35 && height <= 400)
            {
                height = height - 10;
                txtHeight.Text = height.ToString();
                RefreshTableControl();

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

        private void btnLessWidth_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtWidth.Text, out int width) && width >= 35 && width <= 400)
            {
                width = width - 10;
                txtWidth.Text = width.ToString();
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

            // Assuming these other methods and properties do not require validation
            table.DiningArea = areaCreationManager.DiningAreaSelected;
            //table.XCoordinate = UpdateXCoordinateForTableControl(table);
            table.XCoordinate = tableControl.Left;
            table.YCoordinate = UpdateYCoordinateForTableControl(table);
            table.YCoordinate = tableControl.Top;
            tableControl.Table = table;
            TableControlFactory.RedrawTableControl(tableControl, pnlFloorPlan);
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

        private void btnAddServers_Click(object sender, EventArgs e)
        {
            Form form = new frmEditStaff(employeeManager, shiftManager);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {

                UpdateFloorplan();

            }
        }
        private void UpdateFloorplan()
        {
            shiftManager.ServersOnShift = employeeManager.ServersOnShift;
            shiftManager.SelectedFloorplan = shiftManager.Floorplans.FirstOrDefault(fp => fp.DiningArea.ID == areaCreationManager.DiningAreaSelected.ID);
            //flowServersInFloorplan.Controls.Clear();
            //int pointX = 35;
            //int PointY = 5;
            //foreach (Server s in shiftManager.SelectedFloorplan.Servers)
            //{
            //    ServerControl sc = new ServerControl(s, 150, 30);
            //    sc.Location = new Point(pointX, PointY);
            //    PointY += (5 + sc.Height);
            //    //CheckBox cb = CreateServerButton(s);
            //    pnlSections.Controls.Add(sc);
            //}
            nudServerCount.Value = shiftManager.SelectedFloorplan.Servers.Count;
        }

        private CheckBox CreateServerButton(Server server)
        {

            CheckBox cb = new CheckBox
            {
                Appearance = Appearance.Button,
                Width = 150,
                Height = 30,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(5),
                Text = server.Name,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightBlue,
                Tag = server
            };

            //b.Click += ServerButton_Click;
            return cb;
        }

        private void nudServerCount_ValueChanged(object sender, EventArgs e)
        {
            if (nudServerCount.Value > 0)
            {
                lblServerMaxCovers.Text = (shiftManager.SelectedDiningArea.GetMaxCovers() / (float)nudServerCount.Value).ToString("F1");
                lblServerAverageCovers.Text = (shiftManager.SelectedDiningArea.GetAverageCovers() / (float)nudServerCount.Value).ToString("F1");
                //shiftManager.Sections = GetNumberOfSections();
                //shiftManager.SelectedFloorplan.Sections = GetNumberOfSections();
                shiftManager.CreateFloorplanForDiningArea(shiftManager.SelectedDiningArea, DateTime.Now, false, 4, 4);
                shiftManager.SelectedFloorplan = shiftManager.Floorplans.FirstOrDefault(fp => fp.DiningArea == (DiningArea)cboDiningAreas.SelectedItem);
                shiftManager.ServersOnShift = employeeManager.AllServers;
                for (int i = 0; i < 4; i++)
                {
                    shiftManager.SelectedFloorplan.Servers.Add(shiftManager.ServersOnShift[i]);
                }
                CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
            }

        }

        private void cbTeamWait_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTeamWait.Checked == true)
            {
                nudNumberOfTeamWaits.Value = 1;
                nudNumberOfTeamWaits.Visible = true;
                lblTeamWaitLabel.Visible = true;
            }
            else
            {
                nudNumberOfTeamWaits.Value = 0;
                nudNumberOfTeamWaits.Visible = false;
                lblTeamWaitLabel.Visible = false;
            }
        }
        private List<Section> GetNumberOfSections()
        {
            int servers = (int)nudServerCount.Value;
            int teamWaitSections = (int)nudNumberOfTeamWaits.Value;
            int soloSections = servers - (teamWaitSections * 2);
            List<Section> sections = new List<Section>();
            int SectionNumber = 1;
            // Create solo sections.
            for (int i = 1; i <= soloSections; i++)
            {
                sections.Add(new Section
                {

                    Name = $"Section {i}",
                    IsTeamWait = false,
                    Number = SectionNumber
                });
                SectionNumber++;
            }

            // Create team wait sections.
            for (int i = 1; i <= teamWaitSections; i++)
            {
                sections.Add(new Section
                {

                    ID = servers + i,  // To ensure unique IDs.
                    Name = $"Team Wait {i}",
                    IsTeamWait = true,
                    Number = SectionNumber
                });
                SectionNumber++;
            }

            return sections;
        }
        //private void CreateSectionRadioButtons(List<Section> sections)
        //{
        //    // Clear any existing radio buttons from the flow layout panel.
        //    flowSectionSelect.Controls.Clear();

        //    foreach (var section in sections)
        //    {
        //        RadioButton rb = new RadioButton
        //        {
        //            Appearance = Appearance.Button,
        //            FlatStyle = FlatStyle.Flat,
        //            BackColor = section.Color,
        //            ForeColor = section.FontColor,

        //            //Text = section.TeamWait ? $"Team Wait {section.ID}" : $"Section {section.ID}",
        //            Text = section.Name,
        //            Tag = section  // Store the section object in the Tag property for easy access in the event handler.
        //        };
        //        rb.FlatAppearance.BorderSize = 0;
        //        // Add the event handler for the CheckedChanged event.
        //        rb.CheckedChanged += Rb_CheckedChanged;

        //        flowSectionSelect.Controls.Add(rb);
        //    }
        //}
        private List<CheckBox> closerButtons = new List<CheckBox>();
        private List<CheckBox> precloserButtons = new List<CheckBox>();
        private CheckBox selectedSectionButton;
        private CheckBox selectedCloserButton;
        private CheckBox selectedPreCloserButton;
        private void UpdateFloorplanSection()
        {

        }
        private void CreateSectionRadioButtons(List<Section> sections)
        {
            // Clear any existing controls from the flow layout panel.
            flowSectionSelect.Controls.Clear();

            foreach (var section in sections)
            {
                // Create a RadioButton for each section.
                CheckBox rbSection = new CheckBox
                {
                    Appearance = Appearance.Button,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = section.Color,
                    ForeColor = section.FontColor,
                    AutoSize = false,
                    Size = new Size(95, 25),
                    Text = section.Name,
                    Tag = section  // Store the section object in the Tag property for easy access in the event handler.
                };
                rbSection.FlatAppearance.BorderSize = 0;
                rbSection.CheckedChanged += Rb_CheckedChanged;

                // Create two labels for each section.
                Label lblMaxCovers = new Label
                {
                    Text = section.MaxCovers.ToString(),
                    AutoSize = false,
                    Size = new Size(35, 25),
                    Font = new Font("Segoe UI", 12F),
                    TextAlign = ContentAlignment.TopCenter,
                    Margin = new Padding(0, 3, 0, 3)

                };

                Label lblAverageCovers = new Label
                {
                    Text = section.AverageCovers.ToString(),
                    AutoSize = false,
                    Size = new Size(35, 25),
                    Font = new Font("Segoe UI", 12F),
                    TextAlign = ContentAlignment.TopCenter,
                    Margin = new Padding(0, 3, 0, 3)

                };


                CheckBox rbCloser = new CheckBox
                {
                    Text = "CLS",
                    AutoSize = false,
                    Size = new Size(40, 25),
                    Font = new Font("Segoe UI", 10F),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = section.Color,
                    ForeColor = section.FontColor,
                    Appearance = Appearance.Button,
                    Margin = new Padding(0),
                    Tag = section
                };
                rbCloser.CheckedChanged += RbCloser_CheckedChanged;

                CheckBox rbPrecloser = new CheckBox
                {
                    Text = "PRE",
                    AutoSize = false,
                    Size = new Size(40, 25),
                    Font = new Font("Segoe UI", 10F),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = section.Color,
                    ForeColor = section.FontColor,
                    Appearance = Appearance.Button,
                    Margin = new Padding(0),
                    Tag = section
                };
                rbPrecloser.CheckedChanged += RbPrecloser_CheckedChanged;

                //RadioButton rbNeither = new RadioButton
                //{
                //    Text = "Neither",
                //    Checked = true, // default option
                //    AutoSize = true
                //};

                closerButtons.Add(rbCloser);
                precloserButtons.Add(rbPrecloser);

                // Adjust locations and add to the panel.

                //rbNeither.Location = new Point(rbPrecloser.Right + 5, 5);


                Panel sectionPanel = new Panel();
                sectionPanel.Tag = section;
                sectionPanel.AllowDrop = true;
                sectionPanel.BackColor = Color.SlateGray;

                // Attach drag-drop event handlers



                sectionPanel.Controls.Add(rbSection);
                sectionPanel.Controls.Add(lblMaxCovers);
                sectionPanel.Controls.Add(lblAverageCovers);
                sectionPanel.Controls.Add(rbCloser);
                sectionPanel.Controls.Add(rbPrecloser);
                //panel.Controls.Add(rbNeither);

                // Here, you might want to adjust the layout within the panel.
                // For simplicity, I'll just set their locations manually:
                rbSection.Location = new Point(5, 5); // You can adjust these coordinates as needed.
                lblMaxCovers.Location = new Point(rbSection.Right + 5, 5);
                lblAverageCovers.Location = new Point(lblMaxCovers.Right + 5, 5);

                rbCloser.Location = new Point(lblAverageCovers.Right + 10, 5);
                rbPrecloser.Location = new Point(rbCloser.Right + 5, 5);
                // Adjust panel size to fit the controls (or set a predefined size).
                sectionPanel.Size = new Size(rbPrecloser.Right + 5, Math.Max(rbSection.Height, lblAverageCovers.Height) + 10);

                sectionLabels[section] = (lblMaxCovers, lblAverageCovers);
                // Add the panel to the flow layout panel.
                flowSectionSelect.Controls.Add(sectionPanel);
            }
            if (flowSectionSelect.Controls.Count > 0)
            {
                Panel firstPanel = (Panel)flowSectionSelect.Controls[0];
                if (firstPanel.Controls.Count > 0)
                {
                    CheckBox firstSectionCheckBox = (CheckBox)firstPanel.Controls[0];
                    firstSectionCheckBox.Checked = true;
                }
            }

        }




        private void AddSectionLabels(List<Section> sections)
        {
            List<Server> servers = new List<Server>();
            for (int i = 0; i < 4 && i < employeeManager.AllServers.Count; i++)
            {
                servers.Add(employeeManager.AllServers[i]);
            }

            foreach (Section section in sections)
            {
                SectionControl sectionControl = new SectionControl(section);
                sectionControl.Location = FindMidpointOfSectionControls(section);
                sectionControl.Servers = servers;
                pnlFloorPlan.Controls.Add(sectionControl);
                sectionControl.BringToFront();

            }
        }
        private Point FindMidpointOfSectionControls(Section targetSection)
        {
            // 1. Collect all the TableControl controls with a Section
            List<TableControl> tableControlsWithSection = new List<TableControl>();
            foreach (Control ctrl in pnlFloorPlan.Controls)
            {
                if (ctrl is TableControl tableControl && tableControl.Section != null)
                {
                    tableControlsWithSection.Add(tableControl);
                }
            }

            // 2. Group by Section and find the controls with the targetSection
            var targetControls = tableControlsWithSection
                .Where(tc => tc.Section.Equals(targetSection))
                .ToList();

            // 3. Compute the midpoint for the target controls
            return TableControl.FindMiddlePoint(targetControls);
        }


        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox rb = sender as CheckBox;
            if (rb != null && rb.Checked)
            {
                Section selectedSection = rb.Tag as Section;
                if (selectedSection != null)
                {
                    shiftManager.SectionSelected = selectedSection;
                }
                if (rb != selectedSectionButton)
                {
                    if (selectedSectionButton != null) selectedSectionButton.Checked = false;
                    selectedSectionButton = rb;
                }
            }
        }
        private void RbCloser_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox rb = sender as CheckBox;
            if (rb.Checked && rb != selectedCloserButton)
            {
                if (selectedCloserButton != null) selectedCloserButton.Checked = false;
                selectedCloserButton = rb;
            }
            if (rb.Checked)
            {
                Section section = rb.Tag as Section;
                section.IsCloser = true;
            }
        }

        private void RbPrecloser_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox rb = sender as CheckBox;
            if (rb.Checked && rb != selectedPreCloserButton)
            {
                if (selectedPreCloserButton != null) selectedPreCloserButton.Checked = false;
                selectedPreCloserButton = rb;
            }
        }
        private Dictionary<Section, (Label MaxCoversLabel, Label AverageCoversLabel)> sectionLabels = new Dictionary<Section, (Label, Label)>();
        public void UpdateSectionLabels(Section section, int newMaxCoversValue, float newAverageCoversValue)
        {
            float maxPerServer = (shiftManager.SelectedDiningArea.GetMaxCovers() / (float)nudServerCount.Value);
            float avgPerServer = (shiftManager.SelectedDiningArea.GetAverageCovers() / (float)nudServerCount.Value);
            float maxDifference = newMaxCoversValue - maxPerServer;
            float avgDifference = newAverageCoversValue - avgPerServer;
            if (sectionLabels.ContainsKey(section))
            {
                sectionLabels[section].MaxCoversLabel.Text = maxDifference.ToString();

                sectionLabels[section].AverageCoversLabel.Text = avgDifference.ToString();

            }
        }


        private void nudNumberOfTeamWaits_ValueChanged(object sender, EventArgs e)
        {
            shiftManager.Sections = GetNumberOfSections();
            CreateSectionRadioButtons(shiftManager.Sections);
        }


        private void cbLockNodes_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLockNodes.Checked)
            {
                drawingHandler.DrawSectionLinesMode = true;
                isDragging = false;
            }
            else
            {
                drawingHandler.DrawSectionLinesMode = false;
                isDragging = true;
            }

        }

        private void rdoSections_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSections.Checked)
            {
                pnlSections.Visible = true;
                lblPanel2Text.Text = areaCreationManager.DiningAreaSelected.Name;
                this.shiftManager = new ShiftManager(areaCreationManager.DiningAreaSelected);
                lblDiningAreaMaxCovers.Text = shiftManager.SelectedDiningArea.GetMaxCovers().ToString();
                lblDiningAreaAverageCovers.Text = shiftManager.SelectedDiningArea.GetAverageCovers().ToString();
                foreach (Control control in pnlFloorPlan.Controls)
                {
                    if (control is TableControl tableControl)
                    {
                        tableControl.Moveable = false;

                    }
                }
                txtDiningAreaName.Visible = false;
                //cboDiningAreas.Visible = false;
                btnCreateNewDiningArea.Visible = false;
                btnSaveDiningArea.Visible = false;
                btnSaveTables.Visible = false;
                rbInside.Visible = false;
                rbOutside.Visible = false;
            }
        }

        private void rdoDiningAreas_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDiningAreas.Checked)
            {
                pnlSections.Visible = false;
                lblPanel2Text.Text = "Add Tables";
                txtDiningAreaName.Visible = true;
                //cboDiningAreas.Visible = true;
                btnCreateNewDiningArea.Visible = true;
                btnSaveDiningArea.Visible = true;
                btnSaveTables.Visible = true;
                rbInside.Visible = true;
                rbOutside.Visible = true;
                foreach (Control control in pnlFloorPlan.Controls)
                {
                    if (control is TableControl tableControl)
                    {
                        tableControl.Moveable = true;
                    }
                }
            }
        }

        private void btnCreateNewDiningArea_Click(object sender, EventArgs e)
        {

        }

        private void btnAddSectionLabels_Click(object sender, EventArgs e)
        {


            foreach (Section section in shiftManager.SelectedFloorplan.Sections)
            {
                SectionControl sectionControl = new SectionControl(section);
                sectionControl.Location = FindMidpointOfSectionControls(section);
                sectionControl.Servers = shiftManager.SelectedFloorplan.Servers;
                pnlFloorPlan.Controls.Add(sectionControl);
                sectionControl.BringToFront();

            }
            //AddSectionLabels(shiftManager.Sections);
        }

        private void btnSaveFloorplanTemplate_Click(object sender, EventArgs e)
        {
            var drawnLines = drawingHandler.GetDrawnLines();
            FloorplanTemplate template = new FloorplanTemplate(shiftManager.SelectedDiningArea, txtTemplateName.Text,
                (int)nudServerCount.Value, shiftManager.Sections, drawnLines);
            // Assuming you have a FloorplanTemplate object already initialized as template
            template.SectionLines.Clear();
            template.SectionLines.AddRange(drawnLines);

            // Optional: If you want to clear the drawn lines on the panel after adding them to the template
            drawingHandler.ClearLines();
            SqliteDataAccess.SaveFloorplanTemplate(template);
            txtTemplateName.Clear();
            //RefreshTemplateList();

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            nudServerCount.Value = 4;

            UpdateFloorplan();
        }

        private void cboFloorplanTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {


            foreach (Table table in areaCreationManager.DiningAreaSelected.Tables)
            {
                table.DiningArea = areaCreationManager.DiningAreaSelected;
                TableControl tableControl = TableControlFactory.CreateMiniTableControl(table, (float).5, 0);
                //tableControl.TableClicked += Table_TableClicked;  // Uncomment if you want to attach event handler

                //pnlTemplateDemo.Controls.Add(tableControl);
            }




            FloorplanTemplate template = cboFloorplanTemplates.SelectedItem as FloorplanTemplate;
            shiftManager.SetSectionsToTemplate(template);

            shiftManager.AssignSectionNumbers();



        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            shiftManager.SelectedFloorplan.Date = dtpFloorplan.Value;
            SqliteDataAccess.SaveFloorplanAndSections(shiftManager.SelectedFloorplan);
            //FloorplanPrinter printer = new FloorplanPrinter(pnlFloorPlan, drawingHandler.GetSectionLines());

            ////printer.ShowPrintPreview();  // To show print preview
            //printer.Print();  // To print

        }
    }
}