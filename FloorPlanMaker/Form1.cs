using FloorplanClassLibrary;

namespace FloorPlanMaker
{
    public partial class Form1 : Form
    {
        //List<DiningArea> areaList = new List<DiningArea>();
        DiningAreaCreationManager areaManager = new DiningAreaCreationManager();
        StaffManager staffManager = new StaffManager();
        private ShiftManager ShiftManager;
        private int LastTableNumberSelected;
        private TableControl currentEmphasizedTable = null;
        private DrawingHandler drawingHandler;
        public Form1()
        {
            InitializeComponent();
            drawingHandler = new DrawingHandler(pnlFloorPlan);

            this.KeyDown += pnlFloorPlan_KeyDown;
            //pnlFloorPlan.KeyPreview = true;
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
            if (rdoSections.Checked)
            {
                if (ShiftManager.SectionSelected != null)
                {
                    ShiftManager.SectionSelected.Tables.Add(clickedTable);

                    // 2. Fill the table control with the FloorplanManager.SectionSelected.Color
                    clickedTableControl.BackColor = ShiftManager.SectionSelected.Color;

                    // Optionally, you can invalidate the control to request a redraw if needed.
                    clickedTableControl.Invalidate();
                    UpdateSectionLabels(ShiftManager.SectionSelected, ShiftManager.SectionSelected.MaxCovers, ShiftManager.SectionSelected.AverageCovers);
                }

            }
            else
            {
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
            lblPanel2Text.Text = areaManager.DiningAreaSelected.Name;
            this.ShiftManager = new ShiftManager(areaManager.DiningAreaSelected);
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
            areaManager.SelectedTable.XCoordinate = UpdateXCoordinateForTableControl(areaManager.SelectedTable);
            areaManager.SelectedTable.YCoordinate = UpdateYCoordinateForTableControl(areaManager.SelectedTable);


            SqliteDataAccess.UpdateTable(areaManager.SelectedTable);

            //areaManager.DiningAreaSelected.Tables = SqliteDataAccess.LoadTables
            pnlFloorPlan.Controls.Clear();
            foreach (Table table in areaManager.DiningAreaSelected.Tables)
            {
                TableControl tableControl = TableControlFactory.CreateTableControl(table);
                tableControl.Moveable = true;
                tableControl.TableClicked += ExistingTable_TableClicked;
                pnlFloorPlan.Controls.Add(tableControl);
            }
        }
        private int UpdateXCoordinateForTableControl(Table table)
        {
            int xCoordinate = table.XCoordinate;
            foreach (Control control in pnlFloorPlan.Controls)
            {
                if (control is TableControl && control.Tag == areaManager.SelectedTable)
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
                if (control is TableControl && control.Tag == areaManager.SelectedTable)
                {
                    yCoordinate = control.Top;

                }
            }
            return yCoordinate;
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
            areaManager.DiningAreaSelected.Tables.Add(table);
            tableControl.TableClicked += ExistingTable_TableClicked;
            // Subscribe to the TableClicked event for the new table as well
            //table.TableClicked += Table_TableClicked;

            pnlFloorPlan.Controls.Add(tableControl);
        }

        private void btnAddServers_Click(object sender, EventArgs e)
        {
            Form form = new frmEditStaff(staffManager);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                ShiftManager.ServersOnShift = staffManager.ServersOnShift;
            }
        }

