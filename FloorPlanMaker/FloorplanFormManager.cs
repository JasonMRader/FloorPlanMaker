using FloorplanClassLibrary;
using FloorPlanMaker;
using FloorplanUserControlLibrary;
using NetTopologySuite.Triangulate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
//using static System.Collections.Specialized.BitVector32;

namespace FloorPlanMakerUI
{
    public class FloorplanFormManager : ISectionObserver, IShiftObserver
    {
        //TODO: CLicking on Unassigned Label sometiems causes long delay and flickering (When doing it multiple times? espesially when there are lots of servers / sections?
        public Floorplan? Floorplan
        {
            get
            {
                return this.Shift.SelectedFloorplan;
            }
        }
        public Shift Shift;
       // private List<TableControl> _tableControls = new List<TableControl>();
        //private List<SectionLabelControl> _sectionLabels = new List<SectionLabelControl>();
        private List<SectionPanelControl> _sectionPanels = new List<SectionPanelControl>();
        private List<ServerControl> _serverControls = new List<ServerControl>();
        //public event EventHandler SectionLabelRemoved;
        public event EventHandler<UpdateEventArgs> UpdateRequired;
        private ImageLabelControl serverCountImageLabel = new ImageLabelControl();
        private ImageLabelControl coversImageLabel = new ImageLabelControl();
        private ImageLabelControl salesImageLabel = new ImageLabelControl();        
        private TableSalesManager tableSalesManager = new TableSalesManager();
        private SectionHeaderDisplay sectionHeader = new SectionHeaderDisplay();
        private frmSectionServerAssign frmSectionServerAssign = new frmSectionServerAssign();
        private DiningAreaButtonHandeler diningAreaButtonHandeler { get; set; }
        private Panel pnlMainContainer {  get; set; }   
        private Panel pnlFloorplan { get; set; }
        private Panel pnlSideContainer { get; set; }
        
        private FlowLayoutPanel flowSectionsPanel {  get; set; }
        private FlowLayoutPanel flowServersPanel { get; set; }
        
        public DateOnly dateOnly => this.Shift.DateOnly;
        public bool isAm => this.Shift.IsAM;
        public TemplateManager TemplateManager { get; set; }
        public TemplateCreator TemplateCreator { get; set; }
        public Floorplan floorplanTemplateTEMP { get; set; }
        public FloorplanGenerator floorplanGenerator = new FloorplanGenerator();   
        public ToolTip toolTip = new ToolTip();
        private TableControlManager tableControlManager { get; set; }
        private SectionLabelManager sectionLabelManager { get; set; }
        public FloorplanFormManager() 
        {
            this.Shift = new Shift();            
        }
        public FloorplanFormManager(Panel pnlFloorPlan, FlowLayoutPanel flowServersInFloorplan, 
            FlowLayoutPanel flowSectionSelect, Panel pnlContainer, Panel pnlSideContainer,
            SectionHeaderDisplay headerDisplay, DiningAreaButtonHandeler diningAreaButtonHandeler)
        {
            this.Shift = new Shift();
            this.flowSectionsPanel = flowSectionSelect;
            this.flowServersPanel = flowServersInFloorplan;
            this.pnlFloorplan = pnlFloorPlan;
            this.pnlMainContainer = pnlContainer;
            this.pnlSideContainer = pnlSideContainer;
            this.diningAreaButtonHandeler = diningAreaButtonHandeler;
            this.sectionHeader = headerDisplay;
            this.sectionHeader.btnTeamWaitClicked += HeaderTeamWaitClicked;

            tableControlManager = new TableControlManager(pnlFloorPlan);
            tableControlManager.UpdateAveragesPerServer += newUpdateAveragesPerServer;
            tableControlManager.AllTablesAssigned += newAllTablesAssigned;
            tableControlManager.NotAllTablesAssigned += notAllTablesAssigned;

            this.sectionHeader.btnClearSectionClicked += ClearSection;
            
            frmSectionServerAssign.StartPosition = FormStartPosition.Manual;
            frmSectionServerAssign.SignalForInvisible += SectionAssignFormInvisible;
            this.sectionLabelManager = new SectionLabelManager(Floorplan, Shift, pnlFloorPlan);

        }

        private void notAllTablesAssigned()
        {
           sectionLabelManager.ClearAllLabels();
        }

        private void newAllTablesAssigned()
        {
            sectionLabelManager.SetNewFloorplan(Floorplan);
            //UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.TableControl, UpdateType.Refresh, sender));
        }

        private void newUpdateAveragesPerServer(object? sender, EventArgs e)
        {
            UpdateAveragesPerServer();
        }

        public void UpdateTemplatesBasedOnFloorplan()
        {
            if (this.TemplateManager == null)
            {
                this.TemplateManager = new TemplateManager();
            }
            if (this.Floorplan != null)
            {
                this.TemplateManager.DiningArea = Floorplan.DiningArea;
                this.TemplateManager.serverCount = this.Floorplan.Servers.Count - this.Floorplan.Bartenders.Count;
                this.TemplateManager.GetTemplatesForFloorplan(this.Floorplan);
                
            }
            else
            {               
                this.TemplateManager.DiningArea = Shift.SelectedDiningArea;
                this.TemplateManager.GetAllFloorplanTemplatesForDiningArea();
            }
        }
        public List<TableControl> TableControls
        {
            get { return tableControlManager.TableControls; }
            //set { _tableControls = value; }

        }
        //public List<SectionLabelControl> SectionLabels
        //{
        //    get { return sectionLabelManager.SectionLabels; }
           
