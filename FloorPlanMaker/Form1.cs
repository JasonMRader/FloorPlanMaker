
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
        private frmTemplateSelection _frmTemplateSelection;
        private PictureBox loadingScreen = null;
        ImageLabelControl coversImageLabel = new ImageLabelControl();
        ImageLabelControl salesImageLabel = new ImageLabelControl();
        private FloorplanFormManager floorplanManager;
        private bool quicklyChoosingAServer = false;
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
                floorplanManager.IncrementSelectedSection();
                //shiftManager.SelectedFloorplan.MoveToNextSection();
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

        public void UpdateForm1ShiftManager(ShiftManager shiftManagerToAdd)
        {
            dateTimeSelected = new DateTime(shiftManagerToAdd.DateOnly.Year, shiftManagerToAdd.DateOnly.Month, shiftManagerToAdd.DateOnly.Day);
            UpdateDateLabel(0);
            cbIsAM.Checked = shiftManagerToAdd.IsAM;
            foreach (Floorplan fp in shiftManagerToAdd.Floorplans)
            {
                this.shiftManager.AddFloorplanAndServers(fp);
            }
            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked, pnlFloorPlan, flowServersInFloorplan, flowSectionSelect);
            rdoSections.Checked = true;
            rdoViewSectionFlow.Checked = true;
            flowSectionSelect.Visible = true;
            flowServersInFloorplan.Visible = false;
            rdoViewSectionFlow.Image = Resources.lilCanvasBook;
        }
        //public void ViewTemplateSections(FloorplanTemplate template)
        //{
        //    dateTimeSelected = new DateTime(shiftManagerToAdd.DateOnly.Year, shiftManagerToAdd.DateOnly.Month, shiftManagerToAdd.DateOnly.Day);
        //    UpdateDateLabel(0);
        //    cbIsAM.Checked = shiftManagerToAdd.IsAM;
        //    foreach (Floorplan fp in shiftManagerToAdd.Floorplans)
        //    {
        //        this.shiftManager.AddFloorplanAndServers(fp);
        //    }
        //    floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked, pnlFloorPlan, flowServersInFloorplan, flowSectionSelect);
        //    rdoSections.Checked = true;
        //    rdoViewSectionFlow.Checked = true;
        //    flowSectionSelect.Visible = true;
        //    flowServersInFloorplan.Visible = false;
        //    rdoViewSectionFlow.Image = Resources.lilCanvasBook;
        //}
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
            if (isDragging)
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
            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked, pnlFloorPlan, flowServersInFloorplan, flowSectionSelect);
            if (floorplanManager.Floorplan == null)
            {
                NoServersToDisplay();

            }
        }
        public void UpdateWithTemplate()
        {
            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked, pnlFloorPlan, flowServersInFloorplan, flowSectionSelect);
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

            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked, pnlFloorPlan, flowServersInFloorplan, flowSectionSelect);
            this.sectionLineManager = new SectionLineManager(allTableControls);
        }

        private void rdoSections_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSections.Checked)
            {
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
            
            FloorplanTemplate template = new FloorplanTemplate(shiftManager.SelectedFloorplan);
            if (template.IsDuplicate())
            {
                MessageBox.Show("This Template Already Exists");
            }
            else
            {
                SqliteDataAccess.SaveFloorplanTemplate(template);
                MessageBox.Show("Template Saved!");
            }

        }
        private void btnChooseTemplate_Click(object sender, EventArgs e)
        {
            if (_frmTemplateSelection == null)
            {
                _frmTemplateSelection = new frmTemplateSelection(floorplanManager,shiftManager.SelectedDiningArea, this)
                { TopLevel = false, AutoScroll = true };
                pnlTemplateContainer.Controls.Add(_frmTemplateSelection);
            }


            _frmTemplateSelection.Show();
            pnlTemplateContainer.BringToFront();
            //frmTemplateSelection form = new frmTemplateSelection(shiftManager);

            //form.StartPosition = FormStartPosition.CenterScreen;

            //form.BringToFront();

            //form.ShowDialog();
            //if (form.DialogResult == DialogResult.OK)
            //{
            //    UpdateTableControlSections();
            //    form.Dispose();
            //}
            //if (DialogResult == DialogResult.Cancel)
            //{
            //    form.Dispose();
            //}

        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool pickUpAdded = false;
            Section pickUpSection = new Section(floorplanManager.Floorplan);
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
                //UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
                //CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
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
       
        private void dtpFloorplan_ValueChanged(object sender, EventArgs e)
        {
            floorplanManager.SetViewedFloorplan(dateOnlySelected, cbIsAM.Checked, pnlFloorPlan, flowServersInFloorplan, flowSectionSelect);
            this.sectionLineManager = new SectionLineManager(allTableControls);
        }

        private void NoServersToDisplay()
        {
            flowServersInFloorplan.Controls.Clear();
            flowSectionSelect.Controls.Clear();
            coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (flowSectionSelect.Width / 2) - 7, 30);
            salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (flowSectionSelect.Width / 2) - 7, 30);
            coversImageLabel.UpdateText(shiftManager.SelectedDiningArea.GetMaxCovers().ToString("F0"));
            salesImageLabel.UpdateText(shiftManager.SelectedDiningArea.GetAverageCovers().ToString("C0"));
            flowSectionSelect.Controls.Add(coversImageLabel);
            flowSectionSelect.Controls.Add(salesImageLabel);
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

        private void rdoViewSectionFlow_CheckedChanged(object sender, EventArgs e)
        {

            if (rdoViewSectionFlow.Checked)
            {
                flowSectionSelect.Visible = true;
                flowServersInFloorplan.Visible = false;
                rdoViewSectionFlow.Image = Resources.lilCanvasBook;

            }
            else
            {
                flowSectionSelect.Visible = false;
                flowServersInFloorplan.Visible = true;
                rdoViewSectionFlow.Image = Resources.lilBook;
            }
        }

        private void rdoViewServerFlow_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoViewServerFlow.Checked)
            {
                flowSectionSelect.Visible = false;
                flowServersInFloorplan.Visible = true;
                rdoViewSectionFlow.Image = Resources.lilBook;
            }
            else
            {
                flowSectionSelect.Visible = true;
                flowServersInFloorplan.Visible = false;
                rdoViewSectionFlow.Image = Resources.lilCanvasBook;
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
            UpdateDateLabel(0);
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
                    UpdateDateLabel(0);
                }
            }
        }
        //
        //
        //
        //****************    Test  Methods **********************
        //
        //
        //
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

            this.sectionLineManager = new SectionLineManager(allTableControls);
            sectionLineManager.AddTopLines(pnlFloorPlan);
            sectionLineManager.AddRightLines(pnlFloorPlan);
            sectionLineManager.AddRightBorders(pnlFloorPlan);
            sectionLineManager.AddBottomLines(pnlFloorPlan);
            sectionLineManager.DrawSectionLines(pnlFloorPlan);

            sectionLineManager.MakeTopLines(pnlFloorPlan);
            sectionLineManager.MakeSectionTableOutlines();
            foreach (SectionLine sectionLine in sectionLineManager.SectionLines)
            {
                pnlFloorPlan.Controls.Add(sectionLine);
            }
            sectionLineManager.RemoveBottomLines(pnlFloorPlan);
            sectionLineManager.RemoveRightLines(pnlFloorPlan);
            sectionLineManager.DrawSeparationLines(pnlFloorPlan);


            sectionLineManager.AddSectionNodes(pnlFloorPlan);
            //flowServersInFloorplan.Controls.Clear();
            //FloorplanInfoControl fpInfo = new FloorplanInfoControl(shiftManager.SelectedFloorplan, flowServersInFloorplan.Width);
            //fpInfo.UpdatePastLabels(8, 4);
            //flowServersInFloorplan.Controls.Add(fpInfo);

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
            //flowServersInFloorplan.Controls.Clear();
            //ImageLabelControl imgControl = new ImageLabelControl(UITheme.covers, "50", flowServersInFloorplan.Width, 30);
            //imgControl.BackColor = Color.Blue;
            //flowServersInFloorplan.Controls.Add(imgControl);

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


    }
}