        private void nudServerCount_ValueChanged(object sender, EventArgs e)
        {
            if (nudServerCount.Value > 0)
            {
                lblServerMaxCovers.Text = (ShiftManager.SelectedDiningArea.GetMaxCovers() / (float)nudServerCount.Value).ToString("F1");
                lblServerAverageCovers.Text = (ShiftManager.SelectedDiningArea.GetAverageCovers() / (float)nudServerCount.Value).ToString("F1");
                List<Section> sections = GetSections();
                CreateSectionRadioButtons(sections);
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
        private List<Section> GetSections()
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
                    TeamWait = false,
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
                    TeamWait = true,
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
        private List<RadioButton> closerButtons = new List<RadioButton>();
        private List<RadioButton> precloserButtons = new List<RadioButton>();

        private void CreateSectionRadioButtons(List<Section> sections)
        {
            // Clear any existing controls from the flow layout panel.
            flowSectionSelect.Controls.Clear();

            foreach (var section in sections)
            {
                // Create a RadioButton for each section.
                RadioButton rb = new RadioButton
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
                rb.FlatAppearance.BorderSize = 0;
                rb.CheckedChanged += Rb_CheckedChanged;

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


                RadioButton rbCloser = new RadioButton
                {
                    Text = "CLS",
                    AutoSize = false,
                    Size = new Size(40, 25),
                    Font = new Font("Segoe UI", 10F),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = section.Color,
                    ForeColor = section.FontColor,
                    Appearance = Appearance.Button,
                    Margin = new Padding(0)
                };
                rbCloser.CheckedChanged += RbCloser_CheckedChanged;

                RadioButton rbPrecloser = new RadioButton
                {
                    Text = "PRE",
                    AutoSize = false,
                    Size = new Size(40, 25),
                    Font = new Font("Segoe UI", 10F),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = section.Color,
                    ForeColor = section.FontColor,
                    Appearance = Appearance.Button,
                    Margin = new Padding(0)
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


                Panel panel = new Panel();

                panel.Controls.Add(rb);
                panel.Controls.Add(lblMaxCovers);
                panel.Controls.Add(lblAverageCovers);
                panel.Controls.Add(rbCloser);
                panel.Controls.Add(rbPrecloser);
                //panel.Controls.Add(rbNeither);

                // Here, you might want to adjust the layout within the panel.
                // For simplicity, I'll just set their locations manually:
                rb.Location = new Point(5, 5); // You can adjust these coordinates as needed.
                lblMaxCovers.Location = new Point(rb.Right + 5, 5);
                lblAverageCovers.Location = new Point(lblMaxCovers.Right + 5, 5);

                rbCloser.Location = new Point(lblAverageCovers.Right + 10, 5);
                rbPrecloser.Location = new Point(rbCloser.Right + 5, 5);
                // Adjust panel size to fit the controls (or set a predefined size).
                panel.Size = new Size(rbPrecloser.Right + 5, Math.Max(rb.Height, lblAverageCovers.Height) + 10);

                sectionLabels[section] = (lblMaxCovers, lblAverageCovers);
                // Add the panel to the flow layout panel.
                flowSectionSelect.Controls.Add(panel);
            }
        }
        private void RbCloser_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                foreach (var otherRb in closerButtons)
                {
                    if (otherRb != rb)
                        otherRb.Checked = false;
                }
            }
        }

        private void RbPrecloser_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                foreach (var otherRb in precloserButtons)
                {
                    if (otherRb != rb)
                        otherRb.Checked = false;
                }
            }
        }
        private Dictionary<Section, (Label MaxCoversLabel, Label AverageCoversLabel)> sectionLabels = new Dictionary<Section, (Label, Label)>();
        public void UpdateSectionLabels(Section section, int newMaxCoversValue, float newAverageCoversValue)
        {
            if (sectionLabels.ContainsKey(section))
            {
                sectionLabels[section].MaxCoversLabel.Text = newMaxCoversValue.ToString();
                sectionLabels[section].AverageCoversLabel.Text = newAverageCoversValue.ToString();
            }
        }
        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                Section selectedSection = rb.Tag as Section;
                if (selectedSection != null)
                {
                    ShiftManager.SectionSelected = selectedSection;
                }
            }
        }

        private void nudNumberOfTeamWaits_ValueChanged(object sender, EventArgs e)
        {
            List<Section> sections = GetSections();
            CreateSectionRadioButtons(sections);
        }


        private void cbLockNodes_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLockNodes.Checked)
            {
                drawingHandler.DrawSectionLinesMode = true;
            }
            else
            {
                drawingHandler.DrawSectionLinesMode = false;
            }

        }

        private void rdoSections_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSections.Checked)
            {
                pnlSections.Visible = true;
                lblPanel2Text.Text = areaManager.DiningAreaSelected.Name;
                this.ShiftManager = new ShiftManager(areaManager.DiningAreaSelected);
                lblDiningAreaMaxCovers.Text = ShiftManager.SelectedDiningArea.GetMaxCovers().ToString();
                lblDiningAreaAverageCovers.Text = ShiftManager.SelectedDiningArea.GetAverageCovers().ToString();
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
    }
}