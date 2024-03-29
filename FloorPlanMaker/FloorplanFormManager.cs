﻿using FloorplanClassLibrary;
using FloorPlanMaker;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
                return this.ShiftManager.SelectedFloorplan;
            }
        }
        public ShiftManager ShiftManager;
        private List<TableControl> _tableControls = new List<TableControl>();    
        private List<SectionLabelControl> _sectionLabels = new List<SectionLabelControl>();
        private List<SectionPanelControl> _sectionPanels = new List<SectionPanelControl>();
        private List<ServerControl> _serverControls = new List<ServerControl>();
        public event EventHandler SectionLabelRemoved;
        public event EventHandler<UpdateEventArgs> UpdateRequired;
        private ImageLabelControl coversImageLabel = new ImageLabelControl();
        private ImageLabelControl salesImageLabel = new ImageLabelControl();
        private ToolTip toolTip = new ToolTip();
        private TableSalesManager tableSalesManager = new TableSalesManager();
        
        public TemplateManager TemplateManager { get; set; }


        public FloorplanFormManager(ShiftManager shiftManager)
        {
            //this.Floorplan = shiftManager.SelectedFloorplan;
            this.ShiftManager = shiftManager;
            this.TemplateManager = new TemplateManager(shiftManager.SelectedDiningArea);


        }
        public void UpdateTemplatesBasedOnFloorplan()
        {
            if (this.Floorplan != null)
            {
                // If Floorplan is not null, get templates for the specific floorplan
                this.TemplateManager.GetTemplatesForFloorplan(this.Floorplan);
                this.TemplateManager.serverCount = this.Floorplan.Servers.Count;
            }
            else
            {
                // If Floorplan is null, get all floorplan templates
                this.TemplateManager.GetAllFloorplanTemplates();
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
            if (this.ShiftManager != null && this.ShiftManager.SelectedDiningArea != null)
            {
                foreach (Table table in this.ShiftManager.SelectedDiningArea.Tables)
                {
                    table.DiningArea = this.ShiftManager.SelectedDiningArea;
                    TableControl tableControl = TableControlFactory.CreateTableControl(table);
                    tableControl.TableClicked += TableControl_TableClicked;
                    _tableControls.Add(tableControl);
                    panel.Controls.Add(tableControl);
                }
            }
        }
        public void CreateSectionControls()
        {
            _sectionLabels.Clear();
            _sectionLabels.Clear();
            if (ShiftManager.SelectedFloorplan == null) { return; }
            foreach(Section section in  ShiftManager.SelectedFloorplan.Sections) 
            {

            }
        }
        public void SetSectionLabels()
        {
            _sectionLabels.Clear();
            if(ShiftManager.SelectedFloorplan == null) { return; }
            foreach (Section section in ShiftManager.SelectedFloorplan.Sections)
            {
                
                if (section.Tables.Count > 0)
                {
                    SectionLabelControl sectionControl = new SectionLabelControl(section, ShiftManager.SelectedFloorplan.ServersWithoutSection);
                    
                    this._sectionLabels.Add(sectionControl);
                    
                }
                if (section.Server != null)
                {
                    ShiftManager.SelectedFloorplan.ServersWithoutSection.Remove(section.Server);
                    
                    
                }
            }
        }
        /// <summary>
        /// ///SET SELECTED TEMPLATE TO ACTIVE TEMPLATE PREVIEW WILL SET SECTION PANELS AND KEEP TEMPLATE FORM OPEN!!!!!!!!!!!!!!!
        /// </summary>
        public void CopyTemplateSections(FloorplanTemplate template)
        {
            ShiftManager.SelectedFloorplan.CopyTemplateSections(template.Sections);
            SetSectionPanels();
        }
        public void ResetSections()
        { 
            Floorplan.Sections.Clear();
            Floorplan.CreateSectionsForServers();
            //SetSectionPanels();
        }
        public void SetSectionPanels()
        {
            _sectionPanels.Clear();
            if( ShiftManager.SelectedFloorplan == null) { return ; }
            foreach(Section section in ShiftManager.SelectedFloorplan.Sections)
            {
                SectionPanelControl sectionPanel = new SectionPanelControl(section, this.ShiftManager.SelectedFloorplan);
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
               // sectionPanel += SectionAdded?
                //sectionPanel.UpdateRequired += FloorplanManager_UpdateRequired;
                this._sectionPanels.Add(sectionPanel);
            }
            Floorplan.SetSelectedSection(Floorplan.Sections[0]);
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
                SectionPanelControl newSectionPanel = new SectionPanelControl(sectionAdded, this.ShiftManager.SelectedFloorplan);
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
                //SectionRemoved?.Invoke(this.Section);


            }
        }

        public void SetServerControls()
        {
            _serverControls.Clear();
            if (ShiftManager.SelectedFloorplan == null) { return; }
            if (ShiftManager.SelectedFloorplan.Servers.Count <= 0) { return; }
            foreach (Server server in ShiftManager.SelectedFloorplan.Servers)
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

                this._serverControls.Add(serverControl);
            }
            UpdateServerControls();
        }
        private void ServerControl_Click(object? sender, EventArgs e)
        {
            ServerControl serverControl = (ServerControl)sender;
            Server server = serverControl.Server;
            if (ShiftManager.SelectedFloorplan.SectionSelected == null) { return; }   
            
            ShiftManager.SelectedFloorplan.SectionSelected.AddServer(server);
            UpdateServerControls();

        }
        private void setSelectedSection(object? sender, EventArgs e)
        {
            
            SectionPanelControl sectionPanelSender = (SectionPanelControl)sender;
            if (sectionPanelSender.isChecked())
            {
                this.ShiftManager.SetSelectedSection(sectionPanelSender.Section);
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
        private void TeamWaitClicked(object? sender, EventArgs e)
        {
            SectionPanelControl sectionPanel = sender as SectionPanelControl;
           
            Section selectedSection = sectionPanel.Section;

            if (!selectedSection.IsTeamWait)
            {

                //pb.BackColor = AppColors.NoColor;
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
                SectionPanelControl newSectionPanel = new SectionPanelControl(sectionAdded, this.ShiftManager.SelectedFloorplan);
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
            //UpdateSectionLabels(selectedSection, selectedSection.MaxCovers, selectedSection.AverageCovers);
            
            //CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
        }
        private void UpdateAllSectionTotals()
        {

        }
        private void DeleteSection(Section section)
        {

        }
        private void EraseSectionClicked(object? sender, EventArgs e)
        {
            //TODO CHANGE TO DELETE AFTER ERASING
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
                //selectedSection.Server.CurrentSection.RemoveServer(selectedSection.Server);

                //this.UpdateRequired += FloorplanManager_UpdateRequired;
                ShiftManager.SelectedFloorplan.UnassignSection(selectedSection);

                UpdateTableControlColors();
                UpdateServerControls();
                    //AND CLEAR TABLECONTROL SECTIONS,
                    //REMOVE TABLES FROM SECTION
                    //Update ServerControl 
                //}
                //else if (dialogResult == DialogResult.No)
                //{
                //    return;
                //}
            }
            sectionPanel.UpdateLabels();
        }
        
        private Section sectionByNumber(int sectionNumber)
        {
            Section sectionbyNumber = ShiftManager.SelectedFloorplan.Sections.FirstOrDefault(s => s.Number == sectionNumber);
            return sectionbyNumber;
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
            //UpdateTableControlColors(panel);
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
            btnAddPickup.Click += btnAddPickupSection_Click;
            panel.Controls.Add(btnAddPickup);
            
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
            //Section pickUpSection = new Section(Floorplan);
            //pickUpSection.Name = "Pickup";
            //pickUpSection.IsPickUp = true;
            //Floorplan.AddSection(pickUpSection);
            //SectionPanelControl sectionPanel = new SectionPanelControl(pickUpSection, this.ShiftManager.SelectedFloorplan);
            //sectionPanel.CheckBoxChanged += setSelectedSection;
            ////sectionPanel.picEraseSectionClicked += EraseSectionClicked;
            ////sectionPanel.picTeamWaitClicked += TeamWaitClicked;
            //this._sectionPanels.Add(sectionPanel);
            //RefreshSectionPanels();
            ////CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
            Section pickUp = new Section(Floorplan);
            pickUp.Name = "Pickup";
            pickUp.IsPickUp = true;            
            Floorplan.AddSection(pickUp);
            SectionPanelControl newSectionPanel = new SectionPanelControl(pickUp, this.ShiftManager.SelectedFloorplan);
            newSectionPanel.CheckBoxChanged += setSelectedSection;
            newSectionPanel.picEraseSectionClicked += EraseSectionClicked;
            //newSectionPanel.picTeamWaitClicked += TeamWaitClicked;
            //newSectionPanel.picAddServerClicked += SectionAddServerClicked;
            //sectionPanel.picSubtractServerClicked += SectionSubtractServerClicked;
            this._sectionPanels.Add(newSectionPanel);
            UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Add, pickUp));
        }
        public void AddServerControls(FlowLayoutPanel panel)
        {
            foreach (ServerControl serverControl in _serverControls)
            {
                serverControl.Width = panel.Width -10;
                serverControl.Margin = new Padding(5);
                panel.Controls.Add(serverControl);
            }
        }
        public void UpdateTableControlColors(Panel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {

                if (ctrl is TableControl tableControl)
                {
                    tableControl.BackColor = panel.BackColor;
                    tableControl.TextColor = panel.ForeColor;
                    foreach (Section section in ShiftManager.SelectedFloorplan.Sections)
                    {

                        foreach (Table table in section.Tables)
                        {
                            if (tableControl.Table.TableNumber == table.TableNumber)
                            {
                                tableControl.SetSection(section);
                                //tableControl.BackColor = section.MuteColor(0.35f);
                                tableControl.MuteColors();
                                if (section == this.ShiftManager.SectionSelected)
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
            if (ShiftManager.SelectedFloorplan == null) { return; }
            foreach (TableControl tableControl in this._tableControls)
            {
               
                foreach (Section section in ShiftManager.SelectedFloorplan.Sections)
                {
                    foreach (Table table in section.Tables)
                    {
                        if (tableControl.Table.TableNumber == table.TableNumber)
                        {
                            tableControl.SetSection(section);
                            //tableControl.BackColor = section.MuteColor(0.35f);
                            tableControl.MuteColors();
                            if (section == this.ShiftManager.SectionSelected)
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
            
            if (ShiftManager.SectionSelected != null)
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
                ShiftManager.SectionSelected.AddTable(clickedTable);
                clickedTableControl.SetSection(ShiftManager.SectionSelected);
                
                clickedTableControl.BackColor = ShiftManager.SectionSelected.Color;
                clickedTableControl.TextColor = ShiftManager.SectionSelected.FontColor;
                if(ShiftManager.SectionSelected.IsPickUp)
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
        public void UpdateAveragesPerServer()
        {
            coversImageLabel.UpdateText(ShiftManager.SelectedFloorplan.MaxCoversPerServer.ToString("F0"));
            salesImageLabel.UpdateText(ShiftManager.SelectedFloorplan.AvgSalesPerServer.ToString("C0"));
            foreach(SectionPanelControl sectionPanel in _sectionPanels)
            {
                sectionPanel.UpdateLabels();
            }
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
           
            if (ShiftManager.SelectedFloorplan == null) return;
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
        public void SetViewedFloorplan(DateOnly dateOnlySelected, bool isAM,
            Panel pnlFloorPlan, FlowLayoutPanel flowServersInFloorplan, FlowLayoutPanel flowSectionSelect)
        {
            //NoServersToDisplay();
            SetTableSales();

            if (ShiftManager.ContainsFloorplan(dateOnlySelected, isAM, ShiftManager.SelectedDiningArea.ID))
            {
                ShiftManager.SetSelectedFloorplan(dateOnlySelected, isAM, ShiftManager.SelectedDiningArea.ID);
            }
            else
            {
                ShiftManager.SelectedFloorplan = SqliteDataAccess.LoadFloorplanByCriteria(ShiftManager.SelectedDiningArea, dateOnlySelected, isAM);
                if (ShiftManager.SelectedFloorplan != null)
                {
                    UpdateAveragesPerServer();
                }
               
            }

            if (ShiftManager.SelectedFloorplan != null)
            {
                AddTableControls(pnlFloorPlan);
                SetSectionLabels();
                SetSectionPanels();
                AddSectionLabels(pnlFloorPlan);
                SetServerControls();
                UpdateTableControlColors();
                flowSectionSelect.Controls.Clear();
                flowServersInFloorplan.Controls.Clear();
                AddServerControls(flowServersInFloorplan);
                AddSectionPanels(flowSectionSelect);
                AddSectionLabels(pnlFloorPlan);
                //UpdateServerControlsForFloorplan();
                coversImageLabel.UpdateText(ShiftManager.SelectedFloorplan.MaxCoversPerServer.ToString("F0"));
                salesImageLabel.UpdateText(ShiftManager.SelectedFloorplan.AvgSalesPerServer.ToString("C0"));
                LoadTableSalesForPastDate();
                UpdateAveragesPerServer();
                
            }
            else
            {
                foreach(SectionLabelControl sectionLabel in _sectionLabels)
                {
                    pnlFloorPlan.Controls.Remove(sectionLabel);
                }               
                UpdateTableControlColors();
                
            }
           
        }
        private void LoadTableSalesForPastDate()
        {
            if(this.Floorplan != null)
            {
                List<TableStat> statList = SqliteDataAccess.LoadTableStatsByDateAndLunch(this.Floorplan.IsLunch, this.Floorplan.DateOnly);
                if(statList.Count > 0)
                {
                    foreach (Table table in this.Floorplan.DiningArea.Tables)
                    {
                        var matchedStat = statList.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
                        if (matchedStat != null)
                        {
                            table.AverageSales = (float)matchedStat.Sales;
                        }
                        else { table.AverageSales = -1; }
                    }
                }

                
            }
           
        }
        private void SetTableSales()
        {
            foreach (Table table in this.ShiftManager.SelectedDiningArea.Tables)
            {
                var matchedStat = tableSalesManager.Stats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
                if (matchedStat != null)
                {
                    table.AverageSales = (float)matchedStat.Sales;
                }
                else { table.AverageSales = -1; }
            }
            
        }
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
        public void SetSalesManagerStats(List<TableStat> stats)
        {
            if (stats != null)
            {
                this.tableSalesManager.Stats = stats;
            }
            SetTableSales();
        }
        
    }
}