        //}
        public List<SectionPanelControl> SectionPanels
        {
            get { return _sectionPanels; }
            set { _sectionPanels = value; }
        }
        public List<ServerControl> ServerControls
        {
            get { return _serverControls; }
            set { _serverControls = value; }
        }
        
        public void ResetSections()
        {
            Floorplan.RemoveAllServersFromSections();

            Floorplan.floorplanLines.Clear();            
            tableControlManager.ResetSections();
            UpdateServerControls();
            sectionLabelManager.ClearAllLabels();
            //RemoveAllSectionLabels();
            //_sectionLabels.Clear();
            foreach (SectionPanelControl sectionPanelControl in this._sectionPanels)
            {
                sectionPanelControl.UpdateLabels();
            }

        }
        
        public bool AllTablesAreAssigned()
        {
            foreach (TableControl tableControl in TableControls)
            {
                if (tableControl.Section == null)
                {
                    return false;
                }
            }
            return true;
        }
        public int NumberOfUnassignedTables()
        {
            int unassignedTables = 0;
            foreach (TableControl tableControl in TableControls)
            {
                if (tableControl.Section == null)
                {
                    unassignedTables++;
                }
            }
            return unassignedTables;
        }

        public void SelectTables(List<TableControl> selectedTables)
        {
            tableControlManager.SelectTables(selectedTables);
        }
       

        public void CopyTemplateSections(FloorplanTemplate template)
        {
            if(Floorplan.HasAssignedNonBartenders)
            {
                DialogResult result = MessageBox.Show("There are already servers assigned to sections," +
                    " to you want to clear these sections?",
                    "Clear Sections?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if(result == DialogResult.No)
                {
                    return;
                }
            }
            if(Shift.SelectedFloorplan != null)
            {
                Shift.SelectedFloorplan.RemoveAllServersFromSections();
            }
           
            Shift.SelectedFloorplan.CopyTemplateSections(template.Sections);
            if(Shift.SelectedFloorplan.hasBarSection)
            {
                Shift.PairBothBarSections();
            }
            SetSectionPanels();
            foreach(SectionPanelControl sectionPanel in this._sectionPanels)
            {
                sectionPanel.UpdateLabels();
            }
            Floorplan.floorplanLines.AddRange(template.floorplanLines);
        }
       
       

        private void ServerRemovedFromSection(object? sender, EventArgs e)
        {
            UpdateServerControls();
        }

        private void AssignServerToSection(object? sender, EventArgs e)
        {            
            Section sectionToAssign = sender as Section;
            Floorplan.SetSelectedSection(sectionToAssign);
            UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Assign, sectionToAssign));
        }
        

        public void SetServerControls()
        {
            _serverControls.Clear();
            if (Shift.SelectedFloorplan == null) { return; }
            if (Shift.SelectedFloorplan.Servers.Count <= 0) { return; }
            foreach (Server server in Shift.SelectedFloorplan.Servers)
            {
                server.Shifts = SqliteDataAccess.GetShiftsForServer(server);
                ServerControl serverControl = new ServerControl(server, 20, Floorplan.Sections);
                serverControl.Click += ServerControl_Click;
                foreach (ShiftControl shiftControl in serverControl.ShiftControls)
                {

                    shiftControl.ShowClose();
                    shiftControl.ShowTeam();
                    shiftControl.HideOutside();
                }

                this._serverControls.Add(serverControl);
            }
            UpdateServerControls();
        }
        private void ServerControl_Click(object? sender, EventArgs e)
        {
            ServerControl serverControl = (ServerControl)sender;
            Server server = serverControl.Server;
            if (Shift.SelectedFloorplan.SectionSelected == null) { return; }              
            Shift.SelectedFloorplan.SectionSelected.AddServer(server);
            UpdateServerControls();
        }
       

        private void SectionHeaderAssignServerClicked(object? sender, EventArgs e)
        {
            if(Floorplan.SectionSelected.IsPickUp)
            {
                SectionHeaderDisplay controlClicked = (SectionHeaderDisplay)sender;
                Section section = controlClicked.Section;
                frmPickupSectionAssignment pickUpForm = new frmPickupSectionAssignment(section, Shift);
                pickUpForm.StartPosition = FormStartPosition.Manual;
                Point controlLocation = sectionHeader.PointToScreen(Point.Empty);
                pickUpForm.Location = new Point(controlLocation.X + 28, controlLocation.Y + 38);
                pickUpForm.ShowDialog();
                return;
            }
            if (Floorplan.SectionSelected.IsBarSection)
            {
                return;
            }
            else
            {
                Point controlLocation = sectionHeader.PointToScreen(Point.Empty);
                frmSectionServerAssign.Location = new Point(controlLocation.X + 28, controlLocation.Y + 38);
                this.frmSectionServerAssign.Visible = !frmSectionServerAssign.Visible;
            }              
            
        }
        private void SectionAssignFormInvisible(object? sender, EventArgs e)
        {
            this.frmSectionServerAssign.Visible = false;
        }       

