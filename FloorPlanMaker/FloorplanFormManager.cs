﻿using FloorplanClassLibrary;
using FloorPlanMaker;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static System.Collections.Specialized.BitVector32;

namespace FloorPlanMakerUI
{
   public class FloorplanFormManager : ISectionObserver
    {
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
        
        public FloorplanFormManager(ShiftManager shiftManager)
        {
            //this.Floorplan = shiftManager.SelectedFloorplan;
            this.ShiftManager = shiftManager;
            
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
                this._sectionPanels.Add(sectionPanel);
            }
            Floorplan.SetSelectedSection(Floorplan.Sections[0]);
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
                if (sectionRemoved == null)
                {
                    MessageBox.Show("You must clear a section before making another section a teamwait section");
                }
                else
                {
                    
                    selectedSection.ToggleTeamWait();
                    sectionPanel.SetTeamWaitPictureBoxes();
                    UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.SectionPanel, UpdateType.Remove, sectionRemoved));

                }

            }
            else
            {
                selectedSection.MakeSoloSection();
                sectionPanel.SetTeamWaitPictureBoxes();
                Section section = new Section();
                Floorplan.AddSection(section);
                SectionPanelControl newSectionPanel = new SectionPanelControl(section, this.ShiftManager.SelectedFloorplan);
                newSectionPanel.CheckBoxChanged += setSelectedSection;
                newSectionPanel.picEraseSectionClicked += EraseSectionClicked;
                newSectionPanel.picTeamWaitClicked += TeamWaitClicked;
                this._sectionPanels.Add(newSectionPanel);
            }
            sectionPanel.UpdateLabels();
            //UpdateSectionLabels(selectedSection, selectedSection.MaxCovers, selectedSection.AverageCovers);
            
            //CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
        }

        private void EraseSectionClicked(object? sender, EventArgs e)
        {
            SectionPanelControl sectionPanel = (SectionPanelControl)sender;
            Section selectedSection = sectionPanel.Section;
            if (selectedSection != null)
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Remove the Tables and Server from this Section?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
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
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
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
                sectionLabelControl.BringToFront();
            }
        }
        public void AddSectionPanels(FlowLayoutPanel panel)
        {
            panel.Controls.Clear();
            coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (panel.Width / 2) - 7, 30);
            salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (panel.Width / 2) - 7, 30);
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
            Section pickUpSection = new Section(Floorplan);
            pickUpSection.Name = "Pickup";
            pickUpSection.IsPickUp = true;
            Floorplan.AddSection(pickUpSection);
            SectionPanelControl sectionPanel = new SectionPanelControl(pickUpSection, this.ShiftManager.SelectedFloorplan);
            sectionPanel.CheckBoxChanged += setSelectedSection;
            sectionPanel.picEraseSectionClicked += EraseSectionClicked;
            sectionPanel.picTeamWaitClicked += TeamWaitClicked;
            this._sectionPanels.Add(sectionPanel);
            RefreshSectionPanels();
            //CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
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
                //UpdateSectionLabels(sectionEdited, sectionEdited.MaxCovers, sectionEdited.AverageCovers);

                return;
            }
            
            if (ShiftManager.SectionSelected != null)
            {
                if (sectionEdited != null)
                {
                    //sectionEdited.Tables.RemoveAll(t => t.ID == clickedTable.ID);
                    sectionEdited.RemoveTable(clickedTable);
                    clickedTableControl.RemoveSection();
                    //UpdateSectionLabels(sectionEdited, sectionEdited.MaxCovers, sectionEdited.AverageCovers);
                }
                ShiftManager.SectionSelected.AddTable(clickedTable);
                clickedTableControl.SetSection(ShiftManager.SectionSelected);
                
                clickedTableControl.BackColor = ShiftManager.SectionSelected.Color;
                clickedTableControl.TextColor = ShiftManager.SectionSelected.FontColor;

                
                clickedTableControl.Invalidate();
                //UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
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

        public void UpdateServerControls()
        {
            if (ShiftManager.SelectedFloorplan == null) return;

            foreach (ServerControl serverControl in _serverControls)
            {
                
                Server server = serverControl.Server;

                
                Section assignedSection = null;
                foreach (var entry in ShiftManager.SelectedFloorplan.SectionServerMap)
                {
                    if (entry.Value.Contains(server))
                    {
                        assignedSection = entry.Key;
                        break; // Exit the loop once the section is found
                    }
                }

                // Update the label's background color based on the section's color
                if (assignedSection != null)
                {
                    serverControl.Label.BackColor = assignedSection.Color;
                }
                else
                {
                    serverControl.Label.BackColor = UITheme.ButtonColor; // Replace DefaultColor with your default color
                }
            }
        }


        public void UpdateSection(Section section)
        {
           //UpdateServerControls();
        }
        public void SetViewedFloorplan(DateOnly dateOnlySelected, bool isAM,
            Panel pnlFloorPlan, FlowLayoutPanel flowServersInFloorplan, FlowLayoutPanel flowSectionSelect)
        {
            //NoServersToDisplay();

            if (ShiftManager.ContainsFloorplan(dateOnlySelected, isAM, ShiftManager.SelectedDiningArea.ID))
            {
                ShiftManager.SetSelectedFloorplan(dateOnlySelected, isAM, ShiftManager.SelectedDiningArea.ID);
            }
            else
            {
                ShiftManager.SelectedFloorplan = SqliteDataAccess.LoadFloorplanByCriteria(ShiftManager.SelectedDiningArea, dateOnlySelected, isAM);
            }

            if (ShiftManager.SelectedFloorplan != null)
            {
                AddTableControls(pnlFloorPlan);
                SetSectionLabels();
                SetSectionPanels();
                AddSectionLabels(pnlFloorPlan);

                //CreateSectionRadioButtons(shiftManager.SelectedFloorplan.Sections);
                //floorplanManager.SetTableControls();
                SetSectionLabels();
                SetSectionPanels();
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
            }
            //floorplanManager.ShiftManager = shiftManager;
            //floorplanManager.SectionLabelRemoved += FloorplanManager_SectionLabelRemoved;            
            //allTableControls = floorplanManager.TableControls;
            //UpdateTableControlSections();
        }
    }
}