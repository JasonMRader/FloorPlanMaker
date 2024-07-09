
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
        // TODO: keep data Updated when making changes (ie UPdate dining rooms when adding tables, update floorplan Lists
        // when saving a floorplan
        private DiningAreaManager areaCreationManager = new DiningAreaManager();
        EmployeeManager employeeManager = new EmployeeManager();
        private Shift shift { get { return this.floorplanManager.Shift; } }
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
        private frmTemplateCreator _frmTemplateCreator;
        private frmTemplateSelection _frmTemplateSelection;
        private frmSettings _frmSettings;
        private PictureBox loadingScreen = null;
        ImageLabelControl coversImageLabel = new ImageLabelControl();
        ImageLabelControl salesImageLabel = new ImageLabelControl();
        private FloorplanFormManager floorplanManager = new FloorplanFormManager();
        private bool quicklyChoosingAServer = false;
        private TableSalesManager tableSalesManager = new TableSalesManager();
        private bool _isDrawingModeEnabled = false;
        private Point? _startPoint = null;
        private List<FloorplanLine> _lines = new List<FloorplanLine>();
        public TutorialImages.TutorialType tutorialType = TutorialImages.TutorialType.Form1;
        //private bool isViewingTemplates;

        private DateOnly dateOnlySelected
        {
            get
            {
                return new DateOnly(this.dateTimeSelected.Year, this.dateTimeSelected.Month, this.dateTimeSelected.Day);
            }
        }
        private DateTime dateTimeSelected = new DateTime();

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private bool IsLunch;


        private SectionControlsManager sectionControlsManager { get; set; }
        private void SetColors()
        {
            btnCloseApp.BackColor = Color.Red;

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

            //lblCoversPerServerText.Font = AppColors.MainFont;
            //lblSalesPerServerText.Font = AppColors.MainFont;
            lblServerAverageCovers.Font = UITheme.LargeFont;
            lblServerMaxCovers.Font = UITheme.LargeFont;

            btnChooseTemplate.Font = UITheme.MainFont;

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

            if (keyData == Keys.Tab)
            {
                floorplanManager.IncrementSelectedSection();

                return true;
            }

            if (_frmEditStaff != null && _frmEditStaff.Visible)
            {
                if (keyData == Keys.Left)
                {
                    _frmEditStaff.MovedDateBack();
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    _frmEditStaff.MoveDateForward();
                    return true;
                }
            }
            else
            {
                if (keyData == Keys.Left)
                {
                    UpdateDateSelected(-1);
                    return true;
                }
                if (keyData == Keys.Right)
                {
                    UpdateDateSelected(1);
                    return true;
                }
            }
            if (_frmEditDiningAreas != null && _frmEditDiningAreas.Visible)
            {
                if (keyData == Keys.Up)
                {
                    _frmEditDiningAreas.ChangeDiningArea(keyData);
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
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            bool isShiftPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

            if (isShiftPressed)
            {
                // Handle scrolling with Shift key pressed
                if (e.Delta > 0)
                {
                    ChangeDiningAreaUp();
                }
                else if (e.Delta < 0)
                {
                    ChangeDiningAreaDown();
                }
            }
            else
            {
                // Handle normal scrolling
                if (e.Delta > 0)
                {
                    HandleScrollUp();
                }
                else if (e.Delta < 0)
                {
                    HandleScrollDown();
                }
            }
        }

        private void HandleScrollUp()
        {
            floorplanManager.DecrementSelectedSection();
        }

        private void HandleScrollDown()
        {
            floorplanManager.IncrementSelectedSection();
        }

        private void ChangeDiningAreaUp()
        {
            if (cboDiningAreas.SelectedIndex > 0)
            {
                cboDiningAreas.SelectedIndex--;
            }
            else
            {
                cboDiningAreas.SelectedIndex = cboDiningAreas.Items.Count - 1;
            }
        }

        private void ChangeDiningAreaDown()
        {
            if (cboDiningAreas.SelectedIndex < cboDiningAreas.Items.Count - 1)
            {
                cboDiningAreas.SelectedIndex++;
            }
            else
            {
                cboDiningAreas.SelectedIndex = 0;
            }
        }




        public void UpdateForm1ShiftManager(Shift shiftManagerToAdd)
        {
            dateTimeSelected = new DateTime(shiftManagerToAdd.DateOnly.Year, shiftManagerToAdd.DateOnly.Month, shiftManagerToAdd.DateOnly.Day);
            UpdateDateSelected(0);
            cbIsAM.Checked = shiftManagerToAdd.IsAM;
            foreach (Floorplan fp in shiftManagerToAdd.Floorplans)
            {
                this.shift.AddFloorplanAndServers(fp);
            }
            //Checking for doubles
            //shiftManagerToAdd.SetDoubles();
            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked);
            rdoSections.Checked = true;
            rdoViewSectionFlow.Checked = true;
            flowSectionSelect.Visible = true;
            flowServersInFloorplan.Visible = false;
            rdoViewSectionFlow.Image = Resources.lilCanvasBook;
        }


        public Form1()
        {
            InitializeComponent();
            drawingHandler = new DrawingHandler(pnlFloorPlan);
            //shift = new Shift();
            //shiftManager.ServersNotOnShift = SqliteDataAccess.LoadServers();
            this.KeyDown += pnlFloorPlan_KeyDown;
            pnlFloorPlan.MouseDown += pnlFloorplan_MouseDown;
            pnlFloorPlan.MouseUp += pnlFloorplan_MouseUp;
            pnlFloorPlan.MouseMove += pnlFloorplan_MouseMove;
            pnlFloorPlan.Paint += PnlFloorplan_Paint;
            this.sectionLineManager = new SectionLineManager(allTableControls);
            floorplanManager = new FloorplanFormManager(pnlFloorPlan, flowServersInFloorplan, flowSectionSelect, pnlMainContainer);

            // Subscribe to the event
            //floorplanManager.SectionLabelRemoved += FloorplanManager_SectionLabelRemoved;
            floorplanManager.UpdateRequired += FloorplanManager_UpdateRequired;

            //pnlFloorPlan.KeyPreview = true;
        }
        private void FloorplanManager_UpdateRequired(object sender, UpdateEventArgs e)
        {
            switch (e.ControlType)
            {
                case ControlType.SectionLabel:
                    if (e.UpdateType == UpdateType.Remove)
                    {
                        floorplanManager.RemoveSectionLabel(e.UpdateData as Section, pnlFloorPlan);
                    }
                    else
                    {

                    }

                    break;
                case ControlType.ServerControl:
                    if (e.UpdateType == UpdateType.Refresh)
                    {
                        //floorplanManager.UpdateServerControlsInFlowPanel()
                    }
                    break;
                case ControlType.SectionPanel:
                    if (e.UpdateType == UpdateType.Remove)
                    {
                        floorplanManager.RemoveSectionPanel(e.UpdateData as Section, flowSectionSelect);
                    }
                    else if (e.UpdateType == UpdateType.Add)
                    {
                        floorplanManager.AddSectionPanel(e.UpdateData as Section, flowSectionSelect);
                    }
                    else if (e.UpdateType == UpdateType.Assign)
                    {
                        quicklyChoosingAServer = true;
                        rdoViewServerFlow.Checked = true;
                        SubscribeToChildrenClick(flowServersInFloorplan);
                    }
                    break;
                case ControlType.TableControl:

                    floorplanManager.SetSectionLabels();
                    floorplanManager.AddSectionLabels(pnlFloorPlan);

                    break;

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
            foreach (var line in _lines)
            {
                using (Pen pen = new Pen(line.LineColor, line.LineThickness))
                {
                    e.Graphics.DrawLine(pen, line.StartPoint, line.EndPoint);
                }
            }

            if (_isDrawingModeEnabled && _startPoint.HasValue)
            {
                using (Pen pen = new Pen(Color.Gray, 2.0f))
                {
                    e.Graphics.DrawLine(pen, _startPoint.Value, pnlFloorPlan.PointToClient(Cursor.Position));
                }
            }
            if (floorplanManager.Floorplan != null)
            {
                if (floorplanManager.Floorplan.floorplanLines.Count > 0)
                {
                    foreach (var line in floorplanManager.Floorplan.floorplanLines)
                    {
                        using (Pen pen = new Pen(line.LineColor, line.LineThickness))
                        {
                            e.Graphics.DrawLine(pen, line.StartPoint, line.EndPoint);
                        }
                    }
                }
            }
        }

        private void pnlFloorplan_MouseDown(object sender, MouseEventArgs e)
        {
            if (!rdoDiningAreas.Checked)
            {
                if (rdoSections.Checked && !_isDrawingModeEnabled)
                {
                    isDragging = true;
                    dragStartPoint = e.Location;
                }
            }
            if (_isDrawingModeEnabled)
            {
                _startPoint = e.Location;
            }
        }

        private void pnlFloorplan_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging && !_isDrawingModeEnabled)
            {
                isDragging = false;
                dragRectangle = new Rectangle(
                    Math.Min(dragStartPoint.X, e.X),
                    Math.Min(dragStartPoint.Y, e.Y),
                    Math.Abs(dragStartPoint.X - e.X),
                    Math.Abs(dragStartPoint.Y - e.Y));



                var selectedTables = floorplanManager.TableControls
                    .Where(tc => dragRectangle.IntersectsWith(new Rectangle(tc.Location, tc.Size)))
                    .ToList();

                floorplanManager.SelectTables(selectedTables);

                pnlFloorPlan.Invalidate();
            }
            if (_isDrawingModeEnabled && _startPoint.HasValue)
            {
                _lines.Add(new FloorplanLine(_startPoint.Value, e.Location));
                _startPoint = null;
                pnlFloorPlan.Invalidate();
            }
        }

        private void pnlFloorplan_MouseMove(object sender, MouseEventArgs e)
        {
            if (!rdoDiningAreas.Checked)
            {
                if (isDragging && !_isDrawingModeEnabled)
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
            if (_isDrawingModeEnabled && _startPoint.HasValue)
            {
                // Redraw the panel to show the line while drawing
                pnlFloorPlan.Invalidate();
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


            cboDiningAreas.DataSource = areaCreationManager.DiningAreas;
            cboDiningAreas.DisplayMember = "Name";
            cboDiningAreas.ValueMember = "ID";
            rdoSections.Checked = true;
            rdoViewSectionFlow.Checked = true;
            pnlFloorPlan.BackgroundImage = null;
            pnlFloorPlan.Invalidate();
            UpdateDateSelected(0);
            coversImageLabel.SetTooltip("Covers per Server");
            salesImageLabel.SetTooltip("Sales Per Server");
            //rdoViewSectionFlow.Checked = true;
            rdoLastFourWeekdayStats.Text = "Last Four " + dateOnlySelected.DayOfWeek.ToString() + "s";
        }
        private void UpdateDateSelected(int days)
        {
            _lines.Clear();
            pnlFloorPlan.Invalidate();
            dateTimeSelected = dateTimeSelected.AddDays(days);
            lblDateSelected.Text = dateOnlySelected.ToString("ddd, MMM d");
            SpecialEventDate specialEventDate = SqliteDataAccess.GetEventByDate(dateOnlySelected);
            if (specialEventDate != null)
            {
                lblDateSelected.Text = specialEventDate.Name;
            }
            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked);
            if (floorplanManager.Floorplan == null)
            {
                NoServersToDisplay();
                pnlFloorPlan.BackgroundImage = null;
                // foreach()

            }
            else if (AllTablesAreAssigned())
            {  //TODO SECTIONBOARDERS DISABLED
               // CreateSectionBorders();
            }
            else
            {
                pnlFloorPlan.BackgroundImage = null;
            }
            rdoLastFourWeekdayStats.Text = "Last Four " + dateOnlySelected.DayOfWeek.ToString() + "s";

            updateSalesForTables();
            //float areaSales = floorplanManager.Floorplan.DiningArea.GetAverageSales();

        }
        public void UpdateWithTemplate()
        {
            btnAutomatic.Enabled = true;
            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked);
            _lines.Clear();
            _lines = floorplanManager.Floorplan.floorplanLines;

            //CreateSectionBorders();
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
            //_lines.Clear();
            shift.SelectedDiningArea = (DiningArea?)cboDiningAreas.SelectedItem;
            floorplanManager.AddTableControls(pnlFloorPlan);

            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked);
            //this.sectionLineManager = new SectionLineManager(allTableControls);
            if (floorplanManager.Floorplan != null)
            {
                _lines = floorplanManager.Floorplan.floorplanLines;
            }

            if (AllTablesAreAssigned())
            {
                //TODO SECTION BOARDERS DISABLED
                // CreateSectionBorders();
            }
            else
            {
                pnlFloorPlan.BackgroundImage = null;
            }
            updateSalesForTables();

        }

        private void rdoSections_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSections.Checked)
            {
                this.tutorialType = TutorialImages.TutorialType.Form1;
                pnlNavigationWindow.SendToBack();
                pnlNavHighlight.Location = new Point(rdoSections.Left, 0);
                pnlMainContainer.Visible = true;
                pnlSideContainer.Visible = true;
                flowServersInFloorplan.Visible = true;
                foreach (Control control in pnlFloorPlan.Controls)
                {
                    if (control is TableControl tableControl)
                    {
                        tableControl.Moveable = false;
                    }
                }
                if (rdoViewSectionFlow.Checked)
                {
                    flowSectionSelect.Visible = true;
                    flowServersInFloorplan.Visible = false;
                    pnlStatMode.Visible = false;
                    rdoViewSectionFlow.Image = Resources.lilCanvasBook;

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
                this.tutorialType = TutorialImages.TutorialType.EditDistribution;
                pnlNavHighlight.Location = new Point(rdoShifts.Left, 0);
                if (_frmEditStaff == null)
                {
                    _frmEditStaff = new frmEditStaff(employeeManager, shift, this) { TopLevel = false, AutoScroll = true };
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
        private void rdoSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSettings.Checked)
            {
                this.tutorialType = TutorialImages.TutorialType.Settings;
                pnlNavHighlight.Location = new Point(rdoSettings.Left, 0);
                pnlNavHighlight.Width = rdoSettings.Width;
                if (_frmSettings == null)
                {
                    _frmSettings = new frmSettings { TopLevel = false, AutoScroll = true };
                }
                pnlNavigationWindow.Controls.Add(_frmSettings);

                _frmSettings.Show();
                pnlNavigationWindow.BringToFront();

            }
            else
            {
                if (_frmSettings != null)
                {
                    pnlNavHighlight.Width = 160;
                    _frmSettings.Hide();
                }
            }
        }

        private void btnAddSectionLabels_Click(object sender, EventArgs e)
        {
            floorplanManager.SetSectionLabels();
            floorplanManager.AddSectionLabels(pnlFloorPlan);
            //CreateSectionBorders();

        }

        private void btnSaveFloorplanTemplate_Click(object sender, EventArgs e)
        {

            FloorplanTemplate template = new FloorplanTemplate(shift.SelectedFloorplan);
            if (_lines.Count > 0)
            {
                template.floorplanLines = _lines;
            }
            if (template.IsDuplicate())
            {
                DialogResult result = MessageBox.Show("This Template Already Exists, Do you Want to Update the Section Lines?",
                                                " Floorplan Exists",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }
                else
                {

                    template.ID = template.duplicateTemplate().ID;

                    SqliteDataAccess.UpdateTemplateLines(template.ID, _lines);
                    MessageBox.Show("Lines updated");
                }
            }
            else
            {
                SqliteDataAccess.SaveFloorplanTemplate(template);
                MessageBox.Show("Template Saved!");
            }
            this.pnlFloorPlan.Invalidate();

        }
        private void btnChooseTemplate_Click(object sender, EventArgs e)
        {
            btnAutomatic.Enabled = false;
            floorplanManager.TemplateManager = new TemplateManager(floorplanManager.Shift.SelectedDiningArea);


            _frmTemplateSelection = new frmTemplateSelection(floorplanManager, shift.SelectedDiningArea, this)
            { TopLevel = false, AutoScroll = true };
            pnlTemplateContainer.Controls.Add(_frmTemplateSelection);


            _frmTemplateSelection.Show();
            pnlTemplateContainer.BringToFront();

        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool pickUpAdded = false;
            Section pickUpSection = new Section(floorplanManager.Floorplan);
            pickUpSection.IsPickUp = true;
            //shiftManager.SelectedFloorplan = shiftManager.ViewedFloorplan;
            //shift = floorplanManager.Shift;
            shift.SelectedFloorplan.Date = dateTimeSelected;
            shift.SelectedFloorplan.IsLunch = cbIsAM.Checked;
            foreach (Control control in pnlFloorPlan.Controls)
            {
                if (control is TableControl tableControl)
                {
                    if (tableControl.Section == null)
                    {
                        pickUpSection.DiningAreaID = shift.SelectedFloorplan.DiningArea.ID;
                        pickUpSection.Name = "Pick Up";
                        shift.SelectedFloorplan.Sections.Add(pickUpSection);
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
                //UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
                //CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
                SectionLabelControl sectionControl = new SectionLabelControl(pickUpSection, shift.SelectedFloorplan.ServersWithoutSection, shift.ServersOnShift);
                pnlFloorPlan.Controls.Add(sectionControl);
                sectionControl.BringToFront();
            }
            if (shift.SelectedFloorplan.CheckIfAllSectionsAssigned())
            {
                if (!shift.SelectedFloorplan.CheckIfCloserIsAssigned())
                {
                    DialogResult result = MessageBox.Show("There is not a closer assigned. \n Continue anyway?",
                                                "Continue?",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                if (cbTableDisplayMode.Checked)
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
                SqliteDataAccess.SaveFloorplanAndSections(shift.SelectedFloorplan);


                //TODO SECTIONLINES DISABLED

                //DialogResult printWihtLines = MessageBox.Show("Do you want to use these section lines?",
                //                            "Continue?",
                //                            MessageBoxButtons.YesNo,
                //                            MessageBoxIcon.Question);

                //if (printWihtLines == DialogResult.No)
                //{
                //    FloorplanPrinter printerNoLines = new FloorplanPrinter(pnlFloorPlan);
                //    printerNoLines.ShowPrintPreview();
                //    return;
                //}

                //TableGrid grid = new TableGrid(shiftManager.SelectedDiningArea.Tables);
                //grid.FindTableTopBottomNeighbors();
                //grid.FindTableNeighbors();
                //grid.SetTableBoarderMidPoints();
                //grid.CreateNeighbors();
                //grid.SetSections(this.shiftManager.SelectedFloorplan.Sections);
                //SectionLineDrawer edgeDrawer = new SectionLineDrawer(5f);
                //Bitmap edgesBitmap = edgeDrawer.CreateEdgeBitmap(pnlFloorPlan.Size, grid.GetSectionTableBoarders());


                ////List<Edge> edges = grid.GetNeighborEdges();
                ////Bitmap edgesBitmap = edgeDrawer.CreateEdgeBitmap(pnlFloorPlan.Size, edges);

                // pnlFloorPlan.BackgroundImage = edgesBitmap;
                //FloorplanPrinter printer = new FloorplanPrinter(pnlFloorPlan, edgeDrawer, grid.GetSectionTableBoarders());
                //printer.ShowPrintPreview();

                try
                {
                    //MessageBox.Show("Floorplan saved, but An error occurred while trying to print: " + ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FloorplanPrinter printerNoLines = new FloorplanPrinter(pnlFloorPlan, _lines);
                    printerNoLines.ShowPrintPreview();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Floorplan saved, but An error occurred while trying to print: " + ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //printer.Print();
            }
            else
            {
                MessageBox.Show("Not all sections are assigned");
            }
        }

        private void dtpFloorplan_ValueChanged(object sender, EventArgs e)
        {
            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked);
            this.sectionLineManager = new SectionLineManager(allTableControls);
        }

        private void NoServersToDisplay()
        {
            flowServersInFloorplan.Controls.Clear();
            flowSectionSelect.Controls.Clear();
            coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (flowSectionSelect.Width / 2) - 7, 30);
            salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (flowSectionSelect.Width / 2) - 7, 30);
            coversImageLabel.UpdateText(shift.SelectedDiningArea.GetMaxCovers().ToString("F0"));
            salesImageLabel.UpdateText(shift.SelectedDiningArea.GetAverageSales().ToString("C0"));
            flowSectionSelect.Controls.Add(coversImageLabel);
            flowSectionSelect.Controls.Add(salesImageLabel);
            //PictureBox noSections = new PictureBox
            //{
            //    Image = Resources._no_data_Lighter,
            //    SizeMode = PictureBoxSizeMode.Zoom,
            //    Size = new System.Drawing.Size(flowSectionSelect.Width - 50, flowSectionSelect.Height - 300),
            //    Margin = new Padding(35, 0, 0, 0)

            //};
            PictureBox noServers = new PictureBox
            {
                Image = Resources._no_data_Lighter,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new System.Drawing.Size(flowServersInFloorplan.Width - 50, flowServersInFloorplan.Height - 300),
                Margin = new Padding(35, 0, 0, 0)


            };
            Button btnCreateTemplate = new Button
            {
                Text = "Create a Floorplan Template",
                AutoSize = false,
                Size = new Size(flowSectionSelect.Width - 10, 25),
                Font = new Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.CTAColor,
                ForeColor = UITheme.CTAFontColor
            };
            btnCreateTemplate.Click += btnCreateTemplate_Click;


            noServers.BringToFront();
            flowSectionSelect.Controls.Add(btnCreateTemplate);
            flowServersInFloorplan.Controls.Add(noServers);
        }

        private void btnCreateTemplate_Click(object? sender, EventArgs e)
        {


        }

        private void btnDayBefore_Click(object sender, EventArgs e)
        {
            UpdateDateSelected(-1);
        }
        private void btnNextDay_Click(object sender, EventArgs e)
        {
            UpdateDateSelected(1);
        }
        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.BackupDatabase();
            this.Close();
        }
        private void cbTableDisplayMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTableDisplayMode.Checked)
            {
                // floorplanManager.TableControlDisplayModeToSales();
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

        private void rdoViewSectionFlow_CheckedChanged(object sender, EventArgs e)
        {

            if (rdoViewSectionFlow.Checked)
            {
                flowSectionSelect.Visible = true;
                flowServersInFloorplan.Visible = false;
                pnlStatMode.Visible = false;
                rdoViewSectionFlow.Image = Resources.lilCanvasBook;

            }
            else
            {
                flowSectionSelect.Visible = false;

                rdoViewSectionFlow.Image = Resources.lilBook;
            }
        }

        private void rdoViewServerFlow_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoViewServerFlow.Checked)
            {
                flowSectionSelect.Visible = false;
                flowServersInFloorplan.Visible = true;
                pnlStatMode.Visible = false;
                rdoViewSectionFlow.Image = Resources.lilBook;
            }
            else
            {
                //flowSectionSelect.Visible = true;
                flowServersInFloorplan.Visible = false;
                rdoViewSectionFlow.Image = Resources.lilCanvasBook;
            }
        }
        private void rdoSales_CheckedChanged(object sender, EventArgs e)
        {
            pnlStatMode.Visible = true;
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
            IsLunch = cbIsAM.Checked;
            UpdateDateSelected(0);
        }

        private void lblDateSelected_Click(object sender, EventArgs e)
        {
            using (frmDateSelect selectDateForm = new frmDateSelect(dateTimeSelected))
            {
                selectDateForm.StartPosition = FormStartPosition.Manual;
                Point formLocation = this.PointToScreen(lblDateSelected.Location);
                formLocation.Y += lblDateSelected.Height + 50;
                formLocation.X += 465;
                selectDateForm.Location = formLocation;
                DialogResult = selectDateForm.ShowDialog();
                if (DialogResult == DialogResult.OK)
                {
                    this.dateTimeSelected = selectDateForm.dateSelected;
                    UpdateDateSelected(0);
                }
            }
        }
        private void SubscribeToChildrenClick(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                // Subscribe the child control's MouseDown event
                child.MouseDown += flowServersInFloorplan_MouseClick;

                // If the child has its own children, recursively subscribe to their events as well
                if (child.HasChildren)
                {
                    SubscribeToChildrenClick(child);
                }
            }
        }
        private void flowServersInFloorplan_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender == flowServersInFloorplan)
            {
                if (quicklyChoosingAServer)
                {
                    rdoViewSectionFlow.Checked = true;
                }
                quicklyChoosingAServer = false;
            }
        }


        private void CreateSectionBorders()
        {
            TableGrid grid = new TableGrid(shift.SelectedDiningArea.Tables);
            grid.FindTableTopBottomNeighbors();
            grid.FindTableNeighbors();
            grid.SetTableBoarderMidPoints();
            grid.CreateNeighbors();
            grid.SetSections(this.shift.SelectedFloorplan.Sections);
            SectionLineDrawer edgeDrawer = new SectionLineDrawer(5f);
            Bitmap edgesBitmap = edgeDrawer.CreateEdgeBitmap(pnlFloorPlan.Size, grid.GetSectionTableBoarders());


            List<Edge> edges = grid.GetNeighborEdges();
            //Bitmap edgesBitmap = edgeDrawer.CreateEdgeBitmap(pnlFloorPlan.Size, edges);
            pnlFloorPlan.BackgroundImage = edgesBitmap;
        }
        private bool AllTablesAreAssigned()
        {
            foreach (TableControl tableControl in floorplanManager.TableControls)
            {
                if (tableControl.Section == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void btnReportBug_Click(object sender, EventArgs e)
        {
            frmReporting frmReport = new frmReporting();
            frmReport.ShowDialog();
        }
        private void SetTableSalesView()
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

            lblTotalSales.Text = floorplanManager.Shift.SelectedDiningArea.ExpectedSales.ToString("C0");

        }
        private void rdoYesterdayStats_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoYesterdayStats.Checked)
            {
                updateSalesForTables();
            }


            //tableSalesManager.SetTableStats(floorplanManager.Floorplan.DiningArea.Tables, floorplanManager.Floorplan.IsLunch, dateOnlySelected);

        }
        private void updateSalesForTables()
        {
            //TODO: exit each if statment if manager.TableStats is already set to correct stats period, otherwise it calls the methods twice
            if (rdoYesterdayStats.Checked)
            {

                floorplanManager.SetTableSalesStatsPeriod(TableSalesManager.StatsPeriod.Yesterday);
                //List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(IsLunch, dateOnlySelected.AddDays(-1));
                //floorplanManager.SetSalesManagerStats(stats);
                //floorplanManager.ShiftManager.SelectedDiningArea.TableSalesManager.SetDateToToday(dateOnlySelected);
                //floorplanManager.ShiftManager.SelectedDiningArea.ExpectedSales();
                SetTableSalesView();

            }
            if (rdoLastWeekdayStats.Checked)
            {
                floorplanManager.SetTableSalesStatsPeriod(TableSalesManager.StatsPeriod.LastWeekday);
                //List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(IsLunch, dateOnlySelected.AddDays(-7));
                //floorplanManager.SetSalesManagerStats(stats);
                SetTableSalesView();
            }
            if (rdoLastFourWeekdayStats.Checked)
            {
                floorplanManager.SetTableSalesStatsPeriod(TableSalesManager.StatsPeriod.LastFourWeekDays);
                var day = dateOnlySelected;


                var previousWeekdays = new List<DateOnly>();


                for (int i = 1; i <= 4; i++)
                {

                    previousWeekdays.Add(day.AddDays(-7 * i));
                }


                // List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateListAndLunch(IsLunch, previousWeekdays);
                //floorplanManager.SetSalesManagerStats(stats);
                SetTableSalesView();
            }
            if (rdoDayOfStats.Checked)
            {
                floorplanManager.SetTableSalesStatsPeriod(TableSalesManager.StatsPeriod.Today);
                // List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(IsLunch, dateOnlySelected);
                //floorplanManager.SetSalesManagerStats(stats);
                SetTableSalesView();
            }
            //if(floorplanManager.Floorplan != null) {
            //    floorplanManager.UpdateAveragesPerServer();
            //}
            //coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (flowSectionSelect.Width / 2) - 7, 30);
            //salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (flowSectionSelect.Width / 2) - 7, 30);
            coversImageLabel.UpdateText(shift.SelectedDiningArea.GetMaxCovers().ToString("F0"));
            salesImageLabel.UpdateText(shift.SelectedDiningArea.GetAverageSales().ToString("C0"));
            if (floorplanManager.Floorplan != null && floorplanManager.Floorplan.Sections.Count > 0)
            {
                foreach (Section section in floorplanManager.Floorplan.Sections)
                {
                    section.Notify();
                }
            }



        }
        private void rdoLastWeekdayStats_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLastWeekdayStats.Checked)
            {
                updateSalesForTables();
            }
        }
        private void rdoDayOfStats_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDayOfStats.Checked)
            {
                updateSalesForTables();
            }
        }
        private void rdoYearlyAverageStats_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoLastFourWeekdayStats_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLastFourWeekdayStats.Checked)
            {
                updateSalesForTables();
            }
        }

        private void rdoRangeStats_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoSelectedDatesStats_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnAddCustomDate_Click(object sender, EventArgs e)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(dtpCustomStatDateSelect.Value);
            ListBoxDateItem item = new ListBoxDateItem(dateOnly);
            lbFilteredStatDates.Items.Add(item);
        }

        private void btnApplyDates_Click(object sender, EventArgs e)
        {
            List<DateOnly> dates = new List<DateOnly>();
            foreach (ListBoxDateItem item in lbFilteredStatDates.Items)
            {
                dates.Add(item.DateValue);
            }
            //List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateListAndLunch(IsLunch, dates);
            //floorplanManager.SetSalesManagerStats(stats);
            SetTableSalesView();

        }

        private void btnClearDates_Click(object sender, EventArgs e)
        {
            lbFilteredStatDates.Items.Clear();
        }

        private void btnDeleteSelectedFloorplan_Click(object sender, EventArgs e)
        {
            frmConfirmation confirmationForm = new frmConfirmation();
            DialogResult result = confirmationForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                SqliteDataAccess.DeleteFloorplan(floorplanManager.Floorplan);
                UpdateDateSelected(0);
            }

        }

        private void cbDrawToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDrawToggle.Checked == true)
            {
                _isDrawingModeEnabled = true;
                _lines.Clear();
                pnlFloorPlan.Invalidate();
            }
            else { _isDrawingModeEnabled = false; }
        }

        private void btnTemplateCreator_Click(object sender, EventArgs e)
        {
            frmTemplateCreator frm = new frmTemplateCreator(shift.SelectedDiningArea, this) { TopLevel = false, AutoScroll = true };

            pnlNavigationWindow.Controls.Add(frm);
            frm.Show();
            pnlMainContainer.Visible = false;
            //pnlSideBar.Visible = false;
            pnlSideContainer.Visible = false;
            pnlNavigationWindow.BringToFront();
        }
        public void CloseTemplateCreator()
        {
            rdoSections.Checked = true;
            rdoViewSectionFlow.Checked = true;
            flowSectionSelect.Visible = true;
            flowServersInFloorplan.Visible = false;
            pnlMainContainer.Visible = true;
            pnlSideContainer.Visible = true;
            rdoViewSectionFlow.Image = Resources.lilCanvasBook;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            frmTutorialVideos tutorialForm = new frmTutorialVideos(this.tutorialType);
            tutorialForm.Show();
        }

        private void btnAutomatic_Click(object sender, EventArgs e)
        {
            if (floorplanManager.Floorplan == null)
            {
                rdoShifts.PerformClick();
            }
            else if (!floorplanManager.AllTablesAreAssigned())
            {
                btnChooseTemplate.PerformClick();
            }

            else if (floorplanManager.AllTablesAreAssigned())
            {
                floorplanManager.AutoAssignSections();
            }
        }

        private void btnEraseAllSections_Click(object sender, EventArgs e)
        {
            if(floorplanManager.Floorplan == null) { return; }
            DialogResult result = MessageBox.Show("Do you want to unassign all tables and sections?",
                                                "Clear Sections?",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }
            if (result == DialogResult.Yes)
            {
                floorplanManager.ResetSections();
            }
        }
    }
}