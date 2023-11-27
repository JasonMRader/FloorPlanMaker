
using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorPlanMakerUI.Properties;
using FloorplanUserControlLibrary;
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
        private PictureBox loadingScreen = null;
        ImageLabelControl coversImageLabel = new ImageLabelControl();
        ImageLabelControl salesImageLabel = new ImageLabelControl();
        private FloorplanFormManager floorplanManager;
        private DateOnly dateOnlySelected
        {
            get
            {
                return new DateOnly(this.dateTimeSelected.Year, this.dateTimeSelected.Month, this.dateTimeSelected.Day);
            }
        }
        private DateTime dateTimeSelected = new DateTime();

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        private SectionControlsManager sectionControlsManager { get; set; }
        private void SetColors()
        {
            btnCloseApp.BackColor = Color.Red;
            UITheme.FormatCTAButton(btnAddSectionLabels);
            UITheme.FormatCTAButton(btnPrint);
            UITheme.FormatCTAButton(rdoDiningAreas);
            UITheme.FormatCTAButton(rdoSections);
            UITheme.FormatCTAButton(rdoShifts);

            UITheme.FormatMainButton(btnChooseTemplate);
            //AppColors.FormatMainButton(btnGenerateSectionLines);
            UITheme.FormatMainButton(btnSaveFloorplanTemplate);
            UITheme.FormatMainButton(cbTableDisplayMode);

            UITheme.FormatSecondColor(this);
            UITheme.FormatCanvasColor(pnlFloorplanContainer);
            UITheme.FormatCanvasColor(pnlSectionsAndServers);

            //AppColors.FormatSecondColor(lblCoversPerServerText);
            //AppColors.FormatSecondColor(lblSalesPerServerText);
            UITheme.FormatSecondColor(lblServerAverageCovers);
            UITheme.FormatSecondColor(lblServerMaxCovers);
            UITheme.FormatSecondColor(picBxCovers);
            UITheme.FormatSecondColor(picBxSales);
            //lblCoversPerServerText.Font = AppColors.MainFont;
            //lblSalesPerServerText.Font = AppColors.MainFont;
            lblServerAverageCovers.Font = UITheme.LargeFont;
            lblServerMaxCovers.Font = UITheme.LargeFont;

            btnChooseTemplate.Font = UITheme.MainFont;
            btnGenerateSectionLines.Font = UITheme.MainFont;
            btnSaveFloorplanTemplate.Font = UITheme.MainFont;
            cbTableDisplayMode.Font = UITheme.MainFont;

            UITheme.FormatAccentColor(pnlNavigationWindow);
            //AppColors.FormatAccentColor(pnlSideBar);

            UITheme.FormatCanvasColor(pnlFloorPlan);
            UITheme.FormatCanvasColor(flowSectionSelect);
            UITheme.FormatCanvasColor(flowServersInFloorplan);

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if the Tab key is pressed
            if (keyData == Keys.Tab)
                {
                shiftManager.SelectedFloorplan.MoveToNextSection();
                return true; // Indicate that you've handled this key press
            }
            if (_frmEditStaff != null && _frmEditStaff.Visible)
            {
                if (keyData == Keys.Left)
                {
                    _frmEditStaff.MovedDateBack();
                    return true; // Indicate that you've handled this key press
                }
                else if (keyData == Keys.Right)
                {
                    _frmEditStaff.MoveDateForward();
                    return true; // Indicate that you've handled this key press
                }
            }
            else
            {
                if (keyData == Keys.Left)
                {
                    UpdateDateLabel(-1);
                    return true; 
                }

                
                if (keyData == Keys.Right)
                {
                    UpdateDateLabel(1);
                    return true; 
                }
                
            }
            if (_frmEditDiningAreas != null && _frmEditDiningAreas.Visible)
            {
                if (keyData == Keys.Up)
                {
                    _frmEditDiningAreas.ChangeDiningArea( keyData);
                    return true;
                }

                if (keyData == Keys.Down)
                {
                    _frmEditDiningAreas.ChangeDiningArea(keyData);
                    return true;
                }
            }
            else
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
                    return true;
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
                    return true;
                }
            }
            

            return base.ProcessCmdKey(ref msg, keyData);
        }

        

        private void SelectSection(int sectionNumber)
        {

            shiftManager.SetSelectedSection( shiftManager.SelectedFloorplan.Sections.Where(s => s.Number == sectionNumber).FirstOrDefault());
            foreach (Control c in flowSectionSelect.Controls)
            {
                if (c is Panel panel)
                {
                    if (panel.Tag == shiftManager.SectionSelected)
                    {
                        panel.BackColor = shiftManager.SectionSelected.Color;
                    }
                    else
                    {
                        panel.BackColor = Color.SlateGray;
                    }

                }
            }
            foreach (CheckBox cb in selectedSectionButtons)
            {
                if (cb.Tag == shiftManager.SectionSelected)
                {
                    cb.Checked = true;
                }
                else
                {
                    cb.Checked = false;
                }
            }
            FillInTableControlColors();

        }
        public void UpdateForm1ShiftManager(ShiftManager shiftManagerToAdd)
        {
            dateTimeSelected = new DateTime(shiftManagerToAdd.DateOnly.Year, shiftManagerToAdd.DateOnly.Month, shiftManagerToAdd.DateOnly.Day);

            cbIsAM.Checked = shiftManagerToAdd.IsAM;
            foreach (Floorplan fp in shiftManagerToAdd.Floorplans)
            {
                this.shiftManager.AddFloorplanAndServers(fp);
            }


            SetViewedFloorplan();
            rdoSections.Checked = true;
            rdoViewSectionFlow.Checked = true;
            flowSectionSelect.Visible = true;
            flowServersInFloorplan.Visible = false;
            rdoViewSectionFlow.Image = Resources.lilCanvasBook;

            //rdoViewServerFlow.Image = Resources.lilPeople;


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
            floorplanManager = new FloorplanFormManager(shiftManager);

            // Subscribe to the event
            //floorplanManager.SectionLabelRemoved += FloorplanManager_SectionLabelRemoved;
            floorplanManager.UpdateRequired += FloorplanManager_UpdateRequired;

            //pnlFloorPlan.KeyPreview = true;
        }
        private void FloorplanManager_UpdateRequired(object sender, UpdateEventArgs e)
        {
            switch (e.UpdateType)
            {
                case UpdateType.SectionLabel:
                    floorplanManager.RemoveSectionLabel(e.UpdateData as Section, pnlFloorPlan);
                    break;
                case UpdateType.ServerControl:
                    // Handle ServerControl update
                    break;
                case UpdateType.SectionControl:
                    // Handle SectionControl update
                    break;
                case UpdateType.TableControl:
                    //floorplanManager.RemoveTableControlSection(e.UpdateData as Section, pnlFloorPlan);
                    floorplanManager.UpdateTableControlColors(pnlFloorPlan);
                    break;
                    // Add more cases as needed
            }
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
                        if(tableControl.Section != null)
                        {
                            tableControl.Section.RemoveTable(tableControl.Table);
                        }
                       
                        tableControl.IsSelected = true;
                        tableControl.BackColor = shiftManager.SectionSelected.Color;
                        tableControl.ForeColor = shiftManager.SectionSelected.FontColor;

                        if (shiftManager.SelectedFloorplan.Sections != null)
                        {
                            var targetSection = shiftManager.SelectedFloorplan.Sections.FirstOrDefault(sec => sec.Equals(shiftManager.SectionSelected));
                            if (targetSection != null)
                            {
                                targetSection.AddTable(tableControl.Table);
                                tableControl.SetSection(targetSection);
                                UpdateSectionLabels(targetSection, targetSection.MaxCovers, targetSection.AverageCovers);
                            }
                           
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
            dateTimeSelected = DateTime.Now;
            
            List<Floorplan> floorplans = SqliteDataAccess.LoadFloorplanList();
            cboDiningAreas.DataSource = areaCreationManager.DiningAreas;
            cboDiningAreas.DisplayMember = "Name";
            cboDiningAreas.ValueMember = "ID";
            rdoSections.Checked = true;
            rdoViewSectionFlow.Checked = true;
            UpdateDateLabel(0);
            //rdoViewSectionFlow.Checked = true;
        }
        private void UpdateDateLabel(int days)
        {
            dateTimeSelected = dateTimeSelected.AddDays(days);
            lblDateSelected.Text = dateOnlySelected.ToString("ddd, MMM d");
            SetViewedFloorplan();
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


        

        private void cboDiningAreas_SelectedIndexChanged(object sender, EventArgs e)
        {

           
            

            shiftManager.SelectedDiningArea = (DiningArea?)cboDiningAreas.SelectedItem;
            floorplanManager.AddTableControls(pnlFloorPlan);
            
            SetViewedFloorplan();
            this.sectionLineManager = new SectionLineManager(allTableControls);

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
                    ServerControl serverControl = new ServerControl(server, 20);
                    serverControl.Click += ServerControl_Click;
                    foreach (ShiftControl shiftControl in serverControl.ShiftControls)
                    {

                        shiftControl.ShowClose();
                        shiftControl.ShowTeam();
                        shiftControl.HideOutside();
                    }

                    flowServersInFloorplan.Controls.Add(serverControl);
                }
                foreach(Section section in shiftManager.SelectedFloorplan.Sections)
                {
                    if(section.Server != null)
                    {
                        foreach(ServerControl serverControl in flowServersInFloorplan.Controls)
                        {
                            if(serverControl.Server == section.Server)
                            {
                                serverControl.Label.BackColor = section.Color;

                            }
                        }
                    }
                }
            }
        }

        private void ServerControl_Click(object? sender, EventArgs e)
        {
            ServerControl serverControl = (ServerControl)sender;
            Server server = serverControl.Server;
            if (shiftManager.SectionSelected.Server == null)
            {
                shiftManager.SectionSelected.AddServer(server);

            }
            foreach (SectionLabelControl sc in sectionControlsManager.SectionControls)
            {
                if (sc.Section == shiftManager.SectionSelected)
                {
                    sc.UpdateLabel();
                }
            }
            serverControl.Label.BackColor = shiftManager.SectionSelected.Color;
            serverControl.Label.ForeColor = shiftManager.SectionSelected.FontColor;

        }

        private List<CheckBox> TeamWaitButtons = new List<CheckBox>();
        private List<CheckBox> selectedSectionButtons = new List<CheckBox>();
        private CheckBox selectedCloserButton;
        private CheckBox selectedPreCloserButton;

        private void CreateSectionRadioButtons(List<Section> sections)
        {
            if (loadingScreen == null)
            {
                loadingScreen = UITheme.GetPictureBox(Resources.Loading, flowSectionSelect.Width, flowSectionSelect.Height);
                this.Controls.Add(loadingScreen);
                loadingScreen.Location = new Point(67, 115);
                loadingScreen.BackColor = flowSectionSelect.BackColor;
                loadingScreen.BringToFront();
            }
            else
            {
                loadingScreen.Visible = true;
            }
            timer.Interval = 300;
            timer.Tick += (sender, e) =>
            {
                loadingScreen.Visible = false;
                timer.Stop();
            };
            timer.Start();

            
            flowSectionSelect.Controls.Clear();

            coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (flowSectionSelect.Width / 2) - 7, 30);
            salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (flowSectionSelect.Width / 2) - 7, 30);
            flowSectionSelect.Controls.Add(coversImageLabel);
            flowSectionSelect.Controls.Add(salesImageLabel);

            
            if (sections.Count == 0)
            {
                NoServersToDisplay();
            }
            if (sections == null)
            {

                return;
            }
            floorplanManager.AddSectionPanels(flowSectionSelect);
            //foreach (var section in sections)
            //{
            //    //CreateOneSectionPanel(section);
            //    SectionPanelControl sectionPanel = new SectionPanelControl(section, shiftManager.SelectedFloorplan);
            //    sectionPanel.CheckBoxChanged += Rb_CheckedChanged;
               
            //    flowSectionSelect.Controls.Add(sectionPanel);
            //}
            if (flowSectionSelect.Controls.Count > 0)
            {
                //Panel firstPanel = (Panel)flowSectionSelect.Controls[2];
                //if (firstPanel.Controls.Count > 0)
                //{
                //    CheckBox firstSectionCheckBox = (CheckBox)firstPanel.Controls[0];
                //    firstSectionCheckBox.Checked = true;
                //}
                SectionPanelControl firstPanel = (SectionPanelControl)flowSectionSelect.Controls[2];
                if (firstPanel.Controls.Count > 0)
                {
                    //CheckBox firstSectionCheckBox = (CheckBox)firstPanel.Controls[0];
                    firstPanel.CheckBox();
                }
            }
            Button btnAddPickup = new Button
            {
                Text = "Add Pick-Up Section",
                AutoSize = false,
                Size = new Size(flowSectionSelect.Width - 10, 25),
                Font = new Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.ButtonColor,
                ForeColor = Color.Black
            };
            btnAddPickup.Click += btnAddPickupSection_Click;
            flowSectionSelect.Controls.Add(btnAddPickup);
            //flowSectionSelect.Controls.SetChildIndex(lblServerMaxCovers, 1);
            //flowSectionSelect.Controls.SetChildIndex(lblServerAverageCovers, 3);
            flowSectionSelect.Controls.SetChildIndex(coversImageLabel, 0);
            flowSectionSelect.Controls.SetChildIndex(salesImageLabel, 1);

            SelectSection(1);



        }

        

        private void CreateOneSectionPanel(Section section)
        {
            // Create a RadioButton for each section.
            CheckBox rbSection = new CheckBox
            {
                Appearance = Appearance.Button,
                FlatStyle = FlatStyle.Flat,
                BackColor = section.Color,
                ForeColor = section.FontColor,
                //MaximumSize = new Size(55,25),
                AutoSize = false,
                Size = new Size(100, 25),
                Text = section.Name,
                Tag = section  // Store the section object in the Tag property for easy access in the event handler.
            };
            rbSection.FlatAppearance.BorderSize = 0;
            rbSection.CheckedChanged += Rb_CheckedChanged;
            selectedSectionButtons.Add(rbSection);

            // Create two labels for each section.
            Label lblMaxCovers = new Label
            {
                Text = (section.MaxCovers - shiftManager.SelectedFloorplan.MaxCoversPerServer).ToString("F0"),
                AutoSize = false,
                Size = new Size(65, 25),
                Font = UITheme.MainFont,
                TextAlign = ContentAlignment.TopCenter,
                Margin = new Padding(0, 3, 0, 3)

            };

            Label lblAverageCovers = new Label
            {
                Text = Section.FormatAsCurrencyWithoutParentheses(section.AverageCovers - shiftManager.SelectedFloorplan.AvgSalesPerServer), //(section.AverageCovers - shiftManager.SelectedFloorplan.AvgCoversPerServer).ToString("C0;\\-C0", CultureInfo.CurrentCulture),
                AutoSize = true,
                Size = new Size(65, 25),
                Font = UITheme.MainFont,
                TextAlign = ContentAlignment.TopCenter,
                Margin = new Padding(0, 3, 0, 3)

            };

            PictureBox cbTeamWait = new PictureBox
            {

                Size = new Size(40, 25),
                Image = Resources.waiter,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Margin = new Padding(0),
                Tag = section,
                BackColor = UITheme.CTAColor
            };
            cbTeamWait.Click += SectionTeamWait_Click;

            PictureBox deleteSectionPB = new PictureBox
            {

                Size = new Size(40, 25),
                Image = Resources.erase,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Margin = new Padding(0),
                Tag = section,
                BackColor = UITheme.NoColor
            };
            deleteSectionPB.Click += DeleteSection_Click;

            Panel sectionPanel = new Panel();
            sectionPanel.Tag = section;
            sectionPanel.AllowDrop = true;
            sectionPanel.BackColor = Color.SlateGray;

            // Attach drag-drop event handlers



            sectionPanel.Controls.Add(rbSection);
            sectionPanel.Controls.Add(lblMaxCovers);
            sectionPanel.Controls.Add(lblAverageCovers);
            sectionPanel.Controls.Add(cbTeamWait);
            sectionPanel.Controls.Add(deleteSectionPB);
            //sectionPanel.Controls.Add(rbPrecloser);
            //panel.Controls.Add(rbNeither);


            //rbSection.Location = new System.Drawing.Point(5, 5); // You can adjust these coordinates as needed.
            rbSection.Dock = DockStyle.Left;
            lblMaxCovers.Location = new System.Drawing.Point(rbSection.Right + 5, 5);
            lblAverageCovers.Location = new System.Drawing.Point(lblMaxCovers.Right + 5, 5);
            //cbTeamWait.Location = new System.Drawing.Point(lblAverageCovers.Right + 5, 5);
            cbTeamWait.Dock = DockStyle.Right;
            deleteSectionPB.Dock = DockStyle.Right;
            sectionPanel.Size = new Size(flowSectionSelect.Width - 10, lblAverageCovers.Height + 10);
            sectionLabels[section] = (lblMaxCovers, lblAverageCovers);

            flowSectionSelect.Controls.Add(sectionPanel);
            int secondToLastIndex = flowSectionSelect.Controls.Count - 2; // -2 because Count is always 1 more than the last index

            // Check if there are enough controls to move the new one
            if (secondToLastIndex >= 0)
            {
                // Move the new control to the second-to-last position
                flowSectionSelect.Controls.SetChildIndex(sectionPanel, secondToLastIndex);
            }

        }

        private void DeleteSection_Click(object? sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Section selectedSection = pb.Tag as Section;
            if (selectedSection != null)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this section?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    shiftManager.SelectedFloorplan.UnassignSection(selectedSection);
                    UpdateTableControlSections();

                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

        }

        private void SectionTeamWait_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb != null)
            {
                Section selectedSection = pb.Tag as Section;

                if (!selectedSection.IsTeamWait)
                {

                    //pb.BackColor = AppColors.NoColor;
                    Section sectionRemoved = shiftManager.SelectedFloorplan.RemoveHighestNumberedEmptySection();
                    if (sectionRemoved == null)
                    {
                        MessageBox.Show("You must clear a section before making another section a teamwait section");
                    }
                    else
                    {
                        selectedSection.MakeTeamWait();
                        pb.BackColor = UITheme.WarningColor;
                        pb.Image = Resources.waiters;
                        RemoveSectionPanel(sectionRemoved);
                    }

                }
                else
                {
                    selectedSection.MakeSoloSection();
                    pb.BackColor = UITheme.CTAColor;
                    pb.Image = Resources.waiter;
                    Section section = new Section();
                    shiftManager.SelectedFloorplan.AddSection(section);
                    CreateOneSectionPanel(section);
                }
                UpdateSectionLabels(selectedSection, selectedSection.MaxCovers, selectedSection.AverageCovers);
            }
            //CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
        }
        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            SectionPanelControl rb = sender as SectionPanelControl;
            if (rb != null && rb.CheckBoxChecked)
            {
                Section selectedSection = rb.Section as Section;
                SelectSection(selectedSection.Number);
                currentFocusedSectionIndex = selectedSection.Number;
                if (sectionControlsManager != null)
                {
                    sectionControlsManager.SetSelectedSection(selectedSection);
                }

            }
        }
        //private void Rb_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox rb = sender as CheckBox;
        //    if (rb != null && rb.Checked)
        //    {
        //        Section selectedSection = rb.Tag as Section;
        //        SelectSection(selectedSection.Number);
        //        currentFocusedSectionIndex = selectedSection.Number;
        //        if (sectionControlsManager != null)
        //        {
        //            sectionControlsManager.SetSelectedSection(selectedSection);
        //        }

        //    }
        //}
        private void RemoveSectionPanel(Section section)
        {
            foreach (Control c in flowSectionSelect.Controls)
            {
                if (c is Panel panel && c.Tag == section)
                {
                    flowSectionSelect.Controls.Remove(c);
                }
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
            float avgDifference = newAverageCoversValue - shiftManager.SelectedFloorplan.AvgSalesPerServer;
            if (section.IsTeamWait)
            {
                maxDifference = newMaxCoversValue - (shiftManager.SelectedFloorplan.MaxCoversPerServer * 2);
                avgDifference = newAverageCoversValue - (shiftManager.SelectedFloorplan.AvgSalesPerServer * 2);
            }
            if (sectionLabels.ContainsKey(section))
            {
                sectionLabels[section].MaxCoversLabel.Text = maxDifference.ToString("F0");

                sectionLabels[section].AverageCoversLabel.Text = Section.FormatAsCurrencyWithoutParentheses(avgDifference);// avgDifference.ToString("C0;\\-C0", CultureInfo.CurrentCulture);

            }
        }


        private void rdoSections_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSections.Checked)
            {
                pnlNavigationWindow.SendToBack();
                pnlNavHighlight.Location = new Point(rdoSections.Left, 0);
                pnlMainContainer.Visible = true;
                //pnlSideBar.Visible = true;
                pnlSideContainer.Visible = true;

                flowServersInFloorplan.Visible = true;
                //lblPanel2Text.Text = areaCreationManager.DiningAreaSelected.Name;
                //this.shiftManager = new ShiftManager(areaCreationManager.DiningAreaSelected);                
                foreach (Control control in pnlFloorPlan.Controls)
                {
                    if (control is TableControl tableControl)
                    {
                        tableControl.Moveable = false;

                    }
                }

            }
            else
            {
                pnlMainContainer.Visible = false;
                //pnlSideBar.Visible = false;
                pnlSideContainer.Visible = false;
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
            floorplanManager.SetSectionLabels();
            floorplanManager.AddSectionLabels(pnlFloorPlan);
            //sectionControlsManager = new SectionControlsManager(shiftManager.SelectedFloorplan);
            //foreach (SectionLabelControl sectionControl in sectionControlsManager.SectionControls)
            //{
            //    pnlFloorPlan.Controls.Add(sectionControl);
            //    sectionControl.BringToFront();
            //}
            //rdoViewServerFlow.Checked = true;

        }

        private void btnSaveFloorplanTemplate_Click(object sender, EventArgs e)
        {
            //var drawnLines = drawingHandler.GetDrawnLines();
            //FloorplanTemplate template = new FloorplanTemplate(shiftManager.SelectedDiningArea, txtTemplateName.Text,
            //     shiftManager.Sections, drawnLines);

            //template.SectionLines.Clear();
            //template.SectionLines.AddRange(drawnLines);

            //drawingHandler.ClearLines();
            //SqliteDataAccess.SaveFloorplanTemplate(template);


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool pickUpAdded = false;
            Section pickUpSection = new Section();
            pickUpSection.IsPickUp = true;
            //shiftManager.SelectedFloorplan = shiftManager.ViewedFloorplan;
            shiftManager.SelectedFloorplan.Date = dateTimeSelected;
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
                        tableControl.SetSection(pickUpSection);

                        pickUpSection.AddTable(tableControl.Table);

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
                SectionLabelControl sectionControl = new SectionLabelControl(pickUpSection, shiftManager.SelectedFloorplan.ServersWithoutSection);
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

                        return;
                    }

                }
                SqliteDataAccess.SaveFloorplanAndSections(shiftManager.SelectedFloorplan);
                FloorplanPrinter printer = new FloorplanPrinter(pnlFloorPlan, sectionLineManager.SectionLines);
                printer.ShowPrintPreview();
                //printer.Print();


            }
            else
            {
                MessageBox.Show("Not all sections are assigned");
            }

        }

        private void btnChooseTemplate_Click(object sender, EventArgs e)
        {
            frmTemplateSelection form = new frmTemplateSelection(shiftManager);

            form.StartPosition = FormStartPosition.CenterScreen;
            ;
            form.BringToFront();

            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                UpdateTableControlSections();
            }
            if (DialogResult == DialogResult.Cancel)
            {

            }

        }
        private void UpdateTableControlSections()
        {
            //ClearAllSectionControls();
            if (shiftManager.SelectedFloorplan == null)
            {
                flowSectionSelect.Controls.Clear();
                flowServersInFloorplan.Controls.Clear();
                ClearAllTableControlSections();
                NoServersToDisplay();
                return;
            }
            else
            {
                FillInTableControlColors();
                //sectionControlsManager = new SectionControlsManager(shiftManager.SelectedFloorplan);
                //foreach (SectionLabelControl sectionControl in sectionControlsManager.SectionControls)
                //{
                //    pnlFloorPlan.Controls.Add(sectionControl);
                //    sectionControl.BringToFront();
                //}
            }

        }
        private void FillInTableControlColors()
        {
            foreach (Control ctrl in pnlFloorPlan.Controls)
            {

                if (ctrl is TableControl tableControl)
                {
                    tableControl.BackColor = pnlFloorPlan.BackColor;
                    tableControl.TextColor = pnlFloorPlan.ForeColor;
                    foreach (Section section in shiftManager.SelectedFloorplan.Sections)
                    {

                        foreach (Table table in section.Tables)
                        {
                            if (tableControl.Table.TableNumber == table.TableNumber)
                            {
                                tableControl.SetSection(section);
                                //tableControl.BackColor = section.MuteColor(0.35f);
                                tableControl.MuteColors();
                                if (section == shiftManager.SectionSelected)
                                {
                                    tableControl.BackColor = section.MuteColor(1.35f);
                                }

                                //tableControl.ForeColor = section.FontColor;
                                tableControl.Invalidate();
                                break;
                            }
                        }
                    }
                }
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
          
            if (shiftManager.ContainsFloorplan(dateOnlySelected, cbIsAM.Checked, shiftManager.SelectedDiningArea.ID))
            {
                shiftManager.SetSelectedFloorplan(dateOnlySelected, cbIsAM.Checked, shiftManager.SelectedDiningArea.ID);

            }
            else
            {

                shiftManager.SelectedFloorplan = SqliteDataAccess.LoadFloorplanByCriteria(shiftManager.SelectedDiningArea, dateOnlySelected, cbIsAM.Checked);
            }


            if (shiftManager.SelectedFloorplan != null)
            {
                floorplanManager.AddTableControls(pnlFloorPlan);
                floorplanManager.SetSectionLabels();
                floorplanManager.SetSectionPanels();

               
                floorplanManager.AddSectionLabels(pnlFloorPlan);

                //CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
                //floorplanManager.SetTableControls();
                floorplanManager.SetSectionLabels();
                floorplanManager.SetSectionPanels();
                floorplanManager.SetServerControls();
                floorplanManager.UpdateTableControlSections(pnlFloorPlan);
                flowSectionSelect.Controls.Clear();
                flowServersInFloorplan.Controls.Clear();
                floorplanManager.AddServerControls(flowServersInFloorplan);
                floorplanManager.AddSectionPanels(flowSectionSelect);
                floorplanManager.AddSectionLabels(pnlFloorPlan);
                //UpdateServerControlsForFloorplan();
                coversImageLabel.UpdateText(shiftManager.SelectedFloorplan.MaxCoversPerServer.ToString("F0"));
                salesImageLabel.UpdateText(shiftManager.SelectedFloorplan.AvgSalesPerServer.ToString("C0"));
            }
            //floorplanManager.ShiftManager = shiftManager;
            //floorplanManager.SectionLabelRemoved += FloorplanManager_SectionLabelRemoved;
            
           
            
            
            //allTableControls = floorplanManager.TableControls;
            UpdateTableControlSections();
        }
        private void NoServersToDisplay()
        {
            coversImageLabel.UpdateText(shiftManager.SelectedDiningArea.GetMaxCovers().ToString("F0"));
            salesImageLabel.UpdateText(shiftManager.SelectedDiningArea.GetAverageCovers().ToString("C0"));

            PictureBox noSections = new PictureBox
            {
                Image = Resources._no_data_Lighter,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new System.Drawing.Size(flowSectionSelect.Width - 50, flowSectionSelect.Height - 300),
                Margin = new Padding(35, 0, 0, 0)

            };
            PictureBox noServers = new PictureBox
            {
                Image = Resources._no_data_Lighter,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new System.Drawing.Size(flowServersInFloorplan.Width - 50, flowServersInFloorplan.Height - 300),
                Margin = new Padding(35, 0, 0, 0)


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
                if (c is SectionLabelControl sectionControl)
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

                tableControl.Invalidate();
                if (sectionEdited != null)
                {
                    tableControl.Section.RemoveTable(tableControl.Table);
                    tableControl.RemoveSection();
                    UpdateSectionLabels(sectionEdited, sectionEdited.MaxCovers, sectionEdited.AverageCovers);
                }
                tableControl.BackColor = pnlFloorPlan.BackColor;  // Restore the original color
                tableControl.TextColor = pnlFloorPlan.ForeColor;



            }

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


            //sectionLineManager.AddSectionNodes(pnlFloorPlan);
            flowServersInFloorplan.Controls.Clear();
            FloorplanInfoControl fpInfo = new FloorplanInfoControl(shiftManager.SelectedFloorplan, flowServersInFloorplan.Width);
            fpInfo.UpdatePastLabels(8, 4);
            flowServersInFloorplan.Controls.Add(fpInfo);

        }
        private void btnTest2_Click(object sender, EventArgs e)
        {
            ////pnlFloorPlan.Controls.Clear();
            ////SectionLine sectionLine = new SectionLine(100,100,500,100,5f);
            ////pnlFloorPlan.Controls.Add(sectionLine);
            ////Section section = new Section();
            ////section.Number = 1;
            ////foreach (TableControl c in allTableControls)
            ////{
            ////    c.Section = section;
            ////    c.BackColor = section.Color;
            ////    section.Tables.Add(c.Table);
            ////}
            ////shiftManager.SectionSelected = section;
            ////sectionLineManager.RemoveAllLines(pnlFloorPlan);
            ////sectionLineManager.UpdateSectionLinePositions(pnlFloorPlan);
            ////sectionLineManager.AddParallelLines(pnlFloorPlan);

            //foreach (Section section in shiftManager.SelectedFloorplan.Sections)
            //{
            //    SectionNodeManager nodeManager = new SectionNodeManager(section);
            //    Node tlNode = nodeManager.GetTopLeftNode();
            //    Node trNode = nodeManager.GetTopRightNode();
            //    Node brNode = nodeManager.GetBottomRightNode();
            //    SectionLine sectionLine = new SectionLine(tlNode, trNode);
            //    SectionLine sectionLine1 = new SectionLine(trNode, brNode);
            //    pnlFloorPlan.Controls.Add(sectionLine);
            //    pnlFloorPlan.Controls.Add(sectionLine1);

            //}
            flowServersInFloorplan.Controls.Clear();
            ImageLabelControl imgControl = new ImageLabelControl(UITheme.covers, "50", flowServersInFloorplan.Width, 30);
            imgControl.BackColor = Color.Blue;
            flowServersInFloorplan.Controls.Add(imgControl);

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
           
            UpdateDateLabel(-1);
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
           
           
            UpdateDateLabel(1);
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowServersInFloorplan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rdoViewSectionFlow_CheckedChanged(object sender, EventArgs e)
        {

            if (rdoViewSectionFlow.Checked)
            {
                flowSectionSelect.Visible = true;
                flowServersInFloorplan.Visible = false;
                rdoViewSectionFlow.Image = Resources.lilCanvasBook;
                //rdoViewServerFlow.Image = Resources.lilPeople;
            }
            else
            {
                flowSectionSelect.Visible = false;
                flowServersInFloorplan.Visible = true;
                rdoViewSectionFlow.Image = Resources.lilBook;
                //rdoViewServerFlow.Image = Resources.lilPeopleCanvas;

            }
        }

        private void rdoViewServerFlow_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoViewServerFlow.Checked)
            {
                flowSectionSelect.Visible = false;
                flowServersInFloorplan.Visible = true;
                rdoViewSectionFlow.Image = Resources.lilBook;
                //rdoViewServerFlow.Image = Resources.lilPeopleCanvas;
            }
            else
            {
                flowSectionSelect.Visible = true;
                flowServersInFloorplan.Visible = false;
                rdoViewSectionFlow.Image = Resources.lilCanvasBook;
                //rdoViewServerFlow.Image = Resources.lilPeople;


            }
        }

        private void cbIsAM_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsAM.Checked)
            {
                cbIsAM.Image = Resources.smallSunrise;
                cbIsAM.BackColor = Color.FromArgb(251, 175, 0);
            }
            else
            {
                cbIsAM.Image = Resources.smallMoon;
                cbIsAM.BackColor = Color.FromArgb(117, 70, 104);

            }
        }

        private void btnSaveTemplate_Popup(object sender, PopupEventArgs e)
        {

        }

        private void pnlFloorPlan_Click(object sender, EventArgs e)
        {

        }

        private void lblDateSelected_Click(object sender, EventArgs e)
        {
            using (frmDateSelect selectDateForm = new frmDateSelect(dateTimeSelected))
            {
                selectDateForm.StartPosition = FormStartPosition.Manual;
                Point formLocation = this.PointToScreen(lblDateSelected.Location);
                formLocation.Y += lblDateSelected.Height + 50;
                formLocation.X += 465;


                // Set the location of selectDateForm
                selectDateForm.Location = formLocation;
                DialogResult = selectDateForm.ShowDialog();
                if (DialogResult == DialogResult.OK)
                {
                    this.dateTimeSelected = selectDateForm.dateSelected;
                    UpdateDateLabel(0);
                }
            }
        }
    }
}