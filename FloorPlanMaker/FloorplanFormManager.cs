using FloorplanClassLibrary;
using FloorPlanMaker;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Collections.Specialized.BitVector32;

namespace FloorPlanMakerUI
{
    public class FloorplanFormManager : ISectionObserver
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
        private List<TableControl> _tableControls = new List<TableControl>();
        private List<SectionLabelControl> _sectionLabels = new List<SectionLabelControl>();
        private List<SectionPanelControl> _sectionPanels = new List<SectionPanelControl>();
        private List<ServerControl> _serverControls = new List<ServerControl>();
        public event EventHandler SectionLabelRemoved;
        public event EventHandler<UpdateEventArgs> UpdateRequired;
        private ImageLabelControl coversImageLabel = new ImageLabelControl();
        private ImageLabelControl salesImageLabel = new ImageLabelControl();        
        private TableSalesManager tableSalesManager = new TableSalesManager();
        private Panel pnlFloorplan { get; set; }
        private FlowLayoutPanel flowSectionsPanel {  get; set; }
        private FlowLayoutPanel flowServersPanel { get; set; }
        public DateOnly dateOnly => this.Shift.DateOnly;
        public bool isAm => this.Shift.IsAM;
        public TemplateManager TemplateManager { get; set; }
        public TemplateCreator TemplateCreator { get; set; }
        public Floorplan floorplanTemplateTEMP { get; set; }
        public FloorplanGenerator floorplanGenerator = new FloorplanGenerator();       
        public FloorplanFormManager() 
        {
            this.Shift = new Shift();            
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
                this.TemplateManager.GetTemplatesForFloorplan(this.Floorplan);
                this.TemplateManager.serverCount = this.Floorplan.Servers.Count;
            }
            else
            {               
                this.TemplateManager.DiningArea = Shift.SelectedDiningArea;
                this.TemplateManager.GetAllFloorplanTemplatesForDiningArea();
            }
        }
        public List<TableControl> TableControls
        {
            get {  return _tableControls; }
            set { _tableControls = value; }                
            
        }
        public List<SectionLabelControl> SectionLabels
        {
            get { return _sectionLabels; }
            set { _sectionLabels = value; }
        }
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
        public void AddTableControls(Panel panel)
        {
            _tableControls.Clear();
            panel.Controls.Clear();
            panel.Invalidate();
            if (this.Shift != null && this.Shift.SelectedDiningArea != null)
            {
                foreach (Table table in this.Shift.SelectedDiningArea.Tables)
                {
                    table.DiningArea = this.Shift.SelectedDiningArea;
                    TableControl tableControl = TableControlFactory.CreateTableControl(table);
                    tableControl.TableClicked += TableControl_TableClicked;
                    _tableControls.Add(tableControl);
                    panel.Controls.Add(tableControl);
                }
            }
        }       
        public void SetSectionLabels()
        {
            _sectionLabels.Clear();
            if(Shift.SelectedFloorplan == null) { return; }
           
            foreach (Section section in Shift.SelectedFloorplan.Sections)
            {                
                if (section.Tables.Count > 0)
                {
                    SectionLabelControl sectionControl = new SectionLabelControl(section, Shift.SelectedFloorplan.ServersWithoutSection, Shift.ServersOnShift);
                    sectionControl.AssignPickup += SectionLabelAssignPickup;
                    this._sectionLabels.Add(sectionControl);                    
                }
                if (section.Server != null)
                {
                    Shift.SelectedFloorplan.ServersWithoutSection.Remove(section.Server);                   
                    
                }
            }
        }

        private void SectionLabelAssignPickup(object? sender, EventArgs e)
        {
            SectionLabelControl controlClicked= (SectionLabelControl)sender;
            Section section = controlClicked.Section;
            frmPickupSectionAssignment pickUpForm = new frmPickupSectionAssignment(section, Shift);
            pickUpForm.ShowDialog();
        }

        /// <summary>
        /// ///SET SELECTED TEMPLATE TO ACTIVE TEMPLATE PREVIEW WILL SET SECTION PANELS AND KEEP TEMPLATE FORM OPEN!!!!!!!!!!!!!!!
        /// </summary>
        public void CopyTemplateSections(FloorplanTemplate template)
        {
            if(Floorplan.ServersWithoutSection.Count != Floorplan.Servers.Count)
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
            SetSectionPanels();
            foreach(SectionPanelControl sectionPanel in this._sectionPanels)
            {
                sectionPanel.UpdateLabels();
            }
            Floorplan.floorplanLines.AddRange(template.floorplanLines);
        }
        public void ResetSections()
        { 
            Floorplan.Sections.Clear();
            Floorplan.CreateSectionsForServers();            
        }
        public void SetSectionPanels()
        {
            _sectionPanels.Clear();
            if( Shift.SelectedFloorplan == null) { return ; }
            Shift.SelectedFloorplan.MovePickupSectionsToEndOfList();
            foreach (Section section in Shift.SelectedFloorplan.Sections)
            {
                SectionPanelControl sectionPanel = new SectionPanelControl(section, this.Shift.SelectedFloorplan);
                sectionPanel.CheckBoxChanged += setSelectedSection;
                sectionPanel.picEraseSectionClicked += EraseSectionClicked;
                sectionPanel.picTeamWaitClicked += TeamWaitClicked;
                sectionPanel.picAddServerClicked += SectionAddServerClicked;
                sectionPanel.picSubtractServerClicked += SectionSubtractServerClicked;
                sectionPanel.unassignedSpotClicked += AssignServerToSection;
                sectionPanel.ServerRemoved += ServerRemovedFromSection;
                if (section.IsTeamWait)
                {
                    sectionPanel.SetToTeamWait();
                }              
                this._sectionPanels.Add(sectionPanel);
            }
            if(this.Floorplan.Sections.Count > 0)
            {
                Floorplan.SetSelectedSection(Floorplan.Sections[0]);
            }            
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
                SectionPanelControl newSectionPanel = new SectionPanelControl(sectionAdded, this.Shift.SelectedFloorplan);
                newSectionPanel.CheckBoxChanged += setSelectedSection;
                newSectionPanel.picEraseSectionClicked += EraseSectionClicked;
                newSectionPanel.picTeamWaitClicked += TeamWaitClicked;
                newSectionPanel.picAddServerClicked += SectionAddServerClicked;
                newSectionPanel.picSubtractServerClicked += SectionSubtractServerClicked;
                newSectionPanel.unassignedSpotClicked += AssignServerToSection;
                newSectionPanel.ServerRemoved += ServerRemovedFromSection;
                this._sectionPanels.Add(newSectionPanel);
                UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Add, sectionAdded));                

            }
        }

        private void SectionAddServerClicked(object? sender, EventArgs e)
        {
            SectionPanelControl sectionPanel = sender as SectionPanelControl;
            Section selectedSection = sectionPanel.Section;

            Section sectionRemoved = Floorplan.RemoveHighestNumberedEmptySection();
            if (sectionRemoved == null && Floorplan.NotEnoughUnassignedServersCheck(selectedSection))
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
        private void setSelectedSection(object? sender, EventArgs e)
        {
            
            SectionPanelControl sectionPanelSender = (SectionPanelControl)sender;
            if (sectionPanelSender.isChecked())
            {
                this.Shift.SetSelectedSection(sectionPanelSender.Section);
                foreach (SectionPanelControl sectionPanelControl in this._sectionPanels)
                {
                    if (sectionPanelControl != sectionPanelSender)
                    {
                        sectionPanelControl.UnCheckBox();
                    }
                }
            }            
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
        private void TeamWaitClicked(object? sender, EventArgs e)
        {
            SectionPanelControl sectionPanel = sender as SectionPanelControl;
           
            Section selectedSection = sectionPanel.Section;

            if (!selectedSection.IsTeamWait)
            {
                Section sectionRemoved = Floorplan.RemoveHighestNumberedEmptySection();
                if (sectionRemoved == null && Floorplan.NotEnoughUnassignedServersCheck(selectedSection))
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
                SectionPanelControl newSectionPanel = new SectionPanelControl(sectionAdded, this.Shift.SelectedFloorplan);
                newSectionPanel.CheckBoxChanged += setSelectedSection;
                newSectionPanel.picEraseSectionClicked += EraseSectionClicked;
                newSectionPanel.picTeamWaitClicked += TeamWaitClicked;
                newSectionPanel.picAddServerClicked += SectionAddServerClicked;
                newSectionPanel.picSubtractServerClicked += SectionSubtractServerClicked;
                newSectionPanel.unassignedSpotClicked += AssignServerToSection;
                newSectionPanel.ServerRemoved += ServerRemovedFromSection;
                this._sectionPanels.Add(newSectionPanel);
                UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Add, sectionAdded));
            }
            sectionPanel.UpdateLabels();            
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
                //DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Remove the Tables and Server from this Section?",
                //    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dialogResult == DialogResult.Yes)
                //{
                    //SectionLabelRemoved?.Invoke(this, e);
                UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionLabel, UpdateType.Remove, selectedSection));
                this._sectionLabels.Remove(sectionLabelBySection(selectedSection));                
                Shift.SelectedFloorplan.UnassignSection(selectedSection);
                UpdateTableControlColors();
                UpdateServerControls();                   
            }
            sectionPanel.UpdateLabels();
        }      
       
        private SectionLabelControl sectionLabelBySection(Section section)
        {
            SectionLabelControl sectionLabel = this._sectionLabels.FirstOrDefault(s => s.Section == section);
            return sectionLabel;
        }
        private SectionPanelControl sectionPanelControlBySection(Section section)
        {
            SectionPanelControl sectionPanel = this._sectionPanels.FirstOrDefault(s => s.Section == section);
            return sectionPanel;
        }
        public void RemoveSectionLabel(Section section, Panel panel)
        {
            panel.Controls.Remove(sectionLabelBySection((Section)section));            
            panel.Invalidate();
        }
        public void RemoveSectionPanel(Section section, FlowLayoutPanel panel)
        {
            panel.Controls.Remove(sectionPanelControlBySection((Section)section));
            panel.Invalidate();
        }
        public void AddSectionLabels(Panel panel)
        {
            List<Control> controlsToRemove  = new List<Control>();
            foreach(Control c in panel.Controls)
            {
                if (c is SectionLabelControl sectionLabel)
                {
                    controlsToRemove.Add(c);
                }
            }
            foreach(Control c in controlsToRemove)
            {
                panel.Controls.Remove(c);
            }
            foreach(SectionLabelControl sectionLabelControl in _sectionLabels)
            {
                panel.Controls.Add(sectionLabelControl);
                sectionLabelControl.UpdateLabel();
               
                sectionLabelControl.BringToFront();
            }
        }
        public void AddSectionPanels(FlowLayoutPanel panel)
        {
            panel.Controls.Clear();
            coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (panel.Width / 2) - 7, 30);
            salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (panel.Width / 2) - 7, 30);
            coversImageLabel.SetTooltip("Covers per Server");           
            salesImageLabel.SetTooltip("Sales Per Server");
            panel.Controls.Add(coversImageLabel);
            panel.Controls.Add(salesImageLabel);
            if (this.Floorplan == null) { return; }
            Button btnAutoSelectTemplate = new Button
            {
                Text = "Auto Generate Template",
                AutoSize = false,
                Size = new Size(panel.Width - 10, 25),
                Font = new Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.CTAColor,
                ForeColor = UITheme.CTAFontColor
            };
            btnAutoSelectTemplate.Click += btnAutoSelectTemplate_Click;
            panel.Controls.Add(btnAutoSelectTemplate);  
            Button btnAutoAssignServers = new Button
            {
                Text = "Auto Assign Sections",
                AutoSize = false,
                Size = new Size(panel.Width - 10, 25),
                Font = new Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.CTAColor,
                ForeColor = UITheme.CTAFontColor
            };
            btnAutoAssignServers.Click += btnAutoAssignServers_Click;
            panel.Controls.Add(btnAutoAssignServers);
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
            //Button btnAddSection = new Button
            //{
            //    Text = "Add A Section",
            //    AutoSize = false,
            //    Size = new Size(panel.Width - 10, 25),
            //    Font = new Font("Segoe UI", 10F),
            //    FlatStyle = FlatStyle.Flat,
            //    BackColor = UITheme.ButtonColor,
            //    ForeColor = Color.Black
            //};
            //btnAddSection.Click += btnAddSection_Click;
            btnAddPickup.Click += btnAddPickupSection_Click;
            //panel.Controls.Add(btnAddSection);
            panel.Controls.Add(btnAddPickup);
            
        }

        private void btnAutoAssignServers_Click(object? sender, EventArgs e)
        {
            if (!AllTablesAreAssigned())
            {
                MessageBox.Show("All tables are not assigned");
                return;
            }
            this.Floorplan.OrderSectionsByAvgSales();
            List<Server> orderedServers = this.Floorplan.ServersWithoutSection.OrderByDescending(s => s.PreferedSectionWeight).ToList();
            List<Section> unassignedSections = this.Floorplan.UnassignedSections;
            int serverIndex = 0;

            for (int i = 0; i < unassignedSections.Count && serverIndex < orderedServers.Count; i++)
            {
                if (unassignedSections[i].IsPickUp)
                {
                    continue;
                }

                unassignedSections[i].AddServer(orderedServers[serverIndex]);
                serverIndex++;
            }

        }

        private void btnAutoSelectTemplate_Click(object? sender, EventArgs e)
        {
            
            UpdateTemplatesBasedOnFloorplan();
            floorplanGenerator = new FloorplanGenerator(this.Shift);
            FloorplanTemplate template = floorplanGenerator.SelectIdealTemplate(TemplateManager.Templates);
            CopyTemplateSections(template);
            AddTableControls(pnlFloorplan);
            SetSectionLabels();
            SetSectionPanels();
           // AddSectionLabels(pnlFloorPlan);
            SetServerControls();
            UpdateTableControlColors();
            flowSectionsPanel.Controls.Clear();
            flowServersPanel.Controls.Clear();
            AddServerControls(flowServersPanel);
            AddSectionPanels(flowSectionsPanel);
            AddSectionLabels(pnlFloorplan);
            //UpdateServerControlsForFloorplan();
            UpdateTableStats();
            //LoadTableSalesForPastDate();
            // SetTableSales();

            UpdateAveragesPerServer();
            coversImageLabel.UpdateText(Shift.SelectedFloorplan.MaxCoversPerServer.ToString("F0"));
            salesImageLabel.UpdateText(Shift.SelectedFloorplan.AvgSalesPerServer.ToString("C0"));
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

        private void btnAddSection_Click(object? sender, EventArgs e)
        {
            Section section = new Section();

            section.IsPickUp = false;
            TemplateCreator.Sections.Add(section);
            SectionPanelControl newSectionPanel = new SectionPanelControl(section, this.TemplateCreator.Template);
            newSectionPanel.CheckBoxChanged += setSelectedSection;
            newSectionPanel.picEraseSectionClicked += EraseSectionClicked;
            
            this._sectionPanels.Add(newSectionPanel);
            UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Add, section));
            TemplateCreator.SelectedSection = section;
        }

        public void RefreshSectionPanels()
        {
            FlowLayoutPanel panel = coversImageLabel.Parent as FlowLayoutPanel;
            panel.Controls.Clear();
            coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (panel.Width / 2) - 7, 30);
            salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (panel.Width / 2) - 7, 30);
            coversImageLabel.SetTooltip("Covers per Server");
            salesImageLabel.SetTooltip("Sales Per Server");
            panel.Controls.Add(coversImageLabel);
            panel.Controls.Add(salesImageLabel);

            foreach (SectionPanelControl sectionPanel in _sectionPanels)
            {
               
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
            btnAddPickup.Click += btnAddPickupSection_Click;
            panel.Controls.Add(btnAddPickup);

        }
        private void btnAddPickupSection_Click(object sender, EventArgs e)
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
           
            frmEditShiftRoster editRosterForm = new frmEditShiftRoster(Shift);
            editRosterForm.ShowDialog();
            SetServerControls();            
            AddServerControls(this.flowServersPanel);
            SetSectionPanels();
            AddSectionPanels(this.flowSectionsPanel);
            UpdateAveragesPerServer();
        }

        public void UpdateTableControlColors(Panel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {

                if (ctrl is TableControl tableControl)
                {
                    tableControl.BackColor = panel.BackColor;
                    tableControl.TextColor = panel.ForeColor;
                    foreach (Section section in Shift.SelectedFloorplan.Sections)
                    {

                        foreach (Table table in section.Tables)
                        {
                            if (tableControl.Table.TableNumber == table.TableNumber)
                            {
                                tableControl.SetSection(section);
                                //tableControl.BackColor = section.MuteColor(0.35f);
                                tableControl.MuteColors();
                                if (section == this.Shift.SectionSelected)
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
        public void UpdateTableControlColors()
        {
            foreach (TableControl tableControl in this._tableControls)
            {
                tableControl.BackColor = tableControl.Parent.BackColor;
                tableControl.TextColor = tableControl.Parent.ForeColor;
                tableControl.Invalidate();               
                
            }
            if (Shift.SelectedFloorplan == null) { return; }
            foreach (TableControl tableControl in this._tableControls)
            {
               
                foreach (Section section in Shift.SelectedFloorplan.Sections)
                {
                    foreach (Table table in section.Tables)
                    {
                        if (tableControl.Table.TableNumber == table.TableNumber)
                        {
                            tableControl.SetSection(section);
                            //tableControl.BackColor = section.MuteColor(0.35f);
                            tableControl.MuteColors();
                            if (section == this.Shift.SectionSelected)
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
        public void TableControlDisplayModeToSales()
        {
            foreach(TableControl tableControl in this._tableControls)
            {
                tableControl.CurrentDisplayMode = DisplayMode.AverageCovers;
            }
        }
        public void UpdateTableControlSections(Panel panel)
        {
            foreach(Control ctrl in panel.Controls)
            {
                if(ctrl is TableControl tableControl)
                {
                    tableControl.BackColor = Color.Black; 
                    tableControl.ForeColor = Color.Black;
                }
            }
            
        }
        private void TableControl_TableClicked(object sender, TableClickedEventArgs e)
        {

            TableControl clickedTableControl = sender as TableControl;
            Table clickedTable = clickedTableControl.Table;
            Section sectionEdited = (Section)clickedTableControl.Section;
            if (e.MouseButton == MouseButtons.Right && clickedTableControl.Section != null)
            {



                sectionEdited.RemoveTable(clickedTable);

                clickedTableControl.RemoveSection();
                clickedTableControl.BackColor = clickedTableControl.Parent.BackColor;  // Restore the original color
                clickedTableControl.ForeColor = clickedTableControl.Parent.ForeColor;
                clickedTableControl.Invalidate();
                if(sectionEdited.IsPickUp)
                {
                    UpdateAveragesPerServer();
                }
                //UpdateSectionLabels(sectionEdited, sectionEdited.MaxCovers, sectionEdited.AverageCovers);

                return;
            }
            
            if (Shift.SectionSelected != null)
            {
                if (sectionEdited != null)
                {
                   
                    sectionEdited.RemoveTable(clickedTable);
                    clickedTableControl.RemoveSection();
                    if (sectionEdited.IsPickUp)
                    {
                        UpdateAveragesPerServer();
                    }

                }
                Shift.SectionSelected.AddTable(clickedTable);
                clickedTableControl.SetSection(Shift.SectionSelected);
                
                clickedTableControl.BackColor = Shift.SectionSelected.Color;
                clickedTableControl.TextColor = Shift.SectionSelected.FontColor;
                if(Shift.SectionSelected.IsPickUp)
                {
                    UpdateAveragesPerServer();
                }

                
                clickedTableControl.Invalidate();
                if (AllTablesAreAssigned())
                {
                    UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.TableControl, UpdateType.Refresh, clickedTableControl));
                }
                //UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
            }

            

        }
        private bool AllTablesAreAssigned()
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
        

        public void SelectTables(List<TableControl> selectedTables)
        {
            foreach (var tableControl in selectedTables)
            {
                TableClickedEventArgs args = new TableClickedEventArgs(tableControl.Table, false);
                TableControl_TableClicked(tableControl, args);
            }
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
                }
                else
                {
                    serverControl.Label.BackColor = UITheme.ButtonColor; // Replace DefaultColor with your default color
                }
                serverControl.Invalidate();
            }
                        
        }


        public void UpdateSection(Section section)
        {
           
        }
        
        //private void LoadTableSalesForPastDate()
        //{
        //    if(this.Floorplan != null)
        //    {
        //        List<TableStat> statList = SqliteDataAccess.LoadTableStatsByDateAndLunch(this.Floorplan.IsLunch, this.Floorplan.DateOnly);
        //        if(statList.Count > 0)
        //        {
        //            foreach (Table table in this.Floorplan.DiningArea.Tables)
        //            {
        //                var matchedStat = statList.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
        //                if (matchedStat != null)
        //                {
        //                    table.AverageSales = (float)matchedStat.Sales;
        //                }
        //                else { table.AverageSales = 0; }
        //            }
        //        }

                
        //    }
           
        //}
        public string floorplanSalesDisplay { get; set; }
       
        internal void AddSectionPanel(Section? section, FlowLayoutPanel flowSectionSelect)
        {
            SectionPanelControl newPanel = sectionPanelControlBySection(section);

            // Add the new panel to the end of the FlowLayoutPanel initially
            flowSectionSelect.Controls.Add(newPanel);

            // Find the correct index for insertion
            int insertIndex = FindInsertionIndex(section, flowSectionSelect);

            // Move the new panel to the found index
            flowSectionSelect.Controls.SetChildIndex(newPanel, insertIndex);
        }
        private int FindInsertionIndex(Section? section, FlowLayoutPanel flowPanel)
        {
            for (int i = 0; i < flowPanel.Controls.Count; i++)
            {
                var panelControl = flowPanel.Controls[i] as SectionPanelControl;
                if (panelControl != null && panelControl.Section.Number > section.Number)
                {
                    return i;
                }
            }

            // If no suitable position is found, the control stays at the end
            return flowPanel.Controls.Count - 2;
        }
       
        public void SetTableSalesStatsPeriod(TableSalesManager.StatsPeriod statsPeriod)
        {
            this.tableSalesManager.CurrentStatsPeriod = statsPeriod;
            UpdateTableStats();
            if(this.Floorplan == null)
            {
                coversImageLabel.UpdateText(Shift.SelectedDiningArea.GetMaxCovers().ToString("F0"));
                salesImageLabel.UpdateText(Shift.SelectedDiningArea.ExpectedSales.ToString("C0"));
                coversImageLabel.Invalidate();
                salesImageLabel.Invalidate();
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
               
            }
        }
        public void UpdateAveragesPerServer()
        {
            float averagerPerServer = Shift.SelectedDiningArea.ExpectedSales / Floorplan.Servers.Count;
            coversImageLabel.UpdateText(Shift.SelectedFloorplan.MaxCoversPerServer.ToString("F0"));
            salesImageLabel.UpdateText(averagerPerServer.ToString("C0"));
            coversImageLabel.Invalidate();
            salesImageLabel.Invalidate();
            foreach (SectionPanelControl sectionPanel in _sectionPanels)
            {
                sectionPanel.UpdateLabels();
            }
        }
        public void SetViewedFloorplanToNone(FlowLayoutPanel panel)
        {
           
            panel.Controls.Clear();
            coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (panel.Width / 2) - 7, 30);
            salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (panel.Width / 2) - 7, 30);
            coversImageLabel.SetTooltip("Covers per Server");
            salesImageLabel.SetTooltip("Sales Per Server");
            panel.Controls.Add(coversImageLabel);
            panel.Controls.Add(salesImageLabel);
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
             
            //UpdateServerControlsForFloorplan();

            //LoadTableSalesForPastDate();
            // SetTableSales();
            UpdateTableStats();
            UpdateAveragesPerServer();
            coversImageLabel.UpdateText(Shift.SelectedFloorplan.MaxCoversPerServer.ToString("F0"));
            salesImageLabel.UpdateText(Shift.SelectedFloorplan.AvgSalesPerServer.ToString("C0"));
            coversImageLabel.Invalidate();
            salesImageLabel.Invalidate();
            //this.TemplateCreator = new TemplateCreator(Shift.SelectedDiningArea);


        }

        private void btnSaveFloorplan_Click(object? sender, EventArgs e)
        {
            Button btnSaveTemplate = sender as Button;
            btnSaveTemplate.Click += btnCreateTemplate_Click;
            btnSaveTemplate.Click -= btnSaveFloorplan_Click;
            btnSaveTemplate.Text = "Create A Floorplan Template";
        }

        public void SetViewedFloorplan(DateOnly dateOnlySelected, bool isAM,
            Panel pnlFloorPlan, FlowLayoutPanel flowServersInFloorplan, FlowLayoutPanel flowSectionSelect)
        {            
            this.Shift.DateOnly = dateOnlySelected;
            this.Shift.IsAM = isAM;
            this.flowSectionsPanel = flowSectionSelect;
            this.flowServersPanel = flowServersInFloorplan;
            this.pnlFloorplan = pnlFloorPlan;

            if (Shift.ContainsFloorplan(dateOnlySelected, isAM, Shift.SelectedDiningArea.ID))
            {
                Shift.SetSelectedFloorplan(dateOnlySelected, isAM, Shift.SelectedDiningArea.ID);
            }
            else
            {               
                Shift = SqliteDataAccess.LoadShift(Shift.SelectedDiningArea, dateOnlySelected, isAM);                
            }

            ChangeDiningAreaSelected();
        }
        public void ChangeDiningAreaSelected()
        {
            if (Shift.SelectedFloorplan != null)
            {
                Shift.SetDoubles();
                AddTableControls(pnlFloorplan);
                SetSectionLabels();
                SetSectionPanels();
                SetServerControls();
                UpdateTableControlColors();
                flowSectionsPanel.Controls.Clear();
                flowServersPanel.Controls.Clear();
                AddServerControls(flowServersPanel);
                AddSectionPanels(flowSectionsPanel);
                AddSectionLabels(pnlFloorplan);
                UpdateTableStats();
                AddSectionLines();
                UpdateAveragesPerServer();
                coversImageLabel.UpdateText(Shift.SelectedFloorplan.MaxCoversPerServer.ToString("F0"));
                salesImageLabel.UpdateText(Shift.SelectedFloorplan.AvgSalesPerServer.ToString("C0"));
                coversImageLabel.Invalidate();
                salesImageLabel.Invalidate();
            }
            else
            {
                foreach (SectionLabelControl sectionLabel in _sectionLabels)
                {
                    pnlFloorplan.Controls.Remove(sectionLabel);
                }
                UpdateTableControlColors();
                UpdateTableStats();
                SetSectionPanels();
                SetServerControls();
                flowSectionsPanel.Controls.Clear();
                flowServersPanel.Controls.Clear();
                AddServerControls(flowServersPanel);
                AddSectionPanels(flowSectionsPanel);
                coversImageLabel.UpdateText(Shift.SelectedDiningArea.GetMaxCovers().ToString("F0"));
                salesImageLabel.UpdateText(Shift.SelectedDiningArea.ExpectedSales.ToString("C0"));
                coversImageLabel.Invalidate();
                salesImageLabel.Invalidate();

            }
        }

        private void AddSectionLines()
        {
            //throw new NotImplementedException();
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
    }
}