        public void IncrementSelectedSection()
        {
            if(this.Floorplan == null) { return; }
            this.Floorplan.MoveToNextSection();
        }
        public void DecrementSelectedSection()
        {
            if (this.Floorplan == null) { return; }
            this.Floorplan.MoveToPreviousSection();
        }
       
        private void HeaderTeamWaitClicked(object? sender, EventArgs e)
        {
            SectionHeaderDisplay sectionPanel = sender as SectionHeaderDisplay;

            Section selectedSection = sectionPanel.Section;

            SectionHeaderTeamwaitToggle(selectedSection);
        }
        public void SectionHeaderTeamwaitToggle(Section selectedSection)
        {
            if (selectedSection.IsPickUp || selectedSection.IsBarSection) { return; }
            SectionPanelControl sectionPanel = _sectionPanels.FirstOrDefault(sp => sp.Section == selectedSection);
            if (!selectedSection.IsTeamWait)
            {
                selectedSection.ToggleTeamWait();
                //sectionHeader.SetTeamWaitPictureBoxes();
                Floorplan.SetTheAppropriateAmountOfSections();                
            }
            else
            {

                selectedSection.MakeSoloSection();
                //sectionHeader.SetTeamWaitPictureBoxes();
                Floorplan.SetTheAppropriateAmountOfSections();
                //Section sectionAdded = new Section(Floorplan);
                //Floorplan.AddSection(sectionAdded);
                //SectionPanelControl newSectionPanel = new SectionPanelControl(sectionAdded, this.Shift.SelectedFloorplan);
                //newSectionPanel.CheckBoxChanged += setSelectedSection;
                //newSectionPanel.picEraseSectionClicked += EraseSectionClicked;
                //newSectionPanel.picTeamWaitClicked += TeamWaitClicked;
                //newSectionPanel.picAddServerClicked += SectionAddServerClicked;
                //newSectionPanel.picSubtractServerClicked += SectionSubtractServerClicked;
                //newSectionPanel.unassignedSpotClicked += AssignServerToSection;
                //newSectionPanel.ServerRemoved += ServerRemovedFromSection;
                //this._sectionPanels.Add(newSectionPanel);
                //UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Add, sectionAdded));
            }
            sectionPanel.UpdateLabels();

        }
       
