
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
using System.Xml.Linq;



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
        private ToolTip testToolTip;
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
        private bool _autoDrawEnabled = false;
        private Point? _startPoint = null;
        private SalesDataUpdater salesDataUpdater = new SalesDataUpdater();
        private List<FloorplanLine> _lines = new List<FloorplanLine>();
        public TutorialImages.TutorialType tutorialType = TutorialImages.TutorialType.Form1;
        private DiningAreaButtonHandeler diningAreaButtonHandeler { get; set; }
        public ShiftDetailOverviewManager shiftDetailManager { get; set; }
        //private bool isViewingTemplates;

        private DateOnly dateOnlySelected {
            get {
                return new DateOnly(this.dateTimeSelected.Year, this.dateTimeSelected.Month, this.dateTimeSelected.Day);
            }
        }
        private DateTime dateTimeSelected = new DateTime();

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private bool IsLunch;

        //private CustomTooltip customTooltip;
        //private PreviewTooltip previewTooltip;
        public Form1()
        {
            shiftFilterControl = new ShiftFilterControl();


            InitializeComponent();
            pnlStatMode.Controls.Add(shiftFilterControl);

            this.shiftDetailManager = new ShiftDetailOverviewManager(this.flowWeatherDisplay, this.flowResoDisplay, pnlShiftDetails,
                rdoWeather, rdoReservations, rdoSales, pnlStatMode, shiftFilterControl);
            drawingHandler = new DrawingHandler(pnlFloorPlan);
            //shift = new Shift();
            //shiftManager.ServersNotOnShift = SqliteDataAccess.LoadServers();
            this.KeyDown += pnlFloorPlan_KeyDown;
            pnlFloorPlan.MouseDown += pnlFloorplan_MouseDown;
            pnlFloorPlan.MouseUp += pnlFloorplan_MouseUp;
            pnlFloorPlan.MouseMove += pnlFloorplan_MouseMove;
            pnlFloorPlan.Paint += PnlFloorplan_Paint;
            this.sectionLineManager = new SectionLineManager(allTableControls);

            diningAreaButtonHandeler = new DiningAreaButtonHandeler(areaCreationManager.DiningAreas, flowSideDiningAreas,
                    pnlAreaIndicatorContainer, pnlAreaIndicator, pnlIndicator2);
            diningAreaButtonHandeler.DiningAreaChanged += DiningAreaSelectedChanged;

            floorplanManager = new FloorplanFormManager(this, pnlFloorPlan, flowServersInFloorplan, flowSectionSelect,
                pnlMainContainer, pnlSideContainer, sectionHeaderDisplay, diningAreaButtonHandeler,
                shiftFilterControl, sectionTabs, pnlIndicatorChild, shiftDetailManager.ShiftReservationControl);

            // Subscribe to the event
            //floorplanManager.SectionLabelRemoved += FloorplanManager_SectionLabelRemoved;

            shift.SetSelectedDiningArea(areaCreationManager.DiningAreas[0]);
            //previewTooltip = new PreviewTooltip(this.components);

            // Set tooltips for specific controls
            //previewTooltip.SetTooltip(btnEditRoster, "Move Servers To / From This Floorplan", "");
            //customTooltip = new CustomTooltip(btnEditRoster, (btnEditRoster.Width * 2), btnEditRoster.Height - 4,
            //    "Move Servers To / From This Floorplan", "");
            //this.KeyPreview = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen(this);
            splashScreen.Show();
            Application.DoEvents();

            // Wait for the splash screen to complete loading
            while (splashScreen.Visible) {
                Application.DoEvents();
                SetColors();
                dateTimeSelected = DateTime.Now;

                cboDiningAreas.DataSource = areaCreationManager.DiningAreas;
                cboDiningAreas.DisplayMember = "Name";
                cboDiningAreas.ValueMember = "ID";

                //rdoSections.Checked = true;
                rdoViewSectionFlow.Checked = true;
                pnlFloorPlan.BackgroundImage = null;
                pnlFloorPlan.Invalidate();
                if (DateTime.Now.TimeOfDay > new TimeSpan(13, 0, 0)) {
                    cbIsAM.Checked = false;
                    IsLunch = false;
                }
                else {
                    cbIsAM.Checked = true;
                    IsLunch = true;
                }
                UpdateDateSelected(0);
                coversImageLabel.SetTooltip("Covers per Server");
                salesImageLabel.SetTooltip("Sales Per Server");
                //rdoViewSectionFlow.Checked = true;
                //rdoLastFourWeekdayStats.Text = "Last Four " + dateOnlySelected.DayOfWeek.ToString() + "s";
                this.tutorialType = TutorialImages.TutorialType.EditDistribution;
                pnlNavHighlight.Location = new Point(rdoShifts.Left, 0);
                _frmEditStaff = splashScreen.LoadEditStaffForm(employeeManager, shift, this);



            }
            pnlNavigationWindow.Controls.Add(_frmEditStaff);
            _frmEditStaff.Show();
            pnlNavigationWindow.BringToFront();
            pnlMainContainer.Visible = false;
            pnlSuperFPContainer.Visible = false;
            //pnlSideBar.Visible = false;
            pnlSideContainer.Visible = false;
            UpdateMissingSalesData();

        }

        private void DiningAreaSelectedChanged(object? sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            DiningArea diningArea = (DiningArea)radioButton.Tag;
            shift.SetSelectedDiningArea(diningArea);
            //floorplanManager.UpdateTableControls();

            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked);
            //this.sectionLineManager = new SectionLineManager(allTableControls);
            if (floorplanManager.Floorplan != null) {
                _lines = floorplanManager.Floorplan.floorplanLines;
            }
            else {
                _lines.Clear();
            }

            if (AllTablesAreAssigned()) {
                //TODO SECTION BOARDERS DISABLED
                // CreateSectionBorders();
            }
            else {
                pnlFloorPlan.BackgroundImage = null;
            }
            updateSalesForTables();
        }

        public void UpdateMissingSalesData()
        {

            salesDataUpdater.SetNewDate(dateOnlySelected);
            if (this.salesDataUpdater.DatesMissingBeforeLastWeek.Count > 0) {
                lblMissingSalesData.ForeColor = Color.White;
                lblMissingSalesData.BackColor = Color.DarkRed;
                lblMissingSalesData.Text = $"{salesDataUpdater.DatesMissingDisplay}";
                btnUploadSalesData.Visible = true;
                return;
            }
            if (this.salesDataUpdater.DatesMissingLastWeek.Count > 0 && this.salesDataUpdater.DatesMissingLastWeek.Count < 7) {
                lblMissingSalesData.BackColor = Color.Gold;
                lblMissingSalesData.ForeColor = Color.Black;
                btnUploadSalesData.Visible = true;
                lblMissingSalesData.Text = $"{salesDataUpdater.DatesMissingDisplay}";
            }
            else if (this.salesDataUpdater.DatesMissingLastWeek.Count == 0) {
                lblMissingSalesData.ForeColor = Color.White;
                btnUploadSalesData.Visible = false;
                lblMissingSalesData.BackColor = Color.Green;
                lblMissingSalesData.Text = $"Up To Date";
            }
        }
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

            if (_frmEditStaff != null && _frmEditStaff.Visible) {
                if (keyData == Keys.Left) {
                    _frmEditStaff.MovedDateBack();
                    return true;
                }
                else if (keyData == Keys.Right) {
                    _frmEditStaff.MoveDateForward();
                    return true;
                }
                if (keyData == Keys.Up) {
                    //_frmEditDiningAreas.ChangeDiningArea(keyData);
                    //return true;
                }

                if (keyData == Keys.Down) {
                    // _frmEditDiningAreas.ChangeDiningArea(keyData);
                    // return true;
                }
            }
            else if (_frmTemplateSelection != null && _frmTemplateSelection.Visible) {
                if (keyData == Keys.Enter) {
                    _frmTemplateSelection.ApplyIdealTemplate();
                    return true;
                }
            }
            else {
                if (keyData == (Keys.S | Keys.Control)) {
                    SaveTheFloorplan();
                    return true;
                }
                if (keyData == Keys.Left) {
                    UpdateDateSelected(-1);
                    return true;
                }
                if (keyData == Keys.Right) {
                    UpdateDateSelected(1);
                    return true;
                }
                if (keyData == Keys.Tab) {
                    floorplanManager.IncrementSelectedSection();

                    return true;
                }
                if (keyData == Keys.Up) {
                    if (cboDiningAreas.SelectedIndex > 0) {
                        cboDiningAreas.SelectedIndex--;
                    }
                    else {
                        cboDiningAreas.SelectedIndex = cboDiningAreas.Items.Count - 1;
                    }
                    return true;
                }

                if (keyData == Keys.Down) {
                    if (cboDiningAreas.SelectedIndex < cboDiningAreas.Items.Count - 1) {
                        cboDiningAreas.SelectedIndex++;
                    }
                    else {
                        cboDiningAreas.SelectedIndex = 0;
                    }
                    return true;
                }
                if (keyData == Keys.T) {
                    if (floorplanManager.Floorplan != null) {
                        if (floorplanManager.Floorplan.SectionSelected != null) {
                            floorplanManager.SectionTeamwaitToggle(floorplanManager.Floorplan.SectionSelected);
                            return true;
                        }
                    }
                }
                if (keyData == Keys.Enter) {
                    btnAutomatic.PerformClick();
                    return true;
                }
                if (keyData == Keys.P) {
                    if (floorplanManager.Floorplan != null) {
                        floorplanManager.AddPickupSection();
                        return true;
                    }
                }
                if (keyData == Keys.Escape) {
                    if (_autoDrawEnabled) {
                        _startPoint = null;
                        _autoDrawEnabled = false;
                        pnlFloorPlan.Invalidate();
                    }
                    return true;
                }
                //if (keyData == Keys.I) {

                //    previewTooltip.ShowAllTooltips(this);
                //    return true;
                //}
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            bool isShiftPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

            if (isShiftPressed) {
                // Handle scrolling with Shift key pressed
                //if (e.Delta > 0)
                //{
                //    //ChangeDiningAreaUp();
                //    //diningAreaButtonHandeler.ChangeDiningAreaUp();
                //    if (shift.SelectedDiningArea != areaCreationManager.DiningAreas.First())
                //    {
                //        int indexOfSelected = areaCreationManager.DiningAreas.IndexOf(shift.SelectedDiningArea);
                //        shift.SetSelectedDiningArea(areaCreationManager.DiningAreas[indexOfSelected - 1]);

                //    }
                //    else
                //    {
                //        shift.SetSelectedDiningArea(areaCreationManager.DiningAreas.Last());
                //    }


                //}
                //else if (e.Delta < 0)
                //{
                //    //ChangeDiningAreaDown();
                //    //diningAreaButtonHandeler.ChangeDiningAreaDown();
                //    if (shift.SelectedDiningArea != areaCreationManager.DiningAreas.Last())
                //    {
                //        int indexOfSelected = areaCreationManager.DiningAreas.IndexOf(shift.SelectedDiningArea);
                //        shift.SetSelectedDiningArea(areaCreationManager.DiningAreas[indexOfSelected + 1]);
                //    }
                //    else
                //    {
                //        shift.SetSelectedDiningArea(areaCreationManager.DiningAreas[0]);
                //    }
                //}
                //floorplanManager.ChangeDiningAreaSelected();
            }
            else {
                // Handle normal scrolling
                if (e.Delta > 0) {
                    HandleScrollUp();
                }
                else if (e.Delta < 0) {
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
            if (cboDiningAreas.SelectedIndex > 0) {
                cboDiningAreas.SelectedIndex--;
            }
            else {
                cboDiningAreas.SelectedIndex = cboDiningAreas.Items.Count - 1;
            }
        }

        private void ChangeDiningAreaDown()
        {
            if (cboDiningAreas.SelectedIndex < cboDiningAreas.Items.Count - 1) {
                cboDiningAreas.SelectedIndex++;
            }
            else {
                cboDiningAreas.SelectedIndex = 0;
            }
        }

        public void UpdateForm1ShiftManager(Shift shiftManagerToAdd)
        {
            dateTimeSelected = new DateTime(shiftManagerToAdd.DateOnly.Year, shiftManagerToAdd.DateOnly.Month, shiftManagerToAdd.DateOnly.Day);
            UpdateDateSelected(0);
            cbIsAM.Checked = shiftManagerToAdd.IsAM;
            foreach (Floorplan fp in shiftManagerToAdd.Floorplans) {
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
            //diningAreaButtonHandeler.UpdateForShift(shift);
        }




        private void PnlFloorplan_Paint(object sender, PaintEventArgs e)
        {
            if (isDragging) {
                using (Pen dragPen = new Pen(Color.Black) { DashStyle = DashStyle.Dash }) // example styling
                {
                    e.Graphics.DrawRectangle(dragPen, dragRectangle);
                }
            }
            foreach (var line in _lines) {
                using (Pen pen = new Pen(line.LineColor, line.LineThickness)) {
                    e.Graphics.DrawLine(pen, line.StartPoint, line.EndPoint);
                }
            }

            if (_isDrawingModeEnabled && _startPoint.HasValue) {
                using (Pen pen = new Pen(Color.Gray, 2.0f)) {
                    e.Graphics.DrawLine(pen, _startPoint.Value, pnlFloorPlan.PointToClient(Cursor.Position));
                }
            }
            if (_autoDrawEnabled && _startPoint.HasValue) {
                using (Pen pen = new Pen(Color.Gray, 2.0f)) {
                    e.Graphics.DrawLine(pen, _startPoint.Value, pnlFloorPlan.PointToClient(Cursor.Position));
                }
            }
            if (floorplanManager.Floorplan != null) {
                if (floorplanManager.Floorplan.floorplanLines.Count > 0) {
                    foreach (var line in floorplanManager.Floorplan.floorplanLines) {
                        using (Pen pen = new Pen(line.LineColor, line.LineThickness)) {
                            e.Graphics.DrawLine(pen, line.StartPoint, line.EndPoint);
                        }
                    }
                }
            }
        }
        private void pnlFloorPlan_MouseClick(object sender, MouseEventArgs e)
        {
            if (e is MouseEventArgs mouseEventArgs) {
                bool isShiftPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                if (mouseEventArgs.Button == MouseButtons.Right) {
                    if (!_autoDrawEnabled) {
                        _startPoint = e.Location;
                        _autoDrawEnabled = true;
                        return;
                    }

                    if (_autoDrawEnabled && _startPoint.HasValue) {
                        _lines.Add(new FloorplanLine(_startPoint.Value, e.Location));
                        _startPoint = e.Location;
                        pnlFloorPlan.Invalidate();
                    }
                }
                if (mouseEventArgs.Button == MouseButtons.Left && _autoDrawEnabled) {
                    _lines.Add(new FloorplanLine(_startPoint.Value, e.Location));
                    _startPoint = null;
                    _autoDrawEnabled = false;
                    pnlFloorPlan.Invalidate();
                }
            }
        }
        private void ToggleAutoDrawMode()
        {
            Point cursorPosition = pnlFloorPlan.PointToClient(Cursor.Position);

            if (pnlFloorPlan.ClientRectangle.Contains(cursorPosition)) {
                _autoDrawEnabled = !_autoDrawEnabled;
                if (_autoDrawEnabled) {
                    _startPoint = cursorPosition;
                }
                else {

                    _startPoint = null;
                    pnlFloorPlan.Invalidate();
                }

            }

        }
        private void pnlFloorplan_MouseDown(object sender, MouseEventArgs e)
        {
            if (!rdoDiningAreas.Checked) {
                if (rdoSections.Checked && !_isDrawingModeEnabled) {
                    isDragging = true;
                    dragStartPoint = e.Location;
                }
            }
            if (_isDrawingModeEnabled) {
                _startPoint = e.Location;
            }

        }

        private void pnlFloorplan_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging && !_isDrawingModeEnabled) {
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
            if (_isDrawingModeEnabled && _startPoint.HasValue) {
                _lines.Add(new FloorplanLine(_startPoint.Value, e.Location));
                _startPoint = null;
                pnlFloorPlan.Invalidate();
            }
        }

        private void pnlFloorplan_MouseMove(object sender, MouseEventArgs e)
        {
            if (!rdoDiningAreas.Checked) {
                if (isDragging && !_isDrawingModeEnabled) {
                    dragRectangle = new Rectangle(
                        Math.Min(dragStartPoint.X, e.X),
                        Math.Min(dragStartPoint.Y, e.Y),
                        Math.Abs(dragStartPoint.X - e.X),
                        Math.Abs(dragStartPoint.Y - e.Y)
                    );

                    pnlFloorPlan.Invalidate(); // Redraw the form
                }
            }
            if (_isDrawingModeEnabled && _startPoint.HasValue) {
                // Redraw the panel to show the line while drawing
                pnlFloorPlan.Invalidate();
            }
            if (_autoDrawEnabled && _startPoint.HasValue) {
                pnlFloorPlan.Invalidate();
            }
        }


        private void pnlFloorPlan_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z) {
                drawingHandler.UndoLastPoint();
            }
        }

        public void SetDateSelected(DateTime date)
        {
            this.dateTimeSelected = date;
            UpdateDateSelected(0);
        }
        private void UpdateDateSelected(int days)
        {
            _lines.Clear();
            pnlFloorPlan.Invalidate();
            dateTimeSelected = dateTimeSelected.AddDays(days);
            lblDateSelected.Text = dateOnlySelected.ToString("ddd, MMM d");
            SpecialEventDate specialEventDate = SqliteDataAccess.GetEventByDate(dateOnlySelected);
            if (specialEventDate != null) {
                lblDateSelected.Text = specialEventDate.Name;
            }
            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked);
            if (floorplanManager.Floorplan == null) {
                NoServersToDisplay();
                pnlFloorPlan.BackgroundImage = null;


            }
            else if (AllTablesAreAssigned()) {  //TODO SECTIONBOARDERS DISABLED
                                                // CreateSectionBorders();
            }
            else {
                pnlFloorPlan.BackgroundImage = null;
            }
            //***************************************************************************************************
            //rdoLastFourWeekdayStats.Text = "Last Four " + dateOnlySelected.DayOfWeek.ToString() + "s";
            updateSalesForTables();
            UpdateSidePanelDisplay();
            UpdateMissingSalesData();
            //diningAreaButtonHandeler.UpdateForShift(shift);
        }
        public void UpdateWeatherDataLoaded()
        {
            this.shiftDetailManager.ForceUpdateForDate(dateOnlySelected, IsLunch);
            floorplanManager.SetViewedFloorplan(dateOnlySelected, IsLunch);
            floorplanManager.UpdateStats();
        }
        public void UpdateSidePanelDisplay()
        {
            if (floorplanManager.Shift == null) {
                this.shiftDetailManager.UpdateForNewDate(dateOnlySelected, IsLunch);
            }
            else {
                this.shiftDetailManager.SetNewShift(floorplanManager.Shift);
            }

        }
        public void UpdateWithTemplate()
        {
            btnAutomatic.Enabled = true;
            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked);
            _lines.Clear();
            if (floorplanManager.Floorplan != null) {
                _lines = floorplanManager.Floorplan.floorplanLines;
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isDraggingForm = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingForm) {
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
        //private void cboDiningAreas_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //_lines.Clear();
        //    shift.SetSelectedDiningArea((DiningArea?)cboDiningAreas.SelectedItem);
        //    floorplanManager.AddTableControls(pnlFloorPlan);

        //    floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked);
        //    //this.sectionLineManager = new SectionLineManager(allTableControls);
        //    if (floorplanManager.Floorplan != null)
        //    {
        //        _lines = floorplanManager.Floorplan.floorplanLines;
        //    }
        //    else
        //    {
        //        _lines.Clear();
        //    }

        //    if (AllTablesAreAssigned())
        //    {
        //        //TODO SECTION BOARDERS DISABLED
        //        // CreateSectionBorders();
        //    }
        //    else
        //    {
        //        pnlFloorPlan.BackgroundImage = null;
        //    }
        //    updateSalesForTables();

        //}

        private void rdoSections_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSections.Checked) {
                this.tutorialType = TutorialImages.TutorialType.Form1;
                pnlNavigationWindow.SendToBack();
                pnlNavHighlight.Location = new Point(rdoSections.Left, 0);
                pnlSuperFPContainer.Visible = true;
                pnlMainContainer.Visible = true;
                pnlSideContainer.Visible = true;
                flowServersInFloorplan.Visible = true;
                foreach (Control control in pnlFloorPlan.Controls) {
                    if (control is TableControl tableControl) {
                        tableControl.Moveable = false;
                    }
                }
                if (rdoViewSectionFlow.Checked) {
                    flowSectionSelect.Visible = true;
                    flowServersInFloorplan.Visible = false;
                    //pnlStatMode.Visible = false;
                    rdoViewSectionFlow.Image = Resources.lilCanvasBook;

                }
            }
            else {
                pnlMainContainer.Visible = false;
                pnlSuperFPContainer.Visible = false;
                //pnlSideBar.Visible = false;
                pnlSideContainer.Visible = false;
            }
        }
        private void rdoShifts_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoShifts.Checked) {
                this.tutorialType = TutorialImages.TutorialType.EditDistribution;
                pnlNavHighlight.Location = new Point(rdoShifts.Left, 0);
                if (_frmEditStaff == null) {
                    _frmEditStaff = new frmEditStaff(employeeManager, shift, this) { TopLevel = false, AutoScroll = true };
                }
                pnlNavigationWindow.Controls.Add(_frmEditStaff);
                _frmEditStaff.Show();
                pnlNavigationWindow.BringToFront();
            }
            else {
                if (_frmEditStaff != null) {
                    _frmEditStaff.Hide();
                }
            }
        }
        private void rdoDiningAreas_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDiningAreas.Checked) {
                pnlNavHighlight.Location = new Point(rdoDiningAreas.Left, 0);
                if (_frmEditDiningAreas == null) {
                    _frmEditDiningAreas = new frmEditDiningAreas { TopLevel = false, AutoScroll = true };
                }
                pnlNavigationWindow.Controls.Add(_frmEditDiningAreas);

                _frmEditDiningAreas.Show();
                pnlNavigationWindow.BringToFront();
                pnlSideDetails.Controls.Add(_frmEditDiningAreas.diningAreaInfoControl);
                _frmEditDiningAreas.diningAreaInfoControl.BringToFront();

            }
            else {
                if (_frmEditDiningAreas != null) {
                    _frmEditDiningAreas.Hide();
                    pnlSideDetails.Controls.Remove(_frmEditDiningAreas.diningAreaInfoControl);
                }
            }
        }
        private void rdoSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSettings.Checked) {
                this.tutorialType = TutorialImages.TutorialType.Settings;
                pnlNavHighlight.Location = new Point(rdoSettings.Left, 0);
                pnlNavHighlight.Width = rdoSettings.Width;
                if (_frmSettings == null) {
                    _frmSettings = new frmSettings { TopLevel = false, AutoScroll = true };
                }
                pnlNavigationWindow.Controls.Add(_frmSettings);

                _frmSettings.Show();
                pnlNavigationWindow.BringToFront();

            }
            else {
                if (_frmSettings != null) {
                    pnlNavHighlight.Width = 160;
                    _frmSettings.Hide();
                }
            }
        }

        private void btnAddSectionLabels_Click(object sender, EventArgs e)
        {
            //floorplanManager.SetSectionLabels();
            //floorplanManager.AddSectionLabels(pnlFloorPlan);
            //CreateSectionBorders();

        }

        private void btnSaveFloorplanTemplate_Click(object sender, EventArgs e)
        {
            if (shift.SelectedFloorplan == null) {
                MessageBox.Show("There Must Be A Floorplan To Save A Template");
                return;
            }
            FloorplanTemplate template = new FloorplanTemplate(shift.SelectedFloorplan);
            if (!floorplanManager.AllTablesAreAssigned()) {
                MessageBox.Show("All Tables Must Be Assigned To Save A Template");
                return;
            }
            if (_lines.Count > 0) {
                template.floorplanLines = _lines;
            }
            if (template.IsDuplicate()) {
                DialogResult result = MessageBox.Show("This Template Already Exists, Do you Want to Update the Section Lines?",
                                                " Floorplan Exists",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

                if (result == DialogResult.No) {
                    return;
                }
                else {

                    template.ID = template.duplicateTemplate().ID;

                    SqliteDataAccess.UpdateTemplateLines(template.ID, _lines);
                    MessageBox.Show("Lines updated");
                }
            }
            else {
                SqliteDataAccess.SaveFloorplanTemplate(template);
                MessageBox.Show("Template Saved!");
            }
            this.pnlFloorPlan.Invalidate();

        }
        private void btnChooseTemplate_Click(object sender, EventArgs e)
        {
            btnAutomatic.Enabled = false;
            floorplanManager.TemplateManager = new TemplateManager(floorplanManager.Shift.SelectedDiningArea);


            _frmTemplateSelection = new frmTemplateSelection(floorplanManager, shift.SelectedDiningArea, this) { TopLevel = false, AutoScroll = true };
            pnlTemplateContainer.Controls.Add(_frmTemplateSelection);


            _frmTemplateSelection.Show();
            pnlTemplateContainer.BringToFront();

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
            PictureBox noServers = new PictureBox {
                Image = Resources._no_data_Lighter,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new System.Drawing.Size(flowServersInFloorplan.Width - 50, flowServersInFloorplan.Height - 300),
                Margin = new Padding(35, 0, 0, 0)


            };
            Button btnCreateTemplate = new Button {
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
            floorplanManager.Shift.SelectedFloorplan = floorplanManager.Shift.CreateFloorplanForDiningArea(floorplanManager.Shift.SelectedDiningArea, 0, 0);

            floorplanManager.ChangeDiningAreaSelected();

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
            if (cbTableDisplayMode.Checked) {
                // floorplanManager.TableControlDisplayModeToSales();
                foreach (Control c in pnlFloorPlan.Controls) {
                    if (c is TableControl tableControl) {
                        tableControl.CurrentDisplayMode = DisplayMode.AverageCovers;
                        tableControl.Invalidate();
                    }
                }
            }
            else {
                foreach (Control c in pnlFloorPlan.Controls) {
                    if (c is TableControl tableControl) {
                        tableControl.CurrentDisplayMode = DisplayMode.TableNumber;
                        tableControl.Invalidate();
                    }
                }
            }
        }

        private void rdoViewSectionFlow_CheckedChanged(object sender, EventArgs e)
        {

            if (rdoViewSectionFlow.Checked) {
                flowSectionSelect.Visible = true;
                flowServersInFloorplan.Visible = false;
                //pnlStatMode.Visible = false;
                rdoViewSectionFlow.Image = Resources.lilCanvasBook;

            }
            else {
                flowSectionSelect.Visible = false;

                rdoViewSectionFlow.Image = Resources.lilBook;
            }
        }

        private void rdoViewServerFlow_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoViewServerFlow.Checked) {
                flowSectionSelect.Visible = false;
                flowServersInFloorplan.Visible = true;
                //pnlStatMode.Visible = false;
                rdoViewSectionFlow.Image = Resources.lilBook;
            }
            else {
                //flowSectionSelect.Visible = true;
                flowServersInFloorplan.Visible = false;
                rdoViewSectionFlow.Image = Resources.lilCanvasBook;
            }
        }
        private void rdoSales_CheckedChanged(object sender, EventArgs e)
        {
            //pnlStatMode.Visible = true;
        }

        private void cbIsAM_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsAM.Checked) {
                cbIsAM.Image = Resources.smallSunrise;
                cbIsAM.BackColor = Color.FromArgb(251, 175, 0);

            }
            else {
                cbIsAM.Image = Resources.smallMoon;
                cbIsAM.BackColor = Color.FromArgb(117, 70, 104);
            }
            IsLunch = cbIsAM.Checked;
            UpdateDateSelected(0);
        }

        private void lblDateSelected_Click(object sender, EventArgs e)
        {
            using (frmDateSelect selectDateForm = new frmDateSelect(dateTimeSelected)) {
                selectDateForm.StartPosition = FormStartPosition.Manual;
                Point startLocation = new Point(Cursor.Position.X - 50, Cursor.Position.Y);
                selectDateForm.Location = startLocation;
                DialogResult = selectDateForm.ShowDialog();
                if (DialogResult == DialogResult.OK) {
                    this.dateTimeSelected = selectDateForm.dateSelected;
                    UpdateDateSelected(0);
                }
            }
        }
        private void SubscribeToChildrenClick(Control parent)
        {
            foreach (Control child in parent.Controls) {
                // Subscribe the child control's MouseDown event
                child.MouseDown += flowServersInFloorplan_MouseClick;

                // If the child has its own children, recursively subscribe to their events as well
                if (child.HasChildren) {
                    SubscribeToChildrenClick(child);
                }
            }
        }
        private void flowServersInFloorplan_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender == flowServersInFloorplan) {
                if (quicklyChoosingAServer) {
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
            foreach (TableControl tableControl in floorplanManager.TableControls) {
                if (tableControl.Section == null) {
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
            if (cbTableDisplayMode.Checked) {
                foreach (Control c in pnlFloorPlan.Controls) {
                    if (c is TableControl tableControl) {
                        tableControl.CurrentDisplayMode = DisplayMode.AverageCovers;
                        tableControl.Invalidate();
                    }
                }
            }
            //***************************************************************************************************
            //lblTotalSales.Text = floorplanManager.Shift.SelectedDiningArea.ExpectedSales.ToString("C0");

        }
        private void rdoYesterdayStats_CheckedChanged(object sender, EventArgs e)
        {
            //***************************************************************************************************
            //if (rdoYesterdayStats.Checked) {
            //    updateSalesForTables();
            //}


            //tableSalesManager.SetTableStats(floorplanManager.Floorplan.DiningArea.Tables, floorplanManager.Floorplan.IsLunch, dateOnlySelected);

        }
        private void updateSalesForTables()
        {
            //***************************************************************************************************
            //if (rdoYesterdayStats.Checked) {

            //    floorplanManager.SetTableSalesStatsPeriod(TableSalesManager.StatsPeriod.Yesterday);

            //    SetTableSalesView();

            //}
            //if (rdoLastWeekdayStats.Checked) {
            //    floorplanManager.SetTableSalesStatsPeriod(TableSalesManager.StatsPeriod.LastWeekday);

            //    SetTableSalesView();
            //}
            //if (rdoLastFourWeekdayStats.Checked) {
            //    floorplanManager.SetTableSalesStatsPeriod(TableSalesManager.StatsPeriod.LastFourWeekDays);
            //    var day = dateOnlySelected;


            //    var previousWeekdays = new List<DateOnly>();


            //    for (int i = 1; i <= 4; i++) {

            //        previousWeekdays.Add(day.AddDays(-7 * i));
            //    }



            //    SetTableSalesView();
            //}
            //if (rdoDayOfStats.Checked) {
            //    floorplanManager.SetTableSalesStatsPeriod(TableSalesManager.StatsPeriod.Today);

            //    SetTableSalesView();
            //}

            //coversImageLabel.UpdateText(shift.SelectedDiningArea.GetMaxCovers().ToString("F0"));
            //salesImageLabel.UpdateText(shift.SelectedDiningArea.GetAverageSales().ToString("C0"));
            //if (floorplanManager.Floorplan != null && floorplanManager.Floorplan.Sections.Count > 0) {
            //    foreach (Section section in floorplanManager.Floorplan.Sections) {
            //        section.Notify();
            //    }
            //}



        }
        private void rdoLastWeekdayStats_CheckedChanged(object sender, EventArgs e)
        {
            //***************************************************************************************************
            //if (rdoLastWeekdayStats.Checked) {
            //    updateSalesForTables();
            //}
        }
        private void rdoDayOfStats_CheckedChanged(object sender, EventArgs e)
        {
            //***************************************************************************************************
            //if (rdoDayOfStats.Checked) {
            //    updateSalesForTables();
            //}
        }
        private void rdoYearlyAverageStats_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoLastFourWeekdayStats_CheckedChanged(object sender, EventArgs e)
        {
            //***************************************************************************************************
            //if (rdoLastFourWeekdayStats.Checked) {
            //    updateSalesForTables();
            //}
        }

        private void rdoRangeStats_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoSelectedDatesStats_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnAddCustomDate_Click(object sender, EventArgs e)
        {
            //***************************************************************************************************
            //DateOnly dateOnly = DateOnly.FromDateTime(dtpCustomStatDateSelect.Value);
            //ListBoxDateItem item = new ListBoxDateItem(dateOnly);
            //lbFilteredStatDates.Items.Add(item);
        }

        private void btnApplyDates_Click(object sender, EventArgs e)
        {
            //***************************************************************************************************
            //List<DateOnly> dates = new List<DateOnly>();
            //foreach (ListBoxDateItem item in lbFilteredStatDates.Items) {
            //    dates.Add(item.DateValue);
            //}
            ////List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateListAndLunch(IsLunch, dates);
            ////floorplanManager.SetSalesManagerStats(stats);
            //SetTableSalesView();

        }

        private void btnClearDates_Click(object sender, EventArgs e)
        {
            //***************************************************************************************************
            //lbFilteredStatDates.Items.Clear();
        }

        private void btnDeleteSelectedFloorplan_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want To Complete Delete This Floorplan?",
                                                "Delete Floorplan?",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (result == DialogResult.OK) {
                SqliteDataAccess.DeleteFloorplan(floorplanManager.Floorplan);
                UpdateDateSelected(0);
            }

        }

        private void cbDrawToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDrawToggle.Checked == true) {
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
            int unassignedTables = floorplanManager.NumberOfUnassignedTables();
            if (floorplanManager.Floorplan == null) {
                rdoShifts.PerformClick();
            }
            else if (unassignedTables > 5) {
                btnChooseTemplate.PerformClick();
            }
            else if (unassignedTables <= 5 && unassignedTables != 0) {
                if (floorplanManager.Floorplan == null) { return; }
                DialogResult result = MessageBox.Show("Most, but not all Tables are assigned, do you want to make them Pickup?",
                                                    "Make Unassigned Tables Pickup??",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (result == DialogResult.No) {
                    return;
                }
                if (result == DialogResult.Yes) {
                    floorplanManager.MakeUnassignedTablesPickup();
                }
            }

            else if (floorplanManager.AllTablesAreAssigned() &&
                !shift.SelectedFloorplan.CheckIfAllSectionsAssigned()) {
                floorplanManager.AutoAssignSections();
            }
            else if (floorplanManager.AllTablesAreAssigned() &&
                shift.SelectedFloorplan.CheckIfAllSectionsAssigned() &&
                !shift.SelectedFloorplan.CheckIfCloserIsAssigned()) {
                floorplanManager.AutoAssignCloser();
            }
            else if (floorplanManager.AllTablesAreAssigned() &&
                shift.SelectedFloorplan.CheckIfAllSectionsAssigned() &&
                shift.SelectedFloorplan.CheckIfCloserIsAssigned()) {
                btnPrint.PerformClick();
            }
        }

        private void btnEraseAllSections_Click(object sender, EventArgs e)
        {
            if (floorplanManager.Floorplan == null) { return; }
            DialogResult result = MessageBox.Show("Do you want to unassign all tables and sections?",
                                                "Clear Sections?",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (result == DialogResult.No) {
                return;
            }
            if (result == DialogResult.Yes) {
                floorplanManager.ResetSections();
            }
        }

        internal void SelectDiningAreaWithFirstFloorplan(Floorplan? floorplan)
        {
            DiningArea selectedDiningArea = floorplan.DiningArea;
            if (selectedDiningArea == null) {
                selectedDiningArea = shift.DiningAreasUsed[0];
            }
            foreach (var item in cboDiningAreas.Items) {
                if (item is DiningArea diningArea && diningArea.ID == selectedDiningArea.ID) {
                    cboDiningAreas.SelectedItem = item;
                    break;
                }
            }

        }

        private void btnEditRoster_Click(object sender, EventArgs e)
        {
            if (floorplanManager.Floorplan == null) { return; }
            floorplanManager.EditRosterClicked();
        }

        private void btnUploadSalesData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                string defaultDirectory = @"C:\Users\Jason\OneDrive\Source\Databases\LiveDB\Order History";
                string fallbackDirectory = @"C:\";

                // Check if the default directory exists
                if (Directory.Exists(defaultDirectory)) {
                    openFileDialog.InitialDirectory = defaultDirectory;
                }
                else {
                    openFileDialog.InitialDirectory = fallbackDirectory;
                }
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {

                    string filePath = openFileDialog.FileName;
                    frmLoading loadingForm = new frmLoading(frmLoading.GifType.DataDownload, "This Could Take Several Minutes");
                    loadingForm.Show();
                    this.Enabled = false;

                    Task.Run(() => {
                        TableSalesManager tableSalesManager = new TableSalesManager();
                        var allTableStats = tableSalesManager.ProcessCsvFile(filePath);
                        SqliteDataAccess.SaveTableStat(allTableStats);

                        this.Invoke(new Action(() => {
                            // Close the loading form and re-enable the main form
                            loadingForm.Close();
                            this.Enabled = true;
                            this.BringToFront();
                            UpdateMissingSalesData();

                        }));
                    });

                }
            }
        }

        private void MakeUnassignedTablesPickup()
        {
            bool pickUpAdded = false;
            Section pickUpSection = new Section(floorplanManager.Floorplan);
            pickUpSection.IsPickUp = true;

            shift.SelectedFloorplan.Date = dateTimeSelected;
            shift.SelectedFloorplan.IsLunch = cbIsAM.Checked;
            foreach (Control control in pnlFloorPlan.Controls) {
                if (control is TableControl tableControl) {
                    if (tableControl.Section == null) {
                        pickUpSection.DiningAreaID = shift.SelectedFloorplan.DiningArea.ID;
                        pickUpSection.Name = "Pick Up";
                        shift.SelectedFloorplan.Sections.Add(pickUpSection);
                        pickUpAdded = true;
                    }
                    break;
                }
            }
            foreach (Control control in pnlFloorPlan.Controls) {
                if (control is TableControl tableControl) {
                    if (tableControl.Section == null) {
                        tableControl.SetSection(pickUpSection);
                        pickUpSection.AddTable(tableControl.Table);
                        tableControl.BackColor = pickUpSection.Color;
                        // Optionally, you can invalidate the control to request a redraw if needed.
                        tableControl.Invalidate();
                    }
                }
            }
            if (pickUpAdded) {
                //UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
                //CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
                SectionLabelControl sectionControl = new SectionLabelControl(pickUpSection, shift.SelectedFloorplan.ServersWithoutSection, shift.ServersOnShift);
                pnlFloorPlan.Controls.Add(sectionControl);
                sectionControl.BringToFront();
            }
        }
        private bool CheckForCloserAssigned()
        {
            List<Section> nonPickupSections = shift.SelectedFloorplan.Sections.Where(s => !s.IsPickUp
               && !s.IsBarSection).ToList();
            bool hasCloserAssigned = shift.SelectedFloorplan.CheckIfCloserIsAssigned();
            bool hasMultipleServerSections = nonPickupSections.Count > 1;
            if (hasCloserAssigned) {
                return true;
            }
            if (!hasCloserAssigned && hasMultipleServerSections) {
                return false;
            }
            return true;
        }
        private bool CheckIfAllSectionsAssigned()
        {
            return shift.SelectedFloorplan.CheckIfAllSectionsAssigned();
        }
        private void SaveAndPrintNEW()
        {
            if (shift.SelectedFloorplan == null) {
                //NotificationHandler.ShowNotificationLabel(pnlFloorPlan, "No Floorplan to Print!!", UITheme.NoColor, UITheme.NoFontColor,
                //      new Point(0, 0), pnlFloorPlan.Width, 30, TimeSpan.FromSeconds(2));
                NotificationHandler.ShowNotificationOverControl(sectionHeaderDisplay, "No Floorplan to Print!!", UITheme.NoColor, UITheme.NoFontColor,
                      TimeSpan.FromSeconds(2));
                return;
            }

            MakeUnassignedTablesPickup();
            if (!CheckIfAllSectionsAssigned()) {
                MessageBox.Show("Not all sections are assigned");
                return;
            }
            if (!CheckForCloserAssigned()) {
                DialogResult result = MessageBox.Show("There is not a closer assigned. \n Continue anyway?",
                                               "Continue?",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

                if (result == DialogResult.No) {
                    return;
                }
            }
            if (cbTableDisplayMode.Checked) {
                foreach (Control c in pnlFloorPlan.Controls) {
                    if (c is TableControl tableControl) {
                        tableControl.CurrentDisplayMode = DisplayMode.TableNumber;
                        tableControl.Invalidate();
                    }
                }
            }

            PrintFloorplan();
        }
        private void SaveAndPDFNEW()
        {
            if (shift.SelectedFloorplan == null) {
                NotificationHandler.ShowNotificationLabel(pnlFloorPlan, "No Floorplan to Print!!", UITheme.NoColor, UITheme.NoFontColor,
                      new Point(0, 0), pnlFloorPlan.Width, 30, TimeSpan.FromSeconds(2));
                return;
            }
            MakeUnassignedTablesPickup();
            if (!CheckIfAllSectionsAssigned()) {
                MessageBox.Show("Not all sections are assigned");
                return;
            }
            if (!CheckForCloserAssigned()) {
                DialogResult result = MessageBox.Show("There is not a closer assigned. \n Continue anyway?",
                                               "Continue?",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

                if (result == DialogResult.No) {
                    return;
                }
            }
            if (cbTableDisplayMode.Checked) {
                foreach (Control c in pnlFloorPlan.Controls) {
                    if (c is TableControl tableControl) {
                        tableControl.CurrentDisplayMode = DisplayMode.TableNumber;
                        tableControl.Invalidate();
                    }
                }
            }
            SavePDF();

        }
        private void PrintFloorplan()
        {

            SaveTheFloorplan();
            try {
                //MessageBox.Show("PRINTED");
                FloorplanPrinter printerNoLines = new FloorplanPrinter(pnlFloorPlan, _lines);
                printerNoLines.ShowPrintPreview(shift.ToString());

            }
            catch (Exception ex) {
                MessageBox.Show("Floorplan saved, but An error occurred while trying to print: " + ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //SaveFloorplanAndPrint();
            SaveAndPrintNEW();



            //MakeUnassignedTablesPickup();

            //if (shift.SelectedFloorplan.CheckIfAllSectionsAssigned())
            //{
            //    List<Section> nonPickupSections = shift.SelectedFloorplan.Sections.Where(s => !s.IsPickUp
            //    && !s.IsBarSection).ToList();

            //    if (!shift.SelectedFloorplan.CheckIfCloserIsAssigned() && nonPickupSections.Count > 1)
            //    {
            //        DialogResult result = MessageBox.Show("There is not a closer assigned. \n Continue anyway?",
            //                                    "Continue?",
            //                                    MessageBoxButtons.YesNo,
            //                                    MessageBoxIcon.Question);

            //        if (result == DialogResult.No)
            //        {
            //            return;
            //        }
            //    }
            //    if (cbTableDisplayMode.Checked)
            //    {
            //        foreach (Control c in pnlFloorPlan.Controls)
            //        {
            //            if (c is TableControl tableControl)
            //            {
            //                tableControl.CurrentDisplayMode = DisplayMode.TableNumber;
            //                tableControl.Invalidate();
            //            }
            //        }
            //    }
            //    SqliteDataAccess.SaveFloorplanAndSections(shift.SelectedFloorplan);


            //    //TODO SECTIONLINES DISABLED

            //    //DialogResult printWihtLines = MessageBox.Show("Do you want to use these section lines?",
            //    //                            "Continue?",
            //    //                            MessageBoxButtons.YesNo,
            //    //                            MessageBoxIcon.Question);

            //    //if (printWihtLines == DialogResult.No)
            //    //{
            //    //    FloorplanPrinter printerNoLines = new FloorplanPrinter(pnlFloorPlan);
            //    //    printerNoLines.ShowPrintPreview();
            //    //    return;
            //    //}

            //    //TableGrid grid = new TableGrid(shiftManager.SelectedDiningArea.Tables);
            //    //grid.FindTableTopBottomNeighbors();
            //    //grid.FindTableNeighbors();
            //    //grid.SetTableBoarderMidPoints();
            //    //grid.CreateNeighbors();
            //    //grid.SetSections(this.shiftManager.SelectedFloorplan.Sections);
            //    //SectionLineDrawer edgeDrawer = new SectionLineDrawer(5f);
            //    //Bitmap edgesBitmap = edgeDrawer.CreateEdgeBitmap(pnlFloorPlan.Size, grid.GetSectionTableBoarders());


            //    ////List<Edge> edges = grid.GetNeighborEdges();
            //    ////Bitmap edgesBitmap = edgeDrawer.CreateEdgeBitmap(pnlFloorPlan.Size, edges);

            //    // pnlFloorPlan.BackgroundImage = edgesBitmap;
            //    //FloorplanPrinter printer = new FloorplanPrinter(pnlFloorPlan, edgeDrawer, grid.GetSectionTableBoarders());
            //    //printer.ShowPrintPreview();

            //    try
            //    {
            //        //MessageBox.Show("Floorplan saved, but An error occurred while trying to print: " + ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        FloorplanPrinter printerNoLines = new FloorplanPrinter(pnlFloorPlan, _lines);
            //        printerNoLines.ShowPrintPreview();

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Floorplan saved, but An error occurred while trying to print: " + ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    //printer.Print();
            //}

        }

        private void btnSaveColorPDF_Click(object sender, EventArgs e)
        {
            //SavePDF();
            //SaveFloorplanAndPrint();
            SaveAndPDFNEW();
        }
        private void SaveTheFloorplan()
        {
            if (shift.SelectedFloorplan != null) {
                bool wasSaved = SqliteDataAccess.SaveFloorplanAndSections(shift.SelectedFloorplan);
                if (wasSaved) {
                    NotificationHandler.ShowNotificationLabel(pnlFloorPlan, "Floorplan Saved!", UITheme.YesColor, UITheme.YesFontColor,
                        new Point(0, 0), pnlFloorPlan.Width, 30, TimeSpan.FromSeconds(2));
                    diningAreaButtonHandeler.UpdateForShift(shift);
                }
            }
            else {
                NotificationHandler.ShowNotificationLabel(pnlFloorPlan, "No Floorplan to Save!!", UITheme.NoColor, UITheme.NoFontColor,
                       new Point(0, 0), pnlFloorPlan.Width, 30, TimeSpan.FromSeconds(2));
            }


        }
        private void SavePDF()
        {
            //List<FloorplanLine> lines = new List<FloorplanLine>(); // Initialize with actual lines if needed
            FloorplanPrinter printerNoLines = new FloorplanPrinter(pnlFloorPlan, _lines);

            string formattedDate = this.shift.DateOnly.ToString("ddd MMM d");
            string formattedTime = DateTime.Now.ToString("HH-mm-ss");

            string fileName = $"{this.shift.SelectedDiningArea.Name} {formattedDate} {this.shift.IsAmDisplay} {formattedTime}".Replace(":", "-");
            using (SaveFileDialog saveFileDialog = new SaveFileDialog()) {
                //saveFileDialog.InitialDirectory = "C:\\Users\\Jason\\OneDrive\\Working On Now\\Floorplan files"; // Optional: set the initial directory
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                saveFileDialog.FileName = fileName; // Set the default filename

                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string filePath = saveFileDialog.FileName;

                    printerNoLines.CreatePdf(filePath, false, shift.ToString());

                }
            }
            //MessageBox.Show("Printed");
            SaveTheFloorplan();

        }

        private void sectionHeaderDisplay_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            frmCalendar frmCalendar = new frmCalendar();
            frmCalendar.ShowDialog();
        }

        private void btnTestLabels_Click(object sender, EventArgs e)
        {
            //floorplanManager.RemoveLabels();
            SectionLabelManager sectionLabelManager = new SectionLabelManager(floorplanManager.Floorplan, floorplanManager.Shift, pnlFloorPlan);
        }

        private void rdoReservations_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblMissingSalesData_Click(object sender, EventArgs e)
        {        
            string url = "https://www.toasttab.com/restaurants/admin/reports/home#sales-orders";
            try {
                // Open the URL in the default web browser
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }

        //private void Form1_KeyDown(object sender, KeyEventArgs e) {
        //    if (e.KeyCode == Keys.I) {
        //        // Show all tooltips when the "I" key is pressed
        //        //previewTooltip.ShowAllTooltips(this);
        //    }
        //}

        //private void Form1_KeyUp(object sender, KeyEventArgs e) {
        //    if (e.KeyCode == Keys.I) {

        //         HideCustomTooltip();
        //    }
        //}
        //private void ShowCustomTooltip() {

        //    //else if (!this.Controls.Contains(customTooltip)) {
        //    //    this.Controls.Add(customTooltip);
        //    //    customTooltip.BringToFront();
        //    //}
        //}

        //private void HideCustomTooltip() {

        //}

        //private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
        //    if (e.KeyCode == Keys.Alt) {
        //        e.IsInputKey = true; // Tell the system that ALT is an input key
        //        ShowCustomTooltip();
        //    }
        //}
    }
}