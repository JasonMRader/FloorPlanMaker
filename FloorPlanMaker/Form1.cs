
using FloorplanClassLibrary;
using FloorPlanMakerUI;
//using NetTopologySuite.Geometries;
using NetTopologySuite.Triangulate;
using System.Diagnostics.Metrics;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
//using static System.Collections.Specialized.BitVector32;
//using static System.Collections.Specialized.BitVector32;

namespace FloorPlanMaker
{
    public partial class Form1 : Form
    {
        //List<DiningArea> areaList = new List<DiningArea>();
        private DiningAreaCreationManager areaCreationManager = new DiningAreaCreationManager();
        EmployeeManager employeeManager = new EmployeeManager();
        private ShiftManager shiftManager;
        private int LastTableNumberSelected;
        private TableControl currentEmphasizedTableControl = null;
        private DrawingHandler drawingHandler;
        List<TableControl> emphasizedTablesList = new List<TableControl>();
        private bool isDragging = false;
        private bool isDraggingForm = false;
        private System.Drawing.Point lastLocation;
        private System.Drawing.Point dragStartPoint;
        private Rectangle dragRectangle;
        private List<TableControl> allTableControls = new List<TableControl>();
        private int currentFocusedSectionIndex = 0;
        private SectionLineManager sectionLineManager;
        private frmEditDiningAreas _frmEditDiningAreas;
        private frmEditStaff _frmEditStaff;
        private SectionControlsManager sectionControlsManager { get; set; }
        private void SetColors()
        {
            btnCloseApp.BackColor = Color.Red;
            AppColors.FormatCTAButton(btnAddSectionLabels);            
            AppColors.FormatCTAButton(btnPrint);
            AppColors.FormatCTAButton(rdoDiningAreas);
            AppColors.FormatCTAButton(rdoSections);
            AppColors.FormatCTAButton(rdoShifts);

            AppColors.FormatMainButton(btnChooseTemplate);
            AppColors.FormatMainButton(btnAddSection);
            AppColors.FormatMainButton(btnGenerateSectionLines);
            AppColors.FormatMainButton(btnRemoveSection);
            AppColors.FormatMainButton(btnSaveFloorplanTemplate);
            AppColors.FormatMainButton(cbTableDisplayMode);

            AppColors.FormatSecondColor(this);
            AppColors.FormatSecondColor(panel3);
            AppColors.FormatSecondColor(pnlServers);
            AppColors.FormatSecondColor(pnlAddTables);

            AppColors.FormatCanvasColor(pnlFloorPlan);
            AppColors.FormatCanvasColor(flowSectionSelect);
            AppColors.FormatCanvasColor(flowServersInFloorplan);
           
        }
        public void UpdateForm1ShiftManager(ShiftManager shiftManagerToAdd)
        {
            dtpFloorplan.Value = new DateTime(shiftManagerToAdd.DateOnly.Year, shiftManagerToAdd.DateOnly.Month, shiftManagerToAdd.DateOnly.Day);

            cbIsAM.Checked = shiftManagerToAdd.IsAM;
            foreach (Floorplan fp in shiftManagerToAdd.Floorplans)
            {
                this.shiftManager.AddFloorplanAndServers(fp);
            }


            SetViewedFloorplan();
            rdoSections.Checked = true;

        }

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
            this.sectionLineManager = new SectionLineManager(allTableControls);

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
            if (!rdoDiningAreas.Checked)
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
            if (!rdoDiningAreas.Checked)
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
            if (!rdoDiningAreas.Checked)
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
            SetColors();

            List<Floorplan> floorplans = SqliteDataAccess.LoadFloorplanList();
            cboDiningAreas.DataSource = areaCreationManager.DiningAreas;
            cboDiningAreas.DisplayMember = "Name";
            cboDiningAreas.ValueMember = "ID";




        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isDraggingForm = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingForm)
            {
                this.Location = new System.Drawing.Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDraggingForm = false;
        }