        private void ClearSection(object? sender, EventArgs e)
        {
            SectionHeaderDisplay sectionHeader = (SectionHeaderDisplay)sender;
            Section selectedSection = sectionHeader.Section;
            SectionPanelControl sectionPanel = _sectionPanels.FirstOrDefault(x => x.Section == selectedSection);
            if (selectedSection.IsPickUp || selectedSection.IsEmpty())
            {
                Floorplan.DeleteSection(selectedSection);
                UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Remove, selectedSection));
                _sectionPanels.Remove(sectionPanel);
                return;
            }
            if (selectedSection != null)
            {
                UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionLabel, UpdateType.Remove, selectedSection));
                this.sectionLabelManager.RemoveSectionLabelBySection(selectedSection);
                Shift.SelectedFloorplan.ClearSection(selectedSection);

                tableControlManager.RefreshTableControlColors();
                //UpdateTableControlColors();
                UpdateServerControls();
            }
            sectionPanel.UpdateLabels();
        }
        

        
       
        private void btnAutoAssignServers_Click(object? sender, EventArgs e)
        {
            AutoAssignSections();

        }
        public void AutoAssignSections()
        {
            if (!tableControlManager.AllTablesAreAssigned())
            {
                MessageBox.Show("All tables are not assigned");
                return;
            }
            FloorplanGenerator.AssignServerSections(this.Floorplan);
        }

        private void btnAutoSelectTemplate_Click(object? sender, EventArgs e)
        {
            
            UpdateTemplatesBasedOnFloorplan();
            floorplanGenerator = new FloorplanGenerator(this.Shift);
            FloorplanTemplate template = floorplanGenerator.SelectIdealTemplate(TemplateManager.Templates);
            CopyTemplateSections(template);
            tableControlManager.SetFloorplan(Floorplan);
            //tableControlManager.AddTableControls();
            //SetSectionLabels();
            SetSectionPanels();           
            SetServerControls();
            //tableControlManager.UpdateTableControlColors();
            flowSectionsPanel.Controls.Clear();
            flowServersPanel.Controls.Clear();
            AddServerControls(flowServersPanel);
            AddSectionPanels(flowSectionsPanel);
            //AddSectionLabels(pnlFloorplan);
            sectionLabelManager.SetNewFloorplan(Floorplan);
            UpdateTableStats();            

            UpdateAveragesPerServer();
            UpdateImageLabels();
           

        }
        private void UpdateImageLabels()
        {
            if (this.Floorplan == null)
            {
                serverCountImageLabel.UpdateText("0");
                coversImageLabel.UpdateText(Shift.SelectedDiningArea.GetMaxCovers().ToString("F0"));
                salesImageLabel.UpdateText(Shift.SelectedDiningArea.ExpectedSales.ToString("C0"));               
            }
            else
            {
                serverCountImageLabel.UpdateText(Floorplan.Servers.Count.ToString());
                coversImageLabel.UpdateText(Shift.SelectedFloorplan.MaxCoversPerServer.ToString("F0"));
                salesImageLabel.UpdateText(Shift.SelectedFloorplan.AvgSalesPerServer.ToString("C0"));
            }
            
            serverCountImageLabel.Invalidate();
            coversImageLabel.Invalidate();
            salesImageLabel.Invalidate();
        }

        private FloorplanTemplate SelectTheIdealFloorplanTemplate()
        {            
            FloorplanTemplate template = TemplateManager.Templates.FirstOrDefault();
            if(template == null)
            {
                MessageBox.Show("There is no Template that meets these requirements");
                return null;
            }
            return template;
        }
        
       
        public void MakeUnassignedTablesPickup()
        {
            AddPickupSection();
            foreach(TableControl tableControl in this.tableControlManager.TableControls)
            {
                if(tableControl.Section == null)
                {
                    tableControlManager.AddTableControlToSelectedSection(tableControl);
                }
            }
        }
        public void AddServerControls(FlowLayoutPanel panel)
        {
            panel.Controls.Clear();
            if(this.Floorplan == null) { return; }
            Button btnEditRoster = new Button
            {
                Text = "Edit Server Roster",
                AutoSize = false,
                Size = new Size(panel.Width - 30, 25),
                Font = new Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.CTAColor,
                ForeColor = UITheme.CTAFontColor
            };
            btnEditRoster.Click += btnEditRoster_Click;
            panel.Controls.Add(btnEditRoster);
            foreach (ServerControl serverControl in _serverControls)
            {
                serverControl.Width = panel.Width -10;
                serverControl.Margin = new Padding(5);
                panel.Controls.Add(serverControl);
            }
        }
        private void btnEditRoster_Click(object? sender, EventArgs e)
        {
            EditRosterClicked();            
        }
        public void EditRosterClicked()
        {
            frmEditShiftRoster editRosterForm = new frmEditShiftRoster(Shift);
            editRosterForm.ShowDialog();
            SetServerControls();
            AddServerControls(this.flowServersPanel);
            SetSectionPanels();
            AddSectionPanels(this.flowSectionsPanel);
            UpdateAveragesPerServer();
        }        
       
        private void RefreshServerSections()
        {
            foreach (ServerControl serverControl in this._serverControls)
            {
                serverControl.Server.CurrentSection = null;
            }
            foreach (Section section in Floorplan.Sections)
            {
                foreach (Server server in section.ServerTeam)
                {
                    server.CurrentSection = section;
                }
            }
        }
        public void UpdateServerControls()
        {
            RefreshServerSections();           
            if (Shift.SelectedFloorplan == null) return;
            foreach (ServerControl serverControl in _serverControls)
            {
                Server server = serverControl.Server;
                if (server.CurrentSection != null)
                {
                    serverControl.Label.BackColor = server.CurrentSection.Color;
                    serverControl.Label.ForeColor = server.CurrentSection.FontColor;
                }
                else
                {
                    serverControl.Label.BackColor = UITheme.ButtonColor; 
                    serverControl.Label.ForeColor= Color.Black;
                }
                serverControl.Invalidate();
            }                        
        }
        public void UpdateSection(Section section)
        {
           
        }       
        
        private int FindInsertionIndex(Section? section, FlowLayoutPanel flowPanel)
        {
            if (section.IsPickUp)
            {
                return flowPanel.Controls.Count - 3;
            }
            for (int i = 0; i < flowPanel.Controls.Count; i++)
            {
                var panelControl = flowPanel.Controls[i] as SectionPanelControl;
                if (panelControl != null && panelControl.Section.Number > section.Number)
                {
                    return i;
                }
            }
            return flowPanel.Controls.Count - 3;
        }       
        public void SetTableSalesStatsPeriod(TableSalesManager.StatsPeriod statsPeriod)
        {
            this.tableSalesManager.CurrentStatsPeriod = statsPeriod;
            UpdateTableStats();
            if(this.Floorplan == null)
            {
                UpdateImageLabels();
            }
            else
            {
                UpdateAveragesPerServer();
            }
        }
        private void UpdateTableStats()
        {
            this.tableSalesManager.SetStatsList(this.isAm, this.dateOnly);
            Shift.SelectedDiningArea.SetTableSales(tableSalesManager.Stats);
            //TODO: reformate ineffecient, setting tables twice
            if(Shift.Floorplans != null)
            {
                foreach(Floorplan floorplan in  Shift.Floorplans)
                {
                    floorplan.DiningArea.SetTableSales(tableSalesManager.Stats);
                }
                foreach(TableControl tableControl in this.tableControlManager.TableControls)
                {
                    this.toolTip.SetToolTip(tableControl, tableControl.Table.AverageSales.ToString("C0"));
                }
               
            }
        }
        
        public void SetViewedFloorplanToNone(FlowLayoutPanel panel)
        {
            panel.Controls.Clear();
            SetSectionImageLabels(panel);
            Button btnCreateTemplate = new Button
            {
                Text = "Create a Floorplan Template",
                AutoSize = false,
                Size = new Size(flowSectionsPanel.Width - 10, 25),
                Font = new Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.CTAColor,
                ForeColor = UITheme.CTAFontColor
            };
            btnCreateTemplate.Click += btnCreateTemplate_Click;
            panel.Controls.Add(btnCreateTemplate);
        }

        

        private void btnSaveFloorplan_Click(object? sender, EventArgs e)
        {
            Button btnSaveTemplate = sender as Button;
            btnSaveTemplate.Click += btnCreateTemplate_Click;
            btnSaveTemplate.Click -= btnSaveFloorplan_Click;
            btnSaveTemplate.Text = "Create A Floorplan Template";
        }        
        public async void SetViewedFloorplan(DateOnly dateOnlySelected, bool isAM)
        {
           
            if (this.Shift.DateOnly != dateOnlySelected)
            {
                this.Shift.DateOnly = dateOnlySelected;               
            }
            if(this.Shift.IsAM != isAM)
            {
                this.Shift.IsAM = isAM;   
            }            
            this.Shift.RemoveFloorplansFromDifferentShift();
            if (Shift.ContainsFloorplan(dateOnlySelected, isAM, Shift.SelectedDiningArea.ID))
            {
                Shift.SetSelectedFloorplan(dateOnlySelected, isAM, Shift.SelectedDiningArea.ID);
            }
            else if(Shift.Floorplans.Count > 0)
            {
                Shift.SelectedFloorplan = null;                               
            }
            else
            {
                Shift = SqliteDataAccess.LoadShift(Shift.SelectedDiningArea, dateOnlySelected, isAM);
            }
            
            ChangeDiningAreaSelected();
            diningAreaButtonHandeler.UpdateForShift(Shift);
        }
        public void ChangeDiningAreaSelected()
        {
            tableControlManager.SetDiningArea(Shift.SelectedDiningArea);
            if (Shift.SelectedFloorplan != null)
            {
                Shift.SetDoubles();
                //tableControlManager.AddTableControls();
                tableControlManager.SetFloorplan(Floorplan);
                //SetSectionLabels();
                SetSectionPanels();
                SetServerControls();
                //tableControlManager.UpdateTableControlColors();
                flowSectionsPanel.Controls.Clear();
                flowServersPanel.Controls.Clear();
                AddServerControls(flowServersPanel);
                AddSectionPanels(flowSectionsPanel);
                //AddSectionLabels(pnlFloorplan);
                sectionLabelManager.SetNewFloorplan(Floorplan);
                UpdateTableStats();
                AddSectionLines();
                UpdateAveragesPerServer();
                UpdateImageLabels();
            }
            else
            {
                //foreach (SectionLabelControl sectionLabel in _sectionLabels)
                //{
                //    pnlFloorplan.Controls.Remove(sectionLabel);
                //}
                sectionLabelManager.ClearAllLabels();
                tableControlManager.SetDiningArea(Shift.SelectedDiningArea);
                //tableControlManager.UpdateTableControlColors();
                UpdateTableStats();
                SetSectionPanels();
                SetServerControls();
                flowSectionsPanel.Controls.Clear();
                flowServersPanel.Controls.Clear();
                AddServerControls(flowServersPanel);
                AddSectionPanels(flowSectionsPanel);
                UpdateImageLabels();
                sectionHeader.SetSectionToNull();
                pnlMainContainer.BackColor = UITheme.SecondColor; 
                pnlSideContainer.BackColor = UITheme.SecondColor;

            }
        }
        
        private void AddSectionLines()
        {
            //throw new NotImplementedException();
        }
        public void UpdateShift(Shift shift)
        {
            //ChangeDiningAreaSelected();
        }
        public void SetFloorplanToTemplate(FloorplanTemplate? template)
        {
            template.DiningArea = Shift.SelectedDiningArea;
            Shift.SelectedFloorplan = new Floorplan(template);
            Shift.SelectedFloorplan.Date = Shift.DateTime;
            Shift.SelectedFloorplan.IsLunch = Shift.IsAM;
            Shift.AddFloorplanToShift(Shift.SelectedFloorplan);           
            SetSectionPanels();
            foreach (SectionPanelControl sectionPanel in this._sectionPanels)
            {
                sectionPanel.UpdateLabels();
            }
            Floorplan.floorplanLines.AddRange(template.floorplanLines);
        }

        internal void AutoAssignCloser()
        {
            this.Floorplan.AutoAssignCloser();
            //sectionLabelManager.UpdateCloserStatus();
            //foreach(SectionLabelControl sectionLabelControl in this._sectionLabels)
            //{
            //    sectionLabelControl.UpdateLabel();
            //    sectionLabelControl.Invalidate();
            //}
        }
        private void SetSectionImageLabels(FlowLayoutPanel panel)
        {
            serverCountImageLabel = new ImageLabelControl(UITheme.waiter, "0", (panel.Width / 4) - 7, 30);
            serverCountImageLabel.Margin = new Padding(6, 3, 3, 3);
            coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (panel.Width / 4) - 7, 30);
            salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (panel.Width / 2) - 7, 30);
            serverCountImageLabel.SetTooltip("Servers Assigned to this Floorplan");
            coversImageLabel.SetTooltip("Covers per Server");
            salesImageLabel.SetTooltip("Sales Per Server");
            panel.Controls.Add(serverCountImageLabel);
            panel.Controls.Add(coversImageLabel);
            panel.Controls.Add(salesImageLabel);
        }
        public void SetSectionPanels()
        {
            _sectionPanels.Clear();
            if (Shift.SelectedFloorplan == null) { return; }
            Shift.SelectedFloorplan.MovePickupSectionsToEndOfList();
            foreach (Section section in Shift.SelectedFloorplan.Sections)
            {
                AddNewPanelControl(section);

            }
            if (this.Floorplan.Sections.Count > 0)
            {
                Floorplan.SetSelectedSection(Floorplan.Sections[0]);
            }
        }
        private void AddNewPanelControl(Section sectionAdded)
        {
            SectionPanelControl newSectionPanel = new SectionPanelControl(sectionAdded, this.Shift.SelectedFloorplan);
            newSectionPanel.CheckBoxChanged += setSelectedSection;
            newSectionPanel.picEraseSectionClicked += EraseSectionClicked;
            newSectionPanel.picTeamWaitClicked += TeamWaitClicked;
            newSectionPanel.picAddServerClicked += SectionAddServerClicked;
            newSectionPanel.picSubtractServerClicked += SectionSubtractServerClicked;
            newSectionPanel.unassignedSpotClicked += AssignServerToSection;
            newSectionPanel.ServerRemoved += ServerRemovedFromSection;
            this._sectionPanels.Add(newSectionPanel);
            //AddSectionPanel(sectionAdded);
            //UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Add, sectionAdded));
        }
        public void RemoveSectionPanel(Section section, FlowLayoutPanel panel)
        {
            panel.Controls.Remove(sectionPanelControlBySection((Section)section));
            panel.Invalidate();
        }

        public void AddSectionPanels(FlowLayoutPanel panel)
        {
            panel.Controls.Clear();
            SetSectionImageLabels(panel);
            if (this.Floorplan == null) { return; }
            foreach (SectionPanelControl sectionPanel in _sectionPanels)
            {
                sectionPanel.Width = panel.Width - 10;
                sectionPanel.Margin = new Padding(5);
                panel.Controls.Add(sectionPanel);
            }
            Button btnAddPickup = new Button
            {
                Text = "Add Pick-Up Section",
                AutoSize = false,
                Size = new Size(panel.Width - 10, 25),
                Font = new Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.ButtonColor,
                ForeColor = Color.Black
            };
            Button btnAddSection = new Button
            {
                Text = "Add A Section",
                AutoSize = false,
                Size = new Size(panel.Width - 10, 25),
                Font = new Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.ButtonColor,
                ForeColor = Color.Black
            };
            toolTip.SetToolTip(btnAddPickup, "Add a Pickup Section [P]");
            toolTip.SetToolTip(btnAddSection, "Add a Section");
            btnAddSection.Click += btnAddSection_Click;
            btnAddPickup.Click += btnAddPickupSection_Click;
            panel.Controls.Add(btnAddSection);
            panel.Controls.Add(btnAddPickup);
        }

        private void btnAddSection_Click(object? sender, EventArgs e)
        {
            Section section = new Section();

            section.IsPickUp = false;
            Floorplan.AddSection(section);
            
            AddNewPanelControl(section);
            Floorplan.SetSelectedSection(section);
        }
        private void btnAddPickupSection_Click(object sender, EventArgs e)
        {
            AddPickupSection();
        }
        internal void AddSectionPanel(Section? section)
        {
            SectionPanelControl newPanel = sectionPanelControlBySection(section);
            flowSectionsPanel.Controls.Add(newPanel);
            int insertIndex = FindInsertionIndex(section, flowSectionsPanel);
            flowSectionsPanel.Controls.SetChildIndex(newPanel, insertIndex);
        }
        public void UpdateAveragesPerServer()
        {
            UpdateImageLabels();
            foreach (SectionPanelControl sectionPanel in _sectionPanels)
            {
                sectionPanel.UpdateLabels();
            }
        }
        public void AddPickupSection()
        {
            Section pickUp = new Section(Floorplan);
            pickUp.Name = "Pickup";
            pickUp.IsPickUp = true;
            Floorplan.AddSection(pickUp);
            SectionPanelControl newSectionPanel = new SectionPanelControl(pickUp, this.Shift.SelectedFloorplan);
            newSectionPanel.CheckBoxChanged += setSelectedSection;
            newSectionPanel.picEraseSectionClicked += EraseSectionClicked;
            this._sectionPanels.Add(newSectionPanel);
            UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Add, pickUp));
            Floorplan.SetSelectedSection(pickUp);
        }
        
        private void btnCreateTemplate_Click(object? sender, EventArgs e)
        {
            Button btnCreateTemplate = sender as Button;
            FlowLayoutPanel panel = btnCreateTemplate.Parent as FlowLayoutPanel;
            Button btnAddSection = new Button
            {
                Text = "Add a Section",
                AutoSize = false,
                Size = new Size(flowSectionsPanel.Width - 10, 25),
                Font = new Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.CTAColor,
                ForeColor = UITheme.CTAFontColor
            };
            panel.Controls.Add((Button)btnAddSection);
            btnAddSection.Click += btnAddSection_Click;
            btnCreateTemplate.Click -= btnCreateTemplate_Click;
            btnCreateTemplate.Click += btnSaveFloorplan_Click;
            btnCreateTemplate.Text = "Save Template";
            floorplanTemplateTEMP = new Floorplan(Shift.SelectedDiningArea, Shift.DateTime, Shift.IsAM, 1, 1);

            AddSectionPanels(flowSectionsPanel);
            UpdateTableStats();
            UpdateAveragesPerServer();
            UpdateImageLabels();
        }
        private void SectionSubtractServerClicked(object? sender, EventArgs e)
        {
            SectionPanelControl sectionPanel = sender as SectionPanelControl;

            Section selectedSection = sectionPanel.Section;
            bool serverRemoved = selectedSection.DecreaseServerCount();
            if (serverRemoved)
            {
                if (selectedSection.ServerCount == 1)
                {
                    selectedSection.MakeSoloSection();
                    sectionPanel.SetTeamWaitPictureBoxes();
                }
                else
                {
                    sectionPanel.RemoveServerRow();
                }

                Section sectionAdded = new Section(Floorplan);
                Floorplan.AddSection(sectionAdded);
                AddNewPanelControl(sectionAdded);

            }
        }

        private void SectionAddServerClicked(object? sender, EventArgs e)
        {
            SectionPanelControl sectionPanel = sender as SectionPanelControl;
            Section selectedSection = sectionPanel.Section;

            Section sectionRemoved = Floorplan.RemoveHighestNumberedEmptySection(selectedSection);
            if (sectionRemoved == null || Floorplan.NotEnoughUnassignedServersCheck(selectedSection))
            {
                MessageBox.Show("You must clear a section before making another section a teamwait section");
            }
            else
            {
                selectedSection.IncreaseServerCount();
                sectionPanel.AddServerRow();

                UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Remove, sectionRemoved));
                this._sectionPanels.Remove(sectionPanelControlBySection(sectionRemoved));
            }
        }
        private void setSelectedSection(object? sender, EventArgs e)
        {
            SectionPanelControl sectionPanelSender = (SectionPanelControl)sender;
            if (sectionPanelSender.isChecked())
            {
                this.Shift.SetSelectedSection(sectionPanelSender.Section);
                this.pnlMainContainer.BackColor = sectionPanelSender.Section.Color;
                this.pnlSideContainer.BackColor = sectionPanelSender.Section.Color;

                if (Floorplan != null)
                {
                    this.sectionHeader.SetSection(sectionPanelSender.Section, Floorplan);
                    this.sectionHeader.AssignServerClicked -= SectionHeaderAssignServerClicked;
                    this.sectionHeader.AssignServerClicked += SectionHeaderAssignServerClicked;
                    if (frmSectionServerAssign != null)
                    {
                        frmSectionServerAssign.SetNewSectionAndShift(sectionPanelSender.Section, Shift);
                    }
                }
                foreach (SectionPanelControl sectionPanelControl in this._sectionPanels)
                {
                    if (sectionPanelControl != sectionPanelSender)
                    {
                        sectionPanelControl.UnCheckBox();
                    }
                }
            }
        }
        private void TeamWaitClicked(object? sender, EventArgs e)
        {
            SectionPanelControl sectionPanel = sender as SectionPanelControl;

            Section selectedSection = sectionPanel.Section;

            SectionTeamwaitToggle(selectedSection);
        }
        public void SectionTeamwaitToggle(Section selectedSection)
        {
            if (selectedSection.IsPickUp) { return; }
            SectionPanelControl sectionPanel = _sectionPanels.FirstOrDefault(sp => sp.Section == selectedSection);
            if (sectionPanel == null)
            {
                return;
            }
            if (!selectedSection.IsTeamWait)
            {
                Section sectionRemoved = Floorplan.RemoveHighestNumberedEmptySection(selectedSection);
                if (sectionRemoved == null || Floorplan.NotEnoughUnassignedServersCheck(selectedSection))
                {
                    MessageBox.Show("You must clear a section before making another section a teamwait section");
                }
                else
                {
                    selectedSection.ToggleTeamWait();
                    sectionPanel.SetTeamWaitPictureBoxes();
                    UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Remove, sectionRemoved));
                    this._sectionPanels.Remove(sectionPanelControlBySection(sectionRemoved));
                }
            }
            else
            {
                selectedSection.MakeSoloSection();
                sectionPanel.SetTeamWaitPictureBoxes();
                Section sectionAdded = new Section(Floorplan);
                Floorplan.AddSection(sectionAdded);
                AddNewPanelControl(sectionAdded);

            }
            sectionPanel.UpdateLabels();
        }
        private SectionPanelControl sectionPanelControlBySection(Section section)
        {
            SectionPanelControl sectionPanel = this._sectionPanels.FirstOrDefault(s => s.Section == section);
            return sectionPanel;
        }
        private void EraseSectionClicked(object? sender, EventArgs e)
        {
            SectionPanelControl sectionPanel = (SectionPanelControl)sender;
            Section selectedSection = sectionPanel.Section;
            if (selectedSection.IsPickUp || selectedSection.IsEmpty())
            {
                Floorplan.DeleteSection(selectedSection);
                UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Remove, selectedSection));
                _sectionPanels.Remove(sectionPanel);
                return;
            }
            if (selectedSection != null)
            {
                //UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionLabel, UpdateType.Remove, selectedSection));
                //this._sectionLabels.Remove(sectionLabelBySection(selectedSection));
                sectionLabelManager.RemoveSectionLabelBySection(selectedSection);
                Shift.SelectedFloorplan.ClearSection(selectedSection);
                tableControlManager.RefreshTableControlColors();
                UpdateServerControls();
            }
            sectionPanel.UpdateLabels();
        }

        internal void RemoveLabels()
        {
            
            List<Point> points = new List<Point>();
            //foreach(SectionLabelControl labelControl in _sectionLabels)
            //{
               
            //    //points.Add(labelControl.Location);
            //    //SectionLabel label = new SectionLabel(labelControl.Section, Floorplan);
            //    //label.Location = labelControl.Location;
            //    pnlFloorplan.Controls.Remove(labelControl);
            //    //pnlFloorplan.Controls.Add(label);
            //    //label.BringToFront();
                
            //}
            //_sectionLabels.Clear();
        }
        //private SectionLabelControl sectionLabelBySection(Section section)
        //{
        //    SectionLabelControl sectionLabel = this._sectionLabels.FirstOrDefault(s => s.Section == section);
        //    return sectionLabel;
        //}

        //public void RemoveSectionLabel(Section section, Panel panel)
        //{
        //    panel.Controls.Remove(sectionLabelBySection((Section)section));            
        //    panel.Invalidate();
        //}
        //private void RemoveAllSectionLabels()
        //{
        //    foreach(SectionLabelControl sectionLabel in this._sectionLabels)
        //    {
        //        pnlFloorplan.Controls.Remove(sectionLabel);
        //        sectionLabel.Dispose();

        //    }
        //    pnlFloorplan.Invalidate();  
        //}

        //public void AddSectionLabels(Panel panel)
        //{
        //    List<Control> controlsToRemove  = new List<Control>();
        //    foreach(Control c in panel.Controls)
        //    {
        //        if (c is SectionLabelControl sectionLabel)
        //        {
        //            controlsToRemove.Add(c);
        //        }
        //    }
        //    foreach(Control c in controlsToRemove)
        //    {
        //        panel.Controls.Remove(c);
        //    }
        //    foreach(SectionLabelControl sectionLabelControl in _sectionLabels)
        //    {
        //        panel.Controls.Add(sectionLabelControl);
        //        sectionLabelControl.UpdateLabel();

        //        sectionLabelControl.BringToFront();
        //    }
        //}
        //public void SetSectionLabels()
        //{
        //    _sectionLabels.Clear();
        //    if(Shift.SelectedFloorplan == null) { return; }

        //    foreach (Section section in Shift.SelectedFloorplan.Sections)
        //    {                
        //        if (section.Tables.Count > 0)
        //        {
        //            SectionLabelControl sectionControl = new SectionLabelControl(section, Shift.SelectedFloorplan.ServersWithoutSection, Shift.ServersOnShift);
        //            sectionControl.AssignPickup += SectionLabelAssignPickup;
        //            sectionControl.SectionLabelClick += SectionLabel_Clicked;

        //            this._sectionLabels.Add(sectionControl);                    
        //        }
        //        if (section.Server != null)
        //        {
        //            Shift.SelectedFloorplan.ServersWithoutSection.Remove(section.Server);                   

        //        }
        //    }
        //}

        //private void SectionLabel_Clicked(object? sender, EventArgs e)
        //{
        //    SectionLabelControl controlClicked = (SectionLabelControl)sender;
        //    Section section = controlClicked.Section;
        //    if (e is MouseEventArgs mouseEventArgs)
        //    {
        //        bool isShiftPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
        //        if(mouseEventArgs.Button == MouseButtons.Left)
        //        {
        //            if(isShiftPressed)
        //            {
        //                if (!section.IsSelected)
        //                {
        //                    this.Floorplan.SwapServers(this.Floorplan.SectionSelected, section);
        //                }
        //            }
        //            else
        //            {
        //                if (!section.IsSelected)
        //                {
        //                    section.SetToSelected();
        //                }
        //            }                    
        //        }
        //        else if(mouseEventArgs.Button == MouseButtons.Right) 
        //        {
        //            if(isShiftPressed)
        //            {
        //                if(section.Server != null)
        //                {
        //                    section.ClearAllServers();
        //                }
        //            }
        //        }
        //    }            
        //}
        //private void SectionLabelAssignPickup(object? sender, EventArgs e)
        //{
        //    SectionLabelControl controlClicked = (SectionLabelControl)sender;
        //    Section section = controlClicked.Section;
        //    frmPickupSectionAssignment pickUpForm = new frmPickupSectionAssignment(section, Shift);
        //    pickUpForm.ShowDialog();
        //}

    }
}