        private void ExistingTable_TableClicked(object sender, TableClickedEventArgs e)
        {

            TableControl clickedTableControl = sender as TableControl;
            Table clickedTable = clickedTableControl.Table;
            Section sectionEdited = (Section)clickedTableControl.Section;
            if (e.MouseButton == MouseButtons.Right && clickedTableControl.Section != null)
            {

                //ShiftManager.SectionSelected.Tables.Remove(clickedTable); // Remove the table from the section

                sectionEdited.Tables.RemoveAll(t => t.ID == clickedTable.ID);

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
                    if (sectionEdited != null)
                    {
                        sectionEdited.Tables.RemoveAll(t => t.ID == clickedTable.ID);
                        UpdateSectionLabels(sectionEdited, sectionEdited.MaxCovers, sectionEdited.AverageCovers);
                    }
                    shiftManager.SectionSelected.Tables.Add(clickedTable);
                    clickedTableControl.Section = shiftManager.SectionSelected;
                    // 2. Fill the table control with the FloorplanManager.SectionSelected.Color
                    clickedTableControl.BackColor = shiftManager.SectionSelected.Color;

                    // Optionally, you can invalidate the control to request a redraw if needed.
                    clickedTableControl.Invalidate();
                    UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
                }

            }

        }

        private void cboDiningAreas_SelectedIndexChanged(object sender, EventArgs e)
        {

            allTableControls.Clear();
            areaCreationManager.DiningAreaSelected = (DiningArea?)cboDiningAreas.SelectedItem;
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
            SetViewedFloorplan();
            lblDiningRoomName.Text = areaCreationManager.DiningAreaSelected.Name;
            RefreshTemplateList(shiftManager.SelectedDiningArea);
            lblDiningAreaAverageCovers.Text = shiftManager.SelectedDiningArea.GetAverageCovers().ToString("C0");
            lblDiningAreaMaxCovers.Text = shiftManager.SelectedDiningArea.GetMaxCovers().ToString();
            this.sectionLineManager = new SectionLineManager(allTableControls);

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

        private void btnAddServers_Click(object sender, EventArgs e)
        {
            Form form = new frmEditStaff(employeeManager, shiftManager, this);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                dtpFloorplan.Value = new DateTime(shiftManager.DateOnly.Year, shiftManager.DateOnly.Month, shiftManager.DateOnly.Day);

                cbIsAM.Checked = shiftManager.IsAM;

                SetViewedFloorplan();


            }
        }
        private void UpdateFloorplan()
        {

            shiftManager.SelectedFloorplan = shiftManager.Floorplans.FirstOrDefault(fp => fp.DiningArea.ID == areaCreationManager.DiningAreaSelected.ID);
            UpdateServerControlsForFloorplan();
            if (shiftManager.SelectedFloorplan != null)
            {


                CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);

            }

        }
        private void UpdateServerControlsForFloorplan()
        {

            flowServersInFloorplan.Controls.Clear();
            if (shiftManager.SelectedFloorplan == null) { return; }
            if (shiftManager.SelectedFloorplan.Servers.Count > 0)
            {
                foreach (Server server in shiftManager.SelectedFloorplan.Servers)
                {
                    server.Shifts = SqliteDataAccess.GetShiftsForServer(server);
                    ServerControl serverControl = new ServerControl(server, 215, 20);

                    foreach (ShiftControl shiftControl in serverControl.ShiftControls)
                    {

                        shiftControl.ShowClose();
                        shiftControl.ShowTeam();
                        shiftControl.HideOutside();
                    }

                    flowServersInFloorplan.Controls.Add(serverControl);
                }
            }
        }


        private void nudServerCount_ValueChanged(object sender, EventArgs e)
        {
            //if (nudServerCount.Value > 0)
            //{
            //    lblServerMaxCovers.Text = (shiftManager.SelectedDiningArea.GetMaxCovers() / (float)nudServerCount.Value).ToString("F1");
            //    lblServerAverageCovers.Text = (shiftManager.SelectedDiningArea.GetAverageCovers() / (float)nudServerCount.Value).ToString("F1");

            //    shiftManager.SelectedFloorplan = shiftManager.Floorplans.FirstOrDefault(fp => fp.DiningArea.ID == shiftManager.SelectedDiningArea.ID);

            //    if (shiftManager.SelectedFloorplan != null)
            //    {
            //        //shiftManager.SelectedFloorplan.Sections =
            //        GetNumberOfSections();

            //    }
            //    else
            //    {
            //        shiftManager.SelectedFloorplan = new Floorplan();
            //        GetNumberOfSections();
            //        shiftManager.SelectedFloorplan.DiningArea = shiftManager.SelectedDiningArea;

            //    }
            //    CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
            //    //else
            //    //{
            //    //    List<Section> sections = GetNumberOfSections();
            //    //    shiftManager.SelectedTemplate = new FloorplanTemplate(shiftManager.SelectedDiningArea, (int)nudServerCount.Value,"", sections);
            //    //}

            //}

        }


        //private List<Section> GetNumberOfSections()
        //{
        //    int servers = (int)nudServerCount.Value;
        //    int teamWaitSections = (int)nudNumberOfTeamWaits.Value;
        //    int soloSections = servers - (teamWaitSections * 2);
        //    List<Section> sections = new List<Section>();
        //    int SectionNumber = 1;
        //    // Create solo sections.
        //    for (int i = 1; i <= soloSections; i++)
        //    {
        //        shiftManager.SelectedFloorplan.AddSection(new Section
        //        {

        //            Name = $"Section {i}",
        //            IsTeamWait = false,
        //            Number = SectionNumber
        //        });
        //        SectionNumber++;
        //    }

        //    // Create team wait sections.
        //    for (int i = 1; i <= teamWaitSections; i++)
        //    {
        //        shiftManager.SelectedFloorplan.AddSection(new Section
        //        {

        //            ID = servers + i,  // To ensure unique IDs.
        //            Name = $"Team Wait {i}",
        //            IsTeamWait = true,
        //            Number = SectionNumber
        //        });
        //        SectionNumber++;
        //    }

        //    return sections;
        //}


        private List<CheckBox> TeamWaitButtons = new List<CheckBox>();
        private CheckBox selectedSectionButton;
        private CheckBox selectedCloserButton;
        private CheckBox selectedPreCloserButton;

        private void CreateSectionRadioButtons(List<Section> sections)
        {
            // Clear any existing controls from the flow layout panel.
            flowSectionSelect.Controls.Clear();
            if (sections.Count == 0)
            {

                NoServersToDisplay();
            }
            if (sections == null)
            {

                return;
            }
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
                    Size = new Size(55, 25),
                    Text = section.Name,
                    Tag = section  // Store the section object in the Tag property for easy access in the event handler.
                };
                rbSection.FlatAppearance.BorderSize = 0;
                rbSection.CheckedChanged += Rb_CheckedChanged;

                // Create two labels for each section.
                Label lblMaxCovers = new Label
                {
                    Text = (section.MaxCovers - shiftManager.SelectedFloorplan.MaxCoversPerServer).ToString(),
                    AutoSize = false,
                    Size = new Size(35, 25),
                    Font = new Font("Segoe UI", 12F),
                    TextAlign = ContentAlignment.TopCenter,
                    Margin = new Padding(0, 3, 0, 3)

                };

                Label lblAverageCovers = new Label
                {
                    Text = Section.FormatAsCurrencyWithoutParentheses(section.AverageCovers - shiftManager.SelectedFloorplan.AvgCoversPerServer), //(section.AverageCovers - shiftManager.SelectedFloorplan.AvgCoversPerServer).ToString("C0;\\-C0", CultureInfo.CurrentCulture),
                    AutoSize = true,
                    Size = new Size(65, 25),
                    Font = new Font("Segoe UI", 10F),
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

                // closerButtons.Add(rbCloser);
                //precloserButtons.Add(rbPrecloser);

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
                //sectionPanel.Controls.Add(rbCloser);
                //sectionPanel.Controls.Add(rbPrecloser);
                //panel.Controls.Add(rbNeither);

                // Here, you might want to adjust the layout within the panel.
                // For simplicity, I'll just set their locations manually:
                rbSection.Location = new System.Drawing.Point(5, 5); // You can adjust these coordinates as needed.
                lblMaxCovers.Location = new System.Drawing.Point(rbSection.Right + 5, 5);
                lblAverageCovers.Location = new System.Drawing.Point(lblMaxCovers.Right + 5, 5);

                rbCloser.Location = new System.Drawing.Point(lblAverageCovers.Right + 10, 5);
                rbPrecloser.Location = new System.Drawing.Point(rbCloser.Right + 5, 5);
                // Adjust panel size to fit the controls (or set a predefined size).
                //sectionPanel.Size = new Size(lblAverageCovers.Right + 20, Math.Max(rbSection.Height, lblAverageCovers.Height) + 10);
                sectionPanel.Size = new Size(flowSectionSelect.Width - 10, Math.Max(rbSection.Height, lblAverageCovers.Height) + 10);
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
            Button btnAddPickup = new Button
            {
                Text = "Add Pick-Up Section",
                AutoSize = false,
                Size = new Size(flowSectionSelect.Width - 10, 25),
                Font = new Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat,
                BackColor = AppColors.ButtonColor,
                ForeColor = Color.Black
            };
            btnAddPickup.Click += btnAddPickupSection_Click;
            flowSectionSelect.Controls.Add(btnAddPickup);


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
            if (shiftManager.SelectedFloorplan == null) return;

            float maxDifference = newMaxCoversValue - shiftManager.SelectedFloorplan.MaxCoversPerServer;
            float avgDifference = newAverageCoversValue - shiftManager.SelectedFloorplan.AvgCoversPerServer;
            if (sectionLabels.ContainsKey(section))
            {
                sectionLabels[section].MaxCoversLabel.Text = maxDifference.ToString();

                sectionLabels[section].AverageCoversLabel.Text = Section.FormatAsCurrencyWithoutParentheses(avgDifference);// avgDifference.ToString("C0;\\-C0", CultureInfo.CurrentCulture);

            }
        }


        private void nudNumberOfTeamWaits_ValueChanged(object sender, EventArgs e)
        {
            //shiftManager.Sections = GetNumberOfSections();
            //CreateSectionRadioButtons(shiftManager.Sections);
        }


        private void rdoSections_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSections.Checked)
            {
                pnlNavigationWindow.SendToBack();
                pnlNavHighlight.Location = new Point(rdoSections.Left, 0);

                flowServersInFloorplan.Visible = true;
                lblPanel2Text.Text = areaCreationManager.DiningAreaSelected.Name;
                //this.shiftManager = new ShiftManager(areaCreationManager.DiningAreaSelected);
                lblDiningAreaMaxCovers.Text = shiftManager.SelectedDiningArea.GetMaxCovers().ToString();
                lblDiningAreaAverageCovers.Text = shiftManager.SelectedDiningArea.GetAverageCovers().ToString();
                foreach (Control control in pnlFloorPlan.Controls)
                {
                    if (control is TableControl tableControl)
                    {
                        tableControl.Moveable = false;

                    }
                }

            }
        }
        private void rdoShifts_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoShifts.Checked)
            {
                pnlNavHighlight.Location = new Point(rdoShifts.Left, 0);
                if (_frmEditStaff == null)
                {
                    _frmEditStaff = new frmEditStaff(employeeManager, shiftManager, this) { TopLevel = false, AutoScroll = true };
                }
                pnlNavigationWindow.Controls.Add(_frmEditStaff);

                _frmEditStaff.Show();
                pnlNavigationWindow.BringToFront();

            }
            else
            {
                if (_frmEditStaff != null)
                {
                    _frmEditStaff.Hide();
                }
            }


        }
        private void rdoDiningAreas_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDiningAreas.Checked)
            {
                pnlNavHighlight.Location = new Point(rdoDiningAreas.Left, 0);
                if (_frmEditDiningAreas == null)
                {
                    _frmEditDiningAreas = new frmEditDiningAreas { TopLevel = false, AutoScroll = true };
                }
                pnlNavigationWindow.Controls.Add(_frmEditDiningAreas);

                _frmEditDiningAreas.Show();
                pnlNavigationWindow.BringToFront();
                //pnlSections.Visible = false;
                //flowServersInFloorplan.Visible = false;
                //lblPanel2Text.Text = "Add Tables";
                //txtDiningAreaName.Visible = true;
                ////cboDiningAreas.Visible = true;
                //btnCreateNewDiningArea.Visible = true;
                //btnSaveDiningArea.Visible = true;
                //btnSaveTables.Visible = true;
                //rbInside.Visible = true;
                //rbOutside.Visible = true;
                //foreach (Control control in pnlFloorPlan.Controls)
                //{
                //    if (control is TableControl tableControl)
                //    {
                //        tableControl.Moveable = true;
                //    }
                //}
            }
            else
            {
                if (_frmEditDiningAreas != null)
                {
                    _frmEditDiningAreas.Hide();
                }
            }
        }



        private void btnAddSectionLabels_Click(object sender, EventArgs e)
        {


            //foreach (Section section in shiftManager.SelectedFloorplan.Sections)
            //{
            //    SectionControl sectionControl = new SectionControl(section, pnlFloorPlan, shiftManager.SelectedFloorplan.Servers);

            //    pnlFloorPlan.Controls.Add(sectionControl);
            //    sectionControl.BringToFront();

            //}
            sectionControlsManager = new SectionControlsManager(shiftManager.SelectedFloorplan);
            foreach (SectionControl sectionControl in sectionControlsManager.SectionControls)
            {
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

            shiftManager.AssignSectionNumbers(template.Sections);



        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool pickUpAdded = false;
            Section pickUpSection = new Section();
            pickUpSection.IsPickUp = true;
            //shiftManager.SelectedFloorplan = shiftManager.ViewedFloorplan;
            shiftManager.SelectedFloorplan.Date = dtpFloorplan.Value;
            shiftManager.SelectedFloorplan.IsLunch = cbIsAM.Checked;
            foreach (Control control in pnlFloorPlan.Controls)
            {
                if (control is TableControl tableControl)
                {
                    if (tableControl.Section == null)
                    {
                        pickUpSection.DiningAreaID = shiftManager.SelectedFloorplan.DiningArea.ID;
                        pickUpSection.Name = "Pick Up";
                        shiftManager.SelectedFloorplan.Sections.Add(pickUpSection);
                        pickUpAdded = true;
                    }
                    break;
                }

            }
            foreach (Control control in pnlFloorPlan.Controls)
            {
                if (control is TableControl tableControl)
                {
                    if (tableControl.Section == null)
                    {
                        tableControl.Section = pickUpSection;

                        pickUpSection.Tables.Add(tableControl.Table);

                        tableControl.BackColor = pickUpSection.Color;

                        // Optionally, you can invalidate the control to request a redraw if needed.
                        tableControl.Invalidate();

                    }
                }

            }
            if (pickUpAdded)
            {
                UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
                CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
                SectionControl sectionControl = new SectionControl(pickUpSection, sectionControlsManager);
                pnlFloorPlan.Controls.Add(sectionControl);
                sectionControl.BringToFront();
            }
            if (shiftManager.SelectedFloorplan.CheckIfAllSectionsAssigned())
            {
                if (!shiftManager.SelectedFloorplan.CheckIfCloserIsAssigned())
                {
                    DialogResult result = MessageBox.Show("There is not a closer assigned",
                                                "Continue?",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        // User does not want to replace, so exit without saving.

                        return;
                    }

                }
                SqliteDataAccess.SaveFloorplanAndSections(shiftManager.SelectedFloorplan);
                FloorplanPrinter printer = new FloorplanPrinter(pnlFloorPlan, sectionLineManager.SectionLines);
                printer.ShowPrintPreview();
                printer.Print();


            }
            else
            {
                MessageBox.Show("Not all sections are assigned");
            }

            // To show print preview
            // To print

        }

        private void btnChooseTemplate_Click(object sender, EventArgs e)
        {
            frmTemplateSelection form = new frmTemplateSelection(shiftManager);
            //pnlFloorPlan.Visible = false;
            form.StartPosition = FormStartPosition.CenterScreen;
            //form.TopLevel = false;
            form.BringToFront();
            //this.Controls.Add(form);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                UpdateTableControlSections();
            }
            if (DialogResult == DialogResult.Cancel)
            {

            }
            //form.ShowDialog();
        }
        private void UpdateTableControlSections()
        {
            ClearAllSectionControls();
            if (shiftManager.SelectedFloorplan == null)
            {
                flowSectionSelect.Controls.Clear();
                flowServersInFloorplan.Controls.Clear();
                ClearAllTableControlSections();
                NoServersToDisplay();
                return;
            }
            foreach (Control ctrl in pnlFloorPlan.Controls)
            {

                if (ctrl is TableControl tableControl)
                {
                    foreach (Section section in shiftManager.SelectedFloorplan.Sections)
                    {

                        foreach (Table table in section.Tables)
                        {
                            if (tableControl.Table.TableNumber == table.TableNumber)
                            {
                                tableControl.Section = section;
                                tableControl.BackColor = section.Color;
                                tableControl.Invalidate();
                                break; // Once found, no need to check other tables in this section
                            }
                        }
                    }
                }
            }
            sectionControlsManager = new SectionControlsManager(shiftManager.SelectedFloorplan);
            foreach (SectionControl sectionControl in sectionControlsManager.SectionControls)
            {
                pnlFloorPlan.Controls.Add(sectionControl);
                sectionControl.BringToFront();
            }
        }

        private void btnAddPickupSection_Click(object sender, EventArgs e)
        {
            Section pickUpSection = new Section(shiftManager.SelectedFloorplan);
            pickUpSection.Name = "Pickup";
            pickUpSection.IsPickUp = true;
            shiftManager.SelectedFloorplan.Sections.Add(pickUpSection);
            CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
        }

        private void dtpFloorplan_ValueChanged(object sender, EventArgs e)
        {

            SetViewedFloorplan();
            this.sectionLineManager = new SectionLineManager(allTableControls);



        }
        private void SetViewedFloorplan()
        {
            NoServersToDisplay();
            DateOnly date = DateOnly.FromDateTime(dtpFloorplan.Value);
            if (shiftManager.ContainsFloorplan(date, cbIsAM.Checked, shiftManager.SelectedDiningArea.ID))
            {
                shiftManager.SetSelectedFloorplan(date, cbIsAM.Checked, shiftManager.SelectedDiningArea.ID);
                //shiftManager.ViewedFloorplan = shiftManager.SelectedFloorplan;
            }
            else
            {

                shiftManager.SelectedFloorplan = SqliteDataAccess.LoadFloorplanByCriteria(shiftManager.SelectedDiningArea, date, cbIsAM.Checked);
            }


            if (shiftManager.SelectedFloorplan != null)
            {

                CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
                UpdateServerControlsForFloorplan();
                lblServerMaxCovers.Text = shiftManager.SelectedFloorplan.MaxCoversPerServer.ToString("F1");
                lblServerAverageCovers.Text = shiftManager.SelectedFloorplan.AvgCoversPerServer.ToString("C0");
            }

            UpdateTableControlSections();
        }
        private void NoServersToDisplay()
        {

            Label noSections = new Label
            {
                Text = "No Sections For\nThis Floorplan",
                Font = AppColors.LargeFont,
                AutoSize = false,
                Size = new System.Drawing.Size(200, 350),
                TextAlign = ContentAlignment.MiddleCenter,

                ForeColor = Color.Black


            };
            Label noServers = new Label
            {
                Text = "No Servers For\nThis Floorplan",
                Font = AppColors.LargeFont,
                AutoSize = false,
                Size = new System.Drawing.Size(200, 350),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                Margin = new System.Windows.Forms.Padding(15, 0, 0, 0)

            };
            noSections.BringToFront();
            noServers.BringToFront();
            flowSectionSelect.Controls.Add(noSections);
            flowServersInFloorplan.Controls.Add(noServers);
        }
        private void ClearAllSectionControls()
        {
            List<Control> controlsToRemove = new List<Control>();
            foreach (Control c in pnlFloorPlan.Controls)
            {
                if (c is SectionControl sectionControl)
                {
                    controlsToRemove.Add(sectionControl);
                }
            }
            foreach (Control c in controlsToRemove)
            {
                pnlFloorPlan.Controls.Remove(c);
            }
        }
        private void ClearAllTableControlSections()
        {
            foreach (TableControl tableControl in allTableControls)
            {
                Section sectionEdited = tableControl.Section;
                if (sectionEdited != null)
                {
                    tableControl.Section.Tables.Remove(tableControl.Table);
                    tableControl.Section = null;
                    UpdateSectionLabels(sectionEdited, sectionEdited.MaxCovers, sectionEdited.AverageCovers);
                }


                tableControl.BackColor = pnlFloorPlan.BackColor;  // Restore the original color
                tableControl.Invalidate();

            }

        }
        private void btnAddSection_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveSection_Click(object sender, EventArgs e)
        {

        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            //nudServerCount.Value = 4;

            //UpdateFloorplan();
            //SectionLine sectionLine = new SectionLine(0,0,300,300, 5f);
            //sectionLine.StartPoint = new System.Drawing.Point(0, 0);
            //sectionLine.EndPoint = new System.Drawing.Point(300, 300);
            //sectionLine.LineThickness = 5f;
            //pnlFloorPlan.Controls.Add(sectionLine);
            //SqliteDataAccess.UpdateAllFloorplanDates();

            //foreach(Section section in shiftManager.ViewedFloorplan.Sections)
            //{
            //    var edges = section.GetConvexHullEdges();

            //    foreach (var edge in edges)
            //    {
            //        var sectionLine = new SectionLine
            //        {
            //            StartPoint = new System.Drawing.Point((int)edge.Item1.X, (int)edge.Item1.Y),
            //            EndPoint = new System.Drawing.Point((int)edge.Item2.X, (int)edge.Item2.Y)
            //        };
            //        pnlFloorPlan.Controls.Add(sectionLine);
            //    }
            //}
            //allTableControls.Sort((a, b) => a.Top.CompareTo(b.Top));

            //// 3. Determine midpoints and 4. Draw borders.
            //for (int i = 0; i < allTableControls.Count - 1; i++)
            //{
            //    TableControl currentTable = allTableControls[i];
            //    TableControl nextTable = allTableControls[i + 1];

            //    if(currentTable.Section != nextTable.Section)
            //    {
            //        int midpoint = currentTable.Bottom + (nextTable.Top - currentTable.Bottom) / 2;


            //        pnlFloorPlan.CreateGraphics().DrawLine(Pens.Black, currentTable.Table.XCoordinate, midpoint, currentTable.Table.XCoordinate - currentTable.Width, midpoint);
            //    }

            //}

            //this.sectionLineManager = new SectionLineManager(allTableControls);
            //sectionLineManager.AddTopLines(pnlFloorPlan);
            //sectionLineManager.AddRightLines(pnlFloorPlan);
            //sectionLineManager.AddRightBorders(pnlFloorPlan);
            //sectionLineManager.AddBottomLines(pnlFloorPlan);
            //sectionLineManager.DrawSectionLines(pnlFloorPlan);

            //sectionLineManager.MakeTopLines(pnlFloorPlan);
            //sectionLineManager.MakeSectionTableOutlines();
            //foreach(SectionLine sectionLine in sectionLineManager.SectionLines)
            //{
            //    pnlFloorPlan.Controls.Add(sectionLine);
            //}
            //sectionLineManager.RemoveBottomLines(pnlFloorPlan);
            //sectionLineManager.RemoveRightLines(pnlFloorPlan);
            //sectionLineManager.DrawSeparationLines(pnlFloorPlan);
            sectionLineManager.AddSectionNodes(pnlFloorPlan);

        }
        private void btnTest2_Click(object sender, EventArgs e)
        {
            //pnlFloorPlan.Controls.Clear();
            //SectionLine sectionLine = new SectionLine(100,100,500,100,5f);
            //pnlFloorPlan.Controls.Add(sectionLine);
            //Section section = new Section();
            //section.Number = 1;
            //foreach (TableControl c in allTableControls)
            //{
            //    c.Section = section;
            //    c.BackColor = section.Color;
            //    section.Tables.Add(c.Table);
            //}
            //shiftManager.SectionSelected = section;
            //sectionLineManager.RemoveAllLines(pnlFloorPlan);
            //sectionLineManager.UpdateSectionLinePositions(pnlFloorPlan);
            //sectionLineManager.AddParallelLines(pnlFloorPlan);
            foreach (Section section in shiftManager.SelectedFloorplan.Sections)
            {
                SectionNodeManager nodeManager = new SectionNodeManager(section);
                Node tlNode = nodeManager.GetTopLeftNode();
                Node trNode = nodeManager.GetTopRightNode();
                Node brNode = nodeManager.GetBottomRightNode();
                SectionLine sectionLine = new SectionLine(tlNode, trNode);
                SectionLine sectionLine1 = new SectionLine(trNode, brNode);
                pnlFloorPlan.Controls.Add(sectionLine);
                pnlFloorPlan.Controls.Add(sectionLine1);

            }

        }

        private void btnDoAThing_Click(object sender, EventArgs e)
        {
            //foreach (Control c in pnlFloorPlan.Controls)
            //{
            //    if (c is SectionLine sectionline)
            //    {
            //        sectionline.BringToFront();
            //        sectionline.LineThickness = 15;
            //        sectionline.Invalidate();
            //    }
            //}
            //sectionLineManager.RemoveAllLines(pnlFloorPlan);
            sectionLineManager.AddTopLines(pnlFloorPlan);

        }

        private void btnDayBefore_Click(object sender, EventArgs e)
        {
            dtpFloorplan.Value = dtpFloorplan.Value.AddDays(-1);
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            dtpFloorplan.Value = dtpFloorplan.Value.AddDays(1);
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbTableDisplayMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTableDisplayMode.Checked)
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

        private void pnlSections_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }




        //public static List<LineString> ComputeVoronoiEdges(List<Coordinate> coordinates)
        //{
        //    VoronoiDiagramBuilder builder = new VoronoiDiagramBuilder();
        //    builder.SetSites(coordinates);
        //    var diagram = builder.GetDiagram(new GeometryFactory());

        //    var lines = new List<LineString>();

        //    for (int i = 0; i < diagram.NumGeometries; i++)
        //    {
        //        var polygon = diagram.GetGeometryN(i) as Polygon;
        //        if (polygon != null)
        //        {
        //            var boundary = polygon.Boundary as LineString;
        //            if (boundary != null && !boundary.StartPoint.Equals(boundary.EndPoint))  // Exclude degenerate edges
        //            {
        //                lines.Add(boundary);
        //            }
        //        }
        //    }

        //    return lines;
        //}


    }
}